using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterSbsa
{
	public partial class PrincipalMenuBasico : System.Web.UI.MasterPage
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
			TituloBarra.Text = System.Configuration.ConfigurationManager.AppSettings["TituloBarra"] != null ? System.Configuration.ConfigurationManager.AppSettings["TituloBarra"].ToString() : "Santos Brasil";
		}
		#endregion

		#region Temas
		protected void btnTemaAzul_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Azul");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaPadrao_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Padrao");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaAzulEscuro_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "AzulEscuro");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaOuro_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Ouro");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaLimao_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Limao");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaOutubroRosa_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "OutubroRosa");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaVermelho_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Vermelho");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}

		protected void btnTemaSantosBrasil_Click(object sender, EventArgs e)
		{
			HttpCookie CookieTema = new HttpCookie(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString());

			CookieTema.Values.Add(System.Configuration.ConfigurationManager.AppSettings["Tema"].ToString(), "Sbsa");

			CookieTema.Expires = DateTime.Now.AddMonths(1);

			Response.Cookies.Add(CookieTema);

			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString());
		}
		#endregion
	}
}