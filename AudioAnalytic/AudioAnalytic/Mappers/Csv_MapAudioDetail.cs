using AudioAnalytic.Entities;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Mappers
{
    class Csv_MapAudioDetail : ClassMap<AudioDetail>
    {
        public Csv_MapAudioDetail()
        {
            Map(m => m.Uuid).Index(0);
            Map(m => m.Gender).Index(1).Default("orther");
            Map(m => m.Age).Index(2).Default(0);
            Map(m => m.Result).Index(3);
            Map(m => m.FileRaw).Index(4);
            //Map(m => m.Time).Default(0);
            //Map(m => m.Description).Default(string.Empty);
        }
    }
}
