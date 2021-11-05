using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate;
using Pier.Entity;
using System.Diagnostics;
using System.Security.Principal;
using Pier.Base.Dao;

namespace Pier.Dao.Acesso
{
    public class DaoFuncionario : BaseDao<Pier.Entity.Acesso.Funcionario>
    {
        /// <summary>
        /// Construtor da classe Dao.Funcionario.
        /// </summary>
        /// <param name="acesso">Perfil de acesso utilizado para acessar dados da entidade.</param>
        public DaoFuncionario(Pier.Entity.Ambiente ambiente, ISession sessao)
            : base(ambiente, sessao)
        {
        }

        /// <summary>
        /// Método síncrono de busca de dados da entidade Funcionário por Registro.
        /// </summary>
        /// <param name="ambiente">Ambiente do Funcionário.</param>
        /// <param name="registro">Registro do Funcionário.</param>
        /// <returns>Retorna Funcionário de acordo com seu Registro.</returns>
        public Pier.Entity.Acesso.Funcionario GetFuncionarioByRegistro(Ambiente ambiente, long registro)
        {
            try
            {
               // DaoUtil.InicilizadorDao(ambiente);

                var session = DaoUtil.GetSessao(Pier.Entity.Banco.SGTAP,ambiente);

                ICriteria crit = session.CreateCriteria(typeof(Pier.Entity.Acesso.Funcionario));

                crit.Add(Expression.Eq("Registro", registro));

                IList<Pier.Entity.Acesso.Funcionario> list = crit.List<Pier.Entity.Acesso.Funcionario>();


                if (list.Count > 0)
                {
                    NHibernateUtil.Initialize(list[0]);
                    NHibernateUtil.Initialize(list[0].Email);
                    NHibernateUtil.Initialize(list[0].Telefones);
                    //NHibernateUtil.Initialize(list[0].ListaDocumentos);
                    NHibernateUtil.Initialize(list[0].Enderecos);
                    NHibernateUtil.Initialize(list[0].EmpresaControlada);
                    list[0] = (Pier.Entity.Acesso.Funcionario)Pier.Base.Dao.LazyInitializer.TratarProxies(list[0], true, true, session);
                    return list[0];
                }
                else
                {
                    return null;
                };
            }
            catch (Exception ex)
            {
                throw new DaoException("Erro no Método GetFuncionarioByRegistro " + ex.Message, ex);
            }
        }
    }
}
