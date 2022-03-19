using Gestao.Business.Interfaces;
using Gestao.Business.Models;
using Gestao.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly INotificador _notificador;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                  IEnderecoRepository enderecoRepository,
                                  INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
            _notificador = notificador;
        }

        public async Task<bool> Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado");
                return false;
            }

            if (_notificador.TemNotificacao())
                return false;


            await _fornecedorRepository.Adicionar(fornecedor);
            return true;
        }

        public async Task<bool> Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;

            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return false;
            }



            await _fornecedorRepository.Atualizar(fornecedor);
            return true;

        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return false;
            }

            var enderecoFornecedor = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

            if (enderecoFornecedor != null && enderecoFornecedor.FornecedorId == id)
                await _enderecoRepository.Remover(enderecoFornecedor.Id);

            await _fornecedorRepository.Remover(id);

            return true;
        }

        public void Dispose()
        {
            _enderecoRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
