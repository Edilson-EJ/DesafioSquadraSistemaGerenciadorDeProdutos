import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { CadastroDeProdutoComponent } from './pages/cadastro-de-produto/cadastro-de-produto.component';
import { HomeComponent } from './pages/home/home.component';
import { MonitoramentoGeralComponent } from './pages/monitoramento-geral/monitoramento-geral.component';
import { CadastroDeUsuarioComponent } from './pages/cadastro-de-usuario/cadastro-de-usuario.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'cadastroDeProdutos', component: CadastroDeProdutoComponent },
  { path: 'monitoramento-geral', component: MonitoramentoGeralComponent },
  { path: 'cadastro-de-usuario', component: CadastroDeUsuarioComponent },
];
