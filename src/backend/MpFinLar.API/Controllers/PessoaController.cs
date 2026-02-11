using Microsoft.AspNetCore.Mvc;
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
        var resultado = await _aplicacao.CriarAsync(pessoaDTO);
        if (resultado.Sucesso)
            return Ok();
        
        return RespostaDeErro(resultado.Notificacoes);
    }
}
