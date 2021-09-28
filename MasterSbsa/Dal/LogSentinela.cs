using System;

namespace MasterSbsa.Dal
{
    public class LogSentinela : ECRUD.Oracle
	{
		#region Properties
		public string Ambiente { get; private set; }
		public string Usuario { get; private set; }
		#endregion

		#region Construtor
		public LogSentinela(string Usuario)
		{
			Ambiente = System.Configuration.ConfigurationManager.AppSettings["Ambiente"].ToString();
			this.Usuario = Usuario;
		}
		#endregion

		#region CriaLogSentinela
		public Boolean CriaLogSentinela(String strTela, String strAcao, String strUsuario)
		{
			try
			{
				if (Ambiente == "Tecon")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), 10191)";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematTecon, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Imbituba")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), 10193)";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematImbituba, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Logistica")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), 10192)";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematLogistica, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Convicon")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), 10194)";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematConvicon, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region CriaLogSentinelaErro
		public Boolean CriaLogSentinelaErro(String strTela, String strAcao, String strUsuario, String strSistId, String strErro, String strMetodo, String strInstrucao)
		{
			try
			{
				if (Ambiente == "Tecon")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID, LOSE_ERRO, LOSE_METODO, LOSE_INSTRUCAO) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), '" + strSistId.Replace("'", "''") + "', '" + strErro.Replace("'", "''") + "', '" + strMetodo.Replace("'", "''") + "', '" + strInstrucao.Replace("'", "''") + "')";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematTecon, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Imbituba")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID, LOSE_ERRO, LOSE_METODO, LOSE_INSTRUCAO) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), '" + strSistId.Replace("'", "''") + "', '" + strErro.Replace("'", "''") + "', '" + strMetodo.Replace("'", "''") + "', '" + strInstrucao.Replace("'", "''") + "')";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematImbituba, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Logistica")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID, LOSE_ERRO, LOSE_METODO, LOSE_INSTRUCAO) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), '" + strSistId.Replace("'", "''") + "', '" + strErro.Replace("'", "''") + "', '" + strMetodo.Replace("'", "''") + "', '" + strInstrucao.Replace("'", "''") + "')";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematLogistica, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else if (Ambiente == "Convicon")
				{
					String strQuery = "INSERT INTO ESCALA.LOG_SENTINELA (LOSE_ID, LOSE_TELA, LOSE_ACAO, LOSE_USUARIO, LOSE_DATA, LOSE_PIER_SIST_ID, LOSE_ERRO, LOSE_METODO, LOSE_INSTRUCAO) VALUES ((SELECT NVL(MAX(LOSE_ID), 0) + 1 FROM ESCALA.LOG_SENTINELA), '" + strTela.ToUpper().Replace("'", "''") + "', '" + strAcao.ToUpper().Replace("'", "''") + "', '" + strUsuario.ToUpper().Replace("'", "''") + "', TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'), '" + strSistId.Replace("'", "''") + "', '" + strErro.Replace("'", "''") + "', '" + strMetodo.Replace("'", "''") + "', '" + strInstrucao.Replace("'", "''") + "')";
					return InsertUpdateDelete(strQuery, Enums.Bancos.TelematConvicon, Usuario != "" ? Usuario : "AUTOMATICO");
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}