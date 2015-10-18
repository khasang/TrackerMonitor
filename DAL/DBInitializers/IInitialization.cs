using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    public interface IInitialization
    {
        /// <summary>
        /// Метод, который реализует логику инициализации БД
        /// </summary>
        void Initialization(ApplicationDbContext context);
    }
}
