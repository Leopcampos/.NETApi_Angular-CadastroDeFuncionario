import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Component } from '@angular/core';

@Component({
  selector: 'app-consulta-funcionarios',
  templateUrl: './consulta-funcionarios.component.html',
  styleUrls: ['./consulta-funcionarios.component.css']
})

export class ConsultaFuncionariosComponent {

  //atributos
  listagemFuncionarios: any[] = [];
  funcionario: any;

  mensagemSucesso: string = "";
  mensagemErro: string = "";

  //inicializando o componente HttpClient
  constructor(private httpClient: HttpClient) { }

  //função executada quando o componente é carregado 
  ngOnInit(): void {
    this.consultarFuncionarios();
  }

  //função executada para consultar os funcionarios
  consultarFuncionarios(): void {

    //enviando uma chamada GET para a api
    this.httpClient.get<any[]>(environment.apiUrl + "/funcionarios")
      .subscribe(
        (data: any[]) => {
          this.listagemFuncionarios = data;
        },
        e => {
          console.log(e);
        }
      );
  }

  //função para exibir os dados do funcionario e de seus dependentes..
  exibirDetalhes(item: any): void {
    this.funcionario = item;
  }

  //função para excluir um funcionario
  excluirFuncionario(item: any): void {

    //verificando se o usuario deseja excluir o funcionario
    if (window.confirm('Deseja excluir o funcionário?\n' + item.nome)) {

      //requisição HTTP DELETE para a API..
      this.httpClient.delete(environment.apiUrl + "/funcionarios/" + item.id,
        { responseType: 'text' })
        .subscribe(
          data => {
            this.mensagemSucesso = data; //exibindo mensagem..
            this.consultarFuncionarios(); //recarregando a consulta..
          },
          e => {
            switch (e.status) {
              case 403:
                this.mensagemErro = e.error;
                break;

              default:
                console.log(e);
                break;
            }
          }
        );
    }
  }

  //função para limpar as mensagens..
  limparMensagens(): void {
    this.mensagemSucesso = "";
    this.mensagemErro = "";
  }

}
