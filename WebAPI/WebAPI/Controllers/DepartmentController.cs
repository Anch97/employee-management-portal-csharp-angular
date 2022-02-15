using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // To access the configuration of appsettings file we need to make use of the dependency injection
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET (READ) API method to get all department details
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            // Variable to store database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            // Using the SqlConnection and SqlCommand we will execute our query and fill the results into a datatable
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        // POST (CREATE)
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            // insert query to insert data
            string query = @"insert into dbo.Department values ('" + dep.DepartmentName + @"')";
            DataTable table = new DataTable();
            // Variable to store database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            // Using the SqlConnection and SqlCommand we will execute our query and fill the results into a datatable
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        // PUT (UPDATE)
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            // update query to update data
            string query = @"
            update dbo.Department set
            DepartmentName = '" + dep.DepartmentName + @"'
            where DepartmentId = " + dep.DepartmentId + @"
            ";
            DataTable table = new DataTable();
            // Variable to store database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            // Using the SqlConnection and SqlCommand we will execute our query and fill the results into a datatable
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        // DELETE
        // since we are sending id in the url, we need to add id in the route parameter 
        [HttpDelete("{id}")]
        // id of department as input
        public JsonResult Delete(int id)
        {
            // update query to update data
            string query = @"
            delete from dbo.Department
            where DepartmentId = " + id + @"
            ";
            DataTable table = new DataTable();
            // Variable to store database connection string
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            // Using the SqlConnection and SqlCommand we will execute our query and fill the results into a datatable
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}