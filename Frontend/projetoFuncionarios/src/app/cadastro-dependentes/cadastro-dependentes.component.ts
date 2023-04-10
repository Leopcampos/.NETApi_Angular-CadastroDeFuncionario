import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-cadastro-dependentes',
  templateUrl: './cadastro-dependentes.component.html',
  styleUrls: ['./cadastro-dependentes.component.css']
})
export class CadastroDependentesComponent {

  //injeção de dependência
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  cadastrarDependente(formCadastro: any): void {

    //requisição POST para API
    this.httpClient.post('http://localhost:5053/api/dependentes', formCadastro.form.value,
      { responseType: 'text' })
      .subscribe(//captura o promisse da API(retorno de sucesso ou erro)
        (data) => {//retorno de sucesso
          console.log(data)
        },
        e => {//retorno de erro
          console.log(e.error)
        }
      )
  }
}