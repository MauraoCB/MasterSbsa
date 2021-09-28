using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Diagnostics;

namespace MasterSbsa.Util
{
    public class Funcoes : IDisposable
    {
        #region VerificaNumero
        public bool VerificaNumero(string strNumero)
        {
            long vNumero = 0;
            bool verifica = long.TryParse(strNumero, out vNumero);
            if (!verifica)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region VerificaDouble
        public bool VerificaDouble(string strNumero)
        {
            double vNumero = 0;
            bool verifica = double.TryParse(strNumero, out vNumero);
            if (!verifica)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region VerificaData
        public bool VerificaData(string strData)
        {
            DateTime vData;
            bool verifica = DateTime.TryParse(strData, out vData);
            if (!verifica)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region VerificaMoeda
        public bool VerificaMoeda(string strMoeda)
        {
            double vValor;
            bool verifica = double.TryParse(strMoeda, out vValor);
            if (!verifica)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region ConverteMesExtenso
        public String ConverteMesExtenso(int intMes)
        {
            try
            {
                String strMes = "";
                switch (intMes)
                {
                    case 1:
                        strMes = "JANEIRO";
                        break;
                    case 2:
                        strMes = "FEVEREIRO";
                        break;
                    case 3:
                        strMes = "MARCO";
                        break;
                    case 4:
                        strMes = "ABRIL";
                        break;
                    case 5:
                        strMes = "MAIO";
                        break;
                    case 6:
                        strMes = "JUNHO";
                        break;
                    case 7:
                        strMes = "JULHO";
                        break;
                    case 8:
                        strMes = "AGOSTO";
                        break;
                    case 9:
                        strMes = "SETEMBRO";
                        break;
                    case 10:
                        strMes = "OUTUBRO";
                        break;
                    case 11:
                        strMes = "NOVEMBRO";
                        break;
                    case 12:
                        strMes = "DEZEMBRO";
                        break;
                }
                return strMes;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
        #endregion

        #region EnviarEmail
        public bool EnviarEmail(string strMensagem, string strAssunto, string strOrigem, List<string> listaDestinatario)
        {
            try
            {
                MailMessage objEmail = new MailMessage();

                objEmail.From = new MailAddress(strOrigem);

                foreach (var item in listaDestinatario)
                {
                    objEmail.To.Add(item);
                }

                objEmail.Subject = strAssunto;
                objEmail.IsBodyHtml = false;
                objEmail.Body = strMensagem;
                SmtpClient smtpClient = new SmtpClient("mail2.santosbrasil.com.br");
                smtpClient.EnableSsl = false;
                smtpClient.Send(objEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region EnviarEmailHtml
        public bool EnviarEmailHtml(string strMensagem, string strAssunto, string strOrigem, List<string> listaDestinatario)
        {
            try
            {
                MailMessage objEmail = new MailMessage();

                objEmail.From = new MailAddress(strOrigem);

                foreach (var item in listaDestinatario)
                {
                    objEmail.To.Add(item);
                }

                objEmail.Subject = strAssunto;
                objEmail.IsBodyHtml = true;
                objEmail.Body = strMensagem;
                SmtpClient smtpClient = new SmtpClient("mail2.santosbrasil.com.br");
                smtpClient.EnableSsl = false;
                smtpClient.Send(objEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion EnviarEmailHtml

        #region LogErros
        public void LogErros(Exception Ex, String strAmbiente, String strTela, String strMetodo, String strUsuario)
        {
            String strPath = ConfigurationManager.AppSettings[@"PathLog"].ToString() != "" ? ConfigurationManager.AppSettings["PATHLOG"].ToString() : "";
            String strFile = ConfigurationManager.AppSettings[@"PathFile"].ToString() != "" ? ConfigurationManager.AppSettings["PATHFILE"].ToString() : "";
            String strSmtp = ConfigurationManager.AppSettings[@"SmtpClient"] != null ? ConfigurationManager.AppSettings["SmtpClient"].ToString() : "";

            System.Diagnostics.StackTrace stTrace = new System.Diagnostics.StackTrace(Ex, true);
            var strLinha = stTrace.GetFrame(stTrace.FrameCount - 1).GetFileLineNumber().ToString();
            var strTarget = Ex.TargetSite != null ? Ex.TargetSite.Name : "Indefinido";

            if (strPath != "" && strFile != "")
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                if (!File.Exists(strFile))
                {
                    using (StreamWriter Sw = File.CreateText(strFile))
                    {
                        Sw.WriteLine("------" + DateTime.Now.ToString() + "------");
                        Sw.WriteLine("Erro: " + Ex.Message.ToString());
                        Sw.WriteLine("Ambiente: " + strAmbiente);
                        Sw.WriteLine("Tela: " + strTela);
                        Sw.WriteLine("Metodo: " + strMetodo);
                        Sw.WriteLine("Linha: " + strLinha);
                        Sw.WriteLine("Instrucao: " + strTarget);
                        Sw.WriteLine("Usuario: " + strUsuario);
                        Sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter Sw = File.AppendText(strFile))
                    {
                        Sw.WriteLine("");
                        Sw.WriteLine("------" + DateTime.Now.ToString() + "------");
                        Sw.WriteLine("Erro: " + Ex.Message.ToString());
                        Sw.WriteLine("Ambiente: " + strAmbiente);
                        Sw.WriteLine("Tela: " + strTela);
                        Sw.WriteLine("Metodo: " + strMetodo);
                        Sw.WriteLine("Linha: " + strLinha);
                        Sw.WriteLine("Instrucao: " + strTarget);
                        Sw.WriteLine("Usuario: " + strUsuario);
                        Sw.Close();
                    }
                }
            }
        }

        public void LogErros(Exception Ex, object objTela, StackTrace stackTrace)
        {
            //Variaveis recuperando do web.config
            String strPath = ConfigurationManager.AppSettings[@"PathLog"].ToString() != "" ? ConfigurationManager.AppSettings["PATHLOG"].ToString() : "";
            String strFile = ConfigurationManager.AppSettings[@"PathFile"].ToString() != "" ? ConfigurationManager.AppSettings["PATHFILE"].ToString() : "";
            String strSmtp = ConfigurationManager.AppSettings[@"SmtpClient"] != null ? ConfigurationManager.AppSettings["SmtpClient"].ToString() : "";
            String strAmbiente = ConfigurationManager.AppSettings["Ambiente"] != null ? ConfigurationManager.AppSettings["Ambiente"].ToString() : "";
            String strSistId = ConfigurationManager.AppSettings["SistemaID"] != null ? ConfigurationManager.AppSettings["SistemaID"].ToString() : "";
            String strLogSentinela = ConfigurationManager.AppSettings["LogSentinelaErros"] != null ? ConfigurationManager.AppSettings["LogSentinelaErros"].ToString() : "";

            //Recuperando a session
            String strUsuario = HttpContext.Current.Session["UsuarioMasterPage"] != null ? HttpContext.Current.Session["UsuarioMasterPage"].ToString().ToUpper() : "Indefinido";

            //Recuperando a tela
            Page Tela = (Page)objTela;
            String strTela = Tela.Page.ToString().Substring(4, Tela.Page.ToString().Substring(4).Length - 5) + ".aspx";

            //Recuperando o metodo que disparou o erro.
            String strMetodo = stackTrace.GetFrame(0).GetMethod().Name;

            //Recuperando a linha e a instrução do erro.
            System.Diagnostics.StackTrace stTrace = new System.Diagnostics.StackTrace(Ex, true);
            String strLinha = stTrace.GetFrame(stTrace.FrameCount - 1).GetFileLineNumber().ToString();
            String strTarget = Ex.TargetSite != null ? Ex.TargetSite.Name : "Indefinido";

            if (strTarget == "AbortInternal") return;

            //if (strUsuario != "Indefinido")
            //{
            if (strPath != "" && strFile != "")
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                if (!File.Exists(strFile))
                {
                    using (StreamWriter Sw = File.CreateText(strFile))
                    {
                        Sw.WriteLine("------" + DateTime.Now.ToString() + "------");
                        Sw.WriteLine("Erro: " + Ex.Message.ToString());
                        Sw.WriteLine("Ambiente: " + strAmbiente);
                        Sw.WriteLine("Tela: " + strTela);
                        Sw.WriteLine("Metodo: " + strMetodo);
                        Sw.WriteLine("Linha: " + strLinha);
                        Sw.WriteLine("Instrucao: " + strTarget);
                        Sw.WriteLine("Usuario: " + strUsuario);
                        Sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter Sw = File.AppendText(strFile))
                    {
                        Sw.WriteLine("");
                        Sw.WriteLine("------" + DateTime.Now.ToString() + "------");
                        Sw.WriteLine("Erro: " + Ex.Message.ToString());
                        Sw.WriteLine("Ambiente: " + strAmbiente);
                        Sw.WriteLine("Tela: " + strTela);
                        Sw.WriteLine("Metodo: " + strMetodo);
                        Sw.WriteLine("Linha: " + strLinha);
                        Sw.WriteLine("Instrucao: " + strTarget);
                        Sw.WriteLine("Usuario: " + strUsuario);
                        Sw.Close();
                    }
                }
            }
            //}

            if (strLogSentinela == "S")
            {
                if (strUsuario != "Indefinido")
                {
                    using (var BllLogSentinela = new Bll.LogSentinela(strUsuario))
                    {
                        var vRet = BllLogSentinela.CriaLogSentinelaErro(strTela.ToUpper(), "ERRO", strUsuario, strSistId, Ex.Message, strMetodo, strTarget);
                    }
                }
            }
        }
        #endregion

        #region RemoveCondicaoFimString
        public string RemoveCondicaoFimString(string strVerificar, int intInicio, int intFim, string strCond)
        {
            if (strVerificar.Substring(intInicio, intFim).Equals(strCond))
            {
                strVerificar = strVerificar.Remove(intInicio, intFim);
            }
            return strVerificar;
        }
        #endregion

        #region ReplaceCondicaoFimString
        public string ReplaceCondicaoFimString(string strVerificar, int intInicio, int intFim, string strCond, string strReplace)
        {
            if (strVerificar.Substring(intInicio, intFim).Equals(strCond))
            {
                strVerificar = strVerificar.Replace(strCond, strReplace);
            }
            return strVerificar;
        }
        #endregion

        #region VerificaSaidaMaiorEntrada
        public bool VerificaSaidaMaiorEntrada(string strEntrada, string strSaida)
        {
            if (Convert.ToDateTime(strSaida) < Convert.ToDateTime(strEntrada))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Criptografar
        public string Criptografar(String senha)
        {
            var ret = String.Empty;
            for (int i = 0; i < senha.Trim().Length; i++)
            {
                ret += Chr(Asc(senha.Substring(i, 1)) + (i + 1));
            }
            return ret;
        }
        #endregion

        #region ConverteDataPtBrParaUtc
        public string ConverteDataPtBrParaUtc(string strData)
        {
            //2016-11-11T00:01

            DateTime dtConverter = DateTime.Parse(strData);

            strData = dtConverter.Year + "-" + dtConverter.Month.ToString().PadLeft(2, '0') + "-" + dtConverter.Day.ToString().PadLeft(2, '0') + "T" + dtConverter.Hour.ToString().PadLeft(2, '0') + ":" + dtConverter.Minute.ToString().PadLeft(2, '0');

            return strData;
        }
        #endregion

        #region ConverteDataUtcParaPtBr
        public string ConverteDataUtcParaPtBr(string strData)
        {
            return DateTime.Parse(strData).ToString();
        }
        #endregion

        #region MontaINSql
        public string MontaINSql(string strInSql)
        {
            var arrIn = strInSql.Split(',');
            string strIn = "";

            for (int i = 0; i < arrIn.Length; i++)
            {
                if (i == 0)
                {
                    strIn += "'" + arrIn[i].ToString();
                }
                else
                {
                    strIn += "','" + arrIn[i].ToString();
                }
            }
            return strIn + "'";
        }
        #endregion

        #region Asc
        private int Asc(string letra)
        {
            return (int)(Convert.ToChar(letra));
        }
        #endregion

        #region Chr
        private char Chr(int codigo)
        {
            return (char)codigo;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region CriptDecrip
        public new string CriptDecrip(string strString, string strTipo)
        {
            try
            {
                using (var dalUsuario = new Dal.Usuario())
                {
                    return dalUsuario.CriptDecrip(strString, strTipo);   
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetChaveValidacao
        public new String GetChaveValidacao()
        {
            try
            {
                using (var dalUsuario = new Dal.Usuario())
                {
                    return dalUsuario.GetChaveValidacao();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetUrlWebServiceValidadorAD
        public new string GetUrlWebServiceValidadorAD()
        {
            try
            {
                using (var dalUsuario = new Dal.Usuario())
                {
                    return dalUsuario.GetUrlWebServiceValidadorAD();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}