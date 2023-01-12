using Crud_Operation.CommonLayer.Model;
using System.Data.SqlClient;

namespace Crud_Operation.RepositoryLayer
{
    public class CrudOperationRL : ICrudOperationRL
    {

        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        public CrudOperationRL(IConfiguration configuration)
        {
            _configuration= configuration;
            _sqlConnection = new SqlConnection(_configuration["ConnectionStrings:DbSettingConnection"]);
        }
        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            response.IsSuccess= true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Insert Into CrudOperationTable (UserName, Age) values (@UserName, @Age)";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                    _sqlConnection.Open();
                    int Status = await sqlCommand.ExecuteNonQueryAsync();

                    if(Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<ReadRecordResponse> ReadRecord()
        {
            ReadRecordResponse response = new ReadRecordResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Select UserName, Age CrudOperationTable;";
                using(SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    _sqlConnection.Open();
                    using(SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if(sqlDataReader.HasRows)
                        {
                            response.readRecordData = new List<ReadRecordData>();

                            while (await sqlDataReader.ReadAsync())
                            {
                                ReadRecordData dbData = new ReadRecordData();
                                dbData.UserName = sqlDataReader["UserName"] != DBNull.Value ? sqlDataReader["UserName"].ToString() : string.Empty;
                                dbData.Age = sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(sqlDataReader["Age"]) : 0;
                                response.readRecordData.Add(dbData);
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {

            } finally
            {

            }
        }
    }
}
