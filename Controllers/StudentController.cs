using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PracticeClientApp.Controllers
{
    public class StudentController : Controller
    {
        private List<MyStudent> students = new List<MyStudent>();
        private HttpClient client = new HttpClient();
        string url = "";

        public StudentController()
        {
            url = "http://localhost:51640/api/StudentAPI";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<MyStudent> GetStudentById(int StudentId)
        {
            var msg = await client.GetAsync(url + "/" + StudentId);
            var StudentResponse = msg.Content.ReadAsStringAsync();
            MyStudent ResultStudent = JsonConvert.DeserializeObject<MyStudent>(StudentResponse.Result);
            return ResultStudent;
        }
        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            var msg = await client.GetAsync(url);
            var StudentResponse = msg.Content.ReadAsStringAsync();
            students = JsonConvert.DeserializeObject<List<MyStudent>>(StudentResponse.Result);
            return View(students);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await GetStudentById(id));
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(MyStudent StudentToAdd)
        {
            try
            {
                StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(StudentToAdd), Encoding.UTF8, "application/json"
                    );
                var msg = await client.PostAsync(url, stringContent);
                var CourseResponse = msg.Content.ReadAsStringAsync();
                if (CourseResponse.Result.Contains("Conflict"))
                {
                    return RedirectToAction(nameof(Create));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await GetStudentById(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MyStudent StudentToEdit)
        {
            try
            {
                MyStudent student = await GetStudentById(id);
                student.StudentId = StudentToEdit.StudentId;
                student.StudentName = StudentToEdit.StudentName;
                student.Gender = StudentToEdit.Gender;
                student.Contact = StudentToEdit.Contact;
                student.CourseId = StudentToEdit.CourseId;
                StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"
                    );
                var msg = await client.PutAsync(url+"/"+id, stringContent);
                var CourseResponse = msg.Content.ReadAsStringAsync();
                if (CourseResponse.Result.Contains("Conflict"))
                {
                    return RedirectToAction(nameof(Edit), id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await GetStudentById(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var msg = await client.DeleteAsync(url + "/" + id);
                var CourseResponse = msg.Content.ReadAsStringAsync();
                if (CourseResponse.Result.Contains("Conflict"))
                {
                    return RedirectToAction(nameof(Delete), id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
