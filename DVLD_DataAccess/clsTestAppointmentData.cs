
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID, ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,ref object AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
   TestTypeID = Reader["TestTypeID"] != DBNull.Value ? (int)Reader["TestTypeID"] : -1;
   LocalDrivingLicenseApplicationID = Reader["LocalDrivingLicenseApplicationID"] != DBNull.Value ? (int)Reader["LocalDrivingLicenseApplicationID"] : -1;
   AppointmentDate = Reader["AppointmentDate"] != DBNull.Value ? (object)Reader["AppointmentDate"] : null;
   PaidFees = Reader["PaidFees"] != DBNull.Value ? (decimal)Reader["PaidFees"] : 0.0m;
   CreatedByUserID = Reader["CreatedByUserID"] != DBNull.Value ? (int)Reader["CreatedByUserID"] : -1;
   IsLocked = Reader["IsLocked"] != DBNull.Value ? (bool)Reader["IsLocked"] : false;
   RetakeTestApplicationID = Reader["RetakeTestApplicationID"] != DBNull.Value ? (int)Reader["RetakeTestApplicationID"] : -1;

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

        public static int AddNewTestAppointment( int TestTypeID, int LocalDrivingLicenseApplicationID, object AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO TestAppointments ([TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID])
                             VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@TestTypeID",TestTypeID);
   command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
   command.Parameters.AddWithValue("@AppointmentDate",AppointmentDate);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
   command.Parameters.AddWithValue("@IsLocked",IsLocked);
   command.Parameters.AddWithValue("@RetakeTestApplicationID",RetakeTestApplicationID);


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

        public static bool UpdateTestAppointment(int TestAppointmentID,  int TestTypeID, int LocalDrivingLicenseApplicationID, object AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE TestAppointments
                             SET [TestTypeID] = @TestTypeID, [LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID, [AppointmentDate] = @AppointmentDate, [PaidFees] = @PaidFees, [CreatedByUserID] = @CreatedByUserID, [IsLocked] = @IsLocked, [RetakeTestApplicationID] = @RetakeTestApplicationID
                             WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@TestTypeID",TestTypeID);
   command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
   command.Parameters.AddWithValue("@AppointmentDate",AppointmentDate);
   command.Parameters.AddWithValue("@PaidFees",PaidFees);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
   command.Parameters.AddWithValue("@IsLocked",IsLocked);
   command.Parameters.AddWithValue("@RetakeTestApplicationID",RetakeTestApplicationID);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments";
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
