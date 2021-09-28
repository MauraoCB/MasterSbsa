using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using System.DirectoryServices;

namespace MasterSbsa
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioMasterPage"] == null)
            {
                if (Request.QueryString["xpto"] != null && Request.QueryString["strChaveAcesso"] != null)
                {
                    string strUrl = "";
                    string strKey = "";
                    using (var Funcoes = new Util.Funcoes())
                    {
                        strUrl = Funcoes.GetUrlWebServiceValidadorAD();
                        strKey = Funcoes.GetChaveValidacao();
                    }

                    if (string.IsNullOrEmpty(strUrl) && !strKey.Equals(Request.QueryString["strChaveAcesso"].ToString()))
                    {
                        return;
                    }

                    strUrl += "?strLogin=" + Request.QueryString["xpto"].ToString() + "&strChaveAcesso=" + Request.QueryString["strChaveAcesso"].ToString();

                    using (var client = new System.Net.WebClient())
                    {

                        client.Encoding = System.Text.Encoding.UTF8;
                        var ret = client.DownloadString(strUrl);

                        if (!string.IsNullOrEmpty(ret))
                        {
                            var usuarioModelo = JsonConvert.DeserializeObject<Model.UsuarioAd>(ret);
                            if (!string.IsNullOrEmpty(usuarioModelo.cpf))
                            {
                                LoginAd(usuarioModelo.cpf);
                            }
                        }
                    }
                }
            }

            Inicio.InnerHtml = Session["PaginaInicial"] != null ? Session["PaginaInicial"].ToString() : "Index.aspx";
            Menu.InnerHtml = Session["strMenu"] != null ? Session["strMenu"].ToString() : "";
            TituloBarra.Text = System.Configuration.ConfigurationManager.AppSettings["TituloBarra"] != null ? System.Configuration.ConfigurationManager.AppSettings["TituloBarra"].ToString() : "Santos Brasil";
            lblUsuario.Text = Session["UsuarioMasterPage"] != null ? Session["UsuarioMasterPage"].ToString() : "";
            lblNome.Text = Session["strNomeUsuario"] != null ? Session["strNomeUsuario"].ToString() : "";
            //lblNomeMobile.Text = Session["strNomeUsuario"] != null ? Session["strNomeUsuario"].ToString() : "";
            lblPerfil.Text = Session["strPerfilAcesso"] != null ? Session["strPerfilAcesso"].ToString() : "";
            lblSistema.Text = System.Configuration.ConfigurationManager.AppSettings["NomeSistema"] != null ? System.Configuration.ConfigurationManager.AppSettings["NomeSistema"].ToString() : "Santos Brasil";
            lblAmbiente.Text = System.Configuration.ConfigurationManager.AppSettings["Ambiente"] != null ? (System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString() == "Tecon" ? "Tecon | TEV" : System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString() == "Convicon" ? "Vila do Conde" : System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString()) : "";

            if (!IsPostBack)
            {
                if (Session["PerfisDeAcesso"] != null)
                {
                    ddlPerfis.DataSource = (DataTable)Session["PerfisDeAcesso"];
                    ddlPerfis.DataTextField = "PERFIL";
                    ddlPerfis.DataValueField = "ID_ACESSO";
                    ddlPerfis.DataBind();
                    ddlPerfis.Items.Insert(0, "Alterar Perfil");
                    if (Session["IdAcesso"] != null)
                    {
                        ddlPerfis.SelectedValue = Session["IdAcesso"].ToString();                        
                    }
                  
                }
            }
           
            if (!IsPostBack && Session["retorno"] == null)
            {
                if ((Request.Cookies["Perfil-SistemaId-" + System.Configuration.ConfigurationManager.AppSettings["SistemaID"].ToString()] != null) && (Request.Cookies["Perfil-SistemaId-" + System.Configuration.ConfigurationManager.AppSettings["SistemaID"].ToString()].Value.Length != 0))
                {
                    ddlPerfis.SelectedValue = Request.Cookies["Perfil-SistemaId-" + System.Configuration.ConfigurationManager.AppSettings["SistemaID"].ToString()].Value.Split('=')[1].ToString();

                    if (ddlPerfis.Items?.Count > 0 && Session["retorno"] == null)
                    {
                        Session["retorno"] = false;
                        ddlPerfis_SelectedIndexChanged(null, e);
                    }
                }
            }

            if (Session["strUsuaRegistro"] != null)
            {
                if (ValidaFoto("http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tcs/" + Session["strUsuaRegistro"].ToString() + ".jpg"))
                {
                    Foto.ImageUrl = "http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tcs/" + Session["strUsuaRegistro"].ToString() + ".jpg";
                }
                else if (ValidaFoto("http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/log/" + Session["strUsuaRegistro"].ToString() + ".jpg"))
                {
                    Foto.ImageUrl = "http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/log/" + Session["strUsuaRegistro"].ToString() + ".jpg";
                }
                else if (ValidaFoto("http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tci/" + Session["strUsuaRegistro"].ToString() + ".jpg"))
                {
                    Foto.ImageUrl = "http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tci/" + Session["strUsuaRegistro"].ToString() + ".jpg";
                }
                else if (ValidaFoto("http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tcv/" + Session["strUsuaRegistro"].ToString() + ".jpg"))
                {
                    Foto.ImageUrl = "http://acturusfrm4.santosbrasil.com.br/global/intranet/media/cards/funcionarios/tcv/" + Session["strUsuaRegistro"].ToString() + ".jpg";
                }
            }
        }
        #endregion

        #region ValidaFoto
        private bool ValidaFoto(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region NotificacaoSucessoGrande
        public void NotificacaoSucessoGrande(string strTitulo, string strMensagem)
        {
            strTitulo = strTitulo != "" ? strTitulo : " ";
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoGrande(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoGrande(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            strLinkRedirect = strLinkRedirect.Trim().ToUpper().Contains("HTTP") ? strLinkRedirect : "/" + strLinkRedirect;

            if (booCentralizada)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} }).find('.modal-content').css({'margin-top': function () {var modal_height = $('.modal-dialog').first().height();var window_height = $(window).height() - 30;var h = ((window_height / 2) - (modal_height / 2));return h + 'px';}});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} });", true);
            }
        }
        #endregion

        #region NotificacaoSucessoPequena
        public void NotificacaoSucessoPequena(string strTitulo, string strMensagem)
        {
            strTitulo = strTitulo != "" ? strTitulo : " ";
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoPequena(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoPequena(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            strLinkRedirect = strLinkRedirect.Trim().ToUpper().Contains("HTTP") ? strLinkRedirect : "/" + strLinkRedirect;

            if (booCentralizada)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} }).find('.modal-content').css({'margin-top': function () {var modal_height = $('.modal-dialog').first().height();var window_height = $(window).height() - 30;var h = ((window_height / 2) - (modal_height / 2));return h + 'px';}});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} });", true);
            }
        }
        #endregion

        #region NotificacaoErroGrande
        public void NotificacaoErroGrande(string strTitulo, string strMensagem)
        {
            strTitulo = strTitulo != "" ? strTitulo : " ";
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroGrande(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroGrande(Exception Ex, object objTela, StackTrace stackTrace)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();

            //Recuperando a session
            String strUsuario = HttpContext.Current.Session["UsuaLogin"] != null ? HttpContext.Current.Session["UsuaLogin"].ToString().ToUpper() : "Indefinido";

            //Recuperando a tela
            Page Tela = (Page)objTela;
            String strTela = Tela.Page.ToString().Substring(4, Tela.Page.ToString().Substring(4).Length - 5) + ".aspx";

            //Recuperando o metodo que disparou o erro.
            String strMetodo = stackTrace.GetFrame(0).GetMethod().Name;

            //Recuperando a linha e a instrução do erro.
            System.Diagnostics.StackTrace stTrace = new System.Diagnostics.StackTrace(Ex, true);
            String strLinha = stTrace.GetFrame(stTrace.FrameCount - 1).GetFileLineNumber().ToString();
            String strTarget = Ex.TargetSite != null ? Ex.TargetSite.Name : "Indefinido";

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '<b>Humm... algo deu errado <i class=\"fa fa-frown-o\" aria-hidden=\"true\"></i></b><br><br> Erro: " + Ex.Message.Replace("'", @"\'").Replace("\r\n", "<br />") + " <br> Ambiente: " + ObterAmbiente().Replace("'", @"\'") + " <br> Tela: " + strTela.Replace("'", @"\'") + " <br> Método: " + strMetodo.Replace("'", @"\'") + " <br> Linha: " + strLinha.Replace("'", @"\'") + " <br> Instrução: " + strTarget.Replace("'", @"\'") + " <br> Usuário: " + strUsuario.Replace("'", @"\'") + ("<br><br> Solicite análise da TI através do <b><a style=\"color: #d9534f!important;\" href=\"http://servicedesk/citsmart/login/login.load\" target=\"_blank\">Service Desk <i class=\"fa fa-desktop\" aria-hidden=\"true\"></i></a></b>, anexando este print, e os dados da tela anterior para facilitar o atendimento.").Replace("'", @"\'") + "', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroGrande(string strMensagem, int intCodMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "  " + ("<b><a href='Erros.aspx' target='_blank'>Quer ajuda<i class='fa fa-question' aria-hidden='true'></i> (Cod.: " + intCodMensagem + "</a></b>").Replace("'", @"\'") + ")', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroGrande(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
            strLinkRedirect = strLinkRedirect.Trim().ToUpper().Contains("HTTP") ? strLinkRedirect : "/" + strLinkRedirect;

            if (booCentralizada)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoErro', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} }).find('.modal-content').css({'margin-top': function () {var modal_height = $('.modal-dialog').first().height();var window_height = $(window).height() - 30;var h = ((window_height / 2) - (modal_height / 2));return h + 'px';}});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoErro', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} });", true);
            }
        }
        #endregion

        #region NotificacaoErroPequena
        public void NotificacaoErroPequena(string strTitulo, string strMensagem)
        {
            strTitulo = strTitulo != "" ? strTitulo : " ";
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroPequena(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroPequena(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";
            strLinkRedirect = strLinkRedirect.Trim().ToUpper().Contains("HTTP") ? strLinkRedirect : "/" + strLinkRedirect;

            if (booCentralizada)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} }).find('.modal-content').css({'margin-top': function () {var modal_height = $('.modal-dialog').first().height();var window_height = $(window).height() - 30;var h = ((window_height / 2) - (modal_height / 2));return h + 'px';}});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true, callback: function () {window.location = '" + strLinkRedirect + "';} });", true);
            }
        }
        #endregion

        #region NotificacaoToastr
        public void NotificacaoToastr(string strMensagem, TiposNotificacao Tipos, PosicaoNotificacao Posicao = PosicaoNotificacao.CantoSuperiorDireito)
        {
            string strPosicao = "";
            strMensagem = strMensagem != "" ? strMensagem.Replace("'", @"\'") : " ";

            switch (Posicao)
            {
                case PosicaoNotificacao.CantoSuperiorDireito:
                    strPosicao = "toast-top-right";
                    break;
                case PosicaoNotificacao.CantoInferiorDireito:
                    strPosicao = "toast-bottom-right";
                    break;
                case PosicaoNotificacao.CantoInferiorEsquerdo:
                    strPosicao = "toast-bottom-left";
                    break;
                case PosicaoNotificacao.CantoSuperiorEsquerdo:
                    strPosicao = "toast-top-left";
                    break;
                case PosicaoNotificacao.LarguraTotalSuperior:
                    strPosicao = "toast-top-full-width";
                    break;
                case PosicaoNotificacao.LarguraTotalInferior:
                    strPosicao = "toast-bottom-full-width";
                    break;
                case PosicaoNotificacao.CentroSuperior:
                    strPosicao = "toast-top-center";
                    break;
                case PosicaoNotificacao.CentroInferior:
                    strPosicao = "toast-bottom-center";
                    break;
                default:
                    break;
            }

            switch (Tipos)
            {
                case TiposNotificacao.Sucesso:
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "toastr.success('" + strMensagem + "', '', {\"closeButton\": true, \"debug\": false,\"newestOnTop\": false,\"progressBar\": true,\"positionClass\": \"" + strPosicao + "\",\"preventDuplicates\": false,\"showDuration\": \"300\",\"hideDuration\": \"1000\",\"timeOut\": \"5000\",\"extendedTimeOut\": \"1000\",\"showEasing\": \"swing\",\"hideEasing\": \"linear\",\"showMethod\": \"fadeIn\",\"hideMethod\": \"fadeOut\"});", true);
                    break;
                case TiposNotificacao.Informacao:
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "toastr.info('" + strMensagem + "', '', {\"closeButton\": true, \"debug\": false,\"newestOnTop\": false,\"progressBar\": true,\"positionClass\": \"" + strPosicao + "\",\"preventDuplicates\": false,\"showDuration\": \"300\",\"hideDuration\": \"1000\",\"timeOut\": \"5000\",\"extendedTimeOut\": \"1000\",\"showEasing\": \"swing\",\"hideEasing\": \"linear\",\"showMethod\": \"fadeIn\",\"hideMethod\": \"fadeOut\"});", true);
                    break;
                case TiposNotificacao.Alerta:
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "toastr.warning('" + strMensagem + "', '', {\"closeButton\": true, \"debug\": false,\"newestOnTop\": false,\"progressBar\": true,\"positionClass\": \"" + strPosicao + "\",\"preventDuplicates\": false,\"showDuration\": \"300\",\"hideDuration\": \"1000\",\"timeOut\": \"5000\",\"extendedTimeOut\": \"1000\",\"showEasing\": \"swing\",\"hideEasing\": \"linear\",\"showMethod\": \"fadeIn\",\"hideMethod\": \"fadeOut\"});", true);
                    break;
                case TiposNotificacao.Erro:
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "toastr.error('" + strMensagem + "', '', {\"closeButton\": true, \"debug\": false,\"newestOnTop\": false,\"progressBar\": true,\"positionClass\": \"" + strPosicao + "\",\"preventDuplicates\": false,\"showDuration\": \"300\",\"hideDuration\": \"1000\",\"timeOut\": \"5000\",\"extendedTimeOut\": \"1000\",\"showEasing\": \"swing\",\"hideEasing\": \"linear\",\"showMethod\": \"fadeIn\",\"hideMethod\": \"fadeOut\"});", true);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region TiposNotificacao
        public enum TiposNotificacao
        {
            Sucesso,
            Informacao,
            Alerta,
            Erro
        }
        #endregion

        #region PosicaoNotificacao
        public enum PosicaoNotificacao
        {
            CantoSuperiorDireito,
            CantoInferiorDireito,
            CantoInferiorEsquerdo,
            CantoSuperiorEsquerdo,
            LarguraTotalSuperior,
            LarguraTotalInferior,
            CentroSuperior,
            CentroInferior
        }
        #endregion

        #region ObterAmbiente
        public string ObterAmbiente()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Ambiente"] != null ? System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString() : "";
        }
        #endregion

        #region ObterTituloNotificacaoPadrao
        public string ObterTituloNotificacaoPadrao()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TituloNotificacaoPadrao"] != null ? System.Configuration.ConfigurationManager.AppSettings["TituloNotificacaoPadrao"].ToString() : "";
        }
        #endregion

        #region btnLogin
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            long lngIdAcesso = 0;
            Session["strMenu"] = "";
            Session["PaginaInicial"] = "";
            Session["PerfisDeAcesso"] = null;
            Session["MaquinaUsuario"] = "";
            Session["IdAcesso"] = "";

            if (txtLogin.Text.Trim() == "")
            {
                NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Informe o Login!");
                return;
            }

            if (txtSenha.Text.Trim() == "")
            {
                NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Informe a Senha!");
                return;
            }

            Session["PaginaInicial"] = "<button type='button' class='navbar-toggle' data-toggle='collapse' data-target='#bs-example-navbar-collapse-1'><span class='sr-only'></span><span class='icon-bar'></span><span class='icon-bar'></span><span class='icon-bar'></span></button><a class='navbar-brand' href='" + System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString() + "'><img src='Imagens/Logo.png' alt=''></a>";

            using (var bllUsuario = new Bll.Usuario())
            {
                string strErro = "";
                string strErroLDAP = "";
                string strModoLogin = "";

                DataTable DtUsuario = null;

                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["ModoLogin"]))
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "TIFLOW")
                    {
                        DtUsuario = bllUsuario.BuscaDadosDeAcessoTiFlow(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }
                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasImbituba")
                    {
                        DtUsuario = bllUsuario.BuscaDadosColetorMaquinas(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }
                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasNovelis")
                    {
                        DtUsuario = bllUsuario.BuscaDadosColetorMaquinasNovelis(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }
                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasSaboo2")
                    {
                        DtUsuario = bllUsuario.BuscaDadosColetorMaquinasSaboo2(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }

                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "PrestadorGRC")
                    {
                        DtUsuario = bllUsuario.BuscaDadosDeAcessoComPrestador(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }
                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorCostado")
                    {
                        DtUsuario = bllUsuario.BuscaDadosDeAcessoColetorCostado(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                    }
                    else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "LDAP")
                    {
                        try
                        {
                            strModoLogin = "LDAP";

                            DirectoryEntry Ad = new DirectoryEntry("LDAP://SANTOSBRASIL", txtLogin.Text.Trim(), txtSenha.Text);
                            object nativeObject = Ad.NativeObject;

                            DirectorySearcher mySearcher = new DirectorySearcher(Ad);
                            mySearcher.Filter = "(&(objectClass=user)(|(samaccountname=" + txtLogin.Text.Trim() + ")))";
                            SearchResult result = mySearcher.FindOne();

                            if (result == null)
                            {
                                NotificacaoErroGrande(ObterTituloNotificacaoPadrao(), "Usuário de rede " + txtLogin.Text.Trim() + " não encontrado, verifique os dados digitados e tente novamente.");
                                return;
                            }

                            string strCPF = null;
                            strCPF = GetProperty(result, "Pager");

                            if (string.IsNullOrEmpty(strCPF))
                            {
                                NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Seu CPF não está cadastrado no AD! Favor entrar em contato com o setor de TI para regularizar seu cadastro.");
                                return;
                            }

                            DtUsuario = bllUsuario.BuscaDadosDeAcessoLDAP(strCPF.ToString(), out strErro);
                        }
                        catch (DirectoryServicesCOMException cex)
                        {
                            strErroLDAP = cex.Message.ToString();
                        }
                    }
                }
                else
                {
                    DtUsuario = bllUsuario.BuscaDadosDeAcesso(txtLogin.Text.Trim().ToUpper(), txtSenha.Text.Trim().ToUpper(), out strErro);
                }

                if (strModoLogin.ToString() == "LDAP")
                {
                    if (!string.IsNullOrEmpty(strErroLDAP.ToString()))
                    {
                        NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Usuário ou Senha Incorretos!");
                        return;
                    }
                }

                if (DtUsuario != null && DtUsuario.Rows.Count > 0)
                {
                    Session["PerfisDeAcesso"] = DtUsuario;

                    Session["IdAcesso"] = DtUsuario.Rows[0]["ID_ACESSO"].ToString();

                    lngIdAcesso = long.Parse(DtUsuario.Rows[0]["ID_ACESSO"].ToString());

                    Session["strNomeUsuario"] = DtUsuario.Rows[0]["USUA_NOME"].ToString().ToUpper();

                    Session["strPerfilAcesso"] = DtUsuario.Rows[0]["PERFIL"].ToString().ToUpper();

                    Session["strUsuaRegistro"] = DtUsuario.Rows[0]["USUA_REGISTRO"].ToString().ToUpper();

                    if (lngIdAcesso != 0)
                    {
                        Session["UsuarioMasterPage"] = txtLogin.Text.Trim().ToUpper();
                        lblUsuario.Text = Session["UsuarioMasterPage"].ToString();

                        if (System.Configuration.ConfigurationManager.AppSettings["MenuCustomizado"] != null)
                        {
                            MontaMenuCustomizado();
                        }
                        else if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["ModoLogin"]))
                        {
                            if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasImbituba" || 
                                System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasNovelis" ||
                                System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorMaquinasSaboo2")
                            {
                                Inicio.InnerHtml = Session["PaginaInicial"].ToString();
                                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"] != null ? System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString() : "Index.aspx");
                            }
                            else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "PrestadorGRC")
                            {
                                MontaMenu(lngIdAcesso);
                            }
                            else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "ColetorCostado")
                            {
                                MontaMenu(lngIdAcesso);
                            }
                            else if (System.Configuration.ConfigurationManager.AppSettings["ModoLogin"].ToString() == "LDAP")
                            {
                                MontaMenu(lngIdAcesso);
                            }
                        }
                        else
                        {
                            MontaMenu(lngIdAcesso);
                        }
                    }
                }
                else if (DtUsuario == null)
                {
                    if (strErro.Contains("ORA-03113") || strErro.Contains("ORA-12545") || strErro.Contains("ORA-03135") || strErro.Contains("ORA-02068"))
                    {
                        NotificacaoErroGrande(ObterTituloNotificacaoPadrao(), "A conexão foi interrompida ou perdeu contato com o servidor. </br></br> Possíveis causas: </br></br> Problemas de rede </br> Queda de link de comunicação </br></br> Tente novamente, caso o problema persista tente mais tarde.");
                    }
                    else
                    {
                        NotificacaoErroGrande(ObterTituloNotificacaoPadrao(), "Ocorreu algum problema durante o login, entre em contato com o suporte ou tente novamente mais tarde. </br></br> Detalhes: " + strErro);
                    }
                }
                else
                {
                    if (strModoLogin.ToString() == "LDAP")
                    {
                        NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Usuário sem acesso ao sistema!");
                    }
                    else
                    {
                        NotificacaoErroPequena(ObterTituloNotificacaoPadrao(), "Usuário ou Senha Incorretos!");
                    }
                }
            }
        }
        #endregion

        #region btnSair
        protected void btnSair_Click(object sender, EventArgs e)
        {
            lblUsuario.Text = "";
            Session.Clear();
            Session.Abandon();
            Session["UsuarioMasterPage"] = null;
            Response.Redirect("Index.aspx");
            Response.End();
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

        #region Erro
        public string Erro(Exception Ex)
        {
            return System.Configuration.ConfigurationManager.AppSettings["FormatException"] != null ?
                "Erro: " + Ex.GetType().FullName + "<br>" +
                "Descrição: " + System.Configuration.ConfigurationManager.AppSettings["FormatException"].ToString() + "<br>" +
                "Detalhe: " + Ex.Message + "<br>" +
                "Ajuda: " + ("<a href='Erros.aspx' target='_blank'>Ajuda</a>").Replace("'", @"\'") : "";

            //if (Ex.GetType().FullName == "System.SystemException")
            //{
            //    //return new Exception("A failed run time check; used as a base class for other exceptions.");
            //    return new Exception(System.Configuration.ConfigurationManager.AppSettings["FormatException"] != null ? System.Configuration.ConfigurationManager.AppSettings["FormatException"].ToString() + Environment.NewLine + Ex.Message : "");
            //}
            //else if (Ex.GetType().FullName == "System.AccessException")
            //{
            //    return new Exception("Failure to access a type member , such as a method or field.");
            //}
            //else if (Ex.GetType().FullName == "System.ArgumentException")
            //{
            //    return new Exception("An argument to a method was invalid.");
            //}
            //else if (Ex.GetType().FullName == "System.ArgumentNullException")
            //{
            //    return new Exception("A null argument was passed to a method that does not accept it.");
            //}
            //else if (Ex.GetType().FullName == "System.ArgumentOutOfRangeException")
            //{
            //    return new Exception("Argument value is out of range.");
            //}
            //else if (Ex.GetType().FullName == "System.ArithmeticException")
            //{
            //    return new Exception("Arithmetic over or underflow has occurred.");
            //}
            //else if (Ex.GetType().FullName == "System.ArrayTypeMismatchException")
            //{
            //    return new Exception("Attempt to store the wrong type of object in an array.");
            //}
            //else if (Ex.GetType().FullName == "System.BadImageFormatException")
            //{
            //    return new Exception("Image is in wrong format");
            //}
            //else if (Ex.GetType().FullName == "System.CoreException")
            //{
            //    return new Exception("Base class for exceptions thrown by the runtime.");
            //}
            //else if (Ex.GetType().FullName == "System.DivideByZeroException")
            //{
            //    return new Exception("An attempt was made to divide by Zero.");
            //}
            //else if (Ex.GetType().FullName == "System.FormatException")
            //{
            //    if (CustomException)
            //    {
            //        return new Exception(System.Configuration.ConfigurationManager.AppSettings["FormatException"] != null ? System.Configuration.ConfigurationManager.AppSettings["FormatException"].ToString() + Environment.NewLine + Ex.Message : "");
            //    }
            //    else
            //    {
            //        return new Exception("The format of an argument is wrong.");
            //    }
            //}
            //else if (Ex.GetType().FullName == "System.IndexOutofRangeException")
            //{
            //    return new Exception("An Array index is out of range.");
            //}
            //else if (Ex.GetType().FullName == "System.InvalidCastException")
            //{
            //    return new Exception("An attempt was made to cast to an invalid class.");
            //}
            //else if (Ex.GetType().FullName == "System.InvalidOperationException")
            //{
            //    return new Exception("A method was called at an invalid time.");
            //}
            //else if (Ex.GetType().FullName == "System.MissingmemberException")
            //{
            //    return new Exception("An invalid version of a DLL was accessed.");
            //}
            //else if (Ex.GetType().FullName == "System.NotFiniteException")
            //{
            //    return new Exception("A number is not valid.");
            //}
            //else if (Ex.GetType().FullName == "System.NotSupportedException")
            //{
            //    return new Exception("Indicates that a method is not implemented by a class.");
            //}
            //else if (Ex.GetType().FullName == "System.NullReferenceException")
            //{
            //    return new Exception("Attempt to use an unassigned reference.");
            //}
            //else if (Ex.GetType().FullName == "System.OutofmemoryException")
            //{
            //    return new Exception("Not enough memory to continue execution.");
            //}
            //else if (Ex.GetType().FullName == "System.StackOverFlowException")
            //{
            //    return new Exception("A Stack has overflowed.");
            //}
            //else
            //{
            //    return new Exception();
            //}
        }
        #endregion

        #region ddlPerfis_SelectedIndexChanged
        protected void ddlPerfis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPerfis.SelectedItem.Value == "Alterar Perfil")
            {
                return;
            }          
            
            Session["strPerfilAcesso"] = ddlPerfis.SelectedItem.Text.ToString().ToUpper();
            Session["IdAcesso"] = ddlPerfis.SelectedItem.Value;

            CookiesPerfil();

            MontaMenu(long.Parse(ddlPerfis.SelectedItem.Value));
        }
        #endregion

        #region MontaMenu
        protected void MontaMenu(long lngIdAcesso)
        {
            Session["strMenu"] = "";

            using (var bllUsuario = new Bll.Usuario())
            {
                DataTable dtMenusDoPerfil = bllUsuario.BuscaMenusDoPerfil(lngIdAcesso);

                if (dtMenusDoPerfil != null && dtMenusDoPerfil.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMenusDoPerfil.Rows.Count; i++)
                    {
                        Session["strMenu"] += "<li class='dropdown'><a href='" + dtMenusDoPerfil.Rows[i]["MENU_ENDERECO"].ToString() + "' class='dropdown-toggle' data-toggle='dropdown'>" + dtMenusDoPerfil.Rows[i]["MENU_NOME"].ToString() + "<span class='caret'></span></a>";

                        DataTable dtPaginasDoMenu = bllUsuario.BuscaPaginasDoMenu(lngIdAcesso, dtMenusDoPerfil.Rows[i]["MENU_NOME"].ToString());

                        if (dtPaginasDoMenu != null && dtPaginasDoMenu.Rows.Count > 0)
                        {
                            Session["strMenu"] += "<ul class='dropdown-menu' role='menu'>";
                            for (int x = 0; x < dtPaginasDoMenu.Rows.Count; x++)
                            {
                                Session["strMenu"] += "<li><a href='" + dtPaginasDoMenu.Rows[x]["SUB_MENU_ENDERECO"].ToString() + "'>" + dtPaginasDoMenu.Rows[x]["SUB_MENU_NOME"].ToString() + "</a></li>";
                            }
                            Session["strMenu"] += "</ul>";
                        }

                        Session["strMenu"] += "</li>";
                    }
                }
            }

            Inicio.InnerHtml = Session["PaginaInicial"].ToString();
            Menu.InnerHtml = Session["strMenu"].ToString();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"] != null ? System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString() : "Index.aspx");
        }
        #endregion

        #region MontaMenuCustomizado
        protected void MontaMenuCustomizado()
        {
            Session["strMenu"] = "";

            Session["strMenu"] += System.Configuration.ConfigurationManager.AppSettings["MenuCustomizado"].ToString();

            Inicio.InnerHtml = Session["PaginaInicial"].ToString();
            Menu.InnerHtml = Session["strMenu"].ToString();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"] != null ? System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString() : "Index.aspx");
        }
        #endregion

        #region LoginAd
        public void LoginAd(string strCpf)
        {
            long lngIdAcesso = 0;
            Session["strMenu"] = "";
            Session["PaginaInicial"] = "";
            Session["PerfisDeAcesso"] = null;
            Session["MaquinaUsuario"] = "";
            Session["IdAcesso"] = "";

            Session["PaginaInicial"] = "<button type='button' class='navbar-toggle' data-toggle='collapse' data-target='#bs-example-navbar-collapse-1'><span class='sr-only'></span><span class='icon-bar'></span><span class='icon-bar'></span><span class='icon-bar'></span></button><a class='navbar-brand' href='" + System.Configuration.ConfigurationManager.AppSettings["PaginaInicial"].ToString() + "'><img src='Imagens/Logo.png' alt=''></a>";

            using (var bllUsuario = new Bll.Usuario())
            {
                string strErro = "";

                DataTable DtUsuario = null;                
                
                DtUsuario = bllUsuario.BuscaDadosDeAcesso(strCpf.Trim().ToUpper(), out strErro);                

                if (DtUsuario != null && DtUsuario.Rows.Count > 0)
                {
                    Session["PerfisDeAcesso"] = DtUsuario;

                    Session["IdAcesso"] = DtUsuario.Rows[0]["ID_ACESSO"].ToString();

                    lngIdAcesso = long.Parse(DtUsuario.Rows[0]["ID_ACESSO"].ToString());

                    Session["strNomeUsuario"] = DtUsuario.Rows[0]["USUA_NOME"].ToString().ToUpper();

                    Session["strPerfilAcesso"] = DtUsuario.Rows[0]["PERFIL"].ToString().ToUpper();

                    Session["strUsuaRegistro"] = DtUsuario.Rows[0]["USUA_REGISTRO"].ToString().ToUpper();

                    if (lngIdAcesso != 0)
                    {
                        Session["UsuarioMasterPage"] = txtLogin.Text.Trim().ToUpper();
                        lblUsuario.Text = Session["UsuarioMasterPage"].ToString();
                        
                        MontaMenu(lngIdAcesso);                        
                    }
                }
                else if (DtUsuario == null)
                {
                    if (strErro.Contains("ORA-03113") || strErro.Contains("ORA-12545") || strErro.Contains("ORA-03135") || strErro.Contains("ORA-02068"))
                    {
                        NotificacaoErroGrande(ObterTituloNotificacaoPadrao(), "A conexão foi interrompida ou perdeu contato com o servidor. </br></br> Possíveis causas: </br></br> Problemas de rede </br> Queda de link de comunicação </br></br> Tente novamente, caso o problema persista tente mais tarde.");
                    }
                    else
                    {
                        NotificacaoErroGrande(ObterTituloNotificacaoPadrao(), "Ocorreu algum problema durante o login, entre em contato com o suporte ou tente novamente mais tarde. </br></br> Detalhes: " + strErro);
                    }
                }
                else
                {                    
                    Response.Redirect("Index.aspx");
                }
            }
        }
        #endregion

        #region CookiesPerfil
        private void CookiesPerfil()
        {
            if (ddlPerfis.SelectedItem == null) return;

            HttpCookie cookiePerfil = Request.Cookies["Perfil-SistemaId-" + System.Configuration.ConfigurationManager.AppSettings["SistemaID"]];

            cookiePerfil = new HttpCookie("Perfil-SistemaId-" + System.Configuration.ConfigurationManager.AppSettings["SistemaID"].ToString());
            cookiePerfil.Values["Perfil"] = ddlPerfis.SelectedItem.Value;

            cookiePerfil.Expires = DateTime.UtcNow.AddDays(30);

            Response.Cookies.Add(cookiePerfil); 
        }
        #endregion

        #region GetProperty
        public static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion GetProperty
    }
}