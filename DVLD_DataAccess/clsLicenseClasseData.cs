
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsLicenseClasseData
    {
        public static bool GetLicenseClasseInfoByID(int LicenseClasseID, ref int LicenseClassID,ref string ClassName,ref string ClassDescription,ref byte MinimumAllowedAge,ref byte DefaultValidityLength,ref decimal ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClasseID = @LicenseClasseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClasseID", LicenseClasseID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
   LicenseClassID = Reader["LicenseClassID"] != DBNull.Value ? (int)Reader["LicenseClassID"] : -1;
   ClassName = Reader["ClassName"] != DBNull.Value ? (string)Reader["ClassName"] : "";
   ClassDescription = Reader["ClassDescription"] != DBNull.Value ? (string)Reader["ClassDescription"] : "";
   MinimumAllowedAge = Reader["MinimumAllowedAge"] != DBNull.Value ? (byte)Reader["MinimumAllowedAge"] : (byte)0;
   DefaultValidityLength = Reader["DefaultValidityLength"] != DBNull.Value ? (byte)Reader["DefaultValidityLength"] : (byte)0;
   ClassFees = Reader["ClassFees"] != DBNull.Value ? (decimal)Reader["ClassFees"] : 0.0m;

                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                // Log Exception
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewLicenseClasse( int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO LicenseClasses ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees])
                             VALUES (@LicenseClassID, @ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@LicenseClassID",LicenseClassID);
   command.Parameters.AddWithValue("@ClassName",ClassName);
   command.Parameters.AddWithValue("@ClassDescription",ClassDescription);
   command.Parameters.AddWithValue("@MinimumAllowedAge",MinimumAllowedAge);
   command.Parameters.AddWithValue("@DefaultValidityLength",DefaultValidityLength);
   command.Parameters.AddWithValue("@ClassFees",ClassFees);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InsertedID = insertedID;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
            }
            finally
            {
                connection.Close();
            }
            return InsertedID;
        }

        public static bool UpdateLicenseClasse(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE LicenseClasses
                             SET [LicenseClassID] = @LicenseClassID, [ClassName] = @ClassName, [ClassDescription] = @ClassDescription, [MinimumAllowedAge] = @MinimumAllowedAge, [DefaultValidityLength] = @DefaultValidityLength, [ClassFees] = @ClassFees
                             WHERE LicenseClassID = @LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@LicenseClassID",LicenseClassID);
   command.Parameters.AddWithValue("@ClassName",ClassName);
   command.Parameters.AddWithValue("@ClassDescription",ClassDescription);
   command.Parameters.AddWithValue("@MinimumAllowedAge",MinimumAllowedAge);
   command.Parameters.AddWithValue("@DefaultValidityLength",DefaultValidityLength);
   command.Parameters.AddWithValue("@ClassFees",ClassFees);

            

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log Exception
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool DeleteLicenseClasse(int LicenseClasseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM LicenseClasses WHERE LicenseClasseID = @LicenseClasseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClasseID", LicenseClasseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log Exception
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool IsLicenseClasseExist(int LicenseClasseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM LicenseClasses WHERE LicenseClasseID = @LicenseClasseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClasseID", LicenseClasseID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Log Exception
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
