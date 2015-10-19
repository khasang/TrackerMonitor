using DAL.DBInitializers;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            DBInit init = new DBInit(context);

            // Здесь добавляем созданные нами объекты, наследованные от InitializationDB, для инициализации БД
            // Пример DBInitilizers.InitUserAdmin
            init.Add(new InitUserAdmin());  // Добавил Ruslan

            init.Initialization();
        }
    }
}
