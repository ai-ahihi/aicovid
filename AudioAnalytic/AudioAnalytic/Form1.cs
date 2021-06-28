using AudioAnalytic.Entities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // var time = Convert.ToInt32(txtTime.Text);
            var outFolder = txtOut.Text;
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }
            List<PublicTrain> data;
            using (var db = new DataContext())
            {
                data = db.PublicTrains.Where(e => e.Time > 4).ToList();

                Parallel.ForEach(data, (item) =>
                {
                    using (var reader = new AudioFileReader(item.FileRaw))
                    {
                        var Total = item.Time;
                        double timeStart = 0;
                        double timeEnd = 4;
                        var subs = new List<TrainFeature>();
                        while (true)
                        {
                            var fileName = $"{item.Result}_{item.Age}_{item.Gender}_{Guid.NewGuid()}";
                            var sub = new TrainFeature()
                            {
                                FileRaw = Path.Combine(outFolder, fileName) + ".wav", //item.FileRaw,
                                Time = 4,
                                RootFile = item.Uuid,
                            };

                            reader.CurrentTime = TimeSpan.FromSeconds(timeStart); // jump forward to the position we want to start from
                            WaveFileWriter.CreateWaveFile16(sub.FileRaw, reader.Take(TimeSpan.FromSeconds(4)));

                            item.TrainFeatures.Add(sub);

                            if (timeEnd == Total)
                            {
                                break;
                            }
                            timeStart = timeEnd - 1;
                            timeEnd += 4;
                            if (timeEnd > Total)
                            {
                                timeStart = Total - 4;
                                timeEnd = timeStart + 4;
                            }
                        }
                    }
                });
                db.SaveChanges();

                //// valid
                //var files = db.TrainFeatures.Select(s => s.FileRaw).ToList();

                //Parallel.ForEach(files, (f) =>
                //{
                //    using (var reader = new AudioFileReader(f))
                //    {
                //        if (reader.TotalTime > TimeSpan.FromSeconds(4))
                //    }
                //});

            }


         //   this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var outFolder = txtOutTest.Text;
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }
            List<PublicTest> data;
            using (var db = new DataContext())
            {
                data = db.PublicTests.Where(e => e.Time > 4).ToList();

                Parallel.ForEach(data, (item) =>
                {
                    using (var reader = new AudioFileReader(item.FileRaw))
                    {
                        var Total = item.Time;
                        double timeStart = 0;
                        double timeEnd = 4;
                        var subs = new List<TestFeature>();
                        while (true)
                        {
                            var fileName = $"{item.Result}_{item.Age}_{item.Gender}_{Guid.NewGuid()}";
                            var sub = new TestFeature()
                            {
                                FileRaw = Path.Combine(outFolder, fileName) + ".wav", //item.FileRaw,
                                Time = 4,
                                RootFile = item.Uuid,
                            };

                            reader.CurrentTime = TimeSpan.FromSeconds(timeStart); // jump forward to the position we want to start from
                            WaveFileWriter.CreateWaveFile16(sub.FileRaw, reader.Take(TimeSpan.FromSeconds(4)));

                            item.TestFeatures.Add(sub);

                            if (timeEnd == Total)
                            {
                                break;
                            }
                            timeStart = timeEnd - 1;
                            timeEnd += 4;
                            if (timeEnd > Total)
                            {
                                timeStart = Total - 4;
                                timeEnd = timeStart + 4;
                            }
                        }
                    }
                });
                db.SaveChanges();

                //// valid
                //var files = db.TrainFeatures.Select(s => s.FileRaw).ToList();

                //Parallel.ForEach(files, (f) =>
                //{
                //    using (var reader = new AudioFileReader(f))
                //    {
                //        if (reader.TotalTime > TimeSpan.FromSeconds(4))
                //    }
                //});

            }

        }
    }
}
