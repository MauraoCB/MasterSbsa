using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pier.Web.Acesso.Bll
{
    public class Ambiente 
    {
        /// <summary>
        /// Método para listar todos o Ambientes Disponíveis no momento, seu uso utiliza da classe estática DaoUtil. E tenta inicializa-las caso estejam inativas.
        /// </summary>
        /// <returns>Lista de Ambientes.</returns>
        public List<Pier.Entity.Ambiente> AmbientesDisponiveis()
        {

            //USUARIO
            //string sSource;
            //string sLog;
            //string sEvent;

            //sSource = "Application";
            //sLog = "Application";
            //sEvent = "Erro na inicialização no ambiente " + ServiceSecurityContext.Current.WindowsIdentity.Name + " ";

            //if (!EventLog.SourceExists(sSource))
            //    EventLog.CreateEventSource(sSource, sLog);

            //EventLog.WriteEntry(sSource, sEvent);
            //System.Diagnostics.EventLog.WriteEntry(sSource, sEvent, System.Diagnostics.EventLogEntryType.Warning);

            //throw new Exception("Ambiente " + ambiente.ToString().ToLower().Replace(" ", "") + " não disponível.\r\nCausa: " + ex.Message, ex); 


            List<Pier.Entity.Ambiente> ambientes = new List<Pier.Entity.Ambiente>();
            if (Pier.Base.Dao.DaoUtil.FabricasSessoes != null)
            {
                foreach (var item in Pier.Base.Dao.DaoUtil.FabricasSessoes)
                {
                    ambientes.Add(item.Key.Ambiente);
                }
            }
            return ambientes;
        }

    }
}