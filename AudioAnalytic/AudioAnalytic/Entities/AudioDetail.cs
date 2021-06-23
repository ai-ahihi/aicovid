using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Entities
{
    public class AudioDetail
    {
        [Key]
        public string Uuid { get; set; }
        public string Gender { get; set; }
        public float Age { get; set; }
        public int Result { get; set; }
        public string FileRaw { get; set; }
        public long Time { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FileFeature> FileFeatures { get; set; }
    }
}
