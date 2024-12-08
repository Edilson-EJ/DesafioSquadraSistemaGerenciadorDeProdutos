import { Component, OnInit } from '@angular/core';
import { Produto } from '../../models/produto';
import { Usuario } from '../../models/usuario';
import { ProdutoAPIService } from '../../services/produto-api.service';
import { UsuarioAPIService } from '../../services/usuario-api.service';
import { AuthAPIService } from '../../services/auth-api.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-monitoramento-geral',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './monitoramento-geral.component.html',
  styleUrls: ['./monitoramento-geral.component.scss'],
})
export class MonitoramentoGeralComponent implements OnInit {
  produtos: Produto[] = [];
  usuarios: Usuario[] = [];
  userRole: string = '';

  constructor(
    private produtoAPIService: ProdutoAPIService,
    private usuarioAPIService: UsuarioAPIService,
    private authAPIService: AuthAPIService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      this.userRole = localStorage.getItem('userRole') || '';
    }

    if (!this.userRole) {
      this.router.navigate(['/login']);
      return;
    }

    this.loadProdutos();

    if (this.userRole.toLowerCase() === 'gerente') {
      this.loadUsuarios();
    }
  }

  loadProdutos(): void {
    this.produtoAPIService.getProdutos().subscribe(
      (data) => {
        this.produtos = data;
      },
      (error) => {
        console.error('Erro ao carregar produtos:', error);
      }
    );
  }

  onUpdateProduto(produtoId: number): void {
    const produto = this.produtos.find((p) => p.id === produtoId);
    if (produto) {
      this.produtoAPIService.updateProduto(produtoId, produto).subscribe(
        () => {
          this.loadProdutos();
        },
        (error) => {
          console.error('Erro ao atualizar o produto:', error);
        }
      );
    }
  }

  onDeleteProduto(produtoId: number): void {
    if (
      confirm(`Tem certeza que deseja excluir o produto com ID ${produtoId}?`)
    ) {
      this.produtoAPIService.deleteProduto(produtoId).subscribe(
        () => {
          this.loadProdutos();
        },
        (error) => {
          console.error('Erro ao deletar o produto:', error);
        }
      );
    }
  }

  loadUsuarios(): void {
    this.usuarioAPIService.getUsuarios().subscribe(
      (data) => {
        this.usuarios = data;
      },
      (error) => {
        console.error('Erro ao carregar usuários:', error);
      }
    );
  }

  onUpdateUsuario(userId: number): void {
    console.log(`Atualizar usuário com ID: ${userId}`);
  }

  onDeleteUsuario(userId: number): void {
    if (confirm(`Tem certeza que deseja excluir o usuário com ID ${userId}?`)) {
      this.usuarioAPIService.deleteUsuario(userId).subscribe(
        () => {
          this.loadUsuarios();
        },
        (error) => {
          console.error('Erro ao deletar o usuário:', error);
        }
      );
    }
  }

  onAddUsuario(): void {
    this.router.navigate(['/cadastro-de-usuario']);
  }

  onAddProduto(): void {
    this.router.navigate(['/cadastro-de-produto']);
  }

  onLogout(): void {
    this.authAPIService.clearToken();
    localStorage.removeItem('userRole');
    this.router.navigate(['/']);
  }
}
