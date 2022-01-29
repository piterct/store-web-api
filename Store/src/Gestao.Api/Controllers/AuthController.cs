using Gestao.Business.Interfaces;

namespace Gestao.Api.Controllers
{
    public class AuthController : MainController
    {
        public AuthController(INotificador notificador) : base(notificador)
        {
        }
    }
}
