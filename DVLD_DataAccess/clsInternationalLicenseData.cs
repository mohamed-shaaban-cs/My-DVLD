
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicensesID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    ApplicationID = Reader["ApplicationID"] != DBNull.Value ? (int)Reader["ApplicationID"] : -1;
                    DriverID = Reader["DriverID"] != DBNull.Value ? (int)Reader["DriverID"] : -1;
                    IssuedUsingLocalLicenseID = Reader["IssuedUsingLocalLicenseID"] != DBNull.Value ? (int)Reader["IssuedUsingLocalLicenseID"] : -1;
                    IssueDate = Reader["IssueDate"] != DBNull.Value ? (DateTime)Reader["IssueDate"] : DateTime.Now;
                    ExpirationDate = Reader["ExpirationDate"] != DBNull.Value ? (DateTime)Reader["ExpirationDate"] : DateTime.Now;
                    IsActive = Reader["IsActive"] != DBNull.Value ? (bool)Reader["IsActive"] : false;
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
        public static bool GetActiveInternationalLicenseInfoByDriverID(int DriverID, ref int InternationalLicenseID, ref int ApplicationID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Top 1 * FROM InternationalLicenses  
                             WHERE DriverID = @DriverID And  GetDate() between IssueDate and ExpirationDate
                             Order by ExpirationDate Desc;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    InternationalLicenseID = Reader["InternationalLicensesID"] != DBNull.Value ? (int)Reader["InternationalLicensesID"] : -1;
                    ApplicationID = Reader["ApplicationID"] != DBNull.Value ? (int)Reader["ApplicationID"] : -1;
                    IssuedUsingLocalLicenseID = Reader["IssuedUsingLocalLicenseID"] != DBNull.Value ? (int)Reader["IssuedUsingLocalLicenseID"] : -1;
                    IssueDate = Reader["IssueDate"] != DBNull.Value ? (DateTime)Reader["IssueDate"] : DateTime.Now;
                    ExpirationDate = Reader["ExpirationDate"] != DBNull.Value ? (DateTime)Reader["ExpirationDate"] : DateTime.Now;
                    IsActive = Reader["IsActive"] != DBNull.Value ? (bool)Reader["IsActive"] : false;
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
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int internationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Top 1 InternationalLicensesID FROM InternationalLicenses  
                             WHERE DriverID = @DriverID And  GetDate() between IssueDate and ExpirationDate
                             Order by ExpirationDate Desc;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                object Reader = command.ExecuteScalar();
                if (Reader != null)
                {
                    internationalLicenseID = (int)Reader;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                internationalLicenseID = -1;
            }
            finally
            {
                connection.Close();
            }
            return internationalLicenseID;
        }

        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update InternatinoalLicenses
                             Set IsActive = 0
                             DriverID = @DriverID;

                             INSERT INTO InternationalLicenses ([ApplicationID], [DriverID], [IssuedUsingLocalLicenseID]
                                                                , [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID])
                             VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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

        public static bool UpdateInternationalLicense(int InternationalLicenseID,  int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, object IssueDate, object ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE InternationalLicenses
                             SET [ApplicationID] = @ApplicationID, [DriverID] = @DriverID, [IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID, [IssueDate] = @IssueDate, [ExpirationDate] = @ExpirationDate, [IsActive] = @IsActive, [CreatedByUserID] = @CreatedByUserID
                             WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
   command.Parameters.AddWithValue("@DriverID",DriverID);
   command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID",IssuedUsingLocalLicenseID);
   command.Parameters.AddWithValue("@IssueDate",IssueDate);
   command.Parameters.AddWithValue("@ExpirationDate",ExpirationDate);
   command.Parameters.AddWithValue("@IsActive",IsActive);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT InternationalLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive
                             FROM InternationalLicenses
                             Order By IsActive, ExpirationDate desc";
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
        public static DataTable GetInternationalLicensesHistoryWithDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT       InternationalLicensesID, ApplicationID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive
                             FROM            InternationalLicenses 
                             WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
