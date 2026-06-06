
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByID(int DetainedID,ref int LicenseID,ref object DetainDate,ref decimal FineFees,ref int CreatedByUserID,ref bool IsReleased,ref object ReleaseDate,ref int ReleasedByUserID,ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainedID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainedID", DetainedID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
 
   LicenseID = Reader["LicenseID"] != DBNull.Value ? (int)Reader["LicenseID"] : -1;
   DetainDate = Reader["DetainDate"] != DBNull.Value ? (object)Reader["DetainDate"] : null;
   FineFees = Reader["FineFees"] != DBNull.Value ? (decimal)Reader["FineFees"] : 0.0m;
   CreatedByUserID = Reader["CreatedByUserID"] != DBNull.Value ? (int)Reader["CreatedByUserID"] : -1;
   IsReleased = Reader["IsReleased"] != DBNull.Value ? (bool)Reader["IsReleased"] : false;
   ReleaseDate = Reader["ReleaseDate"] != DBNull.Value ? (object)Reader["ReleaseDate"] : null;
   ReleasedByUserID = Reader["ReleasedByUserID"] != DBNull.Value ? (int)Reader["ReleasedByUserID"] : -1;
   ReleaseApplicationID = Reader["ReleaseApplicationID"] != DBNull.Value ? (int)Reader["ReleaseApplicationID"] : -1;

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

        public static int AddNewDetainedLicense( int DetainID, int LicenseID, object DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, object ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO DetainedLicenses ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID])
                             VALUES (@DetainID, @LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@DetainID",DetainID);
   command.Parameters.AddWithValue("@LicenseID",LicenseID);
   command.Parameters.AddWithValue("@DetainDate",DetainDate);
   command.Parameters.AddWithValue("@FineFees",FineFees);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
   command.Parameters.AddWithValue("@IsReleased",IsReleased);
   command.Parameters.AddWithValue("@ReleaseDate",ReleaseDate);
   command.Parameters.AddWithValue("@ReleasedByUserID",ReleasedByUserID);
   command.Parameters.AddWithValue("@ReleaseApplicationID",ReleaseApplicationID);


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

        public static bool UpdateDetainedLicense(int DetainedLicenseID,  int DetainID, int LicenseID, object DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, object ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE DetainedLicenses
                             SET [DetainID] = @DetainID, [LicenseID] = @LicenseID, [DetainDate] = @DetainDate, [FineFees] = @FineFees, [CreatedByUserID] = @CreatedByUserID, [IsReleased] = @IsReleased, [ReleaseDate] = @ReleaseDate, [ReleasedByUserID] = @ReleasedByUserID, [ReleaseApplicationID] = @ReleaseApplicationID
                             WHERE DetainedLicenseID = @DetainedLicenseID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@DetainID",DetainID);
   command.Parameters.AddWithValue("@LicenseID",LicenseID);
   command.Parameters.AddWithValue("@DetainDate",DetainDate);
   command.Parameters.AddWithValue("@FineFees",FineFees);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
   command.Parameters.AddWithValue("@IsReleased",IsReleased);
   command.Parameters.AddWithValue("@ReleaseDate",ReleaseDate);
   command.Parameters.AddWithValue("@ReleasedByUserID",ReleasedByUserID);
   command.Parameters.AddWithValue("@ReleaseApplicationID",ReleaseApplicationID);

            command.Parameters.AddWithValue("@DetainedLicenseID", DetainedLicenseID);

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

        public static bool DeleteDetainedLicense(int DetainedLicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM DetainedLicenses WHERE DetainedLicenseID = @DetainedLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainedLicenseID", DetainedLicenseID);

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

        public static bool IsDetainedLicenseExist(int DetainedLicenseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM DetainedLicenses WHERE DetainedLicenseID = @DetainedLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainedLicenseID", DetainedLicenseID);

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

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses";
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
