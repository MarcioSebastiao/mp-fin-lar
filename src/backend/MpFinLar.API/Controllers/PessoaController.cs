using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Pessoas;

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
        (PessoaRespostaDTO? pessoa, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(pessoaDTO);

        if (resultado.Sucesso)
            return Ok(pessoa);

        return RespostaDeErro(resultado.Notificacoes);

    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(Guid id, PessoaDTO pessoaDTO)
    {
        (PessoaRespostaDTO? pessoa, ResultadoAplicacao resultado) = await _aplicacao.AtualizarAsync(id, pessoaDTO);
        if (resultado.Sucesso)
            return Ok(pessoa);

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
    public async Task<ActionResult<PessoasRespostaDTO>> Obter(int pularItens, int quantidadeItens)
    {
        var pessoas = await _aplicacao.ObterAsync(pularItens, quantidadeItens);
        return Ok(pessoas);
    }
}
