import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ProdutoAPIService } from '../../services/produto-api.service';
import { Produto } from '../../models/produto';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cadastro-de-produto',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './cadastro-de-produto.component.html',
  styleUrls: ['./cadastro-de-produto.component.scss'],
})
export class CadastroDeProdutoComponent implements OnInit {
  produtoForm!: FormGroup;

  constructor(
    private produtoAPIService: ProdutoAPIService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.produtoForm = this.fb.group({
      nome: ['', Validators.required],
      descricao: ['', Validators.required],
      status: ['', Validators.required],
      preco: ['', [Validators.required, Validators.min(0)]],
      quantidadeEstoque: ['', [Validators.required, Validators.min(0)]],
    });
  }

  onSubmit(): void {
    if (this.produtoForm.valid) {
      const produto: Produto = this.produtoForm.value;

      this.produtoAPIService.addProduto(produto).subscribe({
        next: (response) => {
          console.log('Produto cadastrado com sucesso!', response);

          this.router.navigate(['/monitoramento-geral']);
        },
        error: (error) => {
          console.error('Erro ao cadastrar produto:', error);
        },
      });
    } else {
      console.error('Formulário inválido!');
    }
  }

  onCancel(): void {
    this.router.navigate(['/monitoramento-geral']);
  }
}
