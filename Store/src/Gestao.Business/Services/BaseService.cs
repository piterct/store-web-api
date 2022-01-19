﻿using FluentValidation;
using FluentValidation.Results;
using Gestao.Business.Interfaces;
using Gestao.Business.Models;
using Gestao.Business.Notificacoes;

namespace Gestao.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);
            if(validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
