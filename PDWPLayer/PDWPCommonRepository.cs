using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PDWPLayer
{
    public class PDWPCommonRepository
    {
        private PDWP_WorkingPaper_DBEntities db = new PDWP_WorkingPaper_DBEntities();
        public IEnumerable<DateTime?> GetCalendarDates()
        {
            var array = db.Agendas.Select(x => x.Date).ToList();
            return array;
        }
        
    }
}
