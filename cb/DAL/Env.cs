using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace cb.Dal
{
    public static class Env
    {
        //public static string ConnectionString = ConfigurationManager.ConnectionStrings["CbConnectionString"].ConnectionString;
        //public static string ConnectionString = "Data Source=WIN-Server-R2-64Bit.fhict.local;Initial Catalog=cb;User ID=sjoerd;Password=Fiets123";
        public static string ConnectionString = "Data Source=SJOERD-LPT\\SQLEXPRESS;Initial Catalog=cb;Integrated Security=True;";
    }
}