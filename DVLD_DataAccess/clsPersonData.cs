
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo,ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth,ref byte Gender,ref string Address,ref string Phone,ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    ThirdName = Reader["ThirdName"] != DBNull.Value ? (string)Reader["ThirdName"] : "";
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (byte)Reader["Gender"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];
                    Email = Reader["Email"] != DBNull.Value ? (string)Reader["Email"] : "";
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? (string)Reader["ImagePath"] : "";
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


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    PersonID = (int)Reader["PersonID"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    ThirdName = Reader["ThirdName"] != DBNull.Value ? (string)Reader["ThirdName"] : "";
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gender = (byte)Reader["Gendor"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];
                    Email = Reader["Email"] != DBNull.Value ? (string)Reader["Email"] : "";
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? (string)Reader["ImagePath"] : "";

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
        public static int AddNewPerson( string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO People ([NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gender], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath])
                             VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", (ThirdName != null && ThirdName != "" ? ThirdName : (object)DBNull.Value));
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", (Email != null && Email != "" ? Email : (object)DBNull.Value));
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", (ImagePath != null && ImagePath != "" ? ImagePath : (object)DBNull.Value));


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

        public static bool UpdatePerson(int PersonID,  string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People
                             SET [NationalNo] = @NationalNo, [FirstName] = @FirstName, [SecondName] = @SecondName, [ThirdName] = @ThirdName, [LastName] = @LastName, [DateOfBirth] = @DateOfBirth, [Gender] = @Gender, [Address] = @Address, [Phone] = @Phone, [Email] = @Email, [NationalityCountryID] = @NationalityCountryID, [ImagePath] = @ImagePath
                             WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@NationalNo",NationalNo);
   command.Parameters.AddWithValue("@FirstName",FirstName);
   command.Parameters.AddWithValue("@SecondName",SecondName);
   command.Parameters.AddWithValue("@ThirdName",ThirdName);
   command.Parameters.AddWithValue("@LastName",LastName);
   command.Parameters.AddWithValue("@DateOfBirth",DateOfBirth);
   command.Parameters.AddWithValue("@Gender",Gender);
   command.Parameters.AddWithValue("@Address",Address);
   command.Parameters.AddWithValue("@Phone",Phone);
   command.Parameters.AddWithValue("@Email",Email);
   command.Parameters.AddWithValue("@NationalityCountryID",NationalityCountryID);
   command.Parameters.AddWithValue("@ImagePath",ImagePath);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, People.Gender, 

    CASE
        WHEN People.Gender = 0 THEN 'Male' 
        WHEN People.Gender = 1 THEN 'Female'
    END AS GenderCaption,
    Countries.CountryName, People.Phone, People.Email, 
                         People.Address
FROM            Countries INNER JOIN
                         People ON Countries.CountryID = People.NationalityCountryID"; ;
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
