using AudioAnalytic.Entities;
using System;

namespace AudioAnalytic
{
    class Program
    {
        static void Main(string[] args)
        {
            var uuid = new Guid("3284bcf1-2446-4f3a-ac66-14c76b294177");
            using(var db = new DataContext())
            {
                db.Database.EnsureCreated();
                db.AudioDetails.Add(new AudioDetail() {
                Uuid = new Guid("3284bcf1-2446-4f3a-ac66-14c76b294177"),
                Result =1
                
                });
                db.SaveChanges();
            }
            Console.WriteLine(uuid.ToString());
        }
    }
}
