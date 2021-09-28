using System.Data;

namespace MasterSbsa.Bll
{
    public class Usuario : Dal.Usuario
    {
        #region BuscaDadosDeAcesso
		public new DataTable BuscaDadosDeAcesso(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes Funcoes = new Util.Funcoes();

			DataTable DtUsuario = base.BuscaDadosDeAcesso(strLogin, Funcoes.Criptografar(strSenha), out strErro);

			return DtUsuario;
        }
        #endregion

        #region BuscaMenusDoPerfil
        public new DataTable BuscaMenusDoPerfil(long lngIdAcesso)
        {
            return base.BuscaMenusDoPerfil(lngIdAcesso);
        }
        #endregion

        #region BuscaPaginasDoMenu
        public new DataTable BuscaPaginasDoMenu(long lngIdAcesso, string strNomeMenu)
        {
            return base.BuscaPaginasDoMenu(lngIdAcesso, strNomeMenu);
        }
        #endregion

		#region BuscaDadosDeAcessoTiFlow
		public new DataTable BuscaDadosDeAcessoTiFlow(string strLogin, string strSenha, out string strErro)
		{
			Util.Funcoes Funcoes = new Util.Funcoes();

			DataTable DtUsuario = base.BuscaDadosDeAcessoTiFlow(strLogin, strSenha, out strErro);

			return DtUsuario;
		}
        #endregion

        #region BuscaDadosColetorMaquinas
        public new DataTable BuscaDadosColetorMaquinas(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes funcoes = new Util.Funcoes();

            DataTable dtUsuario = base.BuscaDadosColetorMaquinas(strLogin, strSenha, out strErro);

            return dtUsuario;
        }
        #endregion

        #region BuscaDadosColetorMaquinasNovelis
        public new DataTable BuscaDadosColetorMaquinasNovelis(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes funcoes = new Util.Funcoes();

            DataTable dtUsuario = base.BuscaDadosColetorMaquinasNovelis(strLogin, strSenha, out strErro);

            return dtUsuario;
        }
        #endregion

        #region BuscaDadosColetorMaquinasSaboo2
        public new DataTable BuscaDadosColetorMaquinasSaboo2(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes funcoes = new Util.Funcoes();

            DataTable dtUsuario = base.BuscaDadosColetorMaquinasSaboo2(strLogin, strSenha, out strErro);

            return dtUsuario;
        }
        #endregion

        #region BuscaDadosDeAcessoComPrestador
        public new DataTable BuscaDadosDeAcessoComPrestador(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes Funcoes = new Util.Funcoes();

            DataTable DtUsuario = base.BuscaDadosDeAcessoComPrestador(strLogin, Funcoes.Criptografar(strSenha), out strErro);

            return DtUsuario;
        }
        #endregion

        #region BuscaDadosDeAcessoColetorCostado
        public new DataTable BuscaDadosDeAcessoColetorCostado(string strLogin, string strSenha, out string strErro)
        {
            Util.Funcoes Funcoes = new Util.Funcoes();

            DataTable DtUsuario = base.BuscaDadosDeAcessoColetorCostado(strLogin, strSenha, out strErro);

            return DtUsuario;
        }
        #endregion

        #region BuscaDadosDeAcessoLDAP
        public new DataTable BuscaDadosDeAcessoLDAP(string strCPF, out string strErro)
        {
            Util.Funcoes Funcoes = new Util.Funcoes();

            DataTable DtUsuario = base.BuscaDadosDeAcessoLDAP(strCPF, out strErro);

            return DtUsuario;
        }
        #endregion BuscaDadosDeAcessoLDAP

        #region BuscaDadosDeAcesso
        public DataTable BuscaDadosDeAcesso(string strCpf, out string strErro)
        {
            Util.Funcoes Funcoes = new Util.Funcoes();

            DataTable DtUsuario = base.BuscaDadosDeAcesso(strCpf, out strErro);

            return DtUsuario;
        }
        #endregion
    }
}