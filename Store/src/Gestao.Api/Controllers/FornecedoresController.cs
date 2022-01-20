using Gestao.Api.ViewModels;
using Gestao.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedoresController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();

            return Ok(fornecedores);
        }
    }
}
