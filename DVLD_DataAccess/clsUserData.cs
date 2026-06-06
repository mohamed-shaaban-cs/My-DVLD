
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsUserData
    {
        public static bool GetUserInfoByID(int UserID, ref int PersonID,ref string UserName,ref string Password,ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
   PersonID = Reader["PersonID"] != DBNull.Value ? (int)Reader["PersonID"] : -1;
   UserName = Reader["UserName"] != DBNull.Value ? (string)Reader["UserName"] : "";
   Password = Reader["Password"] != DBNull.Value ? (string)Reader["Password"] : "";
   IsActive = Reader["IsActive"] != DBNull.Value ? (bool)Reader["IsActive"] : false;

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

        public static int AddNewUser( int PersonID, string UserName, string Password, bool IsActive)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Users ([PersonID], [UserName], [Password], [IsActive])
                             VALUES (@PersonID, @UserName, @Password, @IsActive);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@PersonID",PersonID);
   command.Parameters.AddWithValue("@UserName",UserName);
   command.Parameters.AddWithValue("@Password",Password);
   command.Parameters.AddWithValue("@IsActive",IsActive);


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

        public static bool UpdateUser(int UserID,  int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Users
                             SET [PersonID] = @PersonID, [UserName] = @UserName, [Password] = @Password, [IsActive] = @IsActive
                             WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@PersonID",PersonID);
   command.Parameters.AddWithValue("@UserName",UserName);
   command.Parameters.AddWithValue("@Password",Password);
   command.Parameters.AddWithValue("@IsActive",IsActive);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users";
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
