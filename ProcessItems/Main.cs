using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace ProcessItems
{
    public partial class Main : Form
    {

        string ConString = Properties.Settings.Default.ConString;
        string Archive = Properties.Settings.Default.ArchivePath;
        string ArchFldr = Properties.Settings.Default.ArchiveFldrName;
        string DefPath = Properties.Settings.Default.DefPath;
        string ZipDir = Properties.Settings.Default.unZipFldrName;
        string DataDB = Properties.Settings.Default.InsertDB;
        string WTHeader = Properties.Settings.Default.WTName;


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
            DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;
            for (int i = 0; i <= DGV.Count - 1; i++)
            {
                textBox1.Text = DGV[i].Value.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string extpath;

            DataGridViewSelectedCellCollection DGV = this.fileList.SelectedCells;
            for (int i = 0; i <= DGV.Count - 1; i++)
            {
                extpath = DGV[i].Value.ToString();
                ZipFile.ExtractToDirectory(extpath, Path.GetDirectoryName(extpath) + ZipDir);
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
                System.IO.File.Copy(extpath, Path.GetDirectoryName(extpath) + ArchFldr + filename);

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
                + "\n" + Properties.Settings.Default.ConString
                + "\n \rArchive Path:"
                + "\n" + Properties.Settings.Default.ArchivePath
                + "\n \rDefault File Path:"
                + "\n" + Properties.Settings.Default.DefPath
                + "\n \rUnzip Folder Name:"
                + "\n" + Properties.Settings.Default.unZipFldrName
                + "\n \rWork Table Header:"
                + "\n" + Properties.Settings.Default.WTName
                + "\n \rWork Table Database:"
                + "\n" + Properties.Settings.Default.InsertDB
                + "\n \rPublishing Database:"
                + "\n" + Properties.Settings.Default.PubDB
                + "\n \rArchive Folder Name:"
                + "\n" + Properties.Settings.Default.ArchiveFldrName
                + "\n \rImage Query:"
                + "\n" + Properties.Settings.Default.ImgSelList
                + "\n \rDocument Query: "
                + "\n" + Properties.Settings.Default.DocSelList
                + "\n \rDescription Query:"
                + "\n" + Properties.Settings.Default.DesSelList
                + "\n \rFeatures Query:"
                + "\n" + Properties.Settings.Default.FeaSelList
                + "\n \rKeywords Query:"
                + "\n" + Properties.Settings.Default.KeySelList
                + "\n \rVideo query:"
                + "\n" + Properties.Settings.Default.VidSelList
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
