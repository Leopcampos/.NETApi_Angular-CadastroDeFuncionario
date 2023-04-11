using Microsoft.AspNetCore.Mvc;
using ProjetoAPI01.Presentation.Models.Dependente;
using ProjetoAPI01.Presentation.Models.Funcionario;
using ProjetoAPI01.Repository.Contracts;
using ProjetoAPI01.Repository.Entities;

namespace ProjetoAPI01.Presentation.Controllers
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
                if (funcionarioRepository.ObterPorCpf(model.Cpf) != null)
                {
                    //HTTP 403 - FORBIDDEN
                    return StatusCode(403, "O CPF informado já encontra-se cadastrado.");
                }

                //criando um objeto funcionario (entidade)
                var funcionario = new Funcionario();

                funcionario.Id = Guid.NewGuid();
                funcionario.Nome = model.Nome;
                funcionario.Cpf = model.Cpf;
                funcionario.Matricula = model.Matricula;
                funcionario.DataAdmissao = DateTime.Parse(model.DataAdmissao);
                funcionario.Salario = decimal.Parse(model.Salario);
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
        public IActionResult Put(FuncionarioEdicaoModel model, [FromServices] IFuncionarioRepository funcionarioRepository)
        {
            try
            {
                //Buscando o funcionário no banco de dados através do ID
                var funcionario = funcionarioRepository.ObterPorId(Guid.Parse(model.Id));

                //Verificando se o funcionário foi encontrado
                if (funcionario != null)
                {
                    funcionario.Nome = model.Nome;
                    funcionario.Matricula = model.Matricula;
                    funcionario.Salario = decimal.Parse(model.Salario);
                    funcionario.DataAdmissao = DateTime.Parse(model.DataAdmissao);

                    funcionarioRepository.Alterar(funcionario);
                    return Ok("Funcionário atualizado com sucesso.");
                }
                else
                {
                    //HTTP 400 - BAD REQUEST
                    return BadRequest("Funcionário não foi encontrado. Verifique o ID enviado");
                }

            }
            catch (Exception e)
            {
                // retornando um status de erro de servidor
                //(HTTP 500 - INTERNAL SERVER ERROR)
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, [FromServices] IFuncionarioRepository funcionarioRepository)
        {
            try
            {
                //Buscando o funcionário no banco de dados através do ID
                var funcionario = funcionarioRepository.ObterPorId(id);

                //Verificando se o funcionário foi encontrado
                if (funcionario != null)
                {
                    //verificar se o funcionario não possui dependentes..
                    if (funcionarioRepository
                    .ObterDependentes(funcionario.Id).Count == 0)
                    {
                        funcionarioRepository.Excluir(funcionario);
                        return Ok("Funcionário excluído com sucesso.");
                    }
                    else
                    {
                        return StatusCode(403, "Não é permitido excluir um funcionário que possua dependentes.");
                    }
                }
                else
                {
                    return BadRequest("Funcionário não foi encontrado. Verifique o ID enviado");
                }
            }
            catch (Exception e)
            {
                // retornando um status de erro de servidor
                //(HTTP 500 - INTERNAL SERVER ERROR)
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromServices] IFuncionarioRepository funcionarioRepository)
        {
            try
            {
                //criando uma lista da classe FuncionarioConsultaModel
                var result = new List<FuncionarioConsultaModel>();
                foreach (var item in funcionarioRepository.ObterTodos())
                {
                    result.Add(new FuncionarioConsultaModel
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Matricula = item.Matricula,
                        Cpf = item.Cpf,
                        Salario = item.Salario,
                        DataAdmissao = item.DataAdmissao,
                        Dependentes = new List<DependenteItemConsultaModel>()
                    });
                }
                //adicionando os dependentes de cada funcionario
                foreach (var item in result)
                {
                    //buscando os dependentes de cada funcionario
                    foreach (var dependente in funcionarioRepository.ObterDependentes(item.Id))
                    {
                        item.Dependentes.Add(new DependenteItemConsultaModel
                        {
                            Id = dependente.Id,
                            Nome = dependente.Nome,
                            DataNascimento = dependente.DataNascimento
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                // retornando um status de erro de servidor
                //(HTTP 500 - INTERNAL SERVER ERROR)
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id, [FromServices] IFuncionarioRepository funcionarioRepository)
        {
            try
            {
                //buscando os dados do funcionario baseado no ID..
                var funcionario = funcionarioRepository.ObterPorId(id);
                //verificando se o funcionario foi encontrado..
                if (funcionario != null)
                {
                    var result = new FuncionarioConsultaModel
                    {
                        Id = funcionario.Id,
                        Nome = funcionario.Nome,
                        Matricula = funcionario.Matricula,
                        Cpf = funcionario.Cpf,
                        Salario = funcionario.Salario,
                        DataAdmissao = funcionario.DataAdmissao
                    };

                    //buscando os dependentes de cada funcionario
                    foreach (var dependente in funcionarioRepository.ObterDependentes(result.Id))
                    {
                        result.Dependentes.Add(new DependenteItemConsultaModel
                        {
                            Id = dependente.Id,
                            Nome = dependente.Nome,
                            DataNascimento = dependente.DataNascimento
                        });
                    }
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Funcionário não foi encontrado. Verifique o ID enviado.");
                }
            }
            catch (Exception e)
            {
                //retornando um status de erro de servidor
                //(HTTP 500 - INTERNAL SERVER ERROR)
                return StatusCode(500, e.Message);
            }
        }
    }
}