namespace BattleCards.Controllers
{
    using BattleCards.Common;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (UserAuthentication.IsLogged(this.User))
            {
                return Redirect("/Cards/All");
            }

            return this.View();
        }

    }
}