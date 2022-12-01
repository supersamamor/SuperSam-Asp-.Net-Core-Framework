using System;
using System.Configuration;
using System.Windows.Forms;

namespace SuperSam.InstallerCreator
{
    public partial class Installer : Form
    {

        public Installer()
        {
            InitializeComponent();
        }

        private void Installer_Load_1(object sender, EventArgs e)
        {
            string applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.Text = $"Install {ConfigurationManager.AppSettings["Title"]}";
            txtClientId.Text = ConfigurationManager.AppSettings["DefaultClientId"];
            txtClientSecret.Text = ConfigurationManager.AppSettings["DefaultClientSecret"];
            txtIntegrationId.Text = ConfigurationManager.AppSettings["DefaultIntegrationId"];
            txtFilePath.Text = ConfigurationManager.AppSettings["DefaultFilePath"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
        }
    }
}
