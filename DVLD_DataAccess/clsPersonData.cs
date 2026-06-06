
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo,ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth,ref byte Gendor,ref string Address,ref string Phone,ref string Email,ref int NationalityCountryID,ref string ImagePath)
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
   NationalNo = Reader["NationalNo"] != DBNull.Value ? (string)Reader["NationalNo"] : "";
   FirstName = Reader["FirstName"] != DBNull.Value ? (string)Reader["FirstName"] : "";
   SecondName = Reader["SecondName"] != DBNull.Value ? (string)Reader["SecondName"] : "";
   ThirdName = Reader["ThirdName"] != DBNull.Value ? (string)Reader["ThirdName"] : "";
   LastName = Reader["LastName"] != DBNull.Value ? (string)Reader["LastName"] : "";
   DateOfBirth = Reader["DateOfBirth"] != DBNull.Value ? (DateTime)Reader["DateOfBirth"] : DateTime.Now;
   Gendor = Reader["Gendor"] != DBNull.Value ? (byte)Reader["Gendor"] : (byte)0;
   Address = Reader["Address"] != DBNull.Value ? (string)Reader["Address"] : "";
   Phone = Reader["Phone"] != DBNull.Value ? (string)Reader["Phone"] : "";
   Email = Reader["Email"] != DBNull.Value ? (string)Reader["Email"] : "";
   NationalityCountryID = Reader["NationalityCountryID"] != DBNull.Value ? (int)Reader["NationalityCountryID"] : -1;
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


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
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
                    PersonID = Reader["PersonID"] != DBNull.Value ? (int)Reader["PersonID"] : -1    ;
                    FirstName = Reader["FirstName"] != DBNull.Value ? (string)Reader["FirstName"] : "";
                    SecondName = Reader["SecondName"] != DBNull.Value ? (string)Reader["SecondName"] : "";
                    ThirdName = Reader["ThirdName"] != DBNull.Value ? (string)Reader["ThirdName"] : "";
                    LastName = Reader["LastName"] != DBNull.Value ? (string)Reader["LastName"] : "";
                    DateOfBirth = Reader["DateOfBirth"] != DBNull.Value ? (DateTime)Reader["DateOfBirth"] : DateTime.Now;
                    Gendor = Reader["Gendor"] != DBNull.Value ? (byte)Reader["Gendor"] : (byte)0;
                    Address = Reader["Address"] != DBNull.Value ? (string)Reader["Address"] : "";
                    Phone = Reader["Phone"] != DBNull.Value ? (string)Reader["Phone"] : "";
                    Email = Reader["Email"] != DBNull.Value ? (string)Reader["Email"] : "";
                    NationalityCountryID = Reader["NationalityCountryID"] != DBNull.Value ? (int)Reader["NationalityCountryID"] : -1;
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
        public static int AddNewPerson( string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int InsertedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO People ([NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath])
                             VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@NationalNo",NationalNo);
   command.Parameters.AddWithValue("@FirstName",FirstName);
   command.Parameters.AddWithValue("@SecondName",SecondName);
   command.Parameters.AddWithValue("@ThirdName",ThirdName);
   command.Parameters.AddWithValue("@LastName",LastName);
   command.Parameters.AddWithValue("@DateOfBirth",DateOfBirth);
   command.Parameters.AddWithValue("@Gendor",Gendor);
   command.Parameters.AddWithValue("@Address",Address);
   command.Parameters.AddWithValue("@Phone",Phone);
   command.Parameters.AddWithValue("@Email",Email);
   command.Parameters.AddWithValue("@NationalityCountryID",NationalityCountryID);
   command.Parameters.AddWithValue("@ImagePath",ImagePath);


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

        public static bool UpdatePerson(int PersonID,  string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People
                             SET [NationalNo] = @NationalNo, [FirstName] = @FirstName, [SecondName] = @SecondName, [ThirdName] = @ThirdName, [LastName] = @LastName, [DateOfBirth] = @DateOfBirth, [Gendor] = @Gendor, [Address] = @Address, [Phone] = @Phone, [Email] = @Email, [NationalityCountryID] = @NationalityCountryID, [ImagePath] = @ImagePath
                             WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

   command.Parameters.AddWithValue("@NationalNo",NationalNo);
   command.Parameters.AddWithValue("@FirstName",FirstName);
   command.Parameters.AddWithValue("@SecondName",SecondName);
   command.Parameters.AddWithValue("@ThirdName",ThirdName);
   command.Parameters.AddWithValue("@LastName",LastName);
   command.Parameters.AddWithValue("@DateOfBirth",DateOfBirth);
   command.Parameters.AddWithValue("@Gendor",Gendor);
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

        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, People.Gendor, 

    Case
        When People.Gendor = 0 THEN 'Male' 
        When People.Gendor = 1 THEN 'Female'
    End As GendorCaption,
    Countries.CountryName, People.Phone, People.Email, 
                         People.Address
FROM            Countries INNER JOIN
                         People ON Countries.CountryID = People.NationalityCountryID";
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
