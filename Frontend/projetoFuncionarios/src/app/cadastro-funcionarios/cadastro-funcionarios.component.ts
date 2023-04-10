import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-cadastro-funcionarios',
  templateUrl: './cadastro-funcionarios.component.html',
  styleUrls: ['./cadastro-funcionarios.component.css']
})
export class CadastroFuncionariosComponent {

  //atributos
  mensagemSucesso: string = '';
  mensagemErro: string = '';

  errosNome = [];
  errosCpf = [];
  errosMatricula = [];
  errosDataAdmissao = [];
  errosSalario = [];

  //injeção de dependência
  constructor(private httpClient: HttpClient) { }

  //função executada quando o componente é carregado
  ngOnInit(): void {
  }

  //função executada no SUBMIT do formulario
  cadastrarFuncionario(formCadastro: any): void {

    this.limparMensagens(); //executando a função para limpas as mensagens da página

    //requisição POST para API
    this.httpClient.post(environment.apiUrl + '/funcionarios', formCadastro.form.value,
      { responseType: 'text' })
      .subscribe(//captura o promisse da API(retorno de sucesso ou erro)
        (data) => {//retorno de sucesso

          //escrevendo a mensagem de sucesso
          this.mensagemSucesso = data;

          //limpar os campos do formulário
          formCadastro.form.reset();
        },
        e => {//retorno de erro
          console.log(e);

          switch (e.status) {
            case 400:
              var result = JSON.parse(e.error);
              
              this.errosNome = result.errors.Nome;
              this.errosCpf = result.errors.Cpf;
              this.errosMatricula = result.errors.Matricula;
              this.errosDataAdmissao = result.errors.DataAdmissao;
              this.errosSalario = result.errors.Salario;

              break;

            case 403:
              this.mensagemErro = e.error;
              break;

            case 500:
              this.mensagemErro = e.error;
              break;
          }
        }
      )
  }

  //função para limpar as mensagens do formulário
  limparMensagens(): void {
    this.mensagemSucesso = "";
    this.mensagemErro = "";

    this.errosNome = [];
    this.errosCpf = [];
    this.errosMatricula = [];
    this.errosDataAdmissao = [];
    this.errosSalario = [];
  }
}