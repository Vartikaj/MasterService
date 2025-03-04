using CommonService.Utility;
using Dapper;
using MasterServiceDemo.Interfaces;
using MasterServiceDemo.Models;
using RabbitMQ.Client;

namespace MasterServiceDemo.Services
{
    public class UserService : IUser
    {
        private DbGateway _dbgateway;
        private readonly RabbitMQConnectionHelper _rabbitMQ;
        public UserService(string _connection)
        {
            this._dbgateway = new DbGateway(_connection);
        }

        public async Task<Response<UserModel>> GetAllData()
        {
            Response<UserModel> response = new Response<UserModel>();
            string requestQueue = "requestQueue";

            var listener = new Utility.ConsumerService(_rabbitMQ);
            listener.ConsumeQueueAsync(requestQueue);

            Console.ReadLine(); // Keep app running

            List<UserModel> userModels = await _dbgateway.ExeQueryList<UserModel>("SELECT iduserData, userDatacol, userEmail, userPhone FROM userdata;");
            response.LSTModel = userModels;

            response.Message = string.Empty;

            return response;

        }
    }
}
