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
    }
}
