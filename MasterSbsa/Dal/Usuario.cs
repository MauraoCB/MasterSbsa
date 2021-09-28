using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace MasterSbsa.Dal
{
    public class Usuario : ECRUD.Oracle
    {
        #region Properties
        public string Ambiente { get; private set; }
        public string SistemaId { get; private set; }
        #endregion

        #region Construtor
        public Usuario()
        {
            Ambiente = System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString();
            SistemaId = System.Configuration.ConfigurationManager.AppSettings["SistemaID"].ToString();
        }
        #endregion

        #region BuscaDadosDeAcesso
        public DataTable BuscaDadosDeAcesso(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";

            try
            {
                if (Ambiente == "Tecon")
                {
                    //String strQuery = "SELECT AU.USUA_ID USUA_ID, AU.USUA_LOGIN, AU.USUA_NOME, PA.NOME PERFIL, PA.ID ID_ACESSO, AU.USUA_REGISTRO FROM PIER.SISTEMA@SGTAP_LINK S, PIER.PERFIL_ACESSO@SGTAP_LINK PA, PIER.ACESSO@SGTAP_LINK A, PIER.SESSAO@SGTAP_LINK SS, PIER.USUARIO@SGTAP_LINK U, ACESSOS.USUARIOS AU WHERE PA.ID = A.PERFIL_ACESSO_ID AND A.SESSAO_ID = SS.ID AND SS.USUARIO = U.ID AND AU.USUA_ID = U.CORP_USUA_ID AND S.ID = PA.SISTEMA_ID AND A.ATIVO = 1 AND PA.SISTEMA_ID = '" + SistemaId + "' AND AU.USUA_LOGIN = '" + strLogin + "' AND AU.USUA_SENHA = '" + strSenha + "'";
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND USUA_LOGIN = '" + strLogin + "' AND (USUA_SENHA = '" + strSenha + "') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematTecon, out strErro);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    //String strQuery = "SELECT AU.USUA_ID USUA_ID, AU.USUA_LOGIN, AU.USUA_NOME, PA.NOME PERFIL, PA.ID ID_ACESSO, AU.USUA_REGISTRO FROM PIER.SISTEMA@SGTAP_LINK S, PIER.PERFIL_ACESSO@SGTAP_LINK PA, PIER.ACESSO@SGTAP_LINK A, PIER.SESSAO@SGTAP_LINK SS, PIER.USUARIO@SGTAP_LINK U, ACESSOS.USUARIOS AU WHERE PA.ID = A.PERFIL_ACESSO_ID AND A.SESSAO_ID = SS.ID AND SS.USUARIO = U.ID AND AU.USUA_ID = U.CORP_USUA_ID AND S.ID = PA.SISTEMA_ID AND A.ATIVO = 1 AND PA.SISTEMA_ID = '" + SistemaId + "' AND AU.USUA_LOGIN = '" + strLogin + "' AND AU.USUA_SENHA = '" + strSenha + "'";
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND USUA_LOGIN = '" + strLogin + "' AND USUA_SENHA = '" + strSenha + "' AND ATIVO = '1'";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematImbituba, out strErro);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    //String strQuery = "SELECT AU.USUA_ID USUA_ID, AU.USUA_LOGIN, AU.USUA_NOME, PA.NOME PERFIL, PA.ID ID_ACESSO, AU.USUA_REGISTRO FROM PIER.SISTEMA@SGTAP_LINK S, PIER.PERFIL_ACESSO@SGTAP_LINK PA, PIER.ACESSO@SGTAP_LINK A, PIER.SESSAO@SGTAP_LINK SS, PIER.USUARIO@SGTAP_LINK U, ACESSOS.USUARIOS AU WHERE PA.ID = A.PERFIL_ACESSO_ID AND A.SESSAO_ID = SS.ID AND SS.USUARIO = U.ID AND AU.USUA_ID = U.CORP_USUA_ID AND S.ID = PA.SISTEMA_ID AND A.ATIVO = 1 AND PA.SISTEMA_ID = '" + SistemaId + "' AND AU.USUA_LOGIN = '" + strLogin + "' AND AU.USUA_SENHA = '" + strSenha + "'";
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND USUA_LOGIN = '" + strLogin + "' AND USUA_SENHA = '" + strSenha + "' AND ATIVO = '1'";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematLogistica, out strErro);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    //String strQuery = "SELECT AU.USUA_ID USUA_ID, AU.USUA_LOGIN, AU.USUA_NOME, PA.NOME PERFIL, PA.ID ID_ACESSO, AU.USUA_REGISTRO FROM PIER.SISTEMA@SGTAP_LINK S, PIER.PERFIL_ACESSO@SGTAP_LINK PA, PIER.ACESSO@SGTAP_LINK A, PIER.SESSAO@SGTAP_LINK SS, PIER.USUARIO@SGTAP_LINK U, ACESSOS.USUARIOS AU WHERE PA.ID = A.PERFIL_ACESSO_ID AND A.SESSAO_ID = SS.ID AND SS.USUARIO = U.ID AND AU.USUA_ID = U.CORP_USUA_ID AND S.ID = PA.SISTEMA_ID AND A.ATIVO = 1 AND PA.SISTEMA_ID = '" + SistemaId + "' AND AU.USUA_LOGIN = '" + strLogin + "' AND AU.USUA_SENHA = '" + strSenha + "'";
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND USUA_LOGIN = '" + strLogin + "' AND USUA_SENHA = '" + strSenha + "' AND ATIVO = '1'";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematConvicon, out strErro);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaMenusDoPerfil
        public DataTable BuscaMenusDoPerfil(long lngIdAcesso)
        {
            try
            {
                if (Ambiente == "Tecon")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID WHERE PA.ID = '" + lngIdAcesso + "' ORDER BY PG.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapTecon);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID WHERE PA.ID = '" + lngIdAcesso + "' ORDER BY PG.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID WHERE PA.ID = '" + lngIdAcesso + "' ORDER BY PG.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapLogistica);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID WHERE PA.ID = '" + lngIdAcesso + "' ORDER BY PG.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapConvicon);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaPaginasDoMenu
        public DataTable BuscaPaginasDoMenu(long lngIdAcesso, string strNomeMenu)
        {
            try
            {
                if (Ambiente == "Tecon")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM, PG1.NOME SUB_MENU_NOME, PG1.ENDERECO SUB_MENU_ENDERECO, PG1.ORDEM SUB_MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINAS_SUBPAGINAS SB1 ON SB1.PAGINA_ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINA PG1 ON SB1.SUBPAGINA_ID = PG1.ID WHERE PA.ID = '" + lngIdAcesso + "' AND PG.NOME = '" + strNomeMenu + "' ORDER BY PG.ORDEM, PG1.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapTecon);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM, PG1.NOME SUB_MENU_NOME, PG1.ENDERECO SUB_MENU_ENDERECO, PG1.ORDEM SUB_MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINAS_SUBPAGINAS SB1 ON SB1.PAGINA_ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINA PG1 ON SB1.SUBPAGINA_ID = PG1.ID WHERE PA.ID = '" + lngIdAcesso + "' AND PG.NOME = '" + strNomeMenu + "' ORDER BY PG.ORDEM, PG1.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM, PG1.NOME SUB_MENU_NOME, PG1.ENDERECO SUB_MENU_ENDERECO, PG1.ORDEM SUB_MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINAS_SUBPAGINAS SB1 ON SB1.PAGINA_ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINA PG1 ON SB1.SUBPAGINA_ID = PG1.ID WHERE PA.ID = '" + lngIdAcesso + "' AND PG.NOME = '" + strNomeMenu + "' ORDER BY PG.ORDEM, PG1.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapLogistica);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    String strQuery = "SELECT SI.NOME SISTEMA, PA.ID ID_ACESSO, PA.NOME PERFIL, PG.NOME MENU_NOME, PG.ENDERECO MENU_ENDERECO, PG.ORDEM MENU_ORDEM, PG1.NOME SUB_MENU_NOME, PG1.ENDERECO SUB_MENU_ENDERECO, PG1.ORDEM SUB_MENU_ORDEM FROM PIER.PERFIL_ACESSO PA INNER JOIN PIER.SISTEMA SI ON SI.ID = PA.SISTEMA_ID INNER JOIN PIER.PERFIL_ACESSO_PAGINAS PP ON PA.ID = PP.PERFIL_ACESSO_ID INNER JOIN PIER.PAGINA PG ON PG.ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINAS_SUBPAGINAS SB1 ON SB1.PAGINA_ID = PP.PAGINA_ID LEFT JOIN PIER.PAGINA PG1 ON SB1.SUBPAGINA_ID = PG1.ID WHERE PA.ID = '" + lngIdAcesso + "' AND PG.NOME = '" + strNomeMenu + "' ORDER BY PG.ORDEM, PG1.ORDEM";
                    return GetDataTable(strQuery, Enums.Bancos.SgtapConvicon);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosDeAcessoTiFlow
        public DataTable BuscaDadosDeAcessoTiFlow(string login, string senha, out string strErro)
        {
            strErro = "";

            try
            {
                var sql = "";
                var log = "";
                try
                {
                    using (var ecrud = new ECRUD.Oracle())
                    {
                        sql = "SELECT -1 ID_ACESSO, FUNC_REGISTRO USUA_REGISTRO, FUNC_NOME USUA_NOME, 'GERAL' PERFIL  FROM ACESSOS.V_FUNCIONARIOS WHERE TO_CHAR(FUNC_REGISTRO) = '" + login.ToUpper() + "' AND SENHA = '" + senha.ToUpper() + "' ";
                        var dt = ecrud.GetDataTable(sql, Enums.Bancos.TelematTecon);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }

                        sql = "SELECT CTIS.F_CRIPTA@CTIS_LINK('" + senha.ToUpper() + "','C') senha FROM DUAL";
                        dt = ecrud.GetDataTable(sql, Enums.Bancos.TelematTecon);
                        var senhaCript = dt.Rows[0]["senha"].ToString();
                        //log += " senha criptografada | sql: " + sql;

                        //procura o usuario na ACESSOS.USUARIOS DO TECON
                        sql = "SELECT -1 ID_ACESSO, USUA_REGISTRO, USUA_NOME, 'GERAL' PERFIL FROM ACESSOS.USUARIOS WHERE usua_ativo <> 0 and  (USUA_LOGIN = '" + login.ToUpper() + "' OR TO_CHAR(USUA_REGISTRO) = '" + login.ToUpper() + "' )AND TRIM(USUA_SENHA) = TRIM('" + senhaCript + "') ";
                        dt = ecrud.GetDataTable(sql, Enums.Bancos.TelematTecon);
                        if (dt != null && dt.Rows.Count > 0) return dt;
                        log += " | ACESSOS.USUARIOS DO TECON / linhas dt: " + (dt != null ? "nulo" : dt.Rows.Count.ToString()) + " | sql: " + sql;
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //procura o usuario na ACESSO.USUARIOS DA LOGISTICA //////////////////////////////////////////////////////////////////////////////////////////////////
                        sql = "SELECT -1 ID_ACESSO, USUA_REGISTRO, USUA_NOME, 'GERAL' PERFIL FROM ACESSO.USUARIOS WHERE usua_ativo <> 0 and  (USUA_LOGIN = '" + login.ToUpper() + "' OR TO_CHAR(USUA_REGISTRO) = '" + login.ToUpper() + "' )AND TRIM(USUA_SENHA) = TRIM('" + senhaCript + "') ";
                        dt = ecrud.GetDataTable(sql, Enums.Bancos.TelematLogistica);
                        if (dt != null && dt.Rows.Count > 0) return dt;
                        log += " |  ACESSO.USUARIOS DA LOGISTICA / linhas dt: " + (dt != null ? "nulo" : dt.Rows.Count.ToString()) + " | sql: " + sql;
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //procura o usuario na FATU.USUARIOS DA LOGISTICA //////////////////////////////////////////////////////////////////////////////////////////////////
                        sql = "SELECT -1 ID_ACESSO, USUA_REGISTRO, USUA_NOME, 'GERAL' PERFIL FROM FATU.USUARIOS WHERE usua_ativo <> 0 and  (USUA_LOGIN = '" + login.ToUpper() + "' OR TO_CHAR(USUA_REGISTRO) = '" + login.ToUpper() + "' )AND TRIM(USUA_SENHA) = TRIM('" + senhaCript + "') ";
                        dt = ecrud.GetDataTable(sql, Enums.Bancos.TelematLogistica);
                        if (dt != null && dt.Rows.Count > 0) return dt;
                        log += " |  FATU.USUARIOS DA LOGISTICA / linhas dt: " + (dt != null ? "nulo" : dt.Rows.Count.ToString()) + " | sql: " + sql;
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //procura o usuario na ACESSOS.USUARIOS DE IMBITUBA //////////////////////////////////////////////////////////////////////////////////////////////////
                        sql = "SELECT -1 ID_ACESSO, USUA_REGISTRO, USUA_NOME, 'GERAL' PERFIL FROM FATU.USUARIOS WHERE usua_ativo <> 0 and  (USUA_LOGIN = '" + login.ToUpper() + "' OR TO_CHAR(USUA_REGISTRO) = '" + login.ToUpper() + "' )AND TRIM(USUA_SENHA) = TRIM('" + senhaCript + "') ";
                        dt = ecrud.GetDataTable(sql, Enums.Bancos.FatuImbituba);
                        if (dt != null && dt.Rows.Count > 0) return dt;
                        log += " |  ACESSOS.USUARIOS DE IMBITUBA / linhas dt: " + (dt != null ? "nulo" : dt.Rows.Count.ToString()) + " | sql: " + sql;
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        return LoginGRCTiFlow(login, md5(senha), md5(senha.ToLower()));
                    }
                }
                catch
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region LoginGRCTiFlow
        public DataTable LoginGRCTiFlow(string login, string senha, string senhaLower)
        {
            try
            {
                using (var ecrud = new ECRUD.Oracle())
                {
                    var strSql = "SELECT -1 ID_ACESSO, p.pess_registro usua_registro ,UPPER (p.pess_nome) usua_nome, 'GERAL' PERFIL  " +
                                  "   FROM grc.v_pessoas @corpdw p, grc.usuario @corpdw u " +
                                  "     WHERE p.pess_id = u.pess_id(+) " +
                                  "    and u.usua_ativo = 'T' " +
                                  "    and(p.pess_registro = upper('" + login + "')  " +
                                  "        or upper(u.usua_login) = upper('" + login + "') ) " +
                                  "    and(u.usua_senha = '" + senha + "' " +
                                  "        or u.usua_senha_insensitive = '" + senhaLower + "')";

                    return ecrud.GetDataTable(strSql, Enums.Bancos.TelematTecon);
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosColetorMaquinas
        public DataTable BuscaDadosColetorMaquinas(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";
            try
            {
                DataTable dt;
                using (var ecrud = new ECRUD.Oracle())
                {
                    string sql = "SELECT CTIS.F_CRIPTA@CTIS_LINK('" + strSenha.ToUpper() + "','C') senha FROM DUAL";
                    dt = ecrud.GetDataTable(sql, Enums.Bancos.CtisImbituba);
                    var strSenhaCript = dt.Rows[0]["senha"].ToString();

                    string strSQL = " SELECT -1 ID_ACESSO,USUA_REGISTRO, TRIM(AUSR_FULL_NAME) USUA_NOME, TRIM(AGRP_NAME) PERFIL FROM CTIS.APPL_USR WHERE UPPER(AUSR_NAME) = '" + strLogin.ToUpper() + "' AND AUSR_PASS = '" + strSenhaCript + "'";
                    dt = ecrud.GetDataTable(strSQL, Enums.Bancos.CtisImbituba);

                    return dt;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosColetorMaquinasNovelis
        public DataTable BuscaDadosColetorMaquinasNovelis(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";
            try
            {
                DataTable dt;
                using (var ecrud = new ECRUD.Oracle())
                {
                    string sql = "SELECT CTIS.F_CRIPTA('" + strSenha.ToUpper() + "','C') senha FROM DUAL";
                    dt = ecrud.GetDataTable(sql, Enums.Bancos.CtisNovelis);
                    var strSenhaCript = dt.Rows[0]["senha"].ToString();

                    string strSQL = " SELECT -1 ID_ACESSO,USUA_REGISTRO, TRIM(AUSR_FULL_NAME) USUA_NOME, TRIM(AGRP_NAME) PERFIL FROM CTIS.APPL_USR WHERE UPPER(AUSR_NAME) = '" + strLogin.ToUpper() + "' AND AUSR_PASS = '" + strSenhaCript + "'";
                    dt = ecrud.GetDataTable(strSQL, Enums.Bancos.CtisNovelis);

                    return dt;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosColetorMaquinasSaboo2
        public DataTable BuscaDadosColetorMaquinasSaboo2(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";
            try
            {
                DataTable dt;
                using (var ecrud = new ECRUD.Oracle())
                {
                    string sql = "SELECT CTIS.F_CRIPTA@CTIS_LINK('" + strSenha.ToUpper() + "','C') senha FROM DUAL";
                    dt = ecrud.GetDataTable(sql, Enums.Bancos.CtisSaboo2);
                    var strSenhaCript = dt.Rows[0]["senha"].ToString();

                    string strSQL = " SELECT -1 ID_ACESSO,USUA_REGISTRO, TRIM(AUSR_FULL_NAME) USUA_NOME, TRIM(AGRP_NAME) PERFIL FROM CTIS.APPL_USR WHERE UPPER(AUSR_NAME) = '" + strLogin.ToUpper() + "' AND AUSR_PASS = '" + strSenhaCript + "'";
                    dt = ecrud.GetDataTable(strSQL, Enums.Bancos.CtisSaboo2);

                    return dt;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosDeAcessoComPrestador
        public DataTable BuscaDadosDeAcessoComPrestador(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";

            try
            {
                if (Ambiente == "Tecon")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT ID_ACESSO, DATA_CADASTRO, ATIVO, SISTEMA_ID, TO_CHAR(PERFIL) PERFIL, MOSTRAR_MENU, RECU_ID_GRC, USUA_LOGIN, USUA_SENHA, USUA_NOME, USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO = '1'");
                    sbQuery.Append("UNION ");
                    sbQuery.Append("SELECT M.ID_ACESSO, TO_DATE(M.DATA_CADASTRO, 'DD/MM/YYYY HH24:MI:SS') DATA_CADASTRO,");
                    sbQuery.Append("    M.ATIVO, M.SISTEMA_ID, M.PERFIL, TO_NUMBER(M.MOSTRAR_MENU) MOSTRAR_MENU, TO_NUMBER(M.RECU_ID_GRC) RECU_ID_GRC,");
                    sbQuery.Append("    M.USUA_LOGIN, M.USUA_SENHA, M.USUA_NOME, M.USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA_PREST M");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO <> '0'");
                    return GetDataTableWithException(sbQuery.ToString(), Enums.Bancos.TelematTecon, out strErro);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT ID_ACESSO, DATA_CADASTRO, ATIVO, SISTEMA_ID, TO_CHAR(PERFIL) PERFIL, MOSTRAR_MENU, RECU_ID_GRC, USUA_LOGIN, USUA_SENHA, USUA_NOME, USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO = '1'");
                    sbQuery.Append("UNION ");
                    sbQuery.Append("SELECT M.ID_ACESSO, TO_DATE(M.DATA_CADASTRO, 'DD/MM/YYYY HH24:MI:SS') DATA_CADASTRO,");
                    sbQuery.Append("    M.ATIVO, M.SISTEMA_ID, M.PERFIL, TO_NUMBER(M.MOSTRAR_MENU) MOSTRAR_MENU, TO_NUMBER(M.RECU_ID_GRC) RECU_ID_GRC,");
                    sbQuery.Append("    M.USUA_LOGIN, M.USUA_SENHA, M.USUA_NOME, M.USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA_PREST M");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO <> '0'");
                    return GetDataTable(sbQuery.ToString(), Enums.Bancos.TelematImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT ID_ACESSO, DATA_CADASTRO, ATIVO, SISTEMA_ID, TO_CHAR(PERFIL) PERFIL, MOSTRAR_MENU, RECU_ID_GRC, USUA_LOGIN, USUA_SENHA, USUA_NOME, USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO = '1'");
                    sbQuery.Append("UNION ");
                    sbQuery.Append("SELECT M.ID_ACESSO, TO_DATE(M.DATA_CADASTRO, 'DD/MM/YYYY HH24:MI:SS') DATA_CADASTRO,");
                    sbQuery.Append("    M.ATIVO, M.SISTEMA_ID, M.PERFIL, TO_NUMBER(M.MOSTRAR_MENU) MOSTRAR_MENU, TO_NUMBER(M.RECU_ID_GRC) RECU_ID_GRC,");
                    sbQuery.Append("    M.USUA_LOGIN, M.USUA_SENHA, M.USUA_NOME, M.USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA_PREST M");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO <> '0'");
                    return GetDataTable(sbQuery.ToString(), Enums.Bancos.TelematLogistica);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT ID_ACESSO, DATA_CADASTRO, ATIVO, SISTEMA_ID, TO_CHAR(PERFIL) PERFIL, MOSTRAR_MENU, RECU_ID_GRC, USUA_LOGIN, USUA_SENHA, USUA_NOME, USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO = '1'");
                    sbQuery.Append("UNION ");
                    sbQuery.Append("SELECT M.ID_ACESSO, TO_DATE(M.DATA_CADASTRO, 'DD/MM/YYYY HH24:MI:SS') DATA_CADASTRO,");
                    sbQuery.Append("    M.ATIVO, M.SISTEMA_ID, M.PERFIL, TO_NUMBER(M.MOSTRAR_MENU) MOSTRAR_MENU, TO_NUMBER(M.RECU_ID_GRC) RECU_ID_GRC,");
                    sbQuery.Append("    M.USUA_LOGIN, M.USUA_SENHA, M.USUA_NOME, M.USUA_REGISTRO");
                    sbQuery.Append("  FROM TELEMAT.MV_LOGIN_MASTER_SBSA_PREST M");
                    sbQuery.Append(" WHERE     SISTEMA_ID = '" + SistemaId + "'");
                    sbQuery.Append("       AND USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND USUA_SENHA = '" + strSenha + "'");
                    sbQuery.Append("       AND ATIVO <> '0'");
                    return GetDataTable(sbQuery.ToString(), Enums.Bancos.TelematConvicon);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosDeAcessoColetorCostado
        public DataTable BuscaDadosDeAcessoColetorCostado(string strLogin, string strSenha, out string strErro)
        {
            strErro = "";

            try
            {
                Util.Funcoes Funcoes = new Util.Funcoes();
                if (Ambiente == "Tecon")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT U.*, UA.*, '3880' ID_ACESSO , 'COSTADO' PERFIL");
                    sbQuery.Append("  FROM ACESSOS.USUARIOS U, ACESSOS.USUA_ACES UA");
                    sbQuery.Append(" WHERE     U.USUA_ID = UA.USUA_ID");
                    sbQuery.Append("       AND UA.ACES_ID = 9");
                    sbQuery.Append("       AND UA.USAC_ATIVO = 1");
                    sbQuery.Append("       AND U.USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND U.USUA_SENHA = '" + strSenha + "'");
                    return GetDataTableWithException(sbQuery.ToString(), Enums.Bancos.TelematTecon, out strErro);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT U.*, UA.*, '3880' ID_ACESSO , 'COSTADO' PERFIL");
                    sbQuery.Append("  FROM ACESSOS.USUARIOS U, ACESSOS.USUA_ACES UA");
                    sbQuery.Append(" WHERE     U.USUA_ID = UA.USUA_ID");
                    sbQuery.Append("       AND UA.ACES_ID = 9");
                    sbQuery.Append("       AND UA.USAC_ATIVO = 1");
                    sbQuery.Append("       AND U.USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND U.USUA_SENHA = '" + strSenha + "'");
                    return GetDataTable(sbQuery.ToString(), Enums.Bancos.TelematImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT U.*, UA.*, '3880' ID_ACESSO , 'COSTADO' PERFIL");
                    sbQuery.Append("  FROM ACESSO.USUARIOS U, ACESSO.USUA_ACES UA");
                    sbQuery.Append(" WHERE     U.USUA_ID = UA.USUA_ID");
                    sbQuery.Append("       AND UA.ACES_ID = 9");
                    sbQuery.Append("       AND UA.USAC_ATIVO = 1");
                    sbQuery.Append("       AND U.USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND U.USUA_SENHA = '" + Funcoes.Criptografar(strSenha) + "'");
                    DataTable dt = GetDataTable(sbQuery.ToString(), Enums.Bancos.SgtapLogistica);
                    if (dt.Rows.Count == 0)
                    {
                        
                        sbQuery.Clear();
                        sbQuery.Append("  SELECT TDRV_ID USUA_REGISTRO ,TDRV_LAST_NAME USUA_NOME ,'3880' ID_ACESSO,'MOTORISTA' PERFIL  ");
                        sbQuery.Append("  FROM CTIS.TRUCK_DRIVER WHERE TDRV_STA_ATI = 'S'  ");
                        sbQuery.Append("  AND TDRV_CPF_NO= '"+ strLogin +"' AND  TDRV_CPF_NO = '"+ strSenha +"'");
                        dt = GetDataTable(sbQuery.ToString(), Enums.Bancos.SgtapLogistica);

                    }
                    return dt;

                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT U.*, UA.*, '3880' ID_ACESSO , 'COSTADO' PERFIL");
                    sbQuery.Append("  FROM ACESSOS.USUARIOS U, ACESSOS.USUA_ACES UA");
                    sbQuery.Append(" WHERE     U.USUA_ID = UA.USUA_ID");
                    sbQuery.Append("       AND UA.ACES_ID = 9");
                    sbQuery.Append("       AND UA.USAC_ATIVO = 1");
                    sbQuery.Append("       AND U.USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND U.USUA_SENHA = '" + strSenha + "'");
                    return GetDataTable(sbQuery.ToString(), Enums.Bancos.TelematConvicon);
                }
                else if (Ambiente.ToString() == "Saboo")
                {
                    StringBuilder sbQuery = new StringBuilder();

                    sbQuery.Append("SELECT U.*, UA.*, '3880' ID_ACESSO , 'COSTADO' PERFIL");
                    sbQuery.Append("  FROM ACESSO.USUARIOS U, ACESSO.USUA_ACES UA");
                    sbQuery.Append(" WHERE     U.USUA_ID = UA.USUA_ID");
                    sbQuery.Append("       AND UA.ACES_ID = 9");
                    sbQuery.Append("       AND UA.USAC_ATIVO = 1");
                    sbQuery.Append("       AND U.USUA_LOGIN = '" + strLogin + "'");
                    sbQuery.Append("       AND U.USUA_SENHA = '" + Funcoes.Criptografar(strSenha) + "'");
                    DataTable dt = GetDataTable(sbQuery.ToString(), Enums.Bancos.CtisSaboo);
                    if (dt.Rows.Count == 0)
                    {

                        sbQuery.Clear();
                        sbQuery.Append("  SELECT TDRV_ID USUA_REGISTRO ,TDRV_LAST_NAME USUA_NOME ,'3880' ID_ACESSO,'MOTORISTA' PERFIL  ");
                        sbQuery.Append("  FROM CTIS.TRUCK_DRIVER WHERE TDRV_STA_ATI = 'S'  ");
                        sbQuery.Append("  AND TDRV_CPF_NO= '" + strLogin + "' AND  TDRV_CPF_NO = '" + strSenha + "'");
                        dt = GetDataTable(sbQuery.ToString(), Enums.Bancos.CtisSaboo);

                    }
                    return dt;

                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region MD5
        private static char[] HEX_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        public String md5(String text)
        {
            if (text == null)
            {
                return text;
            }

            try
            {
                //byte[] result = md5.ComputeHash(bytes);
                Encoding enc = new UTF8Encoding(true, true);
                //Gerando o hash da String
                byte[] bytesOfMessage = enc.GetBytes(text);
                MD5 md = new MD5CryptoServiceProvider();
                byte[] hashValue = md.ComputeHash(bytesOfMessage);

                //Convertendo o hash gerado para String na base hexadecimal
                char[] hexChars = new char[hashValue.Length * 2];
                int hexIndex;
                for (int j = 0; j < hashValue.Length; j++)
                {
                    hexIndex = hashValue[j] & 0xFF;
                    hexChars[j * 2] = HEX_CHARS[hexIndex >> 4];
                    hexChars[j * 2 + 1] = HEX_CHARS[hexIndex & 0x0F];
                }

                return new String(hexChars);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algoritmo MD5 não disponível: " + ex);
            }

            return text;
        }
        #endregion 

        #region CriptDecrip
        public string CriptDecrip(string strString, string strTipo)
        {
            try
            {
                string strQuery = "SELECT F_CRIPTA('" + strString + "', '" + strTipo + "') FROM DUAL";
                DataTable dt = GetDataTable(strQuery, Enums.Bancos.TelematTecon);
                if (dt!= null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetChaveValidacao
        public String GetChaveValidacao()
        {
            try
            {
                string strQuery = "SELECT CTIS.F_GET_CHAVE_VALIDACAO  FROM DUAL";
                DataTable dtValidador = null;

                if (Ambiente == "Tecon")
                {
                    dtValidador = GetDataTable(strQuery, Enums.Bancos.CtisTecon);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    dtValidador = GetDataTable(strQuery, Enums.Bancos.CtisImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    dtValidador = GetDataTable(strQuery, Enums.Bancos.CtisLogistica);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    dtValidador = GetDataTable(strQuery, Enums.Bancos.CtisConvicon);
                }

                if (dtValidador != null && dtValidador.Rows.Count > 0)
                {
                    return !string.IsNullOrEmpty(dtValidador.Rows[0][0].ToString()) ? dtValidador.Rows[0][0].ToString() : string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetUrlWebServiceValidadorAD
        public string GetUrlWebServiceValidadorAD()
        {
            try
            {                
                DataTable dtUrl = null;

                if (Ambiente == "Tecon")
                {
                    string strQuery = "SELECT * FROM CTIS.CONWEB WHERE UPPER(COW_DESCRICAO) = 'VALIDADORLOGINAD' AND COW_STATUS =1";
                    dtUrl = GetDataTable(strQuery, Enums.Bancos.CtisTecon);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    string strQuery = "SELECT * FROM CTIS.CONWEB WHERE UPPER(COW_DESCRICAO) = 'VALIDADORLOGINAD' AND COW_STATUS =1";
                    dtUrl = GetDataTable(strQuery, Enums.Bancos.TelematImbituba);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    string strQuery = "SELECT * FROM CTIS.CONWEB_AD WHERE UPPER(COW_DESCRICAO) = 'VALIDADORLOGINAD' AND COW_STATUS =1";
                    dtUrl = GetDataTable(strQuery, Enums.Bancos.CtisLogistica);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    string strQuery = "SELECT * FROM CTIS.CONWEB WHERE UPPER(COW_DESCRICAO) = 'VALIDADORLOGINAD' AND COW_STATUS =1";
                    dtUrl = GetDataTable(strQuery, Enums.Bancos.CtisConvicon);
                }

                if (dtUrl != null && dtUrl.Rows.Count > 0)
                {
                    return dtUrl.Rows[0]["COW_END_PRINCIPAL"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region BuscaDadosDeAcesso
        public DataTable BuscaDadosDeAcesso(string strCpf, out string strErro)
        {
            strErro = "";

            try
            {
                if (Ambiente == "Tecon")
                {
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND REGEXP_REPLACE(USUA_CPF,'[^[:digit:]]') =  REGEXP_REPLACE('" + strCpf + "','[^[:digit:]]') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematTecon, out strErro);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND REGEXP_REPLACE(USUA_CPF,'[^[:digit:]]') =  REGEXP_REPLACE('" + strCpf + "','[^[:digit:]]') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematImbituba, out strErro);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND REGEXP_REPLACE(USUA_CPF,'[^[:digit:]]') =  REGEXP_REPLACE('" + strCpf + "','[^[:digit:]]') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematLogistica, out strErro);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND REGEXP_REPLACE(USUA_CPF,'[^[:digit:]]') =  REGEXP_REPLACE('" + strCpf + "','[^[:digit:]]') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematConvicon, out strErro);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BuscaDadosDeAcessoLDAP
        public DataTable BuscaDadosDeAcessoLDAP(string strCPF, out string strErro)
        {
            strErro = "";

            try
            {
                string strQuery = "SELECT * FROM TELEMAT.MV_LOGIN_MASTER_SBSA WHERE SISTEMA_ID = '" + SistemaId + "' AND LPAD(REGEXP_REPLACE(USUA_CPF,'[^0-9]',''),15,'0') = LPAD(REGEXP_REPLACE('" + strCPF + "','[^0-9]',''),15,'0') AND ATIVO = '1' ORDER BY ID_ACESSO DESC";

                if (Ambiente == "Tecon")
                {
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematTecon, out strErro);
                }
                else if (Ambiente.ToString() == "Imbituba")
                {
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematImbituba, out strErro);
                }
                else if (Ambiente.ToString() == "Logistica")
                {
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematLogistica, out strErro);
                }
                else if (Ambiente.ToString() == "Convicon")
                {
                    return GetDataTableWithException(strQuery, Enums.Bancos.TelematConvicon, out strErro);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion BuscaDadosDeAcessoLDAP
    }
}