using JWT.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        [HttpPost]
        public ActionResult AddBook(BookViewModel model)
        {


            string jwt = Request.Headers["jwt"].ToString();


            JWT j = new JWT();
            var opentoken =j.DecodeJWT(jwt);
            /////db kaydet
            ///
            return Ok();
        }
    }
}
