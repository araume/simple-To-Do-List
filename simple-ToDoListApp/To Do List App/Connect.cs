using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace To_Do_List_App
{
    public class Connect
    {
        public DataTable DSRec = new DataTable();
        public SqlDataAdapter DArec = new SqlDataAdapter();
        private int rowAffected;
        private SqlConnection connection;
        private string query;

        public DataTable GetDataTable(string sqlquery)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Database=CNTest";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand com = new SqlCommand(sqlquery, connection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                ds.Clear();
                da.Fill(ds, "data");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds.Tables[0];
        }
        public void executeSQL(string query)
        {
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Database=CNTest";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand com = new SqlCommand(query, connection);
                connection.Open();
                rowAffected = com.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
