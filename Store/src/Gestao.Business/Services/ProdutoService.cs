﻿using Gestao.Business.Interfaces;
using Gestao.Business.Models;
using Gestao.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUser _user;

        public ProdutoService(IProdutoRepository produtoRepository,
                                 INotificador notificador,
                                 IUser user) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _user = user;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;


            var user = _user.GetUserId();

            if (_produtoRepository.Buscar(p => p.Id == produto.Id).Result.Any())
            {
                Notificar("Já existe um produto com este id informado");
                return;
            }

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }
        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
