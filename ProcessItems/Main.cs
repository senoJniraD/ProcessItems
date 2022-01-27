using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ProcessItems
{
    public partial class Main : Form
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

        SqlConnection connection;
        private SqlDataAdapter adapter;
        SqlCommandBuilder cmdBuilder;
        DataSet ds = new DataSet();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        DataSet changes;


        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Logged In As: " + Environment.UserName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = DefPath + mfgBox.Text;

            string[] files = Directory.GetFiles(path);


            fileList.Rows.Clear();

            foreach (string file in files)
            {
                fileList.Rows.Add(file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_pies", con))
                {

                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FUNCTION", SqlDbType.VarChar).Value = "1";
                        cmd.Parameters.AddWithValue("@TABLE", SqlDbType.VarChar).Value = "NUL";
                        cmd.Parameters.AddWithValue("@COLUMN", SqlDbType.VarChar).Value = "NUL";
                        cmd.Parameters.AddWithValue("@NEW", SqlDbType.VarChar).Value = "NUL";
                        cmd.Parameters.AddWithValue("@OLD", SqlDbType.VarChar).Value = "NUL";
                        cmd.Parameters.AddWithValue("@Language", SqlDbType.VarChar).Value = Lan.Text;
                        cmd.Parameters.AddWithValue("@DESTINATION", SqlDbType.VarChar).Value = "NUL";


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


            DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;
            for (int i = 0; i <= DGV.Count - 1; i++)
            {
                textBox1.Text = "File Info:\n \r " + DGV[i].Value.ToString() + 
                    "Data Would be displayed here \n \r"+
                    "With other file info \n \r"+
                                        "Data Would be displayed here \n \r" +
                    "With other file info \n \r" +
                                        "Data Would be displayed here \n \r" +
                    "With other file info \n \r" +
                                        "Data Would be displayed here \n \r" +
                    "With other file info \n \r" +
                                        "Data Would be displayed here \n \r" +
                    "With other file info \n \r";
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string extpath;

            DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;
            for (int i = 0; i <= DGV.Count - 1; i++)
            {
                extpath = DGV[i].Value.ToString();
                ZipFile.ExtractToDirectory(extpath, Path.GetDirectoryName(extpath) + unZipFldrName);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string extpath;

            DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;

            extpath = DGV[0].Value.ToString();

            Process.Start("explorer.exe", Path.GetDirectoryName(extpath + ""));


        }

        private void button9_Click(object sender, EventArgs e)
        {
            string extpath;
            string filename;
            {
                DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;
                extpath = DGV[0].Value.ToString();
            }

            filename = Path.GetFileName(extpath);
            {
                System.IO.File.Copy(extpath, Path.GetDirectoryName(extpath) + ArchiveFldrName + filename);

                System.IO.File.Delete(extpath);
            }

            string path = DefPath + mfgBox.Text;

            string[] files = Directory.GetFiles(path);


            fileList.Rows.Clear();

            foreach (string file in files)
            {
                fileList.Rows.Add(file);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "\n\rConnection String:"
                + "\n" + ConString
                + "\n \rArchive Path:"
                + "\n" + ArchivePath
                + "\n \rDefault File Path:"
                + "\n" + DefPath
                + "\n \rUnzip Folder Name:"
                + "\n" + unZipFldrName
                + "\n \rWork Table Header:"
                + "\n" + WTName
                + "\n \rWork Table Database:"
                + "\n" + InsertDB
                + "\n \rPublishing Database:"
                + "\n" + PubDB
                + "\n \rArchive Folder Name:"
                + "\n" + ArchiveFldrName
                + "\n \rImage Query:"
                + "\n" + ImgSelList
                + "\n \rDocument Query: "
                + "\n" + DocSelList
                + "\n \rDescription Query:"
                + "\n" + DesSelList
                + "\n \rFeatures Query:"
                + "\n" + FeaSelList
                + "\n \rKeywords Query:"
                + "\n" + KeySelList
                + "\n \rVideo Query:"
                + "\n" + VidSelList
                + "\n \rApplication Version:"
                + "\n" + Properties.Settings.Default.Version,
                "User Configuration"
                );
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Form documents = new Documents();

            documents.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form Images = new Images();

            Images.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form Descriptions = new Descriptions();

            Descriptions.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form Features = new Features();

            Features.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form Keywords = new Keywords();

            Keywords.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form Video = new Video();

            Video.Show();
        }
    }
}
