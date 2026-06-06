
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public object CreatedDate { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = null;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, object CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            // التعديل هنا
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public static clsDriver Find(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            object CreatedDate = null;

            
            bool IsFound = clsDriverData.GetDriverInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);

            if (IsFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDriver();

                default:
                    return false;
            }
        }

        public static DataTable GetAllDrivers()
        {
            
            return clsDriverData.GetAllDrivers();
        }

        public static bool DeleteDriver(int DriverID)
        {
            
            return clsDriverData.DeleteDriver(DriverID);
        }

        public static bool IsDriverExist(int DriverID)
        {
            
            return clsDriverData.IsDriverExist(DriverID);
        }
    }
}
