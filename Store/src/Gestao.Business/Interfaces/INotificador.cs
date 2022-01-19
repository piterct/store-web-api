﻿using Gestao.Business.Notificacoes;
using System.Collections.Generic;

namespace Gestao.Business.Interfaces
{
    public  interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
