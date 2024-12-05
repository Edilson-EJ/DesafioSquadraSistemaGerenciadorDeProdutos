import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UsuarioAPIService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Obter todos os usuários
  getUsuarios(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(`${this.baseUrl}/api/Usuario/getUsuario`);
  }

  // Obter um usuário por ID
  getUsuarioById(id: number): Observable<Usuario> {
    return this.http.get<Usuario>(
      `${this.baseUrl}/api/Usuario/getUsuarioDetail/${id}`
    );
  }

  // Adicionar um novo usuário
  addUsuario(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(
      `${this.baseUrl}/api/Usuario/postUsuario`,
      usuario
    );
  }

  // Atualizar um usuário existente
  updateUsuario(id: number, usuario: Usuario): Observable<Usuario> {
    return this.http.put<Usuario>(
      `${this.baseUrl}/api/Usuario/updateUsuario/${id}`,
      usuario
    );
  }

  // Excluir um usuário
  deleteUsuario(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/api/Usuario/deleteUsuario/${id}`
    );
  }
}
