using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using APELC.LocalServices.Login;
using APELC.LocalShared;
using APELC.Model;
using APELC.Data;
using APELC.Helper;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.IQueryable;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Net6HrPublicLibrary.PublicServices.Login;
using Net6HrPublicLibrary.PublicShared;
using Net6HrPublicLibrary.Model;
using MySqlConnector;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Web;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System.Net;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;
using static Microsoft.Extensions.Configuration.IConfigurationBuilder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Grpc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
//using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.Data.MySqlClient;
using APELC.LocalServices.Senarai;
using Google.Api;



namespace APELC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LoginController> _logger;
        //ModelParameterAPELC _data = new();
        //public readonly ModelParameterAPELC _context;
        private readonly ApplicationDbContext _context;
        //public static string _encryptCode = SecurityConstantsLocal.EncryptCode();
        public string _screenCodeFunction = "HM100";
        static readonly string ConnMySQLUpnm = LocalConstant.ConnMySQLUpnmDbDs();

        //public LoginController(IConfiguration configuration, IWebHostEnvironment environment)
        //{
        //_configuration = configuration;
        //_environment = environment;
        //}

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }


        //public class DbContext : IDisposable, System.Data.Entity.Infrastructure.IObjectContextAdapter;
        //public class DaftarAkaunController : Controller
        //{
        //
        // 1. Action method for displaying 'Sign Up' page
        //
        //public ActionResult DaftarAkaun()
        //{
        //    // Let's get all states that we need for a DropDownList
        //    var states = GetAllStates();

        //    var model = new ModelParameterAPELC();

        //    // Create a list of SelectListItems so these can be rendered on the page
        //    model.States = GetSelectListItems(states);

        //    return View(model);
        //}

        //
        // 2. Action method for handling user-entered data when 'Sign Up' button is pressed.
        //
        //[HttpPost]
        //public ActionResult DaftarAkaun(DaftarAkaunModel model)
        //{
        //    // Get all states again
        //    var states = DaftarAkaun();

        //    // Set these states on the model. We need to do this because
        //    // only the selected value from the DropDownList is posted back, not the whole
        //    // list of states.
        //    model.States = GetSelectListItems(states);

        //    // In case everything is fine - i.e. both "Name" and "State" are entered/selected,
        //    // redirect user to the "Done" page, and pass the user object along via Session
        //    if (ModelState.IsValid)
        //    {
        //        Session["DaftarAkaunModel"] = model;
        //        return RedirectToAction("Done");
        //    }

        //    // Something is not right - so render the registration page again,
        //    // keeping the data user has entered by supplying the model.
        //    return View("DaftarAkaun", model);
        //}


        // 3. Action method for displaying 'Done' page

        //public ActionResult Done()
        //{
        //    // Get Sign Up information from the session
        //    var model = Session["DaftarAkaunModel"] as DaftarAkaun;

        //    // Display Done.html page that shows Name and selected state.
        //    return View(model);
        //}

        // Just return a list of states - in a real-world application this would call
        // into data access layer to retrieve states from a database.
        //private IEnumerable<string> DaftarAkaunModel()
        //{
        //    return new List<string>
        //{
        //    "ACT",
        //    "New South Wales",
        //    "Northern Territories",
        //    "Queensland",
        //    "South Australia",
        //    "Victoria",
        //    "Western Australia",
        //};
        //}

        // This is one of the most important parts in the whole example.
        // This function takes a list of strings and returns a list of SelectListItem objects.
        // These objects are going to be used later in the SignUp.html template to render the
        // DropDownList.
        //    private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        //    {
        //        // Create an empty list to hold result of the operation
        //        var selectList = new List<SelectListItem>();

        //        // For each string in the 'elements' variable, create a new SelectListItem object
        //        // that has both its Value and Text properties set to a particular value.
        //        // This will result in MVC rendering each item as:
        //        //     <option value="State Name">State Name</option>
        //        foreach (var element in elements)
        //        {
        //            selectList.Add(new SelectListItem
        //            {
        //                Value = element,
        //                Text = element
        //            });
        //        }

        //        return selectList;
        //    }
        //}

        //private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<ModelParameterAPELC> elements)
        //{
        //    //Create an empty list to hold result of the operation
        //    var selectList = new List<SelectListItem>();
        //    ModelParameterAPELC _data = new();
        //    ModelParameterAPELC _context = _data;

        //    //For each string in the 'elements' variable, create a new SelectListItem object
        //   // that has both its Value and Text properties set to a particular value.
        //    // This will result in MVC rendering each item as:
        //         //< option value = "State Name" > State Name </ option >
        //    foreach (var element in elements)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = element,
        //            Text = element
        //        });
        //    }

        //    return selectList;
        //}

        [HttpGet]
        public IActionResult LoginPageApelC(ModelParameterAPELC? _context, string ConnMySQLUpnm)
        {

            // To resolve the error in the example, the first step is to
            // move the declaration of db out of the try block. The following
            // declaration is available throughout the Main method.
            //MyClass2 db = null;
            //try
            //{
            // Inside the try block, use the db variable that you declared
            // previously.
            //    db = new MyClass2();
            //}
            //catch (Exception e)
            //{
            // The following expression no longer causes an error, because
            // the declaration of db is in scope.
            //    if (db != null)
            //        Console.WriteLine("{0}", e);
            //}
            //var model = new ModelParameterAPELC();
            //model.NAMA_PARAMETER = new SelectList(ConnMySQLUpnm.NAMA_PARAMETER, "Idjenis_APEL_param", "Idjenis_APEL_param", 1);
            //ModelParameterAPELC _data = new();
            //ModelParameterAPELC _context = _data;
            //string _jenisapel = _context.NAMA_PARAMETER;
            //string _katpenggunadaftarakaun = _context.NAMA_PARAMETER;
            //string _katperanansuperuserdaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananpelajardaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananluarupnmdaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananurusetiaapeldaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananurusetiafakultidaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananbendaharidaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananpenasihatakaddaftarakaun = _context.NAMA_PARAMETER;
            //ModelParameterAPELC _jenisapel = _context.NAMA_PARAMETER;
            //string _katpenggunadaftarakaun = _context.NAMA_PARAMETER;
            //string _katperanansuperuserdaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananpelajardaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananluarupnmdaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananurusetiaapeldaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananurusetiafakultidaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananbendaharidaftarakaun = _context.NAMA_PARAMETER;
            //string _katperananpenasihatakaddaftarakaun = _context.NAMA_PARAMETER;
            //List<ModelParameterAPELC> _newList = null;
            //int selectedId = 1;
            //ViewBag.ListjenisApelSelect = new SelectList(ConnMySQLUpnm, "Idjenis_APEL_param", "Namajenis_APEL_param", selectedId);
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            ViewBag.ListjenisApelSelect = _context.SelectListjenisApel.ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
            var _jenisapel = ViewBag.ListjenisApelSelect;
            //ViewBag.ListofPublisher = context.Publisher.ToList();
            //ViewBag.ListofCategory = context.Category.ToList();
            //int selectedId = 1;
            //ViewBag.ItemsSelect = new SelectList(ConnMySQLUpnm.Items, "Idjenis_APEL_param", "Idjenis_APEL_param", selectedId);
            List<ModelParameterAPELC> _ListjenisApel = LoginProcess.MtdGetDropdownListjenisApel(_jenisapel, ConnMySQLUpnm);
            //string SilaPiih = "--Sila Pilih--";
            //_jenisApel.Add(new ListJenisAPEL() { Id = 0, Name = SilaPiih });
            //_katpenggunaupnm.Add(new ListKatPenggunaUPNM() { Id = 0, Name = SilaPiih });
            //var viewModel = new ModelParameterAPELC(); 
            //viewModel.Worksites = db.Worksites;
            ////var allCountries = countryRepository.GetAllCountries();
            ////var items = new List<SelectListItem>();
            ////model.Countries = items
            ///
            //ViewData["JenisApelData"] = new SelectList(_jenisapel.OrderBy(s => s.Id), "Id", "Name");
            //ViewData["KatPenggunaData"] = new SelectList(_katpenggunadaftarakaun.OrderBy(s => s.Id), "Id", "Name");
            //string  _functionListjenisApel = LoginProcess.MtdGetDropdownListjenisApel(_jenisapel,ConnMySQLUpnm);
            //string _functionListkatPenggunaDaftarAkaun = LoginProcess.MtdGetDropdownListkatPenggunaDaftarAkaun(_katpenggunadaftarakaun,ConnMySQLUpnm);           
            //string _functionListkatPerananSuperuserDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananSuperuserDaftarAkaun(_katperanansuperuserdaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananPelajarDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananPelajarDaftarAkaun(_katperananpelajardaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananLuarUPNMDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananLuarUPNMDaftarAkaun(_katperananluarupnmdaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananUrusetiaAPELDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananUrusetiaAPELDaftarAkaun(_katperananurusetiaapeldaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananUrusetiaFakultiDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananUrusetiaFakultiDaftarAkaun(_katperananurusetiafakultidaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananBendahariDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananBendahariDaftarAkaun(_katperananbendaharidaftarakaun,ConnMySQLUpnm);
            //string _functionListkatPerananPenasihatAkadDaftarAkaun = LoginProcess.MtdGetDropdownListkatPerananPenasihatAkadDaftarAkaun(_katperananpenasihatakaddaftarakaun,ConnMySQLUpnm);
            string host = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
            ViewData["BaseUrl"] = host;

            //return RedirectToAction("/Login/LoginPageApelC");
            return View("./Views/Login/LoginPageApelC.cshtml");
            //return View("./Views/Login/LoginPageApelC.cshtml");
            //return Ok(new
            //{
            //    Environment = _environment.EnvironmentName,
            //    "./Views/Login/LoginPageApelC.cshtml"
            //});
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPageApelC(ModelParameterAPELC? _context,ModelParameterAPELC? param)
        {
            if (ModelState.IsValid)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string result = _context.NAMA_PARAMETER;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                try
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    string _jenisapel = param.NAMA_PARAMETER;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    //result.ProductCode = (product.CategoryCode) + (product.PublisherCode) + (proCode.Substring(proCode.Length - 2));
                    //result.ProductName = product.ProductName;
                    //result.Price = product.Price;
                    //result.Quantity = product.Quantity;
                    //result.AuthorName = product.AuthorName;
                    //result.ReleaseYear = product.ReleaseYear;
                    //result.Ver = product.Ver;
                    //result.Used = product.Used;
                    //result.Review = product.Review;
                    //result.CategoryCode = product.CategoryCode;
                    //result.PublisherCode = product.PublisherCode;
                    //result.Picture = product.Picture;

                    //_context.SaveChanges();
                }
                catch (Exception exx)
                {
                    ViewBag.Msg = exx.Message;
                }

                return RedirectToAction("Login", "LoginPageApelC");
            }
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            ViewBag.ListjenisApelSelect = _context.SelectListjenisApel.ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
            return View("./Views/Login/LoginPageApelC.cshtml");
        }

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    HttpContext.Response.Cookies.Delete("sresu");
        //    HttpContext.Response.Cookies.Delete("dswap");
        //    HttpContext.Session.Remove("ApelCUser");
        //    HttpContext.Response.Cookies.Delete("SSOJWT");
        //    return RedirectToAction("LoginPageApelC", "Login");
        //    //return Redirect("Views/Login/LoginPageApelC");
        //    //return Redirect("https://wwww.myapelc.upnm.edu.my/");
        //}

        // GET: MtdGetVerifyUserByCookies
        //        public IActionResult MtdGetVerifyUserByCookies()
        //        {
        //            SessionModel _data = new();
        //            string _getCookieUser = HttpContext.Request.Cookies["sresu"] ?? "";
        //            string _getCookiePwsd = HttpContext.Request.Cookies["dswap"] ?? "";
        //            if (_getCookieUser != null && _getCookiePwsd != null)
        //            {
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //                string _user = _getCookieUser;
        //                _data.ID_PENGGUNA = _getCookieUser;
        //                string _passwd = _getCookiePwsd;
        //                _data.KATA_LALUAN_PENGGUNA = _getCookiePwsd;
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //                SessionModel _result = _data;
        //                _result.RESULTSET = "1";
        //                _result.RESULTSET_TEXT = "BEGIN VERIFY USER ID AND PASSWORD. ";

        //                //_result = LoginProcess.MtdSemakPengguna(_user, _passwd);
        //                if (_result.RESULTSET == "0")
        //                {
        //                    _data.RESULTSET = "0";
        //                    _data.RESULTSET_TEXT = _result.RESULTSET_TEXT;
        //                }
        //                else
        //                {
        //                    if (_result.JENIS_PERANAN_FK == "PENGGUNA SUPER" || _result.JENIS_PERANAN_FK == "PENTADBIR/URUSETIA(APEL)" || _result.JENIS_PERANAN_FK == "BENDAHARI" || _result.JENIS_PERANAN_FK == "Peranan" ||
        //                        _result.JENIS_PERANAN_FK == "PENGAWAS UJIAN CABARAN" || _result.JENIS_PERANAN_FK == "PANEL PENILAI" || _result.JENIS_PERANAN_FK == "MODERATOR" || _result.JENIS_PERANAN_FK == "PENASIHAT AKADEMIK" ||
        //                        _result.JENIS_PERANAN_FK == "PENGGUBAL DOKUMEN" || _result.JENIS_PERANAN_FK == "PENILAI INSTRUMEN" || _result.JENIS_PERANAN_FK == "JK FAKULTI(TIMBALAN DEKAN)" || _result.JENIS_PERANAN_FK == "SENAT(DEKAN)" ||
        //                        _result.JENIS_PERANAN_FK == "JK FAKULTI(KERANI JABATAN)")
        //                    {
        //                        CookieOptions option = new CookieOptions();
        //                        option.Expires = DateTime.Now.AddMinutes(30);
        //                        HttpContext.Response.Cookies.Append("sresu", _getCookieUser, option);
        //                        HttpContext.Response.Cookies.Append("dswap", _getCookiePwsd, option);
        //                        MtdGetCreateOldStyleSession(_result);
        //                        MtdGetReLoginFromLocal(_user, _passwd);
        //                        _data.RESULTSET = "1";
        //                        _data.RESULTSET_TEXT = "VERIFY SUCCESSFULL";
        //                    }
        //                    else
        //                    {
        //                        return RedirectToAction("Logout");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                HttpContext.Response.Cookies.Delete("sresu");
        //                HttpContext.Response.Cookies.Delete("dswap");
        //            }
        //            return RedirectToAction("Dashboard", "Home");
        //        }

        //private void MtdGetPenggunaSession(string id, string id2)
        //{
        //    HttpContext.Session.SetString("_titleTop", "APELC UPNM");
        //    HttpContext.Session.SetString("_titleSmall", id);
        //    HttpContext.Session.SetString("_titleSmallSub", id2);
        //}

        // GET AJAX : MtdGetVerifyUserIdPasswordAjax
        //        [HttpPost]
        //        public JsonResult MtdGetVerifyUserIdPasswordAjax([FromBody] PenggunaApelCMain _data)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            string _user = _data.ID_PENGGUNA.ToString();
        //            log.Info("MtdGetVerifyUserIdPasswordAjax _user ~ " + _user);
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.          
        //            string _passwd = _data.KATA_LALUAN_PENGGUNA.ToString();
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            PenggunaApelCMain _result = _data;
        //            _result.RESULTSET = "1";
        //            _result.RESULTSET_TEXT = "BEGIN VERIFY USER ID AND PASSWORD. ";

        //            _result = LoginProcess.MtdSemakPengguna(_user, _passwd);
        //            _result.KOD_USER = "FFATS";
        //            _result.SPR_NOKP = "";

        //            if (_result.RESULTSET == "0")
        //            {
        //                _data.RESULTSET = "0";
        //                _data.RESULTSET_TEXT = _result.RESULTSET_TEXT;
        //            }
        //            else
        //            {
        //                CookieOptions option = new CookieOptions();
        //                option.Expires = DateTime.Now.AddMinutes(30);
        //                HttpContext.Response.Cookies.Append("sresu", _user, option);
        //                HttpContext.Response.Cookies.Append("dswap", _passwd, option);

        //                MtdGetCreateOldStyleSession(_result);
        //                MtdGetReLoginFromLocal(_user, _passwd);
        //                _data.RESULTSET = "1";
        //                _data.RESULTSET_TEXT = "VERIFY SUCCESSFULL";
        //            }
        //            return Json(_data);
        //        }

        //GET : LoginJwt
        //public ActionResult LoginFromJWT()
        //{
        //    var log = NLog.LogManager.GetCurrentClassLogger();
        //    log.Info("LoginFromJWT Mula ~ ");
        //    string _msg = "";
        //    var _token2 = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiJ9.eyJzdWIiOiJ6YW1hbiIsInJvbGVzIjpbIlNUQUYiXSwiaXNzIjoiaWRwIiwiZnVsbG5hbWUiOiJIQUlSVVpBTUFOIEJJTiBNT0hBTUVEICAgICAgICAgICAgICAgICAgIiwiZXhwIjoxNjAzNTkwNzk1LCJpYXQiOjE2MDM1OTA2NzUsImVtYWlsIjoiemFtYW5AdXRtLm15IiwiZW1wbG95ZWVOdW1iZXIiOiI3NTkwIn0.ffHwwJYaGs8S6PBl0_fPd_UJf3C5uurWOOs9s_OG9sAF8AbmTmVz7FKHtd-UKTke1Os9w_NI40LjRcjirVDlqMVCPpIPrfvu9rE_LRRI1jXvM_hLCoQ5g-7VgsdLCp9JTLbadNMMgtwuZoxZDucX_MUFx_qVLGzZ4GzElPfF598";
        //    if (_token2 != null)
        //        //HttpContext.Request.Cookies["sresu"];
        //        if (Request.Cookies["SSOJWT"] != null)      //check cookie exist
        //        {
        //            var _token = Request.Cookies["SSOJWT"]; //.Value;   //get token from cookie   _token2;// 
        //            var handler = new JwtSecurityTokenHandler();
        //            var tokenS = handler.ReadToken(_token) as JwtSecurityToken;

        //            var _userid = tokenS.Claims.First(claim => claim.Type == "sub").Value;
        //            //var _noPekerja = tokenS.Claims.First(claim => claim.Type == "employeeNumber").Value;
        //            SessionModel _userSemak = LoginProcess.MtdSemakUserFromLoginFromJWT(_userid);
        //            if (_userSemak.RESULTSET == "0")
        //            {
        //                _msg = _userSemak.RESULTSET_TEXT;
        //                ViewBag.Msg = _msg;
        //                if (_msg != "")
        //                {
        //                    return RedirectToAction("Logout");
        //                }
        //            }
        //            else
        //            {
        //                if (_result.JENIS_PERANAN_FK == "PENTADBIR" || _result.JENIS_PERANAN_FK == "Peranan" || _result.JENIS_PERANAN_FK == "PENASIHATAKAD" || _result.JENIS_PERANAN_FK == "PANELPENILAI" || _result.JENIS_PERANAN_FK == "MODERATOR" || _result.JENIS_PERANAN_FK == "BENDAHARI" || _result.JENIS_PERANAN_FK == "JKFAKULTI" || _result.JENIS_PERANAN_FK == "SENAT")
        //                {
        //                    MtdGetsaveJWTOnSession(_userSemak, _token, new JWTModel(_token));
        //                }
        //                else
        //                {
        //                    return RedirectToAction("Logout");
        //                }
        //            }

        //            if (HttpContext.Session.GetString("_token") != null)               //if token exist but user goes login page
        //            {
        //                var expired_jwt = System.Convert.ToDateTime(HttpContext.Session.GetString("_expiredJWT").ToString());
        //                if (DateTime.Now < expired_jwt)         //if still valid
        //                {
        //                    return RedirectToAction("Dashboard", "Home");
        //                }
        //                else    //if token already expired
        //                {
        //                    return RedirectToAction("Logout");
        //                }
        //            }
        //            else   //not from my2 and token 
        //            {
        //                return RedirectToAction("MtdGetVerifyUserByCookies");
        //            }
        //        }
        //        else
        //        {
        //            //return View("Logout");
        //            //return RedirectToAction("LoginPage");
        //            return RedirectToAction("Dashboard", "DashboardStatistik");
        //        }
        //}
        //private bool savePenggunaOnSession(PenggunaApelCMain ApelCModel)
        //{
        //    var ApelCHelper = new ApelCHelper();
        //    HttpContext.Session.SetString("ApelCUser", JsonConvert.SerializeObject(ApelCHelper.GetApelCInfo(ApelCModel)));
        //    return true;
        //}

        //private bool MtdGetsaveJWTOnSession(SessionModel _userSemak, string _token, JWTModel jwtmodel)
        //{
        //    MtdGetCreateOldStyleSession(_userSemak);
        //    string _link2 = @"LocalApps?KOD=" + EncryptLocal.EncryptString4url((_userSemak.ID_PENGGUNA + "~~" + _userSemak.KATA_LALUAN_PENGGUNA), _encryptCode);
        //    Session["_uR66gG88"] = _link2;
        //    HttpContext.Session.SetString("_token", _token);                                                       // save token
        //    HttpContext.Session.SetString("_expiredJWT", DateTime.Now.AddMinutes(240).ToString());                // save datetime expired
        //    //FormsAuthentication.SetAuthCookie(token, false);
        //    return true;
        //}

        //        private void MtdGetCreateOldStyleSession(SessionModel _result)
        //        {
        //            //Log info Nlog store session and generate log file
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("MtdGetCreateOldStyleSession _result.ID_PENGGUNA ~ " + _result.ID_PENGGUNA);
        //            //log.Info("MtdGetCreateOldStyleSession _result.NO_PEKERJA ~ " + _result.NO_PEKERJA);
        //            //log.Info("MtdGetCreateOldStyleSession _result.REALNAME ~ " + _result.REALNAME);
        //            //log.Info("MtdGetCreateOldStyleSession _result.EMAIL ~ " + _result.EMAIL);
        //            //log.Info("MtdGetCreateOldStyleSession _result.KOD_PTJ ~ " + _result.KOD_PTJ);
        //            //log.Info("MtdGetCreateOldStyleSession _result.KOD_USER ~ " + _result.KOD_USER);
        //            //log.Info("MtdGetCreateOldStyleSession _result.SPR_NOKP ~ " + _result.SPR_NOKP);
        //            //log.Info("MtdGetCreateOldStyleSession _result.HRSTAFFK ~ " + _result.HRSTAFFK);
        //#pragma warning disable CS8604 // Possible null reference argument.
        //            string functions = MtdGetFunctionListString(_result.ID_PENGGUNA, _screenCodeFunction);
        //            log.Info("MtdGetCreateOldStyleSession functions ~ " + functions);

        //            HttpContext.Session.SetString("_idpengguna", _result.ID_PENGGUNA);
        //            HttpContext.Session.SetString("_idpenggunaEnc", _result.ID_PENGGUNA != null && _result.ID_PENGGUNA != "" ? EncryptLocal.EncryptString(_result.ID_PENGGUNA, _encryptCode) : "-");
        //            //HttpContext.Session.SetString("_PENGGUNA_NAME", _result.REALNAME);
        //            //HttpContext.Session.SetString("_countRecord", _result.COUNTRECORD);
        //            //HttpContext.Session.SetString("_email", _result.EMAIL);
        //            //HttpContext.Session.SetString("_kodPtj", _result.KOD_PTJ);
        //            //HttpContext.Session.SetString("_nokpEnc", EncryptHr.NewEncrypt(_result.NO_KP, _encryptCode));
        //            //HttpContext.Session.SetString("_gdOk#yb", _result.KOD_USER);
        //            //HttpContext.Session.SetString("_pelajarId", _result.SPR_NOKP);
        //            //HttpContext.Session.SetString("_pelajarIdEnc", "");
        //            //if (_result.SPR_NOKP != null && _result.SPR_NOKP.Length > 0)
        //            //{
        //            //    HttpContext.Session.SetString("_pelajarIdEnc", EncryptHr.NewEncrypt(_result.SPR_NOKP, _encryptCode));
        //            //    HttpContext.Session.SetString("_nopekerjaEnc", EncryptHr.NewEncrypt(_result.SPR_NOKP, _encryptCode));
        //            //}
        //#pragma warning restore CS8604 // Possible null reference argument.
        //            HttpContext.Session.SetString("_userId", _result.ID_PENGGUNA);
        //            HttpContext.Session.SetString("_userIdEnc", EncryptLocal.EncryptString(_result.ID_PENGGUNA, _encryptCode));
        //            //HttpContext.Session.SetString("_hrStafFk", _result.HRSTAFFK);
        //            //HttpContext.Session.SetString("_hrStafFkEnc", EncryptHr.NewEncrypt(_result.HRSTAFFK, _encryptCode));
        //            //HttpContext.Session.SetString("_functions", functions);
        //            HttpContext.Session.SetString("_hg%6ds", "");
        //            //if (functions.Contains("DEVELOPER"))
        //            //{
        //            //    HttpContext.Session.SetString("_hg%6ds", "ik&&h65NN");
        //            //}

        //            if (_result.JENIS_PERANAN_FK != null)
        //            {
        //                HttpContext.Session.SetString("_hrApelCRole", _result.JENIS_PERANAN_FK);
        //            }
        //            //if (_result.NAMA_PERANAN != null)
        //            //{
        //            //    HttpContext.Session.SetString("_namaperanan", _result.NAMA_PERANAN);
        //            //}
        //            HttpContext.Session.SetString("_olTju7YY8", "");

        //        }

//        private void MtdGetReLoginFromLocal(string _user, string _passwd)
//        {
//            //string _link = "";
//            if (_user != null && _passwd != null && _user.Length > 0 && _passwd.Length > 0)
//            {
//                //_link = EncryptHr.NewEncrypt(_user, _encryptCode) + "~~~~" + EncryptHr.NewEncrypt(_passwd, _encryptCode);
//                //bool _readCard = LoginProcess.MtdGetReLoginFromLocal();
//                //if (_readCard)
//                //{
//                //    HttpContext.Session.SetString("_uR67gG89", _link);
//                //}
//            }
//            else
//            {
//#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
//                HttpContext.Session.SetString("_uR67gG89", null);
//#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
//            }
//        }

        //private IEnumerable<string> MtdGetFunctionList(string _username, string _screenCodeFunction)
        //{
        //    IEnumerable<string> _function = LoginProcess.MtdGetFunctions(_username, _screenCodeFunction);
        //    IEnumerable<string> _function2 = LoginProcess.MtdGetFunctions(_username, "HKP");
        //    List<string> _newList = _function.ToList();
        //    foreach (var _item in _function2)
        //    {
        //        _newList.Add(_item);
        //    }
        //    _function = _newList;
        //    return _function;
        //}
        //private IEnumerable<string> MtdGetFunctionListString(string _username, string _screenCodeFunction)
        //{
        //    List<string> _function = LoginProcess.MtdGetFunctions(_username, _screenCodeFunction).ToList();
        //    IEnumerable<string> _function2 = LoginProcess.MtdGetFunctions(_username, "HM98");
        //    IEnumerable<string> _function3 = LoginProcess.MtdGetFunctions(_username, "HM06");
        //    IEnumerable<string> _function4 = LoginProcess.MtdGetFunctions(_username, "HM43");
        //    IEnumerable<string> _function5 = LoginProcess.MtdGetFunctions(_username, "HM22");
        //    List<string> _newList = _function.ToList();
        //    if (_function2.Count() > 0)
        //    {
        //        foreach (var _item in _function2)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function3.Count() > 0)
        //    {
        //        foreach (var _item in _function3)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function4.Count() > 0)
        //    {
        //        foreach (var _item in _function4)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function5.Count() > 0)
        //    {
        //        foreach (var _item in _function5)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
            //bool _tpFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06001");
            //if (_tpFakulti)
            //{
            //    _newList.Add("TP06001");
            //}
            //bool _ppFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06002");
            //if (_ppFakulti)
            //{
            //    _newList.Add("PP06002");
            //}
            //bool _psmFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06004");
            //if (_ppFakulti)
            //{
            //    _newList.Add("HM06S11B01");
            //}
            //bool _pghFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "03001");
            //if (_pghFakulti)
            //{
            //    _newList.Add("HM22S29B01");
            //}
            //bool _kjbFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "04001");
            //if (_kjbFakulti)
            //{
            //    _newList.Add("HM22S29B02");
            //}

            //string _balik = "";
            //foreach (var _item in _newList)
            //{
            //    _balik += (_item + ":");
            //}
            //return _balik;
        //}

        //private IEnumerable<string> MtdGetFunctionList(string _username)
        //{
        //    IEnumerable<string> _function = LoginProcess.MtdGetFunctions(_username);     
        //    List<string> _newList = _function.ToList();
        //    foreach (var _item in _function)
        //    {
        //        _newList.Add(_item);
        //    }
        //    _function = _newList;
        //    return _function;
        //}
        //private IEnumerable<string> MtdGetFunctionListString(string _username)
        //{
        //    List<string> _function = LoginProcess.MtdGetFunctions(_username).ToList();
        //    IEnumerable<string> _function2 = LoginProcess.MtdGetFunctions(_username);
        //    IEnumerable<string> _function3 = LoginProcess.MtdGetFunctions(_username);
        //    IEnumerable<string> _function4 = LoginProcess.MtdGetFunctions(_username);
        //    IEnumerable<string> _function5 = LoginProcess.MtdGetFunctions(_username);
        //    List<string> _newList = _function.ToList();
        //    if (_function2.Count() > 0)
        //    {
        //        foreach (var _item in _function2)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function3.Count() > 0)
        //    {
        //        foreach (var _item in _function3)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function4.Count() > 0)
        //    {
        //        foreach (var _item in _function4)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }
        //    if (_function5.Count() > 0)
        //    {
        //        foreach (var _item in _function5)
        //        {
        //            _newList.Add(_item);
        //        }
        //    }

        //}

        //public IActionResult CapaiGambar(string id)
        //{
        //    var log = NLog.LogManager.GetCurrentClassLogger();
        //    log.Info("CapaiGambar id ~ " + id);
        //    string _kod = HttpContext.Session.GetString("_gdOk#yb") ?? "-";

        //    var _host = HttpContext.Request.Host;
        //    if (id != null && id != "" && id != "-")
        //    {
        //        string _stafPk = EncryptHr.NewDecrypt(id, _encryptCode);
        //        log.Info("CapaiGambar _noPekerja ~ " + _noPekerja);
        //        ModelUserDTO _photo = new();
        //        _photo.HRSTAFFK = _stafPk;
        //        _photo = LoginDBStafUPNM.MtdGetPhotoStaf(_photo);

        //        if (_photo.PHOTO != null && _photo.PHOTO.Length > 0)
        //        {
        //            return File(_photo.PHOTO, "image/jpeg");
        //        }
        //        else
        //        {
        //            string _pic = @"/images/no_image.jpg";
        //            var path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //            return File(path, "image/jpeg");
        //        }
        //    }
        //    else
        //    {
        //        string _pic = @"/images/no_image.jpg";
        //        string wwwPath = this.Environment.WebRootPath;
        //        string contentPath = this.Environment.ContentRootPath;
        //        var _path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //        return File(_path, "image/jpeg");
        //    }
        //}

        //public IActionResult CapaiGambarStudent(string id)
        //{
        //    var log = NLog.LogManager.GetCurrentClassLogger();
        //    log.Info("CapaiGambar id ~ " + id);
        //    string _kod = HttpContext.Session.GetString("_gdOk#yb") ?? "-";

        //    var _host = HttpContext.Request.Host;
        //    if (id != null && id != "" && id != "-")
        //    {
        //        string _stafPk = EncryptHr.NewDecrypt(id, _encryptCode);
        //        string _nokp = EncryptHr.NewDecrypt(id, _encryptCode);
        //        log.Info("CapaiGambar _noPekerja ~ " + _noPekerja);
        //        ModelUserDTO _photo = new();
        //        _photo.NO_KP = _nokp;
        //        _photo = LoginDBPelajarUPNM.MtdGetPhotoStudent(_nokp);

        //        if (_photo.PHOTO != null && _photo.PHOTO.Length > 0)
        //        {
        //            return File(_photo.PHOTO, "image/jpeg");
        //        }
        //        else
        //        {
        //            string _pic = @"/images/no_image.jpg";
        //            var path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //            return File(path, "image/jpeg");
        //        }
        //    }
        //    else
        //    {
        //        string _pic = @"/images/no_image.jpg";
        //        string wwwPath = this.Environment.WebRootPath;
        //        string contentPath = this.Environment.ContentRootPath;
        //        var _path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //        return File(_path, "image/jpeg");
        //    }
        //}

        //GET: ScreenEncryptDecrypt
        //public IActionResult ScreenEncryptDecrypt()
        //{
        //    string _ePUserId = HttpContext.Session.GetString("_idpenggunaEnc") ?? "";
        //    //string _nopekerja = HttpContext.Session.GetString("_nopekerja") ?? "0";
        //    if (_ePUserId != "")
        //    {
        //        HttpContext.Session.SetString("_titleTop", "Sample");
        //        HttpContext.Session.SetString("_titleSmall", "Encrypt-Decrypt");
        //        PenggunaApelCMain _cariData = new();
        //        return View(_cariData);
        //    }
        //    else
        //    {
        //        return RedirectToAction("LoginPageApelC", "Login");
        //        // return RedirectToAction("Logout", "Login");
        //    }
        //}

        // GET AJAX : MtdGetEncryptCode
        //[HttpPost]
        //        public JsonResult MtdGetEncryptCode([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                //string _viewField = EncryptHr.NewEncrypt(_data.KOD, _encryptCode);
        //                string _viewField = EncryptHr.NewEncrypt(_data.KOD, _encryptCode);
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.NAMA_PARAMETER = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }

//        [HttpPost]
//        public JsonResult MtdGetEncryptCode([FromBody] SessionModel _dataUserIdIn)
//        {
//            _dataUserIdIn.RESULTSET = "0";
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            string _eUserIdIn = HttpContext.Session.GetString("_idpenggunaEnc") != null ? EncryptLocal.EncryptString(HttpContext.Session.GetString("_idpenggunaEnc").ToString(), _encryptCode) : "";
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            if (_eUserIdIn != "")
//            {
//                _dataUserIdIn.ID_PENGGUNA = "";
//                //string _viewField = EncryptHr.NewEncrypt(_data.KOD, _encryptCode);
//                string _viewField = EncryptLocal.EncryptString(_dataUserIdIn.ID_PENGGUNA, _encryptCode);
//                _dataUserIdIn.ID_PENGGUNA = _viewField;
//                return Json(_dataUserIdIn);
//            }
//            else
//            {
//                return Json(_dataUserIdIn);
//            }
//        }

        // GET AJAX : MtdGetDecryptCode
        //[HttpPost]
        //        public JsonResult MtdGetDecryptCode([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                string _viewField = EncryptHr.DecryptString4url(_data.Value, _encryptCode);
        //                string _viewField = EncryptHr.NewDecrypt(_data.Value, _encryptCode);
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.Text = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }

        //[HttpPost]
        //public JsonResult MtdGetDecryptCode([FromBody] SessionModel _dataUserIdIn)
        //{
        //    _dataUserIdIn.RESULTSET = "0";
        //    string _eUserIdIn = HttpContext.Session.GetString("_idpenggunaEnc") != null ? EncryptLocal.DecryptString(HttpContext.Session.GetString("_idpenggunaEnc").ToString(), _encryptCode) : "";
        //    if (_eUserIdIn != "")
        //    {
        //        _dataUserIdIn.ID_PENGGUNA = "";
        //        //string _viewField = EncryptHr.DecryptString4url(_data.Value, _encryptCode);
        //        string _viewField = EncryptLocal.DecryptString(_dataUserIdIn.ID_PENGGUNA, _encryptCode);
        //        _dataUserIdIn.ID_PENGGUNA = _viewField;
        //        return Json(_dataUserIdIn);
        //    }
        //    else
        //    {
        //        return Json(_dataUserIdIn);
        //    }
        //}

        // GET AJAX : MtdGetEncryptCode8bit
        //[HttpPost]
        //        public JsonResult MtdGetEncryptCode8bit([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                string _viewField = EncryptHr.OldEncryptString4url(_data.KOD, PublicConstant.OldEncryptCode());
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.NAMA_PARAMETER = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }


        // GET AJAX : MtdGetDecryptCode8bit
        //[HttpPost]
        //        public JsonResult MtdGetDecryptCode8bit([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                string _viewField = EncryptHr.OldDecryptString4url(_data.Value, PublicConstant.OldEncryptCode());
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.Text = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }

        // GET AJAX : MtdGetPublicEncryptCode16bit
        //[HttpPost]
        //        public JsonResult MtdGetPublicEncryptCode16bit([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                string _viewField = EncryptHr.NewEncrypt(_data.KOD, PublicConstant.New2022EncryptCode());
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.NAMA_PARAMETER = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }


        // GET AJAX : MtdGetPublicDecryptCode16bit
        //[HttpPost]
        //        public JsonResult MtdGetPublicDecryptCode16bit([FromBody] ModelParameterHr _data)
        //        {
        //            _data.RESULTSET = "0";
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //            string _eUserIdIn = "";
        //            //string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //            if (_eUserIdIn != "")
        //            {
        //                _data.NAMA_PARAMETER = "";
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                //string _viewField = EncryptHr.NewDecrypt(_data.Value, PublicConstant.New2022EncryptCode());
        //                string _viewField = "";
        //#pragma warning restore CS8604 // Possible null reference argument.
        //                _data.Text = _viewField;
        //                return Json(_data);
        //            }
        //            else
        //            {
        //                return Json(_data);
        //            }
        //        }

    }
}
