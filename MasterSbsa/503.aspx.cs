using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterSbsa
{
    public partial class _503 : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}