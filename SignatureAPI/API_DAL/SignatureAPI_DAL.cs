using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SignatureAPI.API_DAL
{
    public class SignatureAPI_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        public bool SignatureGeneration(Profile model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "dbo.usp_SignatureFunctionSP";
                _command.Parameters.AddWithValue("@Query", "insert");
                _command.Parameters.AddWithValue("@name", model.Name);
                _command.Parameters.AddWithValue("@email", model.Email);
                _command.Parameters.AddWithValue("@phone", model.Phone);
                _command.Parameters.AddWithValue("@signature",model.Signature);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }
        public List<Profile> Get(string email)
        {
            List<Profile> list = new List<Profile>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "dbo.usp_SignatureFunctionSP";
                    _command.Parameters.AddWithValue("@Query", "search");
                    _command.Parameters.AddWithValue("@email", email);
                    _connection.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = _command;
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new Profile
                        {
                            Name = Convert.ToString(dr[1]),
                            Email = Convert.ToString(dr[2]),
                            Phone = Convert.ToString(dr[3]),
                            Signature = Convert.ToString(dr[4]),
                           
                        });
                    }
                    _connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
