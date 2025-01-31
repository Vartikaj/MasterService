﻿using MasterServiceDemo.Interfaces;
using MasterServiceDemo.Services;

namespace MasterServiceDemo.Utility
{
    public class ServiceToScope
    {
        public IConfiguration Configuration { get;}
        public ServiceToScope(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void AddToScope(IServiceCollection services)
        {
            services.AddTransient<IUser>(s => new UserService(Configuration.GetSection("ConnectionString:SqlConnection").Value));
        }


    }
}
