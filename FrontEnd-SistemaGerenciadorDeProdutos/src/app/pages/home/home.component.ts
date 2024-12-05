import { Component } from '@angular/core';
import { ProdutoAPIService } from '../../services/produto-api.service';
import { Produto } from '../../models/produto';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  produtos: Produto[] = [];

  constructor(private produtoService: ProdutoAPIService) {}

  ngOnInit(): void {
    this.produtoService.getProdutos().subscribe((data) => {
      this.produtos = data;
    });
  }
}
