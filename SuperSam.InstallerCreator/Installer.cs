using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperSam.InstallerCreator
{
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }

        private void Installer_Load(object sender, EventArgs e)
        {
            this.Text = $"Install {ConfigurationManager.AppSettings["Title"]}";
        }

        private void Installer_Load_1(object sender, EventArgs e)
        {

        }
    }
}
