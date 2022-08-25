using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ContactApp.Models;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]  //this method allow only get requests, return value from it is json array
        public JsonResult Get()
        {
            string query = @"
                           select CategoryId, CategoryName from
                           dbo.Category";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
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

        [HttpPost] //method to add records, return value from it is json array
        public JsonResult Post(Category cat)
        {
            string query = @"
                           insert into dbo.Category
                            values (@CategoryName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"CategoryName", cat.CategoryName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added");
        }

        [HttpPut] //method to update records, return value from it is json array
        public JsonResult Put(Category cat)
        {
            string query = @"
                           update dbo.Category
                            set CategoryName=@CategoryName
                            where CategoryId=@CategoryId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"CategoryId", cat.CategoryId);
                    myCommand.Parameters.AddWithValue(@"CategoryName", cat.CategoryName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated");
        }

        [HttpDelete("{id}")] //method to delete records based on id for ex. localhost:24292/api/category/2
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Category
                            where CategoryId=@CategoryId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"CategoryId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted");
        }

    }
}
