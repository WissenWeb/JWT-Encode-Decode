using JWT.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        //public string Get(string username,string password,string deneme)
        //{

        //    if (username == "wissen" & password == "test") { 

        //    }
        //    return "";
        //}
        [HttpGet]
        public ActionResult Login()
        {
            // Header kavramı : Header istek yaparken, bir üst bilgidir.
            // içerisinde, bir çok şey taşınabilir.
            // Kullanıcı adı şifre gibi veriler, 
            // Web apinin hangi veri tipi ile çalışacağı bilgisi
            // Tarih zaman damgası
            // Bir takım güvenlik parametreleri 
            //vb.

            // kitabi user'a kaydet
            //userid

            string email = Request.Headers["email"].ToString();

            string password = Request.Headers["password"].ToString();

            string jwt = new JWT().Encode(email, password);

            return Ok(new LoginViewModel
            {


                 JWT =jwt
            });
        }
        
    }
}
