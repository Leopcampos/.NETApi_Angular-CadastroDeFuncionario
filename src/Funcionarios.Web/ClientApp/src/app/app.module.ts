import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CadastroFuncionariosComponent } from './cadastro-funcionarios/cadastro-funcionarios.component';
import { ConsultaFuncionariosComponent } from './consulta-funcionarios/consulta-funcionarios.component';
import { CadastroDependentesComponent } from './cadastro-dependentes/cadastro-dependentes.component';
import { ConsultaDependentesComponent } from './consulta-dependentes/consulta-dependentes.component';

//importando a biblioteca para mapeamento de de rotas no Angular
import { RouterModule, Routes } from '@angular/router';

//importando a biblioteca para acesso à API 
import { HttpClientModule } from '@angular/common/http';

//importando a biblioteca para desenvolvimento de formulários
import { FormsModule, ReactiveFormsModule} from '@angular/forms';

//mapear as rotas de cada componente
const appRoutes: Routes = [
  {path: "cadastro-funcionarios", component: CadastroFuncionariosComponent},
  {path: "cadastro-dependentes", component: CadastroDependentesComponent},
  {path: "consulta-funcionarios", component: ConsultaFuncionariosComponent},
  {path: "consulta-dependentes", component: ConsultaDependentesComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    CadastroFuncionariosComponent,
    ConsultaFuncionariosComponent,
    CadastroDependentesComponent,
    ConsultaDependentesComponent
  ],
  imports: [
    BrowserModule,
    //Registrando as rotas do projeto
    RouterModule.forRoot(appRoutes),

    //Registrando o componente para requisições à API
    HttpClientModule,

    //Registrando o componente de desenvolvimento de formulários
    FormsModule, 
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
