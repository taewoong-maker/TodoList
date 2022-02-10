using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using TodoList.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Globalization;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : BaseController
    {
        public WeatherForecastController(IConfiguration config) : base(config)
        {
        }

        [HttpGet]
        [Route("~/api/getCount")]
        public List<EmployeeEntity> getCount()
        {
            string query = @"Select * from dbo.Employee";
            string sqlDataSource = DataSource;
            SqlDataReader myReader;
            var myList = new List<EmployeeEntity>();

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCon.Open();
                    myReader = myCommand.ExecuteReader();
                    var myDataTable = new DataTable();
                    myDataTable.Load(myReader);

                    myList = (from DataRow dr in myDataTable.Rows
                                   select new EmployeeEntity()
                                   {
                                       EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                       EmployeeName = dr["EmployeeName"].ToString(),
                                       Department = dr["Department"].ToString(),
                                       DateOfJoining = (DateTime)dr["DateOfJoining"],
                                       PhotoFileName = dr["PhotoFileName"].ToString(),
                                   }).ToList();
                }
            }
            return myList;
        }

        [HttpGet]
        [Route("~/api/get2")]
        public string Get2()
        {
            return "Hello";
        }
    }
}
