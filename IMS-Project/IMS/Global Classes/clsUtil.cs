using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.Global_Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();

        }
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string FileName = sourceFile;
            FileInfo file = new FileInfo(FileName);
            string extension = file.Extension;
            return GenerateGUID() + extension;
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }

            }
            return true;
        }
        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
                return false;
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, DestinationFile, true);
            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = DestinationFile;
            return true;

        }
    }
}
