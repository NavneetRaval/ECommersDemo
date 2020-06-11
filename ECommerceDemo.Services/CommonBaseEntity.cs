using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDemo.Services
{
    public abstract  class CommonBaseEntity
    {
        public SqlConnection con;

        public void connection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["productManagementContext"].ConnectionString;
            con = new SqlConnection(connStr);
        }
    }
}
