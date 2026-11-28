
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        clsDriver _DriverInfo;
        bool _isDriverLoaded = false;
        public clsDriver DriverInfo
        {
            get
            {
                if (!_isDriverLoaded)
                {
                    _DriverInfo = clsDriver.Find(DriverID);
                    _isDriverLoaded = true;
                }
                return _DriverInfo;
            }
        }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsInternationalLicense(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {

            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;
            base.Mode = clsApplication.enMode.Update;
             

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            // التعديل هنا
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            
            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
            {
                clsApplication app = clsApplication.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(ApplicationID,app.ApplicantPersonID, app.ApplicationDate, app.ApplicationTypeID, (byte)app.ApplicationStatus, app.LastStatusDate, app.PaidFees, CreatedByUserID,InternationalLicenseID,  DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;
        }
        public static clsInternationalLicense FindByDriverID(int DriverID)
        {
            int InternationalLicenseID = GetInt_LicenseIDByDriverID(DriverID);
            if (InternationalLicenseID != -1)
                return Find(InternationalLicenseID);
            else
                return null;
            
        }
        public static int GetInt_LicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)this.Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();

                default:
                    return false;
            }
        }

        public static DataTable GetAllInternationalLicenses()
        {
            
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
        public static DataTable GetInternationalLicensesHistoryWithDriverID(int DriverID)
        {
            
            return clsInternationalLicenseData.GetInternationalLicensesHistoryWithDriverID(DriverID);
        }

        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            
            return clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        }

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            
            return clsInternationalLicenseData.IsInternationalLicenseExist(InternationalLicenseID);
        }
    }
}
