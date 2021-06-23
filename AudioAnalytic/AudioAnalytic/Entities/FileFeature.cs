using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioAnalytic.Entities
{
    public class TrainFeature
    {
        [Key]
        public int Id { get; set; }
        public string FileRaw { get; set; }
        public string FileSpec { get; set; }
        public string Feature { get; set; }
        public long Time { get; set; }
        public string RootFile { get; set; }
        [ForeignKey("RootFile")]
        public virtual PublicTrain PublicTrain { get; set; }
    }
    public class TestFeature
    {
        [Key]
        public int Id { get; set; }
        public string FileRaw { get; set; }
        public string FileSpec { get; set; }
        public string Feature { get; set; }
        public long Time { get; set; }
        public string RootFile { get; set; }
        [ForeignKey("RootFile")]
        public virtual PublicTest PublicTest { get; set; }
    }
}