using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Entities
{
    public class ImportToSQL
    {
        public static void Init()
        {
            AnalyticData pt = new AnalyticData();

            using (var db = new DataContext())
            {
                if (db.PublicTrains.Count() == 0)
                {
                    db.PublicTrains.AddRange(pt.PublicTrains);
                    db.PublicTests.AddRange(pt.PublicTests);
                    db.SaveChanges();
                }
            }
        }
    }
}
