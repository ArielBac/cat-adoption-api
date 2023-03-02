using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAdoptionApiXUnitTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        { }

        public void CatSeed(CatAdoptionContext context)
        {
            context.Cats.Add(new Cat { Name = "Pingo", Breed = "Viralata", Weight = 2.5, Age = 2, Color = "Amarelo", Gender = "M" });
            context.Cats.Add(new Cat { Name = "Jonas", Breed = "Viralata", Weight = 2.1, Age = 1, Color = "Preto", Gender = "M" });
            context.Cats.Add(new Cat { Name = "Joana", Breed = "Viralata", Weight = 3.5, Age = 3, Color = "Branco", Gender = "F" });
            context.Cats.Add(new Cat { Name = "Maria", Breed = "Viralata", Weight = 2.4, Age = 5, Color = "Mesclado", Gender = "F" });
            context.Cats.Add(new Cat { Name = "Gabi", Breed = "Viralata", Weight = 4.5, Age = 9, Color = "Marrom", Gender = "F" });

            context.SaveChanges();
        }

        public void VaccineSeed(CatAdoptionContext context)
        {
            context.Vaccines.Add(new Vaccine { Name = "v5", Producer = "Fabricante 1", Applied_at = DateTime.Parse("2020-05-12T15:30:00"), CatId = 1 });
            context.Vaccines.Add(new Vaccine { Name = "v5", Producer = "Fabricante 1", Applied_at = DateTime.Parse("2022-04-12T09:48:00"), CatId = 2 });
            context.Vaccines.Add(new Vaccine { Name = "v5", Producer = "Fabricante 1", Applied_at = DateTime.Parse("2022-04-12T08:23:00"), CatId = 5 });
            context.Vaccines.Add(new Vaccine { Name = "v4", Producer = "Fabricante 3", Applied_at = DateTime.Parse("2020-04-12T14:20:00"), CatId = 3 });
            context.Vaccines.Add(new Vaccine { Name = "v3", Producer = "Fabricante 2", Applied_at = DateTime.Parse("2021-06-10T07:55:00"), CatId = 4 });

            context.SaveChanges();
        }
    }
}
