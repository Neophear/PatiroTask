using PatiroTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatiroTask.Data
{
    public interface IClinicData
    {
        IEnumerable<Clinic> GetClinics();
        Clinic GetClinic(int id);
        void UpdateClinic(int id, Clinic clinic);
    }

    public class ClinicData : IClinicData
    {
        private List<Clinic> clinics;

        public ClinicData()
        {
            clinics = new List<Clinic>
            {
                new Clinic
                {
                    Id = 1,
                    Name = "Clinic 1",
                    Description = "Description of Clinic 1",
                    IsActive = true,
                    City = "Aalborg",
                    ZipCode = "9000",
                    Members = new List<string>(),
                    CreatedAt = new DateTimeOffset(new DateTime(2000, 12, 10)),
                    CreatedBy = "clinic1creator@gmail.com"
                },
                new Clinic
                {
                    Id = 2,
                    Name = "Clinic 2",
                    Description = "Description of Clinic 2",
                    IsActive = false,
                    City = "Aalborg",
                    ZipCode = "9000",
                    Members = new List<string> {
                        "clinic2member@gmail.com"
                    },
                    CreatedAt = new DateTimeOffset(new DateTime(2001, 4, 12)),
                    CreatedBy = "clinic2creator@gmail.com"
                },
                new Clinic
                {
                    Id = 3,
                    Name = "Clinic 3",
                    Description = "Description of Clinic 3",
                    IsActive = true,
                    City = "Dronninglund",
                    ZipCode = "9330",
                    Members = new List<string>(),
                    CreatedAt = new DateTimeOffset(new DateTime(2009, 9, 27)),
                    CreatedBy = "clinic3creator@gmail.com"
                },
                new Clinic
                {
                    Id = 4,
                    Name = "Clinic 4",
                    Description = "Description of Clinic 4",
                    IsActive = true,
                    City = "Hjallerup",
                    ZipCode = "9320",
                    Members = new List<string>(),
                    CreatedAt = new DateTimeOffset(new DateTime(2018, 1, 2)),
                    CreatedBy = "clinic4creator@gmail.com"
                }
            };
        }

        public IEnumerable<Clinic> GetClinics() => clinics;

        public Clinic GetClinic(int id) => clinics.FirstOrDefault(c => c.Id == id);

        public void UpdateClinic(int id, Clinic clinic)
        {
            int index = clinics.FindIndex(c => c.Id == id);

            if (index >= 0)
            {
                clinic.Id = id; //Make sure that the updated clinic has the correct id
                clinics.RemoveAt(index); //Remove outdated clinic
                clinics.Insert(index, clinic); //Insert updated clinic at same index
            }
        }
    }
}
