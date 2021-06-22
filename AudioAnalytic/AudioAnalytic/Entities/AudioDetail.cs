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
        public Guid Uuid { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public int Result { get; set; }
        public string FileRaw { get; set; }
        public long Time { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FileFeature> FileFeatures { get; set; }
    }
}
