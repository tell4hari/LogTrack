using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogTracker.Data;
using LogTracker.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace LogTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: User
        public IActionResult Index()
        {
            DataTable userTable= new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                string SQL = "Select * from [dbo].[Users]";
                sqlConnection.Open();
                
                SqlDataAdapter  sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(userTable);


            }
            return View(userTable);
        }



        // GET: /User/AddOrEdit/5
        public IActionResult AddOrEdit(int? id)
        {
            UserViewModel userModel = new UserViewModel();
            if (id > 0)
                userModel= GetUSerDetails(id);
            return View(userModel);

        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddOrEdit(int id, [Bind("Id,LoginName,Password,Phone,Email,IsAdmin")] UserViewModel userViewModel)
        {
            //if (id != userViewModel.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) 
                    {
                        string SQL = "INSERT INTO [dbo].[Users]  ([LoginName],[PWD],[Phone],[Email],[IsAdmin]) VALUES (" +
                            "'"+ userViewModel.LoginName + "'," + "'" + userViewModel.Password + "',"+ "'" + userViewModel.Phone + "'," 
                            + "'" + userViewModel.Email + "'," + userViewModel.IsAdmin + ")";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(SQL, sqlConnection);
                        cmd.ExecuteNonQuery();

                    }
                   
                }
                catch (Exception )
                {
                     throw;
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: User/Delete/5
        public IActionResult Delete(int? id)
        {

            UserViewModel userViewModel = GetUSerDetails(id);
            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                string SQL = "DELETE FROM [DBO].[USERS] WHERE ID=" + id;
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(SQL, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public UserViewModel GetUSerDetails(int? id)
        {
            UserViewModel userDetails = new UserViewModel();


            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                DataTable userTable = new DataTable();
                string SQL = "SELECT * FROM [DBO].[USERS] WHERE ID="+ id;
                sqlConnection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(userTable);

                if(id > 0)
                {
                    userDetails.Id = Convert.ToInt16(userTable.Rows[0]["id"].ToString());
                    userDetails.LoginName = Convert.ToString(userTable.Rows[0]["LoginName"].ToString());
                    userDetails.Password = Convert.ToString(userTable.Rows[0]["PWD"].ToString());
                    userDetails.Email = Convert.ToString(userTable.Rows[0]["email"].ToString());
                    userDetails.Phone = Convert.ToString(userTable.Rows[0]["Phone"].ToString());
                    userDetails.IsAdmin = Convert.ToInt16( userTable.Rows[0]["IsAdmin"]);
                }

            }
            return userDetails;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public  ActionResult GetLoginUser(string LoginName, string Password)
        {
            UserViewModel userDetails = new UserViewModel();


            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                DataTable userTable = new DataTable();
                if (LoginName.Length >0 && Password.Length > 0)
                {
                    string SQL = "SELECT * FROM [DBO].[USERS] WHERE LoginName='" + LoginName + "' AND PWD='" + Password + "'";
                    sqlConnection.Open();

                    SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                    sqlData.Fill(userTable);

                    if (userTable.Rows.Count >0)
                    {
                        var _user = userTable.Rows[0]["LoginName"].ToString();
                       
                        //session["user"]=_user;
                      //return   RedirectToAction("Create", "Sales");
                        //userDetails.Id = Convert.ToInt16(userTable.Rows[0]["id"].ToString());
                        //userDetails.LoginName = Convert.ToString(userTable.Rows[0]["LoginName"].ToString());
                        //userDetails.Password = Convert.ToString(userTable.Rows[0]["PWD"].ToString());
                        //userDetails.Email = Convert.ToString(userTable.Rows[0]["email"].ToString());
                        //userDetails.Phone = Convert.ToString(userTable.Rows[0]["Phone"].ToString());
                        //userDetails.IsAdmin = Convert.ToInt16(userTable.Rows[0]["IsAdmin"]);
                        return RedirectToAction("Index", "Product", new { user= _user });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");

                    }

                }
           

            }

            //return userDetails;

            return RedirectToAction("Index", "Home");

        }

    }
}
