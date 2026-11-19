
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_BusinessLogic
{
    public class clsLocalDrivingLicenseApplication :clsApplication
    {
         enum enMode { AddNew = 0, Update = 1 };
         enMode _Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }
        private clsLicenseClass _LicenseClassInfo;
        public clsLicenseClass LicenseClassInfo
        {
            get
            {
                if (_LicenseClassInfo == null && this.LicenseClassID != -1)
                    _LicenseClassInfo = clsLicenseClass.Find(this.LicenseClassID);

                return _LicenseClassInfo;
            }

        }

        // Default Constructor للمستخدم الجديد
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID,
            int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            _Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            // التعديل هنا
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }


        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                clsApplication app = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, ApplicationID, app.ApplicantPersonID, app.ApplicationDate, app.ApplicationTypeID, (byte)app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            //int LocalDrivingLicenseApplicationID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = -1;


            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID,ref LocalDrivingLicenseApplicationID,ref LicenseClassID);


            if (IsFound)
            {
                clsApplication app = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID,ApplicationID, app.ApplicantPersonID, app.ApplicationDate, app.ApplicationTypeID, (byte)app.ApplicationStatus, app.LastStatusDate, app.PaidFees, app.CreatedByUserID);
            }
            else
                return null;
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)_Mode;
            if(!base.Save())
                return false;
            

            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

                default:
                    return false;
            }
        }
        
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            int ApplicationID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).ApplicationID;
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID) && clsApplication.DeleteApplication(ApplicationID);

        }

        public static bool IsLocalDrivingLicenseApplicationExist(int LocalDrivingLicenseApplicationID)
        {
            
            return clsLocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationExist(LocalDrivingLicenseApplicationID);
        }

        
        public bool DoseAttendTestType( clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoseAttendTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DosePassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DosePassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte TotalTrialsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        
        public static byte countPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.CountPassedTests(LocalDrivingLicenseApplicationID);
        }
        public  byte countPassedTests()
        {
            return clsLocalDrivingLicenseApplicationData.CountPassedTests(LocalDrivingLicenseApplicationID);
        }
        public static bool IsHavePassedTestPerTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHavePassedTestPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsHaveActiveTestAppointment(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHaveActiveTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsLicenseIssued()
        {
            return clsLocalDrivingLicenseApplicationData.IsLicenseIssued(ApplicationID);
        }
    }
}
