using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using MoneerProject.Models;
using MoneerProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoneerProject.Controllers
{
    public class HomeController : Controller
    {

        private string FilePath;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PblogsContext _context;
        public HomeController(PblogsContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;  
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cover()
        {
            return View();
        }

        /// <summary>
        /// section 2
        /// </summary>
        /// <returns></returns>
        public IActionResult Section2()
        {
            return View();
        }
        public IActionResult AddSection2()
        {
            return View();
        }
        public async Task<object> GetSection2Data()
        {
            try
            {
                List<TabelSection2> data = _context.TabelSection2.ToList();
                return Json(new { success = true, data = data });
            }
            catch(Exception ex)
            { 
                return Json(new { success = false,msg="لا توجد بيانات" });
            }
        }
        public async Task<object> GetSection2ById(int section2Id)
        {
            try
            {
                TabelSection2 data = _context.TabelSection2.Where(x=>x.Section2Id==section2Id).FirstOrDefault();
                return Json(new { success = true, data = data });
            }
            catch(Exception ex)
            { 
                return Json(new { success = false,msg= "حدث خطأ" });
            }
        }
        public async Task<object> DeleteSection2(int section2Id)
        {
            try
            {
                TabelSection2 data = _context.TabelSection2.Where(x=>x.Section2Id==section2Id).FirstOrDefault();
                _context.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true});
            }
            catch(Exception ex)
            { 
                return Json(new { success = false,msg= "حدث خطأ" });
            }
        }
        [HttpPost]
        public async Task<object> PostSection2(TabelSection2 section2)
        {
            try
            {
                TabelSection2 id = new TabelSection2();
                if(section2.Section2Id==0)
                {
                    _context.Add(section2);
                }
                else
                {
                    _context.Entry(section2).State = EntityState.Modified;
                   
                }
                _context.SaveChanges();
                id = section2;
                return Json(new { success = true, data = id, msg = "تم الحفظ" }) ;
            }
            catch(Exception ex) {

                return Json(new { success = false, msg = "حدث خطأ" });
            }
        }
        [HttpPost]
        [Route("Home/UploadFile/{id}")]
        public async Task UploadFileAsync(int id)
        {
            IFormFileCollection file = Request.Form.Files;
            try
            {
                FilePath = "";
                long size = 0;

                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Value.Trim('"');
                FilePath = _hostEnvironment.WebRootPath + $@"\Image\ImageSection2\{ filename}";
                size += file[0].Length;
                await using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }

                TabelSection2 section2s = _context.TabelSection2.Where(x => x.Section2Id == id).FirstOrDefault() ;
                section2s.Image =  $@"\Image\ImageSection2\{ filename}";
                _context.Entry(section2s).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            { 
            }
        }
        [HttpPost]
        [Route("Home/UploadMp3/{id}")]
        public async Task UploadMp3Async(int id)
        {
            IFormFileCollection file = Request.Form.Files;
            try
            {
                FilePath = "";
                long size = 0;

                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Value.Trim('"');
                FilePath = _hostEnvironment.WebRootPath + $@"\mp3\section2\{ filename}";
                size += file[0].Length;
                await using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }

                TabelSection2 section2s = _context.TabelSection2.Where(x => x.Section2Id == id).FirstOrDefault() ;
                section2s.PathMp3 =  $@"/mp3/section2/{ filename}";
                _context.Entry(section2s).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            { 
            }
        }

        /// <summary>
        /// section 3
        /// </summary>
        /// <returns></returns>
        public IActionResult Section3()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("login", "Home");
            }
            else if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("login", "Home");
            }
            return View();
        }
        public IActionResult AddSection3()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("login", "Home");
            }
            else if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("login", "Home");
            }
            return View();
        }
        public async Task<object> GetSection3Data()
        {
            try
            {
                int userid = 0;
                if (HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("login", "Home");
                }
                else if (HttpContext.Session.GetInt32("UserId") == 0) { 
                    return RedirectToAction("login", "Home");
                }
                else
                {
                    userid = (int)HttpContext.Session.GetInt32("UserId");
                    List<TableSection3> data = _context.TableSection3.Where(y => y.UserId == userid).ToList();
                    return Json(new { success = true, data = data });
                }
            }
            catch(Exception ex)
            { 
                return Json(new { success = false,msg="لا توجد بيانات" });
            }
        }
        public async Task<object> GetSection3ById(int section3Id)
        {
            try
            {
                TableSection3 data = _context.TableSection3.Where(x => x.Section3Id == section3Id).FirstOrDefault();
                return Json(new { success = true, data = data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "حدث خطأ" });
            }
        }
        public async Task<object> DeleteSection3(int section3Id)
        {
            try
            {
                TableSection3 data = _context.TableSection3.Where(x => x.Section3Id == section3Id).FirstOrDefault(); 
                _context.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "حدث خطأ" });
            }
        }

        [HttpPost]
        public async Task<object> PostSection3(TableSection3 section3)
        {
            try
            {
                TableSection3 id = new TableSection3();

                section3.UserId = (int)HttpContext.Session.GetInt32("UserId");
                if (section3.Section3Id == 0)
                {
                    _context.Add(section3);
                }
                else
                {
                    _context.Entry(section3).State = EntityState.Modified;

                }
                _context.SaveChanges();
                id = section3;
                return Json(new { success = true, data = id, msg = "تم الحفظ" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, msg = "حدث خطأ" });
            }
        }
        [HttpPost]
        [Route("Home/UploadFile2/{id}")]
        public async Task UploadFile2Async(int id)
        {
            IFormFileCollection file = Request.Form.Files;
            try
            {
                FilePath = "";
                long size = 0;

                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Value.Trim('"');
                FilePath = _hostEnvironment.WebRootPath + $@"\Image\ImageSection3\{ filename}";
                size += file[0].Length;
                await using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }

                TableSection3 section3s = _context.TableSection3.Where(x => x.Section3Id == id).FirstOrDefault();
                section3s.Image = $@"\Image\ImageSection3\{ filename}";
                _context.Entry(section3s).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        [HttpPost]
        [Route("Home/UploadMp32/{id}")]
        public async Task UploadMp32Async(int id)
        {
            IFormFileCollection file = Request.Form.Files;
            try
            {
                FilePath = "";
                long size = 0;

                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Value.Trim('"');
                FilePath = _hostEnvironment.WebRootPath + $@"\mp3\section3\{ filename}";
                size += file[0].Length;
                await using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }

                TableSection3 section3s = _context.TableSection3.Where(x => x.Section3Id == id).FirstOrDefault();
                section3s.PathMp3 = $@"/mp3/section3/{ filename}";
                _context.Entry(section3s).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// login and sign up
        /// </summary>
        /// <returns></returns>

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = _context.Users.Where(a => a.Name.Equals(objUser.Name) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    HttpContext.Session.SetInt32("UserId", obj.UserId); 
                    return RedirectToAction("Section3");
                }
            }
            return View(objUser);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Users objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   _context.Add(objUser);
                    _context.SaveChanges(); 

                    HttpContext.Session.SetInt32("UserId", objUser.UserId);
                    return RedirectToAction("Section3"); 
                }
            }
            catch(Exception ex) { }
            return View(objUser);
        }
    }
}
