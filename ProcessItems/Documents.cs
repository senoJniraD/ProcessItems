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

            updateCmd.Parameters.Add("@rev", SqlDbType.Int, 1, "RevisionNumber");
            updateCmd.Parameters.Add("@sID", SqlDbType.Int, 6, "SalesOrderID");

            da.UpdateCommand = updateCmd;
            da.Update(dt);
            sqlConn.Close();
            sqlConn.Dispose();
        }
    }
}
