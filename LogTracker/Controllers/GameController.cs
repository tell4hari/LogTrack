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

namespace LogTracker.Controllers
{
    public class GameController : Controller
    {
        private readonly IConfiguration _configuration;

        public GameController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Game
        public IActionResult Index()
        {

            DataTable gameTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                string SQL = "Select * from [dbo].[Game]";
                sqlConnection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(gameTable);


            }
            return View();
        }

        // GET: Game/Details/5
        public IActionResult Details(int? id)
        {
            
            return View();
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Desc,StartDate,EndDate,SoldCount,SoldValue")] GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Game/Edit/5
        public IActionResult Edit(int? id)
        {
         
            return View();
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Name,Desc,StartDate,EndDate,SoldCount,SoldValue")] GameViewModel gameViewModel)
        {
           
            return View();
        }

        // GET: Game/Delete/5
        public IActionResult Delete(int? id)
        {
            

            return View();
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
