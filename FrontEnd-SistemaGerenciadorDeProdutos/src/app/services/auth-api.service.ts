import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { LoginModel } from '../models/loginModel';

@Injectable({
  providedIn: 'root',
})
export class AuthAPIService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Login e armazenar token
  login(credentials: LoginModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Auth/login`, credentials);
  }

  // Salvar token no localStorage
  storeToken(token: string): void {
    localStorage.setItem('authToken', token);

    // Decodificar o token JWT para extrair a role
    const userRole = this.decodeTokenRole(token);
    if (userRole) {
      localStorage.setItem('userRole', userRole); // Salvar role no localStorage
    }
  }

  // Recuperar token armazenado
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Remover token ao fazer logout
  clearToken(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userRole'); // Remover role também
  }

  private decodeTokenRole(token: string): string | null {
    if (!token) {
      return null;
    }

    try {
      // Decodifica o token JWT e extrai o payload
      const payload = JSON.parse(atob(token.split('.')[1]));

      // Acessa o valor da claim 'role' no token (cuidado com o namespace completo)
      return (
        payload[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ] || null
      );
    } catch (error) {
      console.error('Erro ao decodificar o token:', error);
      return null;
    }
  }

  // Verificar se o usuário está autenticado
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  // Recuperar a role do usuário armazenada no localStorage
  getUserRole(): string | null {
    return localStorage.getItem('userRole');
  }
}
