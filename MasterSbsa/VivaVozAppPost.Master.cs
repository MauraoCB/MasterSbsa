using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI;

namespace MasterSbsa
{
    public partial class VivaVozAppPost : System.Web.UI.MasterPage
    {
        #region NotificacaoSucessoGrande
        public void NotificacaoSucessoGrande(string strTitulo, string strMensagem)
        {
            strTitulo = strTitulo != "" ? strTitulo : " ";
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoGrande(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoGrande(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
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
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoPequena(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoSucesso', backdrop: true });", true);
        }

        public void NotificacaoSucessoPequena(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
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
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroGrande(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
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

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '<b>Humm... algo deu errado <i class=\"fa fa-frown-o\" aria-hidden=\"true\"></i></b><br><br>" + ("Solicite análise da TI através do <b><a style=\"color: #d9534f!important;\" href=\"http://servicedesk/citsmart/login/login.load\" target=\"_blank\">Service Desk <i class=\"fa fa-desktop\" aria-hidden=\"true\"></i></a></b>, anexando este print, e os dados da tela anterior para facilitar o atendimento.").Replace("'", @"\'") + "', size: 'large', className: 'NotificacaoErro', backdrop: true });", true);
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
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroPequena(string strMensagem)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Notificacao", "bootbox.alert({ title: '" + strTitulo + "', message: '" + strMensagem + "', size: 'small', className: 'NotificacaoErro', backdrop: true });", true);
        }

        public void NotificacaoErroPequena(string strMensagem, string strLinkRedirect, bool booCentralizada)
        {
            string strTitulo = ObterTituloNotificacaoPadrao();
            strMensagem = strMensagem != "" ? strMensagem : " ";
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
    }
}