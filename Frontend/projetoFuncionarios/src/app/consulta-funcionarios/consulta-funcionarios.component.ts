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
}