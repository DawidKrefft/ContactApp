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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ContactController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get() //method to get the data, return value is json array
        {
            string query = @"
                            select ContactId, ContactName,Category,
                            convert(varchar(10),DateOfBirth,120) as DateOfBirth,PhotoFileName,PhoneNumber
                            from
                            dbo.Contact
                            ";

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

        [HttpPost] //method to add the data, return value is json array
        public JsonResult Post(Contact cont)
        {
            string query = @"
                           insert into dbo.Contact
                           (ContactName,Category,DateOfBirth,PhotoFileName,PhoneNumber)
                    values (@ContactName,@Category,@DateOfBirth,@PhotoFileName,@PhoneNumber)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactName", cont.ContactName);
                    myCommand.Parameters.AddWithValue("@Category", cont.Category);
                    myCommand.Parameters.AddWithValue("@DateOfBirth", cont.DateOfBirth);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", cont.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", cont.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added");
        }


        [HttpPut] //method to update the data, return value is json array
        public JsonResult Put(Contact cont)
        {
            string query = @"
                           update dbo.Contact
                           set ContactName= @ContactName,
                            Category=@Category,
                            DateOfBirth=@DateOfBirth,
                            PhotoFileName=@PhotoFileName,
                            PhoneNumber=@PhoneNumber
                            where ContactId=@ContactId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactId", cont.ContactId);
                    myCommand.Parameters.AddWithValue("@ContactName", cont.ContactName);
                    myCommand.Parameters.AddWithValue("@Category", cont.Category);
                    myCommand.Parameters.AddWithValue("@DateOfBirth", cont.DateOfBirth);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", cont.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", cont.PhoneNumber);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated");
        }

        [HttpDelete("{id}")] //method to delete the data based on id
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Contact
                            where ContactId=@ContactId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ContactId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted");
        }


        [Route("SaveFile")] //method to save image
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("sample.png");
            }
        }

    }
}
