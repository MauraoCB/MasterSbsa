using System;
using System.Diagnostics;

namespace MasterSbsa.Util
{
    public class Base : System.Web.UI.Page
	{
		public new MasterSbsa.Principal Master
		{
			get { return base.Master as MasterSbsa.Principal; }
		}

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
			try
			{
				#region Tratamentos
				if (Session["UsuarioMasterPage"] == null)
				{
					Session.Clear();
					Session.Abandon();
					Response.Redirect("Index.aspx", false);
					return;
				}
				#endregion
			}
			catch (Exception Ex)
			{
				using (var Funcoes = new MasterSbsa.Util.Funcoes())
				{
					Funcoes.LogErros(Ex, this, new StackTrace());
				}
				Master.NotificacaoErroGrande(Ex, this, new StackTrace());
			}
		}
		#endregion
	}
}