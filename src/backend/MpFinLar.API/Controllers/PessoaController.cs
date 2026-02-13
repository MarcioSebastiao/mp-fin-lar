using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Controllers;

public sealed class PessoaController : MainController
{
    private readonly IPessoaAplicacao _aplicacao;

    public PessoaController(IPessoaAplicacao aplicacao)
    {
        _aplicacao = aplicacao;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(PessoaDTO pessoaDTO)
    {
        (Pessoa? pessoa, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(pessoaDTO);

        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        return Ok(new PessoaRespostaDTO { Id = pessoa!.Id, Nome = pessoa.Nome, Idade = pessoa.Idade });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(Guid id, PessoaDTO pessoaDTO)
    {
        (Pessoa? pessoa, ResultadoAplicacao resultado) = await _aplicacao.AtualizarAsync(id, pessoaDTO);
        if (resultado.Sucesso)
            return Ok(new PessoaRespostaDTO { Id = pessoa!.Id, Nome = pessoa.Nome, Idade = pessoa.Idade });

        return RespostaDeErro(resultado.Notificacoes);
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover(Guid id)
    {
        var resultado = await _aplicacao.RemoverAsync(id);
        if (resultado.Sucesso)
            return Ok();

        return RespostaDeErro(resultado.Notificacoes);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaRespostaDTO>>> Obter()
    {
        var pessoas = await _aplicacao.ObterAsync();
        return Ok(pessoas);
    }
}
