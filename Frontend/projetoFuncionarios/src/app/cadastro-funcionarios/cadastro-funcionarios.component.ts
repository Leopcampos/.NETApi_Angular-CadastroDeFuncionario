import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-cadastro-funcionarios',
  templateUrl: './cadastro-funcionarios.component.html',
  styleUrls: ['./cadastro-funcionarios.component.css']
})
export class CadastroFuncionariosComponent {

  //injeção de dependência
  constructor(private httpClient:HttpClient) { }

  ngOnInit(): void {
  }

  cadastrarFuncionario(formCadastro: any): void {
    
    //requisição POST para API
    this.httpClient.post('http://localhost:5053/api/funcionarios', formCadastro.form.value,
    {responseType: 'text'})
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