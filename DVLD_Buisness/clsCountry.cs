
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int CountryID { get; set; }
        public string CountryName { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

            Mode = enMode.Update;
        }

        private bool _AddNewCountry()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.CountryID = clsCountryData.AddNewCountry(this.CountryName);
            return (this.CountryID != -1);
        }

        private bool _UpdateCountry()
        {
            // التعديل هنا
            return clsCountryData.UpdateCountry(this.CountryID, this.CountryName);
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            
            bool IsFound = clsCountryData.GetCountryInfoByID(CountryID, ref CountryName);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }
        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;


            bool IsFound = clsCountryData.GetCountryInfoByName(CountryName, ref CountryID);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCountry();

                default:
                    return false;
            }
        }

        public static DataTable GetAllCountries()
        {
            
            return clsCountryData.GetAllCountries();
        }

        public static bool DeleteCountry(int CountryID)
        {
            
            return clsCountryData.DeleteCountry(CountryID);
        }

        public static bool IsCountryExist(int CountryID)
        {
            
            return clsCountryData.IsCountryExist(CountryID);
        }
    }
}
