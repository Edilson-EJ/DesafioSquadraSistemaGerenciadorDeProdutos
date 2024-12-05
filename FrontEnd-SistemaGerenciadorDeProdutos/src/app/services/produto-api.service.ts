import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ProdutoAPIService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Obter todos os produtos
  getProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${this.baseUrl}/api/Produto/getProduto`);
  }

  // Obter um produto por ID
  getProdutoById(id: number): Observable<Produto> {
    return this.http.get<Produto>(
      `${this.baseUrl}/api/Produto/getDetailProduto/${id}`
    );
  }

  // Adicionar um novo produto
  addProduto(produto: Produto): Observable<Produto> {
    return this.http.post<Produto>(
      `${this.baseUrl}/api/Produto/postProduto`,
      produto
    );
  }

  // Atualizar um produto existente
  updateProduto(id: number, produto: Produto): Observable<Produto> {
    return this.http.put<Produto>(
      `${this.baseUrl}/api/Produto/updateProduto/${id}`,
      produto
    );
  }

  // Excluir um produto
  deleteProduto(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/api/Produto/deleteProduto/${id}`
    );
  }
}
