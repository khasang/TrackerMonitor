using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializers
{
    abstract public class InitializationDB
    {
        public abstract void Initialization(ApplicationDbContext context);
    }
}
