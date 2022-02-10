using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("base/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private static string _dataSource;

        public static string DataSource 
        { 
            get { return _dataSource; } 
        }

        public BaseController(IConfiguration config)
        {
            this.configuration = config;
            _dataSource = this.configuration.GetConnectionString("DefaultConnectionString");
        }

        [HttpGet]
        [Route("~/base/get2")]
        public string Get2()
        {
            
            string query = @"Select * from dbo.Employee";
            string sqlDataSource = DataSource;
            return "ggg";
        }
    }
}
