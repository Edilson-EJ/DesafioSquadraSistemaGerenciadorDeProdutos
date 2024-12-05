import { Component } from '@angular/core';
import { AuthAPIService } from '../../services/auth-api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginModel } from '../../models/loginModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  loginModel: LoginModel = { email: '', password: '' };
  token: string | null = null;
  error: string | null = null;

  constructor(private authAPIService: AuthAPIService, private router: Router) {}

  login(): void {
    this.authAPIService.login(this.loginModel).subscribe(
      (response) => {
        const token = response.token;
        this.authAPIService.storeToken(token);

        console.log('Login bem-sucedido:', token);
        this.error = null;

        this.router.navigate(['/monitoramento-geral']);
      },
      (err) => {
        console.error('Erro ao fazer login:', err);
        this.error = err.error || 'Credenciais inválidas.';
      }
    );
  }
}
