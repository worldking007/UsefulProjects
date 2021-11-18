using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReportingLib
{
    public class DirecoryReporting
    {
        private string[] _directoryArray { get; set; }

        public DirecoryReporting(string[] sourceDirectoryArray)
        {
            Helper.CheckDirectory(sourceDirectoryArray);
            this._directoryArray = sourceDirectoryArray;
        }

        public void WriteReport(string destinationFilePath)
        {
            Helper.CheckFile(destinationFilePath);
            foreach (var item in this._directoryArray)
            {
                var fileList = Helper.GetFilesFromDir(item);
                Helper.WriteAllLines(destinationFilePath, fileList,true);
            }

        }

        public void Move(string destinationFolderPath)
        {
            Helper.CheckDirectory(destinationFolderPath);
            foreach (var item in this._directoryArray)
            {
                Helper.DirectoryCopy(item, destinationFolderPath,true,true);
                Helper.DeleteDir(item, true);
            }
        }
    }
}
