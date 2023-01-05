using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Console.Services
{
    public static class ProjectFolder
    {
        public static IList<string> GetProjectFolderList(string connectionString)
        {
            IList<string> projectFolders = new List<string>();
            using (SqlConnection connProjectFolder = new SqlConnection(connectionString))
            {
                string projectFolderQuery = "select SalesUploadFolder from Project where SalesUploadFolder is not null order by SalesUploadFolder asc";
                using (SqlCommand cmdProjectFolderQuery = new SqlCommand(projectFolderQuery))
                {
                    connProjectFolder.Open();
                    cmdProjectFolderQuery.Connection = connProjectFolder;
                    using (SqlDataReader projectFolderSdr = cmdProjectFolderQuery.ExecuteReader())
                    {
                        while (projectFolderSdr.Read())
                        {
                            projectFolders.Add(projectFolderSdr["SalesUploadFolder"].ToString());
                        }
                    }
                    connProjectFolder.Close();
                }
            }
            return projectFolders;
        }
    }
}
