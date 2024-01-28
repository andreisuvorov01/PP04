using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesPmSuvorov.DB
{
    public static class DbSingleTone
    {
        public static RemontSpravEntities Db_s = new RemontSpravEntities();
    }
}
