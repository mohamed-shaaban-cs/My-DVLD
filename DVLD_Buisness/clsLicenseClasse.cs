
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsLicenseClasse
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsLicenseClasse()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength =  0;
            this.ClassFees = 0.0m;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsLicenseClasse(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            Mode = enMode.Update;
        }

        private bool _AddNewLicenseClasse()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.LicenseClassID = clsLicenseClasseData.AddNewLicenseClasse(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClasse()
        {
            // التعديل هنا
            return clsLicenseClasseData.UpdateLicenseClasse(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public static clsLicenseClasse Find(int LicenseClasseID)
        {
            int LicenseClassID = -1;
            string ClassName = "";
            string ClassDescription = "";
            byte MinimumAllowedAge = 0;    
            byte DefaultValidityLength = 0;
            decimal ClassFees = 0.0m;

            
            bool IsFound = clsLicenseClasseData.GetLicenseClasseInfoByID(LicenseClasseID, ref LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees);

            if (IsFound)
                return new clsLicenseClasse(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClasse())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicenseClasse();

                default:
                    return false;
            }
        }

        public static DataTable GetAllLicenseClasses()
        {
            
            return clsLicenseClasseData.GetAllLicenseClasses();
        }

        public static bool DeleteLicenseClasse(int LicenseClasseID)
        {
            
            return clsLicenseClasseData.DeleteLicenseClasse(LicenseClasseID);
        }

        public static bool IsLicenseClasseExist(int LicenseClasseID)
        {
            
            return clsLicenseClasseData.IsLicenseClasseExist(LicenseClasseID);
        }
    }
}
