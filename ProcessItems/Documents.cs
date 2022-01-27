using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessItems
{
    public partial class Documents : Form
    {

        string ConString = Properties.Settings.Default.ConString;
        string ArchivePath = Properties.Settings.Default.ArchivePath;
        string DefPath = Properties.Settings.Default.DefPath;
        string unZipFldrName = Properties.Settings.Default.unZipFldrName;
        string WTName = Properties.Settings.Default.WTName;
        string InsertDB = Properties.Settings.Default.InsertDB;
        string PubDB = Properties.Settings.Default.PubDB;
        string ArchiveFldrName = Properties.Settings.Default.ArchiveFldrName;
        string ImgSelList = Properties.Settings.Default.ImgSelList;
        string DocSelList = Properties.Settings.Default.DocSelList;
        string DesSelList = Properties.Settings.Default.DesSelList;
        string FeaSelList = Properties.Settings.Default.FeaSelList;
        string KeySelList = Properties.Settings.Default.KeySelList;
        string VidSelList = Properties.Settings.Default.VidSelList;

        DataTable dt = new DataTable();

        public Documents()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            SqlConnection sqlConn = new SqlConnection(ConString);
            string query = "SELECT * FROM Sales.SalesOrderHeader"; //will be doc select list
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);          
            dt.Clear();
            da.Fill(dt);
            sqlConn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = false;

            label1.Text = "Rows: "+dt.Rows.Count.ToString();

            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConString);
            sqlConn.Open();
            SqlCommand updateCmd = new SqlCommand(

                "UPDATE [Test_2].[Sales].[SalesOrderHeader]" +
                " SET [revisionnumber] = @rev -1" +
                " where [SalesOrderID] = @sID", sqlConn);

            SqlDataAdapter da = new SqlDataAdapter(updateCmd);
            updateCmd.Parameters.Add("@rev", SqlDbType.Int, 6, "RevisionNumber");
            updateCmd.Parameters.Add("@sID", SqlDbType.Int, 6, "SalesOrderID");
            da.UpdateCommand = updateCmd;
            da.Update(dt);
            dataGridView1.DataSource = null;
            sqlConn.Close();
            sqlConn.Dispose();



            SqlConnection sqlConn2 = new SqlConnection(ConString);
            string query = "SELECT * FROM Sales.SalesOrderHeader where RevisionNumber = 66 or RevisionNumber = 122"; //will be doc select list
            SqlCommand cmd = new SqlCommand(query, sqlConn2);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
            sqlConn2.Open();
            dt.Clear();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            label1.Text = "Rows: " + dt.Rows.Count.ToString();
            sqlConn2.Close();
            sqlConn2.Dispose();

            if (label1.Text == "Rows: 0")
            {
                MessageBox.Show("There are no errors to process. You may now publish! \n \r WorkTable has now ben locked for review only!","Verification Complete!");

                SqlConnection sqlConn3 = new SqlConnection(ConString);
                string query1 = "SELECT * FROM Sales.SalesOrderHeader"; //will be doc select list
                SqlCommand cmd1 = new SqlCommand(query1, sqlConn3);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd1);
                sqlConn3.Open();
                dt.Clear();
                da3.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.ReadOnly = true;
                label1.Text = "Rows: " + dt.Rows.Count.ToString();

                sqlConn3.Close();
                sqlConn3.Dispose();

            }

            
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("DBO.AIO_TEST", con))
                {

                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@FUNCTION", SqlDbType.VarChar).Value = "1";
                        //cmd.Parameters.AddWithValue("@TABLE", SqlDbType.VarChar).Value = "NUL";
                        //cmd.Parameters.AddWithValue("@COLUMN", SqlDbType.VarChar).Value = "NUL";
                        //cmd.Parameters.AddWithValue("@NEW", SqlDbType.VarChar).Value = "NUL";
                        //cmd.Parameters.AddWithValue("@OLD", SqlDbType.VarChar).Value = "NUL";
                        //cmd.Parameters.AddWithValue("@LANGUAGE", SqlDbType.VarChar).Value = Lan.Text;
                        //cmd.Parameters.AddWithValue("@DESTINATION", SqlDbType.VarChar).Value = "NUL";


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    

                }
            }



            
        }
    }
}
