﻿using System;
using System.Configuration;
using System.IO;
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
            try
            {
                string applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string[] fileEntries = Directory.GetFiles(applicationPath);
                foreach (var fileFullPath in fileEntries)
                {
                    if (fileFullPath.Contains("CTI.SalesUpload.Console.exe.config"))
                    {
                        string text = File.ReadAllText(fileFullPath);
                        text = text.Replace("{{ClientId}}", txtClientId.Text);
                        text = text.Replace("{{ClientSecret}}", txtClientSecret.Text);
                        text = text.Replace("{{IntegrationId}}", txtIntegrationId.Text);
                        text = text.Replace("{{FilePath}}", txtFilePath.Text);
                        File.WriteAllText(fileFullPath, text);
                    }
                }
                MessageBox.Show("Installation Complete", ConfigurationManager.AppSettings["Title"], MessageBoxButtons.OK);
                Application.Exit();
                this.Close();
            }
            catch
            {
                MessageBox.Show("An error has occured, please report to system administrator.", ConfigurationManager.AppSettings["Title"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
