using AudioAnalytic.Entities;
using AudioAnalytic.Mappers;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic
{
    public class AnalyticData
    {
        public List<AudioDetail> AudioDetails { get; }
        public AnalyticData(string fileData, string folderdir = @"F:\Covid\aicovid\AudioAnalytic\AudioAnalytic\datas")
        {
            string pathFile = Path.Combine(folderdir, fileData);
            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                
            };
            using (var reader = new StreamReader(pathFile))
            using (var csv = new CsvReader(reader, conf))
            {
                csv.Context.RegisterClassMap<Csv_MapAudioDetail>();
                var records = csv.GetRecords<AudioDetail>().ToList();
                AudioDetails = records.Select(s => new AudioDetail()
                {
                    Uuid = s.Uuid,
                    Age = s.Age,
                    Gender = s.Gender,
                    FileRaw = Path.Combine(folderdir, "files", s.FileRaw),
                    Result = s.Result,
                    Description = s.FileRaw
                }).ToList();
            }


        }
    }
}
