export interface Produto {
  id: number;
  nome: string;
  descricao?: string;
  status: string;
  preco: number;
  quantidadeEstoque: number;
}
