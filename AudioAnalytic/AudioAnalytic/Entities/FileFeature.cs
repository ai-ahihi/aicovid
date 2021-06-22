using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioAnalytic.Entities
{
    public class FileFeature
    {
        [Key]
        public int Id { get; set; }
        public string FileRaw { get; set; }
        public string FileSpec { get; set; }
        public string Feature { get; set; }
        public long Time { get; set; }
        public Guid RootFile { get; set; }
        [ForeignKey("RootFile")]
        public virtual AudioDetail AudioDetail { get; set; }
    }
}