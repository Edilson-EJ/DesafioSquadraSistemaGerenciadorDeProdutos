import { Component } from '@angular/core';
import { AuthAPIService } from '../../services/auth-api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginModel } from '../../models/loginModel';

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

  constructor(private authAPIService: AuthAPIService) {}

  login(): void {
    // Envia o objeto loginModel ao serviço
    this.authAPIService.login(this.loginModel).subscribe(
      (response) => {
        this.token = response.token;
        console.log('Login bem-sucedido:', response.token);
        this.error = null;
      },
      (err) => {
        console.error('Erro ao fazer login:', err);
        this.error = err.error || 'Credenciais inválidas.';
        this.token = null;
      }
    );
  }
}
