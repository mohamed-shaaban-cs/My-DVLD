using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsApplicationData
    {
        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID,
            ref DateTime ApplicationDate,ref int ApplicationTypeID,ref byte ApplicationStatus,
            ref DateTime LastStatusDate,ref decimal PaidFees,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = Reader["ApplicantPersonID"] != DBNull.Value ? (int)Reader["ApplicantPersonID"] : -1;
                    ApplicationDate = Reader["ApplicationDate"] != DBNull.Value ? (DateTime)Reader["ApplicationDate"] : DateTime.Now;
                    ApplicationTypeID = Reader["ApplicationTypeID"] != DBNull.Value ? (int)Reader["ApplicationTypeID"] : -1;
                    ApplicationStatus = Reader["ApplicationStatus"] != DBNull.Value ? (byte)Reader["ApplicationStatus"] : (byte)1;
                    LastStatusDate = Reader["LastStatusDate"] != DBNull.Value ? (DateTime)Reader["LastStatusDate"] : DateTime.Now;
                    PaidFees = Reader["PaidFees"] != DBNull.Value ? (decimal)Reader["PaidFees"] : 0.0m;
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

        public static int AddNewApplication( int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Applications ([ApplicantPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID])
                             VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@ApplicantPersonID",ApplicantPersonID);
   command.Parameters.AddWithValue("@ApplicationDate",ApplicationDate);
   command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationTypeID);
   command.Parameters.AddWithValue("@ApplicationStatus",ApplicationStatus);
   command.Parameters.AddWithValue("@LastStatusDate",LastStatusDate);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
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

        public static bool UpdateApplication(int ApplicationID,  int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Applications
                             SET [ApplicantPersonID] = @ApplicantPersonID, [ApplicationDate] = @ApplicationDate, [ApplicationTypeID] = @ApplicationTypeID, [ApplicationStatus] = @ApplicationStatus, [LastStatusDate] = @LastStatusDate, [PaidFees] = @PaidFees, [CreatedByUserID] = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@ApplicantPersonID",ApplicantPersonID);
   command.Parameters.AddWithValue("@ApplicationDate",ApplicationDate);
   command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationTypeID);
   command.Parameters.AddWithValue("@ApplicationStatus",ApplicationStatus);
   command.Parameters.AddWithValue("@LastStatusDate",LastStatusDate);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications";
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

        public static int GetActiveApplicationID(int PersonID , int ApplicationTypeID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ApplicationID FROM Applications WHERE ApplicantPersonID = @PersonID AND ApplicationTypeID = @ApplicationTypeID AND ApplicationStatus = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    ApplicationID = (int)result;
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
            return ApplicationID;
        }

        public static bool DoesPersonHaveActiveApplicatino(int PersonID , int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID,int ApplicationTypeID,int LicenseClassID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT       Applications.ApplicationID
            FROM            Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
            Where   Applications.ApplicationPersonID = @PersonID
            and     ApplicationTypeID = @ApplicationTypeID
            and     LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
            and    Applications.ApplicationStatus = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    ApplicationID = (int)result;
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
            return ApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Applications
                             SET [ApplicationStatus] = @NewStatus,
                                 [LastStatusDate] = @StatusDate
                             WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("@StatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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

    }
}
    