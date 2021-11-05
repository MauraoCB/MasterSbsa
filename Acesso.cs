using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pier.Web.Acesso.Bll
{
    public class Acesso : Pier.Base.Bll.BaseAbstractBll<Entity.Acesso.Acesso>, IDisposable
    {
        static Object _lock = new Object();

        #region Login
        public static Entity.CORP.Acesso.PriUsuario Login(String usuario, String senha)
        {
            lock (_lock)
            {
                var user = Pier.Dao.Acesso.Acesso.Login(usuario);

                if (user == null)
                {
                    throw new Exception("Usuario não encontrado.");
                }
                if (user.Bloqueio != null)
                {
                    throw new Exception("Usuario bloqueado.");
                }
                string senhaCrip = DescriptografaSenha(user.Senha).Trim();
                Int16 erroSenha = 0;
                HttpCookie cookie = null;
                if (System.Web.HttpContext.Current.Request.Cookies["erroSenha"] != null)
                {
                    cookie = System.Web.HttpContext.Current.Request.Cookies["erroSenha"];
                }
                else
                {
                    cookie = new HttpCookie("erroSenha");
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                if (String.IsNullOrEmpty(cookie.Values[user.Login]))
                {
                    cookie.Values.Add(user.Login, "0");
                }
                erroSenha = Convert.ToInt16(cookie.Values[user.Login]);
                if (!senhaCrip.Trim().ToUpper().Equals(senha.Trim().ToUpper()))
                {
                    cookie.Values[user.Login] = Convert.ToString(++erroSenha);

                    if (erroSenha == 3)
                    {
                        Pier.Dao.Acesso.Acesso.BloqueiaUsuario(user);
                        cookie.Values[user.Login] = "0";
                        throw new Exception("Usuário foi bloqueado.");
                    }
                    //descomentar após acesso correto
                   // throw new Exception("Senha incorreta.");
                }
                cookie.Values[user.Login] = "0";
                if (!user.Ativo)
                {
                    throw new Exception("Usuário inativo.");
                }
                if (user.Usuario == null)
                {
                    throw new Exception("Sem acesso ao WebSic.");
                }
                if (user.LimiteTrocaSenha < DateTime.Now)
                {
                    throw new AcessoException("Necessário trocar senha.", user);
                }
                LimpaAmbientesSemAcesso(user);

                HttpCookie cookieUser = null;
                if (System.Web.HttpContext.Current.Request.Cookies["userWebSic"] != null)
                {
                    cookieUser = System.Web.HttpContext.Current.Request.Cookies["userWebSic"];
                }
                else
                {
                    cookieUser = new HttpCookie("userWebSic");
                }

                var retCUser = Pier.Web.Util.Criptografia.Encrypt (new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new Pier.Entity.CORP.Acesso.PriUsuario
                {
                    Login = user.Login,
                    Nome = user.Nome,
                    Senha = DescriptografaSenha(user.Senha).Trim(),
                    Id = user.Id
                }));
                cookieUser.Expires = DateTime.Now.AddHours(1);
                cookieUser.Value = retCUser;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookieUser);
                return user;
            }
        }
        #endregion

        #region LoginGet
        public static Entity.CORP.Acesso.PriUsuario LoginGet(string strCpf)
        {
            lock (_lock)
            {
                var user = Pier.Dao.Acesso.Acesso.LoginGet(strCpf);

                if (user == null)
                {
                    throw new Exception("Usuario não encontrado.");
                }
                if (user.Bloqueio != null)
                {
                    throw new Exception("Usuario bloqueado.");
                }
                string senhaCrip = DescriptografaSenha(user.Senha).Trim();
                //Int16 erroSenha = 0;
                HttpCookie cookie = null;
                if (System.Web.HttpContext.Current.Request.Cookies["erroSenha"] != null)
                {
                    cookie = System.Web.HttpContext.Current.Request.Cookies["erroSenha"];
                }
                else
                {
                    cookie = new HttpCookie("erroSenha");
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                if (String.IsNullOrEmpty(cookie.Values[user.Login]))
                {
                    cookie.Values.Add(user.Login, "0");
                }
                // = Convert.ToInt16(cookie.Values[user.Login]);
                //if (!senhaCrip.Trim().ToUpper().Equals(senha.Trim().ToUpper()))
                //{
                //    cookie.Values[user.Login] = Convert.ToString(++erroSenha);

                //    if (erroSenha == 3)
                //    {
                //        Pier.Dao.Acesso.Acesso.BloqueiaUsuario(user);
                //        cookie.Values[user.Login] = "0";
                //        throw new Exception("Usuário foi bloqueado.");
                //    }
                //    throw new Exception("Senha incorreta.");
                //}
                cookie.Values[user.Login] = "0";
                if (!user.Ativo)
                {
                    throw new Exception("Usuário inativo.");
                }
                if (user.Usuario == null)
                {
                    throw new Exception("Sem acesso ao WebSic.");
                }
               //if (user.LimiteTrocaSenha < DateTime.Now)
               // {
               //     throw new AcessoException("Necessário trocar senha.", user);
               //  }
               // LimpaAmbientesSemAcesso(user);

                HttpCookie cookieUser = null;
                if (System.Web.HttpContext.Current.Request.Cookies["userWebSic"] != null)
                {
                    cookieUser = System.Web.HttpContext.Current.Request.Cookies["userWebSic"];
                }
                else
                {
                    cookieUser = new HttpCookie("userWebSic");
                }

                var retCUser = Pier.Web.Util.Criptografia.Encrypt(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new Pier.Entity.CORP.Acesso.PriUsuario
                {
                    Login = user.Login,
                    Nome = user.Nome,
                    Senha = DescriptografaSenha(user.Senha).Trim(),
                    Id = user.Id
                }));
                cookieUser.Expires = DateTime.Now.AddHours(1);
                cookieUser.Value = retCUser;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookieUser);
                return user;
            }
        }
        #endregion

        private static void LimpaAmbientesSemAcesso(Entity.CORP.Acesso.PriUsuario user)
        {
            user.Usuario.Sessoes = user.Usuario.Sessoes.Where(x => x.Acessos.Any(z => z.Ativo)).ToList();
        }

        private static void ValidaAlteracaoSenha(String senhaAntiga, String senhaNova, String confirmacaoSenhaNova)
        {
            if (String.IsNullOrEmpty(senhaNova) || String.IsNullOrWhiteSpace(senhaNova))
                throw new Exception("Campo senha está vazio.");

            if (String.IsNullOrEmpty(confirmacaoSenhaNova) || String.IsNullOrWhiteSpace(confirmacaoSenhaNova))
                throw new Exception("Campo de nova senha está vazio.");

            if (String.IsNullOrEmpty(senhaAntiga) || String.IsNullOrWhiteSpace(senhaAntiga))
                throw new Exception("Informe a senha atual.");

            if (!senhaNova.Trim().Equals(confirmacaoSenhaNova.Trim()))
                throw new Exception("As novas senhas não conferem.");

            if (senhaNova.Length < 8 || senhaNova.Length > 20)
                throw new Exception("Sua nova senha deve conter de 8 até 20 caracteres.");

            if (senhaAntiga.Equals(senhaNova))
                throw new Exception("Senha nova e antiga idênticas.");

            bool temLetra = false, temNumero = false;

            foreach (var letra in senhaNova.ToArray())
            {
                long numero = 0L;
                if (long.TryParse(Convert.ToString(letra), out numero))
                {
                    temNumero = true;
                }
                else
                {
                    temLetra = true;
                }
                if (temLetra && temNumero) break;
            }
            if (!temLetra || !temNumero)
            {
                throw new Exception("Sua nova senha deve conter letras e números.");
            }

        }

        private static String CriptografaSenha(String senha)
        {
            var ret = String.Empty;
            for (int i = 0; i < senha.Trim().Length; i++)
            {
                ret += Chr(Asc(senha.Substring(i, 1)) + (i + 1));
            }
            return ret;
        }

        private static String DescriptografaSenha(String senha)
        {
            var ret = String.Empty;
            for (int i = 0; i < senha.Trim().Length; i++)
            {
                ret += Chr(Asc(senha.Substring(i, 1)) - (i + 1));
            }
            return ret;
        }

        public static Entity.CORP.Acesso.PriUsuario AlteraSenha(Entity.CORP.Acesso.PriUsuario user, String senhaAntiga, String senhaNova, String confirmacaoSenhaNova)
        {
            string senhaCrip = DescriptografaSenha(user.Senha).Trim();
            if (!senhaCrip.Equals(senhaAntiga.ToUpper().Trim()))
                throw new Exception("Senha atual não confere");

            ValidaAlteracaoSenha(senhaAntiga.ToUpper().Trim(), senhaNova.ToUpper().Trim(), confirmacaoSenhaNova.ToUpper().Trim());

            if (user.HistoricoSenhas != null)
            {
                int i = 0;
                foreach (var item in user.HistoricoSenhas.OrderByDescending(x => x.Id))
                {
                    var sHist = DescriptografaSenha(item.Senha).ToUpper().Trim();
                    if (senhaNova.Trim().Equals(sHist.Trim()))
                        throw new Exception("Senha utilizada recentemente (menos de três vezes)");

                    if (sHist.Trim().Contains(senhaNova.ToUpper().Trim()) || senhaNova.ToUpper().Trim().Contains(sHist.Trim()))
                        throw new Exception("Nova senha contem parte do histórico de senhas.");

                    if (++i == 3) break;
                }
            }
            user.Senha = CriptografaSenha(senhaNova.ToUpper().Trim());
            user.LimiteTrocaSenha = DateTime.Now.AddDays(45);
            user.UltimaTroca = DateTime.Now;
            if (user.HistoricoSenhas == null) user.HistoricoSenhas = new List<Entity.CORP.Acesso.HistoricoSenha>();
            user.HistoricoSenhas.Add(
                new Entity.CORP.Acesso.HistoricoSenha
                {
                    DataTroca = DateTime.Now,
                    ProximaTroca = DateTime.Now.AddDays(45),
                    Senha = user.Senha
                });
            Pier.Dao.Acesso.Acesso.AtualizaLogin(user);

            return user;

        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private static int Asc(string letra)
        {
            return (int)(Convert.ToChar(letra));
        }

        private static char Chr(int codigo)
        {
            return (char)codigo;
        }
    }

    public class AcessoException : Exception
    {
        public Entity.CORP.Acesso.PriUsuario Usuario { get; private set; }

        public AcessoException(String info, Entity.CORP.Acesso.PriUsuario usuario)
            : base(info)
        {
            this.Usuario = usuario;
        }
    }
}