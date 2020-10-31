using System;
using System.Diagnostics;

namespace ProjectNamePlaceHolder
{
    partial class ProjectNamePlaceHolderService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {         
            this.ServiceName = "ProjectNamePlaceHolderService";
            ServiceProcess();
        }
        #endregion

        private void ServiceProcess()
        {            
            try
            {
                //Change the logic here
                string folderName = @"c:\Test Service Folder";
                System.IO.Directory.CreateDirectory(folderName);
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(ex?.Message?.ToString() + "/" + ex?.StackTrace, EventLogEntryType.Error);
                }
            }
        }
    }
}
