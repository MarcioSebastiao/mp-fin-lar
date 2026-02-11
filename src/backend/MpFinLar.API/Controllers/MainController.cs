using Microsoft.AspNetCore.Mvc;

namespace MpFinLar.API.Controllers;

/// <summary>
/// Classe base para os controllers
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class MainController : ControllerBase
{

    /// <summary>
    /// Retorna um BadRequest contendo as mensagens de erro
    /// </summary>
    /// <param name="MensagensDeErros">
    /// Coleção de mensagens de erro a serem retornadas na resposta.
    /// </param>
    protected ActionResult RespostaDeErro(IEnumerable<string> MensagensDeErros)
    {
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", MensagensDeErros.ToArray()}
            }));
    }
}
