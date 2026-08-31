using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DVLD.Global_Classes
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser = new clsUser();

        public static bool RememberUsernameAndPassword(string username, string password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                string filePath = CurrentDirectory + "\\data.txt";

                if(username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string DataToSave = clsCryptoHelper.AESEncrypt($"{username}#//#{password}");


                //Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential(ref string username,ref string password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                string filePath = CurrentDirectory + "\\data.txt";

                if (!File.Exists(filePath))
                    return false;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine();

                    string DecryptedLine = clsCryptoHelper.AESDecrypt(line);
                    string[] Result = DecryptedLine.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    username = Result[0];
                    password = Result[1];

                    return true;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

            
        }
    }
}
