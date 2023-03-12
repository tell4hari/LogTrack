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
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Product
        public IActionResult Index(string? user)
        {
            DataTable productTable = new DataTable();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                string SQL = "Select * from [dbo].[Product]";
                sqlConnection.Open();

                SqlDataAdapter sqlData = new SqlDataAdapter(SQL, sqlConnection);
                sqlData.Fill(productTable);


            }
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                ProductViewModel item = new ProductViewModel();

                item.Id = Convert.ToInt16( productTable.Rows[i]["id"]);
                item.GameName = Convert.ToString(productTable.Rows[i]["PRDName"]);
                item.Desc = Convert.ToString(productTable.Rows[i]["PRDDesc"]);
                item.SKU = Convert.ToString(productTable.Rows[i]["PRDSKU"]);
                item.StartDate = DateTime.Now;
                item.EndDate = DateTime.Now;
                item.Price = Convert.ToDouble(productTable.Rows[i]["Price"]);
                item.SoldCount = 0;
                item.SoldValue = 0;
                item.CredatedDate = DateTime.Now;

                productViewModels.Add(item);
            }
            ViewBag.user = user;
            return View(productViewModels);
         
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("Id,GameName,Desc,SKU,StartDate,EndDate,Price,SoldCount,SoldValue,CredatedDate,CredatedBy")] ProductViewModel productViewModel)
        public ActionResult Create(List<ProductViewModel> Model)
        {
            var data = ViewBag.MyData;
            return View();
        }
        public IActionResult Edit(int? id)
        {
                      
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,GameName,Desc,SKU,StartDate,EndDate,Price,SoldCount,SoldValue,CredatedDate,CredatedBy")] ProductViewModel productViewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                  
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int? id)
        {
            
        
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
          
            return RedirectToAction(nameof(Index));
        }

    }
}
