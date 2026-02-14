using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Transacoes;

namespace MpFinLar.API.Controllers;

public sealed class TransacaoController : MainController
{
    private readonly ITransacaoAplicacao _aplicacao;
    public TransacaoController(ITransacaoAplicacao aplicacao)
    {
        _aplicacao = aplicacao;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(TransacaoDto transacaoDto)
    {
        (TransacaoRespostaDTO? transacao, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(transacaoDto);

        if (resultado.Sucesso)
            return Ok(transacao);

        return RespostaDeErro(resultado.Notificacoes);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoRespostaDTO>>> Obter()
    {
        return Ok(await _aplicacao.ObterAsync());
    }
}