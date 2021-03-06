using AudioAnalytic.Entities;
using AudioAnalytic.Mappers;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using NAudio.Wave;
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
        //  public List<AudioDetail> _audioDetails { get; }
        public List<PublicTrain> PublicTrains { get; }
        public List<PublicTest> PublicTests { get; }
        public AnalyticData(string folderdir = @"F:\Covid\aicovid\AudioAnalytic\AudioAnalytic\datas")
        {

            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            // train
            string pathFileTrain = Path.Combine(folderdir, "public_train/metadata_train_challenge.csv");
            using (var reader = new StreamReader(pathFileTrain))
            using (var csv = new CsvReader(reader, conf))
            {
                csv.Context.RegisterClassMap<Csv_MapAudioDetail>();
                var _audioDetails = csv.GetRecords<AudioDetail>().ToList();

                PublicTrains = new List<PublicTrain>();
                Parallel.ForEach(_audioDetails, (s) =>
                {
                    var item = new PublicTrain
                    {
                        AgeRaw = s.AgeRaw,
                        Description = s.Description,
                        FileRaw = Path.Combine(folderdir, @"public_train\files", s.FileRaw),
                        Gender = s.Gender,
                        Result = s.Result,
                        Uuid = s.Uuid
                    };
                    item.Time =(new AudioFileReader(item.FileRaw).TotalTime.TotalSeconds);

                    PublicTrains.Add(item);
                });
            }
            // test
            string pathFileTest = Path.Combine(folderdir, @"public_test\metadata_public_test.csv");
            using (var reader = new StreamReader(pathFileTest))
            using (var csv = new CsvReader(reader, conf))
            {
                csv.Context.RegisterClassMap<Csv_MapTestDetail>();
                var _audioDetails = csv.GetRecords<PublicTest>().ToList();

                //PublicTests = _audioDetails.Select(s => new PublicTest
                //{
                //    AgeRaw = s.AgeRaw,
                //    Description = s.Description,
                //    FileRaw = Path.Combine(folderdir, "public_test/files", s.FileRaw),
                //    Gender = s.Gender,
                //    Result = s.Result,
                //    Uuid = s.Uuid,
                //    Time = 0
                //}).ToList();
                PublicTests = new List<PublicTest>();
                Parallel.ForEach(_audioDetails, (s) =>
                {
                    var item = new PublicTest
                    {
                        AgeRaw = s.AgeRaw,
                        Description = s.Description,
                        FileRaw = Path.Combine(folderdir, @"public_test\files", s.FileRaw),
                        Gender = s.Gender,
                        Result = 0, // fix test = 0
                        Uuid = s.Uuid
                    };
                    item.Time = (new AudioFileReader(item.FileRaw).TotalTime.TotalSeconds);

                    PublicTests.Add(item);
                });
            }
        }
    }
}
