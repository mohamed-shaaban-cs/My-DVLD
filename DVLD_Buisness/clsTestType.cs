
using System;
using System.Data;
using DVLD_DataAccess; // تأكد إن ده نفس اسم الـ Namespace بتاع طبقة الداتا عندك

namespace DVLD_BusinessLogic
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }


        // Default Constructor للمستخدم الجديد
        public clsTestType()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0.0m;

            Mode = enMode.AddNew;
        }

        // Parameterized Constructor لتحميل بيانات موجودة
        private clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            // التعديل هنا: استخدام cls{ClassName}Data
            this.TestTypeID = clsTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
            return (this.TestTypeID != -1);
        }

        private bool _UpdateTestType()
        {
            // التعديل هنا
            return clsTestTypeData.UpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public static clsTestType Find(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFees = 0.0m;

            
            bool IsFound = clsTestTypeData.GetTestTypeInfoByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);

            if (IsFound)
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestType();

                default:
                    return false;
            }
        }

        public static DataTable GetAllTestTypes()
        {
            
            return clsTestTypeData.GetAllTestTypes();
        }

        public static bool DeleteTestType(int TestTypeID)
        {
            
            return clsTestTypeData.DeleteTestType(TestTypeID);
        }

        public static bool IsTestTypeExist(int TestTypeID)
        {
            
            return clsTestTypeData.IsTestTypeExist(TestTypeID);
        }
    }
}
