using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignatureMain.Models;
using System.Reflection;
using System.Text;

namespace SignatureMain.Controllers
{
    public class ProfileController : Controller
    {
        // GET: ProfileController
        public ActionResult Index()
        {
            return View();
        }


        // GET: ProfileController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string email)
        {
            Profile model;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7176/api/Signature?email="+email);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var Response = JsonConvert.DeserializeObject<List<Profile>>(json);
                    model = Response.FirstOrDefault();
                    return View(model);
                }
            }
            return View();
        }


        // GET: ProfileController/Create
        public ActionResult Profile()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        public ActionResult Profile(Profile model)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = null;
                HttpResponseMessage response = null;
                content= new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                response = httpClient.PostAsync("https://localhost:7176/api/Signature", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "Profile", new { email = model.Email });
                }
            }

            return RedirectToAction("Details", "Profile", new { email = model.Email });
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
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

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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
