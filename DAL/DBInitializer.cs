using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        private Random rnd = new Random();

        protected override void Seed(ApplicationDbContext context)
        {
            // Здесь инициализируем БД
        }
    }
}
