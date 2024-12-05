import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ProdutoAPIService } from '../../services/produto-api.service';
import { Produto } from '../../models/produto';

@Component({
  selector: 'app-cadastro-de-produto',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './cadastro-de-produto.component.html',
  styleUrls: ['./cadastro-de-produto.component.scss'],
})
export class CadastroDeProdutoComponent {
  produto: Produto = {
    id: 0, // Inicializado com zero ou `null` conforme necessário
    nome: '',
    descricao: '',
    preco: 0,
    quantidadeEstoque: 0,
    status: 'Em estoque',
  };

  constructor(private produtoAPIService: ProdutoAPIService) {}

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.produtoAPIService.addProduto(this.produto).subscribe({
        next: (response) => {
          console.log('Produto cadastrado com sucesso!', response);
          form.resetForm();
        },
        error: (error) => {
          console.error('Erro ao cadastrar produto:', error);
        },
      });
    } else {
      console.error('Formulário inválido!');
    }
  }
}
