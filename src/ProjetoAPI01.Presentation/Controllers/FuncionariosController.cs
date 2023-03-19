using Microsoft.AspNetCore.Mvc;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Entities;
using ProjettoAPI01.Presentation.Models;
using System;

namespace ProjettoAPI01.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(FuncionarioCadastroModel model, [FromServices] IFuncionarioRepository funcionarioRepository)
        {
            try
            {
                //Verificar se o CPF informado já está cadastrado
                if(funcionarioRepository.ObterPorCpf(model.Cpf) != null)
                {
                    //HTTP 400 - BAD REQUEST
                    return StatusCode(400, "");
                }

                //criando um objeto funcionario (entidade)
                var funcionario = new Funcionario();

                funcionario.Id = Guid.NewGuid();
                funcionario.Nome = model.Nome;
                funcionario.Cpf = model.Cpf;
                funcionario.Matricula = model.Matricula;
                funcionario.DataAdmissao = model.DataAdmissao;
                funcionario.Salario = model.Salario;
                funcionarioRepository.Inserir(funcionario);

                return Ok("Funcionário cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                //retornando um status de erro de servidor
                //(HTTP 500 - INTERNAL SERVER ERROR)
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}