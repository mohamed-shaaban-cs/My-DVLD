
using System;
using System.Data;
using System.Net.NetworkInformation;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enum enApplicationType
        {
            NewLocalDrivingLicense = 1, RenewDrivingLicense = 2,
            ReplacementForALostDrivingLicense = 3, ReplacementForDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        }

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }

        public enMode Mode = enMode.AddNew;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        private clsPerson _PersonInfo;
        public clsPerson PersonInfo
        {
            get
            {
                // Lazy loading: Load PersonInfo only when it's accessed for the first time
                if (_PersonInfo == null && ApplicantPersonID != -1)
                    _PersonInfo = clsPerson.Find(ApplicantPersonID);

                return _PersonInfo;
            }

            set
            {
                _PersonInfo = value;
            }
        }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(this.ApplicantPersonID)?.FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        private clsApplicationType _ApplicationTypeInfo;
        public clsApplicationType ApplicationTypeInfo
        {
            get
            {
                if( _ApplicationTypeInfo == null && ApplicationTypeID != -1)
                    _ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);

                return _ApplicationTypeInfo;
            }
            set
            {
                _ApplicationTypeInfo = value;
            }
        }

        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (this.ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown Status";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        private clsUser _CreatedByUserInfo;
        public clsUser CreatedByUserInfo
        {
            get
            {
                if (_CreatedByUserInfo == null && CreatedByUserID != -1)
                    _CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

                return _CreatedByUserInfo;
            }
            set
            {
                _CreatedByUserInfo = value;
            }
        }


        // Default Constructor للمستخدم الجديد
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0.0m;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            // التعديل هنا
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = (byte)0;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = 0.0m;
            int CreatedByUserID = -1;


            bool IsFound = clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (IsFound)
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enApplicationStatus.Cancelled);
        }
        public bool Complete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enApplicationStatus.Completed);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplication();

                default:
                    return false;
            }
        }

        public static DataTable GetAllApplications()
        {

            return clsApplicationData.GetAllApplications();
        }

        public static bool DeleteApplication(int ApplicationID)
        {

            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {

            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DosePersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplicatino(PersonID, ApplicationTypeID);
        }

        public bool DosePersonHaveActiveApplication(int PersonID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplicatino(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationType)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (byte)ApplicationType);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID,clsApplication.enApplicationType ApplicationType, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (byte)ApplicationType, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationType)
        {
            return clsApplicationData.GetActiveApplicationID(this.ApplicantPersonID, (byte)ApplicationType);
        }
    }
}
