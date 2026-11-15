
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementForLost = 3, ReplacementForDamaged = 4};
        public enMode Mode = enMode.AddNew;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        clsApplication _ApplicationInfo;
        public clsApplication ApplicationInfo
        {
            get
            {
                if (_ApplicationInfo == null && this.ApplicationID != -1)
                    _ApplicationInfo = clsApplication.FindBaseApplication(this.ApplicationID);
                return _ApplicationInfo;
            }
        }
        public int DriverID { get; set; }
        clsDriver _DriverInfo;
        public clsDriver DriverInfo
        {
            get
            {
                if (_DriverInfo == null && this.DriverID != -1)
                    _DriverInfo = clsDriver.Find(this.DriverID);
                return _DriverInfo;
            }
        }

        public int LicenseClassID { get; set; }
        clsLicenseClass _LicenseClassInfo;
        public clsLicenseClass LicenseClassInfo
        {
            get
            {
                if (_LicenseClassInfo == null && this.LicenseClassID != -1)
                    _LicenseClassInfo = clsLicenseClass.Find(this.LicenseClassID);
                return _LicenseClassInfo;
            }
        }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0.0m;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = 0;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            // التعديل هنا
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0.0m;
            bool IsActive = false;
            byte IssueReason = (byte)enIssueReason.FirstTime;
            int CreatedByUserID = 0;

            
            bool IsFound = clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (IsFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }
        public static clsLicense FindbyApplicationID(int ApplicationID)
        {
            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0.0m;
            bool IsActive = false;
            byte IssueReason = (byte)enIssueReason.FirstTime;
            int CreatedByUserID = 0;


            bool IsFound = clsLicenseData.GetLicenseInfoByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (IsFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static clsLicense GetActiveLicenseInfoBYPersonID(int PersonID,int LicenseClass)
        {
            int LicenseID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0.0m;
            bool IsActive = false;
            byte IssueReason = (byte)enIssueReason.FirstTime;
            int CreatedByUserID = 0;


            bool IsFound = clsLicenseData.GetActiveLicenseInfoByPersonID(PersonID, LicenseClass, ref LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (IsFound)
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;
        }

        public static bool IsActiveLicenseExistByPersonID(int PersonID, int LicenseClass)
        {
            return GetActiveLicenseInfoBYPersonID(PersonID, LicenseClass) != null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();

                default:
                    return false;
            }
        }

        public static DataTable GetAllLicenses()
        {
            
            return clsLicenseData.GetAllLicenses();
        }

        public static bool DeleteLicense(int LicenseID)
        {
            
            return clsLicenseData.DeleteLicense(LicenseID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            
            return clsLicenseData.IsLicenseExist(LicenseID);
        }
    }
}
