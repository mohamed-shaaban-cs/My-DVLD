using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DVLD
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

        public static bool CreateFolderIfDoesNotExist(string folderPath)
        {
            // this function will create a folder if it does not exist.
            // and return true if the folder is created or already exists.
            // and return false if the folder could not be created.
            if (!System.IO.Directory.Exists(folderPath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGUID(string filePath)
        {
            // this function will replace the file name with a GUID and return the new file Name without the path.
            // it will keep the same extension of the file.
            string extension = System.IO.Path.GetExtension(filePath);
            string newFileName = GenerateGUID() + extension;
            return newFileName;
        }
        public static string CreateFolderInDocumentFolderIfDoesNotExist(string folderName)
        {
            // this function will create a folder in the document folder if it does not exist.
            // and return the path of the folder.
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string localDocuments = System.IO.Path.Combine(userProfilePath, "Documents");
            string DestinationFolder = (Path.Combine(localDocuments, "DVLD-People-Images")).TrimEnd();

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return null;
            }
            return DestinationFolder;
        }
        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            // this function will copy the image to the project images. ,
            // folder after renaming it with GUID with the same extension.
            // and Update the sourceFile with the new path name.

            string DestinationFolder = CreateFolderInDocumentFolderIfDoesNotExist("DVLD-People-Images");
            // check if the destination folder is Exists or Not
            if (DestinationFolder == null)
                return false;

            string DestinationFile = Path.Combine( DestinationFolder , ReplaceFileNameWithGUID(sourceFile));
            try
            {
                File.Copy(sourceFile, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = DestinationFile;
            return true;
        }
    }
}
