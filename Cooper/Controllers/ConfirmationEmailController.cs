using Cooper.Configuration;
using Cooper.Repository;
using Cooper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ConfirmationEmailController : ControllerBase
{
    private readonly UserRepository userRepository;

    public ConfirmationEmailController(IJwtHandlerService jwtHandler, IConfigProvider configProvider)
    {
        userRepository = new UserRepository(jwtHandler, configProvider);
    }

    /// <summary>
    /// Confirms email by token.
    /// </summary>
    /// <remarks>
    /// If the user is confirmed, then a redirect to "/auth".
    /// If the user is not confirmed, then he is confirmed and a redirect to "/auth".
    /// </remarks>
    /// <returns>A redirect to "/auth"</returns>
    /// <response code="302">Always</response>  
    [HttpPost]
    [Route("confirm")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult Confirm()
    {
        IActionResult result;

        string token = Request.Query["token"];
        string email = userRepository.GetVerifyEmail($"\'{token}\'");

        if (email == null)
        {
            result = Redirect("/auth");
        }
        else
        {
            userRepository.ConfirmEmail(token, email);
            userRepository.DeleteToken($"\'{token}\'");

            result = Redirect("/auth");
            //TODO: Auth
        }

        return result;
    }
}
