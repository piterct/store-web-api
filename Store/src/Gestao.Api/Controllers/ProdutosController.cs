using AutoMapper;
using Gestao.Business.Interfaces;

namespace Gestao.Api.Controllers
{
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutosController(INotificador notificador) : base(notificador)
        {
        }
    }
}
