using CommonService.Utility;
using Dapper;
using MasterServiceDemo.Interfaces;
using MasterServiceDemo.Models;

namespace MasterServiceDemo.Services
{
    public class UserService : IUser
    {
        private DbGateway _dbgateway;
        public UserService(string _connection)
        {
            this._dbgateway = new DbGateway(_connection);
        }

        public async Task<Response<UserModel>> GetAllData()
        {
            Response<UserModel> response = new Response<UserModel>();

            List<UserModel> userModels = await _dbgateway.ExeQueryList<UserModel>("SELECT iduserData, userDatacol, userEmail, userPhone FROM userdata;");
            response.LSTModel = userModels;

            response.Message = string.Empty;

            return response;

        }
    }
}
