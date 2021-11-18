using DirectoryReportingLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReport
{
    class Program
    {
        static void Main(string[] args)
        {
             NameValueCollection appSett = ConfigurationManager.AppSettings;
            
            //// getting multiple source directory
            var filteredSourceDirctories = appSett.AllKeys.Where(x => x.StartsWith("source_directory")).ToArray();

            string[] sourceDirectories = new string[filteredSourceDirctories.Count()];
            for (int i = 0; i < filteredSourceDirctories.Count(); i++)
            {
                sourceDirectories[i] = appSett.Get(filteredSourceDirctories[i]);
            }

            //// getting destination folder where source directories will be moved
            string destinationFolder = appSett.Get("destination_folder");

            //// report path for listing moving filelist
            string reportPath = appSett.Get("report_path");

            DirecoryReporting direcoryReport = new DirecoryReporting(sourceDirectories);

            //// writing reports
            direcoryReport.WriteReport(reportPath);

            //// moving directies
            direcoryReport.Move(destinationFolder);


            
        }
    }
}
