import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Component } from '@angular/core';

@Component({
  selector: 'app-consulta-funcionarios',
  templateUrl: './consulta-funcionarios.component.html',
  styleUrls: ['./consulta-funcionarios.component.css']
})
export class ConsultaFuncionariosComponent {
  listagemFuncionarios: any[] = [];

  constructor(private httpClient: HttpClient) {}

  consultarFuncionarios(): void {
    this.httpClient.get<any[]>(environment.apiUrl + "/funcionarios")
      .subscribe(
        (data) => {
          this.listagemFuncionarios = data;
        },
        (error) => {
          console.log(error);
        }
      );
  }
}