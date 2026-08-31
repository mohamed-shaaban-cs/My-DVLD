
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
        public clsPerson PersonInfo { get; set; }
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
            PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {

            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static clsUser FindByUserID(int UserID)
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
        public static clsUser FindByPersonID(int PersonID)
        {
            string Username = "";
            int UserID = -1;
            string Password = "";
            bool IsActive = false;


            bool IsFound = clsUserData.GetUserInfoByPersonID(PersonID , ref UserID, ref Username, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            else
                return null;
        }
        public static clsUser FindByUsernameAndPassword(string Username,string Password)
        {
            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;


            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword(Username, Password, ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            else
                return null;
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

        public static bool IsPersonHasAccount(int PersonID)
        {

            return clsUserData.IsPersonHasAccount(PersonID);
        }

        public static bool IsUserAndPasswordExist(string UserName, string Password)
        {

            return clsUserData.IsUserAndPasswordExist(UserName, Password);
        }

        public static bool IsUserActive(string UserName)
        {
            return clsUserData.IsUserActive(UserName);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update; // Change mode to Update after successful addition
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
    }
}
