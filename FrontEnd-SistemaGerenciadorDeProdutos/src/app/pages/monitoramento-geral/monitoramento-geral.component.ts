import { Component } from '@angular/core';
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
  styleUrl: './monitoramento-geral.component.scss',
})
export class MonitoramentoGeralComponent {
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
    // Recupera o cargo do usuário armazenado no localStorage
    this.userRole = localStorage.getItem('userRole') || '';
    console.log('função', this.userRole);
    if (!this.userRole) {
      // Se o cargo não estiver presente no localStorage, redireciona para login
      this.router.navigate(['/login']);
    } else {
      this.loadProdutos();
      if (this.userRole === 'gerente' || this.userRole === 'Gerente') {
        this.loadUsuarios();
      }
    }
  }

  // Carregar produtos
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

  // Atualizar produto
  onUpdateProduto(produtoId: number): void {
    const produto = this.produtos.find((p) => p.id === produtoId);
    if (produto) {
      this.produtoAPIService.updateProduto(produtoId, produto).subscribe(
        (updatedProduto) => {
          console.log('Produto atualizado com sucesso:', updatedProduto);
          this.loadProdutos();
        },
        (error) => {
          console.error('Erro ao atualizar o produto:', error);
        }
      );
    } else {
      console.error('Produto não encontrado para atualização');
    }
  }

  // Deletar produto (somente gerente)
  onDeleteProduto(produtoId: number): void {
    if (
      confirm(`Tem certeza que deseja excluir o produto com ID ${produtoId}?`)
    ) {
      this.produtoAPIService.deleteProduto(produtoId).subscribe(
        () => {
          console.log('Produto deletado com sucesso');
          this.loadProdutos();
        },
        (error) => {
          console.error('Erro ao deletar o produto:', error);
        }
      );
    }
  }

  // Carregar usuários (somente gerente)
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

  // Atualizar usuário (somente gerente)
  onUpdateUsuario(userId: number): void {
    console.log(`Atualizar usuário com ID: ${userId}`);
    // Lógica para atualizar o usuário
  }

  // Deletar usuário (somente gerente)
  onDeleteUsuario(userId: number): void {
    if (confirm(`Tem certeza que deseja excluir o usuário com ID ${userId}?`)) {
      this.usuarioAPIService.deleteUsuario(userId).subscribe(
        () => {
          console.log('Usuário deletado com sucesso');
          this.loadUsuarios();
        },
        (error) => {
          console.error('Erro ao deletar o usuário:', error);
        }
      );
    }
  }

  // Adicionar um novo usuário
  onAddUsuario(): void {
    this.router.navigate(['/cadastro-de-usuario']);
  }

  onLogout(): void {
    // Limpar o token e a função (role) armazenados no localStorage
    this.authAPIService.clearToken();
    localStorage.removeItem('userRole');

    // Redirecionar para a página de login
    this.router.navigate(['/']);
  }
}
