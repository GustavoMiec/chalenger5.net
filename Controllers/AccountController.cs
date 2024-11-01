using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(string idToken)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            var uid = decodedToken.Uid;
            
            // Usuário autenticado com sucesso, redirecione ou retorne a resposta desejada
            return Ok(new { UserId = uid });
        }
        catch
        {
            return Unauthorized();
        }
    }
}
