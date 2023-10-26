import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-consulta-dependentes',
  templateUrl: './consulta-dependentes.component.html',
  styleUrls: ['./consulta-dependentes.component.css']
})
export class ConsultaDependentesComponent implements OnInit {

  //atributos
  listagemDependentes: any[] = [];
  mensagem: string = "";

  //declarando e inicializando o httpClient
  constructor(private httpClient: HttpClient) { }

  //função executada quando o componente é carregado
  ngOnInit(): void {
    this.consultarDependentes(); //executando a consulta
  }

  //função para realizar a consulta de dependentes
  consultarDependentes(): void {
    this.httpClient.get<any[]>(environment.apiUrl + "/dependentes")
      .subscribe(
        (data: any[]) => {
          this.listagemDependentes = data;
        },
        e => {
          console.log(e);
        }
      );
  }

  //função para excluir o dependente
  excluirDependente(id: any): void {

    if (window.confirm('Deseja realmente excluir o dependente?')) {

      //realizando a chamada de exclusão da API..
      this.httpClient.delete(environment.apiUrl + '/dependentes/' + id,
        { responseType: 'text' })
        .subscribe(
          data => {
            this.mensagem = data;
            this.consultarDependentes();
          },
          e => {
            console.log(e);
          }
        );
    }
  }

  //função para limpar as mensagens..
  limparMensagens(): void {
    this.mensagem = "";
  }
}
