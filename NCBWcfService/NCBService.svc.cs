using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NCBWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NCBService : INCBService
    {
        public string Message()
        {
            return "Hello World, this is my Second WCF web service. ";
        }

        public bool Verify(string name, string cardNum)
        {
            getCardData g = new getCardData();
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Database=NCB1Context-46f8183e-abbc-4afc-bcd5-61a7874dc080;Trusted_Connection=True;MultipleActiveResultSets=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Name,CardNum from Customer where CardNum ='" + cardNum + "' and First ='" + name + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            //g.ScotiaCustomer = dt;
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool VerifyFunds(float amount, string cardNum)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Database=NCB1Context-46f8183e-abbc-4afc-bcd5-61a7874dc080;Trusted_Connection=True;MultipleActiveResultSets=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Balance from Customer where CardNum='" + cardNum + "'", con);
            SqlDataReader rdr = cmd.ExecuteReader();

            //DataTable table = new DataTable();
            //table.Columns.Add("Available");
           // double ava = 0.0;
            double bal = 0.0;
            while (rdr.Read())
            {

                bal = Convert.ToDouble(rdr["Balance"]);
            }



            if (bal > amount)
            {
                return true;
            }
            return false;
        }

        public double CusFunds(string cardNum, double amnt)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Database=NCB1Context-46f8183e-abbc-4afc-bcd5-61a7874dc080;Trusted_Connection=True;MultipleActiveResultSets=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Customer set  Balance=Balance-" + amnt + " where CardNum='" + cardNum + "'", con);
            SqlDataReader rdr = cmd.ExecuteReader();

            //DataTable table = new DataTable();
            //table.Columns.Add("Available");
            //double ava = 0.0;
            double bal = 0.0;
            while (rdr.Read())
            {

                bal = Convert.ToDouble(rdr["Balance"]);
            }

            return bal;

        }
    }
}
