
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            // التعديل هنا
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            
            bool IsFound = clsUserData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();

                default:
                    return false;
            }
        }

        public static DataTable GetAllUsers()
        {
            
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {

            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserAndPasswordExist(string UserName, string Password)
        {

            return clsUserData.IsUserAndPasswordExist(UserName, Password);
        }

        public static bool IsUserActive(string UserName)
        {
            return clsUserData.IsUserActive(UserName);
        }
    }
}
