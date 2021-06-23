using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Entities
{
    public class PublicTrain //: AudioDetail
    {
        public PublicTrain()
        {
            TrainFeatures = new List<TrainFeature>();
        }
        [Key]
        public string Uuid { get; set; }
        public string Gender { get; set; }
        public float AgeRaw { get; set; }
        public int Age
        {
            get
            {
                return Convert.ToInt16(AgeRaw);
            }
        }
        public int? Result { get; set; }
        public string FileRaw { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TrainFeature> TrainFeatures { get; set; }
    }
    public class PublicTest //: AudioDetail
    {
        public PublicTest()
        {
            TestFeatures = new List<TestFeature>();
        }
        [Key]
        public string Uuid { get; set; }
        public string Gender { get; set; }
        public float AgeRaw { get; set; }
        public int Age
        {
            get
            {
                return Convert.ToInt16(AgeRaw);
            }
        }
        public int? Result { get; set; }
        public string FileRaw { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TestFeature> TestFeatures { get; set; }
    }
}
