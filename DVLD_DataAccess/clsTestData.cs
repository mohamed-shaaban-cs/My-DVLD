
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsTestData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
   TestAppointmentID = Reader["TestAppointmentID"] != DBNull.Value ? (int)Reader["TestAppointmentID"] : -1;
   TestResult = Reader["TestResult"] != DBNull.Value ? (bool)Reader["TestResult"] : false;
   Notes = Reader["Notes"] != DBNull.Value ? (string)Reader["Notes"] : "";
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

        public static int AddNewTest( int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Tests ([TestAppointmentID], [TestResult], [Notes], [CreatedByUserID])
                             VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);
   command.Parameters.AddWithValue("@TestResult",TestResult);
   command.Parameters.AddWithValue("@Notes",Notes);
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

        public static bool UpdateTest(int TestID,  int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Tests
                             SET [TestAppointmentID] = @TestAppointmentID, [TestResult] = @TestResult, [Notes] = @Notes, [CreatedByUserID] = @CreatedByUserID
                             WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);
   command.Parameters.AddWithValue("@TestResult",TestResult);
   command.Parameters.AddWithValue("@Notes",Notes);
   command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            command.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Tests WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool IsTestExist(int TestID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Tests WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

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

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests";
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
