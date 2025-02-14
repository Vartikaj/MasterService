using CommonService.Utility;
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

        public async Task<Response<object>> GetAllData()
        {
            Response<object> response = new Response<object>();

            response.Model = _dbgateway.ExeScalarQuery<object>("SELECT iduserData, userDatacol, userEmail, userPhone FROM userdata;");

            response.Message = string.Empty;

            return response;
            
        }
    }
}
