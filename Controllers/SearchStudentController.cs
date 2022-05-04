using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeClientApp.Controllers
{
    public class SearchStudentController : Controller
    {
        private StudentIndexer si = new StudentIndexer();
        private List<MyStudent> students;
        // GET: SearchStudentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SearchStudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchStudentController/Create
        public ActionResult Search()
        {
            return View();
        }

        // POST: SearchStudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string txtName)
        {
            try
            {
                students = si[txtName];
                ViewData["students"] = students;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchStudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchStudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchStudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchStudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
