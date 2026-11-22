
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;

namespace DVLD_BusinessLogic
{
    public class clsLocalDrivingLicenseApplication :clsApplication
    {
         enum enMode { AddNew = 0, Update = 1 };
         enMode _Mode = enMode.AddNew;

        public int _LocalDrivingLicenseApplicationID { get; set; }
        
        public int _LicenseClassID { get; set; }
        private clsLicenseClass _LicenseClassInfo;
        public clsLicenseClass LicenseClassInfo
        {
            get
            {
                if (_LicenseClassInfo == null && this._LicenseClassID != -1)
                    _LicenseClassInfo = clsLicenseClass.Find(this._LicenseClassID);

                return _LicenseClassInfo;
            }

        }

        // Default Constructor للمستخدم الجديد
        public clsLocalDrivingLicenseApplication()
        {
            this._LocalDrivingLicenseApplicationID = -1;
            this._LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID,
            int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._LicenseClassID = LicenseClassID;
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
            this._LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this._LicenseClassID);
            return (this._LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            // التعديل هنا
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this._LocalDrivingLicenseApplicationID, this.ApplicationID, this._LicenseClassID);
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

        public clsTest GetLastTestPerTestTypeID(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindtLastTestPerTestTypeAndLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID,TestTypeID);
        }
        
        public bool DoseAttendTestType( clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoseAttendTestType(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DosePassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DosePassTestType(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public byte TotalTrialsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTestType(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        

        public  byte countPassedTests()
        {
            return clsLocalDrivingLicenseApplicationData.CountPassedTests(_LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return this.countPassedTests() == 3;
        }

        public static bool IsHavePassedTestPerTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHavePassedTestPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsHaveActiveTestAppointment(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHaveActiveTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public  bool IsHaveActiveTestAppointment(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHaveActiveTestAppointment(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public int GetActiveLicenseID()
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID( this.ApplicantPersonID , this._LicenseClassID);
        }
        public int IssueDriverLicenseFirstTime(string notes,int currentUserID)
        {
            clsDriver Driver =  clsDriver.FindByPersonID(ApplicantPersonID);
            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = ApplicantPersonID;
                Driver.CreatedByUserID = currentUserID;
                if (!Driver.Save())
                {
                    return -1;
                }
            } 
            clsLicense License = new clsLicense();
            License.ApplicationID = ApplicationID;
            License.DriverID = Driver.DriverID;
            License.LicenseClassID = _LicenseClassID;
            License.ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength); // Example: +10 year expiration
            License.Notes = notes;
            License.PaidFees = LicenseClassInfo.ClassFees;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID =currentUserID;
            if (License.Save())
            {
                this.Complete();
                return License.LicenseID;
            }
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return clsLocalDrivingLicenseApplicationData.IsLicenseIssued(ApplicationID);
        }
    }
}
