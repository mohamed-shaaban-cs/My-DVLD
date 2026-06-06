
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public object DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public object ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = null;
            this.FineFees = 0.0m;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsDetainedLicense(int DetainID, int LicenseID, object DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, object ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            // التعديل هنا
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            
            int LicenseID = -1;
            object DetainDate = null;
            decimal FineFees = 0.0m;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            object ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            
            bool IsFound = clsDetainedLicenseData.GetDetainedLicenseInfoByID( DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);

            if (IsFound)
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetainedLicense();

                default:
                    return false;
            }
        }

        public static DataTable GetAllDetainedLicenses()
        {
            
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public static bool DeleteDetainedLicense(int DetainedLicenseID)
        {
            
            return clsDetainedLicenseData.DeleteDetainedLicense(DetainedLicenseID);
        }

        public static bool IsDetainedLicenseExist(int DetainedLicenseID)
        {
            
            return clsDetainedLicenseData.IsDetainedLicenseExist(DetainedLicenseID);
        }
    }
}
