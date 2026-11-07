
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        
        public int TestTypeID { get; set; }
        clsTestType _TestTypeInfo;
        public clsTestType TestTypeInfo {
            get 
            { 
                if(this.TestTypeID == -1 && this._TestTypeInfo == null)
                    return null;

                return clsTestType.Find((clsTestType.enTestType)this.TestTypeID); 
            } 
        }
        public int LocalDrivingLicenseApplicationID { get; set; }

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo
        {
            get
            {
                if (this.LocalDrivingLicenseApplicationID != -1 && this._LocalDrivingLicenseApplicationInfo == null)
                    this._LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(this.LocalDrivingLicenseApplicationID);
                
                    return this._LocalDrivingLicenseApplicationInfo;
            }
        }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        clsApplication _ApplicationRetakeInfo = null;
        public clsApplication ApplicationRetakeInfo { 
            get 
            { 
                if(this._ApplicationRetakeInfo == null && this.RetakeTestApplicationID != -1)
                    this._ApplicationRetakeInfo = clsApplication.FindBaseApplication(this.RetakeTestApplicationID);

                return this._ApplicationRetakeInfo;
            } 
        }


        // Default Constructor للمستخدم الجديد
        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0.0m;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            // التعديل هنا
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0.0m;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            
            bool IsFound = clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID);

            if (IsFound)
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();

                default:
                    return false;
            }
        }

        public static DataTable GetAllTestAppointments()
        {
            
            return clsTestAppointmentData.GetAllTestAppointments();
        }
        public static DataTable GetAllTestAppointmentsByTestType(clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            
            return clsTestAppointmentData.GetAllTestAppointmentsByTestTypeIDAndLocalDrivingLicenseApplicationID((int)TestTypeID, LocalDrivingLicenseApplicationID);
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            
            return clsTestAppointmentData.IsTestAppointmentExist(TestAppointmentID);
        }
    }
}
