using System;

namespace MasterSbsa.Bll
{
    public class LogSentinela : Dal.LogSentinela, IDisposable
	{
		#region Construtor
		public LogSentinela(string Usuario) : base(Usuario)
		{

		}
		#endregion

		#region Dispose
		public new void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		#endregion

		#region CriaLogSentinela
		public new Boolean CriaLogSentinela(String strTela, String strAcao, String strUsuario)
		{
			try
			{
				return base.CriaLogSentinela(strTela, strAcao, strUsuario);
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region CriaLogSentinelaErro
		public new Boolean CriaLogSentinelaErro(String strTela, String strAcao, String strUsuario, String strSistId, String strErro, String strMetodo, String strInstrucao)
		{
			try
			{
				return base.CriaLogSentinelaErro(strTela, strAcao, strUsuario, strSistId, strErro, strMetodo, strInstrucao);
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}