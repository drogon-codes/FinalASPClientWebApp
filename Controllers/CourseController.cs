using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PracticeClientApp.Controllers
{
    public class CourseController : Controller
    {
        private static List<MyCourse> courses = new List<MyCourse>();
        private HttpClient client = new HttpClient();
        string url = "";

        public CourseController()
        {
            url = "http://localhost:51640/api/CourseAPI";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<MyCourse> GetCourseById(int CourseId)
        {
            var msg = await client.GetAsync(url + "/" + CourseId);
            var CourseResponse = msg.Content.ReadAsStringAsync();
            MyCourse ResultCourse = JsonConvert.DeserializeObject<MyCourse>(CourseResponse.Result);
            return ResultCourse;
        }
        // GET: CourseController
        public async Task<ActionResult> Index()
        {
            var msg = await client.GetAsync(url);
            var CourseResponse = msg.Content.ReadAsStringAsync();
            courses = JsonConvert.DeserializeObject<List<MyCourse>>(CourseResponse.Result);
            return View(courses);
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await GetCourseById(id));
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyCourse CourseToAdd)
        {
            try
            {
                StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(CourseToAdd), Encoding.UTF8, "application/json"
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

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await GetCourseById(id));
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MyCourse CourseToEdit)
        {
            try
            {
                MyCourse course = await GetCourseById(id);
                course.CourseName = CourseToEdit.CourseName;

                StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json"
                    );
                var msg = await client.PutAsync(url+"/"+id, stringContent);
                var CourseResponse = msg.Content.ReadAsStringAsync();
                if (CourseResponse.Result.Contains("Conflict"))
                {
                    return RedirectToAction(nameof(Edit),id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await GetCourseById(id));
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MyCourse CourseToDelete)
        {
            try
            {
                var msg = await client.DeleteAsync(url + "/" + id);
                var CourseResponse = msg.Content.ReadAsStringAsync();
                if (CourseResponse.Result.Contains("Conflict"))
                {
                    return RedirectToAction(nameof(Delete),id);
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
