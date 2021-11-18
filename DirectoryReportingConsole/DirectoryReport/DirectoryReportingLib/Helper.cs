using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReportingLib
{
    public static class Helper
    {
        /// <summary>
        /// check filepath is valid and through the error
        /// </summary>
        /// <param name="path">filePath</param>
        public static void CheckFile(string path)
        {
            //// null or empty check
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
            }
            //// file exist check
            if (!File.Exists(path))
            {
                throw new ArgumentException($"'{nameof(path)}' not valid file", nameof(path));
            }
        }

        /// <summary>
        /// check directory and through error message
        /// </summary>
        /// <param name="dir">list of directoires</param>
        public static void CheckDirectory(string dir)
        {
            //// null or empty check
            if (string.IsNullOrWhiteSpace(dir))
            {
                throw new ArgumentException($"'{nameof(dir)}' cannot be null or empty.", nameof(dir));
            }
            if (!Directory.Exists(dir))
            {
                throw new ArgumentException($"'{nameof(dir)}' not valid directory", nameof(dir));
            }

        }

        /// <summary>
        /// over load method to check list of directories
        /// </summary>
        /// <param name="dirs">list of directories</param>
        public static void CheckDirectory(string[] dirs)
        {
            foreach (var dir in dirs)
            {
                Helper.CheckDirectory(dir);
            }
        }

       
        /// <summary>
        /// get list of files from directory
        /// </summary>
        /// <param name="dir">directory path</param>
        /// <param name="isAlldirectorys">include subdirectory or not</param>
        /// <param name="fileFilter">any filters for files ex: *.txt</param>
        /// <returns></returns>
        public static string[] GetFilesFromDir(string dir, bool isAlldirectorys=true, string fileFilter = "*")
        {
            var searchOption = isAlldirectorys ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] files = Directory.GetFiles(dir, fileFilter, searchOption);
            return files;
            
        }

        /// <summary>
        /// write a file with string array
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="inputLines"></param>
        /// <param name="isAppend"></param>
        public static void WriteAllLines(string filePath,string[] inputLines,bool isAppend)
        {
            using (StreamWriter sw = new StreamWriter(filePath, append:isAppend ))
            {
                sw.WriteLine($"************************** Report Date :{DateTime.UtcNow.ToLongDateString()} ***************************");
                foreach (var line in inputLines)
                {
                    sw.WriteLine(line);
                }
                
            }
                
        }

        /// <summary>
        /// move folder from one dir to another : NOT TESTED
        /// </summary>
        /// <param name="source">source folder</param>
        /// <param name="destination">destination folder</param>
        public static void MoveDir(string source, string destination)
        {
            Directory.Move(source, destination);
        }

        public static void DeleteDir(string dir, bool delSubDirs)
        {
            Directory.Delete(dir, delSubDirs);
        }

        /// <summary>
        /// Copy directory from one folder to another
        /// </summary>
        /// <param name="sourceDirName">source directory</param>
        /// <param name="destDirName">destination directory</param>
        /// <param name="copySubDirs">do you need copy sub directory?</param>
        /// <param name="overWriteFiles">do you need overwited content if file already exists?</param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs,bool overWriteFiles)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, overWriteFiles);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs, overWriteFiles);
                }
            }
        }
    }
}
