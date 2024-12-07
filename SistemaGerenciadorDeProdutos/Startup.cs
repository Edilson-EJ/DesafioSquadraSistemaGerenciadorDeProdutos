using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaGerenciadorDeProdutos.Data;
using SistemaGerenciadorDeProdutos.Services;
using System.Globalization;
using System.Text;
using Microsoft.OpenApi.Models;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração de CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", builder =>
            {
                builder
                    .WithOrigins("http://localhost:4200") 
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Configura o DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Configuração de autenticação JWT
        var jwtSettings = Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("SecretKey");

        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException("A chave secreta JWT não está configurada.");
        }

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };

            // Eventos de autenticação
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Erro de autenticação: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validado com sucesso.");
                    return Task.CompletedTask;
                }
            };
        });

        // Configurações de Controllers
        services.AddControllers();

        // Configuração do Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sistema Gerenciador de Produtos API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira o token JWT com o prefixo 'Bearer '",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
        });

        // Injeção de dependências
        services.AddScoped<IUsuarioInterface, UsuarioService>();
        services.AddScoped<IProdutoInterface, ProdutoService>();
        services.AddScoped<IAuthService, AuthService>();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
    {
        // Configurações de ambiente
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Autenticação e Autorização
        app.UseAuthentication();
        // Configuração de rotas
        app.UseRouting();
        // Use a política CORS definida
        app.UseCors("AllowAngularApp");
        app.UseAuthorization();
        // Configuração de endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Redirecionamento HTTPS
        //app.UseHttpsRedirection();

        // Arquivos estáticos
        //app.UseStaticFiles();

        

        // CORS
        app.UseCors("AllowAllOrigins");

        // Configuração do Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Gerenciador de Produtos API V1");
            c.RoutePrefix = string.Empty;
        });

        

        // Configuração de cultura e fuso horário
        var brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var dateTimeFormat = new DateTimeFormatInfo
        {
            ShortDatePattern = "dd/MM/yyyy",
            LongTimePattern = "HH:mm:ss"
        };
        var cultureInfo = new CultureInfo("pt-BR")
        {
            DateTimeFormat = dateTimeFormat
        };

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        

    }
}
