
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public object IssueDate { get; set; }
        public object ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = null;
            this.ExpirationDate = null;
            this.IsActive = false;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, object IssueDate, object ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
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
            object IssueDate = null;
            object ExpirationDate = null;
            bool IsActive = false;
            int CreatedByUserID = -1;

            
            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
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
