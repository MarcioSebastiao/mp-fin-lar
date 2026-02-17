using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Dominio.Modelos;

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

    [HttpGet("{pessoaId:guid}")]
    public async Task<ActionResult<TransacoesRespostaDTO>> Obter(Guid pessoaId, int pularItens, int quantidadeItens)
    {
        return Ok(await _aplicacao.ObterTransacoesDePessoaAsync(pessoaId, pularItens, quantidadeItens));
    }

    [HttpGet("{categoriaId:guid}/valores-transacoes")]
    public async Task<ActionResult<ValoresTransacoes>> ObterValoresPorCategoria(Guid categoriaId, int pularItens, int quantidadeItens)
    {
        return Ok(await _aplicacao.ObterValoresPorCategoriaAsync(categoriaId));
    }

}