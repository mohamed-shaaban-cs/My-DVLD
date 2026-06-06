
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsLicenseData
    {
        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID,ref int DriverID,ref int LicenseClass,ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref decimal PaidFees,ref bool IsActive,ref byte IssueReason,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
   ApplicationID = Reader["ApplicationID"] != DBNull.Value ? (int)Reader["ApplicationID"] : -1;
   DriverID = Reader["DriverID"] != DBNull.Value ? (int)Reader["DriverID"] : -1;
   LicenseClass = Reader["LicenseClass"] != DBNull.Value ? (int)Reader["LicenseClass"] : -1;
   IssueDate = Reader["IssueDate"] != DBNull.Value ? (DateTime)Reader["IssueDate"] : DateTime.Now;
   ExpirationDate = Reader["ExpirationDate"] != DBNull.Value ? (DateTime)Reader["ExpirationDate"] : DateTime.Now;
   Notes = Reader["Notes"] != DBNull.Value ? (string)Reader["Notes"] : "";
   PaidFees = Reader["PaidFees"] != DBNull.Value ? (decimal)Reader["PaidFees"] : 0.0m;
   IsActive = Reader["IsActive"] != DBNull.Value ? (bool)Reader["IsActive"] : false;
   IssueReason = Reader["IssueReason"] != DBNull.Value ? (byte)Reader["IssueReason"] : (byte)0;
   CreatedByUserID = Reader["CreatedByUserID"] != DBNull.Value ? (int)Reader["CreatedByUserID"] : -1;

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

        public static int AddNewLicense( int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Licenses ([ApplicationID], [DriverID], [LicenseClass], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID])
                             VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
   command.Parameters.AddWithValue("@DriverID",DriverID);
   command.Parameters.AddWithValue("@LicenseClass",LicenseClass);
   command.Parameters.AddWithValue("@IssueDate",IssueDate);
   command.Parameters.AddWithValue("@ExpirationDate",ExpirationDate);
   command.Parameters.AddWithValue("@Notes",Notes);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
   command.Parameters.AddWithValue("@IsActive",IsActive);
   command.Parameters.AddWithValue("@IssueReason",IssueReason);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);


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

        public static bool UpdateLicense(int LicenseID,  int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Licenses
                             SET [ApplicationID] = @ApplicationID, [DriverID] = @DriverID, [LicenseClass] = @LicenseClass, [IssueDate] = @IssueDate, [ExpirationDate] = @ExpirationDate, [Notes] = @Notes, [PaidFees] = @PaidFees, [IsActive] = @IsActive, [IssueReason] = @IssueReason, [CreatedByUserID] = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
   command.Parameters.AddWithValue("@DriverID",DriverID);
   command.Parameters.AddWithValue("@LicenseClass",LicenseClass);
   command.Parameters.AddWithValue("@IssueDate",IssueDate);
   command.Parameters.AddWithValue("@ExpirationDate",ExpirationDate);
   command.Parameters.AddWithValue("@Notes",Notes);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
   command.Parameters.AddWithValue("@IsActive",IsActive);
   command.Parameters.AddWithValue("@IssueReason",IssueReason);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool DeleteLicense(int LicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool IsLicenseExist(int LicenseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses";
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
