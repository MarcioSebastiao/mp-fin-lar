using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Constantes;

namespace MpFinLar.API.Controllers;

public sealed class PessoaController : MainController
{
    private readonly IPessoaAplicacao _aplicacao;
    private readonly IMemoryCache _cache;

    public PessoaController(IPessoaAplicacao aplicacao, IMemoryCache cache)
    {
        _aplicacao = aplicacao;
        _cache = cache;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(PessoaDTO pessoaDTO)
    {
        (PessoaRespostaDTO? pessoa, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(pessoaDTO);
        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        _cache.Remove(CacheConstantes.PessoasRespostaCacheKey);

        return Ok(pessoa);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(Guid id, PessoaDTO pessoaDTO)
    {
        (PessoaRespostaDTO? pessoa, ResultadoAplicacao resultado) = await _aplicacao.AtualizarAsync(id, pessoaDTO);
        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        _cache.Remove(CacheConstantes.PessoasRespostaCacheKey);

        return Ok(pessoa);
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover(Guid id)
    {
        var resultado = await _aplicacao.RemoverAsync(id);
        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        _cache.Remove(CacheConstantes.PessoasRespostaCacheKey);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<PessoasRespostaDTO>> Obter(int pularItens, int quantidadeItens)
    {
        if (_cache.TryGetValue(CacheConstantes.PessoasRespostaCacheKey, out PessoasRespostaDTO? pessoas))
            return pessoas!;

        pessoas = await _aplicacao.ObterAsync(pularItens, quantidadeItens);
        _cache.Set(CacheConstantes.PessoasRespostaCacheKey, pessoas, TimeSpan.FromMinutes(10));

        return Ok(pessoas);
    }
}
