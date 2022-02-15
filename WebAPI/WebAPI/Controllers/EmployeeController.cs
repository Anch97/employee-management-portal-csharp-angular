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
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Dependency injection to access the configuration of appsettings file
        private readonly IConfiguration _configuration;
        // Dependency injection to get the application path to folder 
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // GET (READ) API method to get all department details
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeId, EmployeeName, Department,
            convert(varchar(10), DateOfJoining, 120) as DateOfJoining,
            PhotoFileName
            from dbo.Employee
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

            return new JsonResult(table);
        }

        // POST (CREATE)
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            // insert query to insert data
            string query = @"insert into dbo.Employee
            (EmployeeName, Department, DateOfJoining, PhotoFileName)
            values (
            '" + emp.EmployeeName + @"',
            '" + emp.Department + @"',
            '" + emp.DateOfJoining + @"',
            '" + emp.PhotoFileName + @"'
            )
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

            return new JsonResult("Added Successfully");
        }

        // PUT (UPDATE)
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            // update query to update data
            string query = @"
            update dbo.Employee set
            EmployeeName = '" + emp.EmployeeName + @"',
            Department = '" + emp.Department + @"',
            DateOfJoining = '" + emp.DateOfJoining + @"'
            where EmployeeId = " + emp.EmployeeId + @"
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
            delete from dbo.Employee
            where EmployeeId = " + id + @"
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            // extracting the first file which is attached in the request body
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                // saving the file in the folder and returning filename
                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        // This method is to get all the department names in the dropdown menu
        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                select DepartmentName from dbo.Department
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

            return new JsonResult(table);
        }
    }
}
