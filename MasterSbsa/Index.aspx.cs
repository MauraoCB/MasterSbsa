using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterSbsa
{
    public partial class Index : System.Web.UI.Page
    {
        #region PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if ((Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString()] != null) && (Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString()].Value.Length != 0))
            {
                Page.Theme = Request.Cookies[System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString()].Value.Split('=')[1].ToString();
            }
            else
            {
                Page.Theme = "Padrao";
            }
        }
        #endregion

		#region Load
		protected void Page_Load(object sender, EventArgs e)
        {
			if (System.Configuration.ConfigurationManager.AppSettings["MensagemBoasVindas"] != null)
			{
				h1BemVindo.Visible = true;
				h1BemVindo.InnerText = "BEM-VINDO AO";
				h1Sistema.Visible = true;
				h1Sistema.InnerText = System.Configuration.ConfigurationManager.AppSettings["MensagemBoasVindas"].ToString().ToUpper();
			}
		}
        #endregion

        protected string MensagemTelaInicial()
        {
            string strMensagemTelaInicial = "";

            if (System.Configuration.ConfigurationManager.AppSettings["MensagemTelaInicialLogin"] != null)
            {
                strMensagemTelaInicial = System.Configuration.ConfigurationManager.AppSettings["MensagemTelaInicialLogin"].ToString();
            }
            else
            {
                strMensagemTelaInicial = "Faça login para utilizar as funcionalidades do sistema.";
            }

            strMensagemTelaInicial += "<br />ou<br />";

            if (System.Configuration.ConfigurationManager.AppSettings["MensagemTelaInicialAcesso"] != null)
            {
                strMensagemTelaInicial += System.Configuration.ConfigurationManager.AppSettings["MensagemTelaInicialAcesso"].ToString();
            }
            else
            {
                strMensagemTelaInicial += "Solicite seu acesso através do GRC.";
            }

            return strMensagemTelaInicial;
        }
    }
}