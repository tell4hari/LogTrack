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
    public class SalesController : Controller
    {
        private readonly IConfiguration _configuration;

        public SalesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Sales
        public IActionResult Index()
        {
            DataTable salesTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                string SQL = "SELECT *  FROM [dbo].[Sales]";
                sqlConnection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(salesTable);


            }
            return View(salesTable);
        }

        // GET: Sales/Details/5
        public IActionResult Details(int? id)
        {
            DataTable salesTable = new DataTable();
            SalesViewModel salesViewModel = new SalesViewModel();
            //if (id == null || _context.SalesViewModel == null)
            //{
            //    return NotFound();
            //}

            //var salesViewModel = await _context.SalesViewModel
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (salesViewModel == null)
            //{
            //    return NotFound();
            //}

            return View(salesViewModel);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,LoginName,StartDate,EndDate,SoldCount,SoldValue,ShiftId,CreatedBy,CreatedDate")] SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
              
                return RedirectToAction(nameof(Index));
            }
            return View(salesViewModel);
        }

        // GET: Sales/Edit/5
        public IActionResult AddOrEdit(int? id)
        {
            SalesViewModel SalesModel = new SalesViewModel();
            if (id > 0)
                SalesModel = GetSalesDetails(id);
            return View(SalesModel);
        }
        [NonAction]
        public SalesViewModel GetSalesDetails(int? id)
        {
            SalesViewModel SalesDetails = new SalesViewModel();


            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                DataTable SalesTable = new DataTable();
                string SQL = "SELECT * FROM [DBO].[Sales] WHERE ID=" + id;
                sqlConnection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(SalesTable);

                if (id > 0)
                {
                    SalesDetails.Id = Convert.ToInt16(SalesTable.Rows[0]["id"].ToString());
                    SalesDetails.UserId = Convert.ToInt16(SalesTable.Rows[0]["UserID"].ToString());
                   // SalesDetails.LoginName = Convert.ToString(SalesTable.Rows[0]["LoginName"].ToString());
                    SalesDetails.StartDate = Convert.ToDateTime(SalesTable.Rows[0]["StartDate"].ToString());
                    SalesDetails.EndDate = Convert.ToDateTime(SalesTable.Rows[0]["EndDate"].ToString());
                    //SalesDetails.ProductId = Convert.ToInt16(SalesTable.Rows[0]["ProductID"].ToString());
                    SalesDetails.SoldCount = Convert.ToInt16(SalesTable.Rows[0]["SoldCount"]);
                    SalesDetails.SoldValue = Convert.ToInt16(SalesTable.Rows[0]["SoldValue"]);
                    SalesDetails.ShiftId = Convert.ToInt16(SalesTable.Rows[0]["ShiftID"]);
                    SalesDetails.CreatedBy = Convert.ToString(SalesTable.Rows[0]["CreatedBy"]);
                    SalesDetails.CreatedDate = Convert.ToDateTime(SalesTable.Rows[0]["CreatedDate"]);
                    
                }

            }
            return SalesDetails;
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("Id,UserId,LoginName,StartDate,EndDate,SoldCount,SoldValue,ShiftId,CreatedBy,CreatedDate")] SalesViewModel salesViewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                    {
                        string SQL = "INSERT INTO [dbo].[Sales]([UserID],[StartDate],[EndDate],[ProductID],[SoldCount],[SoldValue],[ShiftID]" +
                            ",[TimeStamp],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate]) VALUES" +
                             + salesViewModel.UserId + "," + "'" + salesViewModel.StartDate + "'," + "'" + salesViewModel.EndDate + "',"
                             + salesViewModel.ProductId + "," + salesViewModel.SoldCount+ "," + salesViewModel.ShiftId
                             + "," + salesViewModel.CreatedBy + "," + salesViewModel.CreatedDate + ")";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(SQL, sqlConnection);
                        cmd.ExecuteNonQuery();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salesViewModel);
        }

        // GET: Sales/Delete/5
        public IActionResult Delete(int? id)
        {

            SalesViewModel salesViewModel = GetSalesDetails(id);
            return View(salesViewModel);
            
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
           
            return RedirectToAction(nameof(Index));
        }

    }
}
