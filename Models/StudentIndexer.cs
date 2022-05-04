using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PracticeClientApp.Models
{
    public class StudentIndexer
    {
        private static List<MyStudent> students = new List<MyStudent>();
        private HttpClient client = new HttpClient();
        private string url = "";
        public StudentIndexer()
        {
            url = "http://localhost:51640/api/StudentAPI";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _ = getStudents();
        }

        private async Task getStudents()
        {
            var msg = await client.GetAsync(url);
            var StudentResponse = msg.Content.ReadAsStringAsync();
            students = JsonConvert.DeserializeObject<List<MyStudent>>(StudentResponse.Result);
        }

        public List<MyStudent> this[string QueryString]
        {
            get 
            {
                List<MyStudent> list = (from s in students
                        where s.StudentName.StartsWith(QueryString)
                        select s).ToList();
                if (list == null)
                {
                    list = (from s in students
                            where s.CourseName.StartsWith(QueryString)
                            select s).ToList();
                }
                return list;
            }
        }
    }
}
