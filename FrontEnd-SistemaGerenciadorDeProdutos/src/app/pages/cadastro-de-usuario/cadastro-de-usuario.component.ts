import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioAPIService } from '../../services/usuario-api.service';
import { Usuario } from '../../models/usuario';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cadastro-de-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cadastro-de-usuario.component.html',
  styleUrls: ['./cadastro-de-usuario.component.scss'],
})
export class CadastroDeUsuarioComponent {
  cadastroForm: FormGroup;
  errorMessage: string | null = null;
  funcoes: string[] = ['Gerente', 'Funcionário'];

  constructor(
    private fb: FormBuilder,
    private usuarioAPIService: UsuarioAPIService,
    private router: Router
  ) {
    // Inicializa o formulário
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(4)]],
      funcao: ['', Validators.required],
    });
  }

  // Método de envio do formulário
  onSubmit(): void {
    if (this.cadastroForm.valid) {
      const novoUsuario: Usuario = {
        id: 0,
        nome: this.cadastroForm.value.nome,
        email: this.cadastroForm.value.email,
        senhaHash: this.cadastroForm.value.senha,
        funcao: this.cadastroForm.value.funcao,
      };

      this.usuarioAPIService.addUsuario(novoUsuario).subscribe(
        () => {
          console.log('Usuário cadastrado com sucesso!');
          this.errorMessage = null;
          this.router.navigate(['/monitoramento-geral']);
        },
        (error) => {
          console.error('Erro ao cadastrar usuário:', error);
          this.errorMessage = 'Falha ao cadastrar usuário. Tente novamente.';
        }
      );
    } else {
      this.errorMessage = 'Preencha o formulário corretamente.';
    }
  }
  onCancel(): void {
    this.router.navigate(['/monitoramento-geral']);
  }
}
