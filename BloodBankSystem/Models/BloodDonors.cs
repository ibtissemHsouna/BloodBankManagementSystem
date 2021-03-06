using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace BloodBankSystem.Models
{
    public class BloodDonors
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        string query;
        public BloodDonors()
        {
            conn = new SqlConnection("Data Source=desktop-9k4jrdu;Initial Catalog=BBMMSS;Integrated Security=True;");
        }
        public ArrayList GetAllDonor()
        {
            ArrayList blooddonors = new ArrayList();
            

            try
            {
                conn.Open();
                query = "Select * from BloodDonors";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    BloodDonor a = new BloodDonor();
                    a.Name = reader.GetString(reader.GetOrdinal("Name"));
                    a.Username = reader.GetString(reader.GetOrdinal("Username"));
                    a.Blood_Group = reader.GetString(reader.GetOrdinal("Blood_Group"));
                    a.Last_Donate_Date = reader.GetDateTime(reader.GetOrdinal("Last_Donate_Date"));
                    a.Donate_Serial = reader.GetInt32(reader.GetOrdinal("Donate_Serial"));
                   


                    blooddonors.Add(a);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert");
                conn.Close();

            }
            return blooddonors;
        }
        public ArrayList GetDonor()
        {
            ArrayList a = new ArrayList();


            try
            {
                conn.Open();
                query = "SELECT count(Blood_Group) as Total,Blood_Group from BloodDonors Group by Blood_Group";

                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    a.Add(reader.GetString(reader.GetOrdinal("Blood_Group")));

                    a.Add(reader.GetInt32(reader.GetOrdinal("Total")));



                  
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert");
                conn.Close();

            }
            return a;
        }
        public ArrayList GetAllDonateHistory(string username)
        {
            ArrayList blooddonors = new ArrayList();


            try
            {
                conn.Open();
                query = "Select * from BloodDonors where Username ='" + username + "'";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    BloodDonor a = new BloodDonor();
                    a.Name = reader.GetString(reader.GetOrdinal("Name"));
                    a.Username = reader.GetString(reader.GetOrdinal("Username"));
                    a.Blood_Group = reader.GetString(reader.GetOrdinal("Blood_Group"));
                    a.Last_Donate_Date = reader.GetDateTime(reader.GetOrdinal("Last_Donate_Date"));
                    a.Donate_Serial = reader.GetInt32(reader.GetOrdinal("Donate_Serial"));



                    blooddonors.Add(a);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Alert");
                conn.Close();

            }
            return blooddonors;
        }
        public dynamic GetLastDonateDate(string username)
        {
            //DateTime dt = DateTime.Now;
            //string lastdonatdate = dt.ToString("dd/MM/yyyy");

            DateTime lastdonatdate = DateTime.Parse("12/12/2015 12:00:00 AM");
          
            try
            {
                conn.Open();
                query = "select max(Last_Donate_Date) as Date from BloodDonors where Username='" + username + "'";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    lastdonatdate = reader.GetDateTime(reader.GetOrdinal("Date"));

                }
                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
                return lastdonatdate;
               
            }
            return lastdonatdate;
        }
        public void InsertDonateInfo(Member member)
        {
            var dateTime = DateTime.Now;
            string formatted = dateTime.ToString(
                "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            try
            {


                conn.Open();
                query = "INSERT INTO BloodDonors (Username,Name,Blood_Group,Last_Donate_Date) VALUES('" + member.Username + "','" + member.Name + "','" + member.Blood_Group + "','" + formatted + "')";
                cmd = new SqlCommand(query, conn);
                
               cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "Alert");
                conn.Close();
            }
          
        }

    }
}
