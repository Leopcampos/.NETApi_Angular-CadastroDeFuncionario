import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-cadastro-dependentes',
  templateUrl: './cadastro-dependentes.component.html',
  styleUrls: ['./cadastro-dependentes.component.css']
})
export class CadastroDependentesComponent {

  //atributo
  listagemFuncionarios: any[] = [];
  mensagem: string = ""; //exibir mensagens de erro ou sucesso

  //atributos para capturar as mensagens de erro
  errosNome = [];
  errosDataNascimento = [];
  errosFuncionarioId = [];

  //injeção de dependência
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    // chamada para consultar os funcionários
    this.consultarFuncionarios();
  }

  //função executada para consultar os funcionarios
  consultarFuncionarios(): void {
    //enviando uma chamada GET para a api
    this.httpClient.get<any[]>(environment.apiUrl + "/funcionarios")
      .subscribe(
        (data: any[]) => { //retorno de sucesso da API
          this.listagemFuncionarios = data;
        },
        e => {
          console.log(e);
        }
      );
  }

  //função para realizar o cadastro do dependente
  cadastrarDependente(formCadastro: any): void {

    //limpar as mensagens de erro
    this.errosNome = [];
    this.errosDataNascimento = [];
    this.errosFuncionarioId = [];

    this.mensagem = "Processando, por favor aguarde...";

    //executando a requisição POST para a API..
    this.httpClient.post(environment.apiUrl + '/dependentes',
      formCadastro.form.value, { responseType: 'text' })
      .subscribe(
        data => { //retorno de sucesso da API..
          this.mensagem = data; //exibindo a mensagem obtida da API..
          formCadastro.form.reset(); //limpar o conteudo do formulário
        },
        e => { //retorno de erro da API..

          this.mensagem = "";

          //verificar o tipo de erro retornado pela api..
          switch (e.status) {
            case 400: //BAD REQUEST (Erro de validação)
              var result = JSON.parse(e.error);

              this.errosNome = result.errors.Nome;
              this.errosDataNascimento = result.errors.DataNascimento;
              this.errosFuncionarioId = result.errors.FuncionarioId;

              break;

            case 500: //INTERNAL SERVER ERROR
              this.mensagem = e.error;
              break;
          }
        }
      )
  }

  //função para limpar todas as mensagens da página
  limparMensagens(): void {
    this.errosNome = [];
    this.errosDataNascimento = [];
    this.errosFuncionarioId = [];

    this.mensagem = "";
  }
}
