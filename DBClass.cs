using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace lab3
{
    class DBClass
    {
        SqlConnection con;
        public void Connect()
        {
            string ConString = "Data Source=WANNASLEEPPC;Initial Catalog=vb;Integrated Security=True";

            con = new SqlConnection(ConString);
            con.Open();
        }
        public Boolean Disconnect()
        {
            con.Close();
            return true;
        }

        public SqlCommand Command(string sql)
        {
            var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            return cmd;
        }
    }
}
