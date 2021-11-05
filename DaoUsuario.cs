using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord.Framework;
using NHibernate.Criterion;
using NHibernate;
using System.Diagnostics;
using System.Security.Principal;

namespace Pier.Dao.Acesso
{
    /// <summary>
    /// Classe de acesso a dados da entidade Usuário.
    /// </summary>
    public class DaoUsuario : Pier.Base.Dao.BaseDao<Pier.Entity.Acesso.Usuario>
    {
        /// <summary>
        /// Construtor da classe Dao.Usuario.
        /// </summary>
        /// <param name="acesso">Perfil de acesso utilizado para acessar dados da entidade.</param>
        public DaoUsuario(Pier.Entity.Ambiente ambiente, ISession sessao)
            : base(ambiente,  sessao)
        {
        }

        /// <summary>
        /// Método síncrono de busca de dados da entidade Usuario por nome e pessoa.
        /// </summary>
        /// <param name="pessoa">Pessoa do Usuario.</param>
        /// <param name="senha">Senha do Usuario.</param>
        /// <returns>Retorna Usuario de acordo com sua Pessoa e Senha.</returns>
        public Pier.Entity.Acesso.Usuario GetUsuarioAtivosByNome(Pier.Entity.Ambiente ambiente, Pier.Entity.Pessoa pessoa, string senha)
        {
            try
            {
                var session = Pier.Base.Dao.DaoUtil.GetSessao(Pier.Entity.Banco.SGTAP, ambiente);
                ICriteria crit = session.CreateCriteria(typeof(Pier.Entity.Acesso.Usuario));

                crit.Add(Expression.Eq("Pessoa", pessoa));
                crit.Add(Expression.Eq("Senha", senha));

                IList<Pier.Entity.Acesso.Usuario> list = crit.List<Pier.Entity.Acesso.Usuario>();

                if (list.Count > 0)
                {
                    list[0] = (Pier.Entity.Acesso.Usuario)Pier.Base.Dao.LazyInitializer.TratarProxies(list[0], true, true, session);
                    //session.Close(); 
                    return list[0];
                }
                else
                {
                    //session.Close(); 
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Pier.Base.Dao.DaoException("Erro no Método GetUsuarioAtivosByNome " + ex.Message, ex);
            }
        }

        /*public Pier.Entity.Acesso.Usuario GetUsuarioLogado()
        {
            try
            {
                var session = DaoUtil.GetSessao(Pier.Entity.Banco.SGTAP,base.Ambiente);
                return base.RetornarUsuario(session);
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro no Método GetUsuario " + ex.Message, ex);
            }
        }*/
        
        public List<Pier.Entity.Acesso.Usuario> UsuariosLogados()
        {
            return Pier.Base.Dao.DaoUtil.Usuarios.Values.ToList();            
        }

        public Pier.Entity.Acesso.Usuario GetUsuario(Pier.Entity.Pessoa pessoa)
        {
            try
            {
                var session = Pier.Base.Dao.DaoUtil.GetSessao(Pier.Entity.Banco.SGTAP, Ambiente);
                ICriteria crit = session.CreateCriteria(typeof(Pier.Entity.Acesso.Usuario));

                crit.Add(Expression.Eq("Pessoa", pessoa));

                IList<Pier.Entity.Acesso.Usuario> list = crit.List<Pier.Entity.Acesso.Usuario>();

                if (list.Count > 0)
                {
                    list[0] = (Pier.Entity.Acesso.Usuario)Pier.Base.Dao.LazyInitializer.TratarProxies(list[0], true, true, session);
                    int i = list[0].Sessoes.Count;

                   // session.Close();
                    return list[0];
                }
                else
                {
                   // session.Close();
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw new Pier.Base.Dao.DaoException("Erro no Método GetUsuario " + ex.Message, ex);
            }
        }
    }
}
