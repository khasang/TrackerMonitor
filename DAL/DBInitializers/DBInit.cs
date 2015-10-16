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
        List<IInitialization> list = new List<IInitialization>();

        public void Add(IInitialization initObject)
        {
            list.Add(initObject);
        }

        public void Initialization()
        {
            foreach (var init in list)
                init.Initialization();
        }
    }
}
