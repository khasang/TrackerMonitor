using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    public class DBInit
    {
        List<IInitialization> methods = new List<IInitialization>();
        ApplicationDbContext context;

        public DBInit(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(IInitialization initObject)
        {
            methods.Add(initObject);
        }

        public void Initialization()
        {
            foreach (var method in methods)
                method.Initialization(context);
        }
    }
}
