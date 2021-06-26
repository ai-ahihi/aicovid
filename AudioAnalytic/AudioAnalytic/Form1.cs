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

                foreach (var item in data)
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
                            WaveFileWriter.CreateWaveFile16(sub.FileRaw, reader.Take(TimeSpan.FromSeconds(Math.Abs(timeEnd - timeStart))));

                            item.TrainFeatures.Add(sub);

                            if (timeEnd == Total)
                            {
                                break;
                            }
                            timeStart = timeEnd - 1;
                            timeEnd = timeEnd + 4;
                            if (timeEnd > Total)
                            {
                                timeStart = Total - 4;
                                timeEnd = Total;
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }

            this.Close();
        }
    }
}
