using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pier.Web.Acesso.Bll
{
    public class Usuario : Pier.Base.Bll.BaseAbstractBll<Entity.Acesso.Usuario>,IDisposable
    {

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}