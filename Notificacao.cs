using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pier.Web.Acesso.Bll
{
    public class Notificacao : Pier.Base.Bll.BaseAbstractBll<Entity.Acesso.Notificacao>, IDisposable
    {
        public static void InformaMensagemLida(Entity.Acesso.Usuario usuario, long idMensagem)
        {
            using (var s = Pier.Base.Dao.DaoUtil.GetSessao(Entity.Banco.SGTAP, Entity.Ambiente.TECON_SANTOS))
                new Pier.Web.Acesso.Dao.Acesso.Notificacao(s).InformaMensagemLida(usuario, idMensagem);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}