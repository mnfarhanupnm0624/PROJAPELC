using Microsoft.AspNetCore.Mvc;
using APELC.LocalServices.Login;
using APELC.PublicServices.Login;
using APELC.Model;
using System.Diagnostics;
using APELC.LocalShared;
using System.IdentityModel.Tokens.Jwt;

namespace APELC.Controllers
{
    public class LoginController : Controller
    {
        readonly static string _encryptCode = SecurityConstants.EncryptCode();
        readonly string _screenCodeFunction = "HM100";
        public IActionResult LoginPageApelC()
        {

            //return RedirectToAction("/Login/LoginPageApelC");
            return View("./Views/Login/LoginPageApelC.cshtml");
            //return View();
        }

        public IActionResult RegistrationApelC()
        {

            //return RedirectToAction("/Login/LoginPageApelC");
            return View("./Views/Login/RegistrationApelC.cshtml");
            //return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("sresu");
            HttpContext.Response.Cookies.Delete("dswap");
            HttpContext.Session.Remove("ApelCUser");
            HttpContext.Response.Cookies.Delete("SSOJWT");
            return RedirectToAction("LoginPageApelC", "Login");
            //return Redirect("Views/Login/LoginPageApelC");
            //return Redirect("https://wwww.myapelc.upnm.edu.my/");
        }

        // GET: MtdGetVerifyUserByCookies
        public IActionResult MtdGetVerifyUserByCookies()
        {
            ModelUserDTO _data = new();
            string _getCookieUser = HttpContext.Request.Cookies["sresu"] ?? "";
            string _getCookiePwsd = HttpContext.Request.Cookies["dswap"] ?? "";
            if (_getCookieUser != null && _getCookiePwsd != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string _user = _getCookieUser;
                _data.USERNAME = _getCookieUser;
                string _passwd = _getCookiePwsd;
                _data.KATALALUAN = _getCookiePwsd;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                ModelUserDTO _result = _data;
                _result.RESULTSET = "1";
                _result.RESULTSET_TEXT = "BEGIN VERIFY USER ID AND PASSWORD. ";

                //_result = LoginProcess.MtdSemakPengguna(_user, _passwd);
                _result.KOD_USER = "FFATS";
                _result.SPR_NOKP = "";

                if (_result.RESULTSET == "0")
                {
                    _data.RESULTSET = "0";
                    _data.RESULTSET_TEXT = _result.RESULTTEXT;
                }
                else
                {
                    if (_result.ROLE == "PENTADBIR" || _result.ROLE == "PEMOHON" || _result.ROLE == "PENASIHATAKAD" || _result.ROLE == "PANELPENILAI" || _result.ROLE == "MODERATOR" || _result.ROLE == "BENDAHARI" || _result.ROLE == "JKFAKULTI" || _result.ROLE == "SENAT")
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddMinutes(30);
                        HttpContext.Response.Cookies.Append("sresu", _getCookieUser, option);
                        HttpContext.Response.Cookies.Append("dswap", _getCookiePwsd, option);

                        MtdGetCreateOldStyleSession(_result);
                        MtdGetReLoginFromLocal(_user, _passwd);
                        _data.RESULTSET = "1";
                        _data.RESULTSET_TEXT = "VERIFY SUCCESSFULL";
                    }
                    else
                    {
                        return RedirectToAction("Logout");
                    }
                }
            }
            else
            {
                HttpContext.Response.Cookies.Delete("sresu");
                HttpContext.Response.Cookies.Delete("dswap");
            }
            return RedirectToAction("Dashboard", "Home");
        }

        // GET AJAX : MtdGetVerifyUserIdPasswordAjax
        [HttpPost]
        public JsonResult MtdGetVerifyUserIdPasswordAjax([FromBody] ModelUserDTO _data)
        {
            var log = NLog.LogManager.GetCurrentClassLogger();
            string _user = _data.USERNAME.ToString();
            log.Info("MtdGetVerifyUserIdPasswordAjax _user ~ " + _user);
#pragma warning disable CS8602 // Dereference of a possibly null reference.          
            string _passwd = _data.KATALALUAN.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ModelUserDTO _result = _data;
            _result.RESULTSET = "1";
            _result.RESULTSET_TEXT = "BEGIN VERIFY USER ID AND PASSWORD. ";

            //_result = LoginProcess.MtdSemakPengguna(_user, _passwd);
            _result.KOD_USER = "FFATS";
            _result.SPR_NOKP = "";

            if (_result.RESULTSET == "0")
            {
                _data.RESULTSET = "0";
                _data.RESULTSET_TEXT = _result.RESULTTEXT;
            }
            else
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Response.Cookies.Append("sresu", _user, option);
                HttpContext.Response.Cookies.Append("dswap", _passwd, option);

                MtdGetCreateOldStyleSession(_result);
                MtdGetReLoginFromLocal(_user, _passwd);
                _data.RESULTSET = "1";
                _data.RESULTSET_TEXT = "VERIFY SUCCESSFULL";
            }
            return Json(_data);
        }

        // GET : LoginJwt
        //public ActionResult LoginFromJWT()
        //{
        //    //var log = NLog.LogManager.GetCurrentClassLogger();
        //    //log.Info("LoginFromJWT Mula ~ ");
        //    string _msg = "";
        //    var _token2 = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiJ9.eyJzdWIiOiJ6YW1hbiIsInJvbGVzIjpbIlNUQUYiXSwiaXNzIjoiaWRwIiwiZnVsbG5hbWUiOiJIQUlSVVpBTUFOIEJJTiBNT0hBTUVEICAgICAgICAgICAgICAgICAgIiwiZXhwIjoxNjAzNTkwNzk1LCJpYXQiOjE2MDM1OTA2NzUsImVtYWlsIjoiemFtYW5AdXRtLm15IiwiZW1wbG95ZWVOdW1iZXIiOiI3NTkwIn0.ffHwwJYaGs8S6PBl0_fPd_UJf3C5uurWOOs9s_OG9sAF8AbmTmVz7FKHtd-UKTke1Os9w_NI40LjRcjirVDlqMVCPpIPrfvu9rE_LRRI1jXvM_hLCoQ5g-7VgsdLCp9JTLbadNMMgtwuZoxZDucX_MUFx_qVLGzZ4GzElPfF598";
        //    if (_token2 != null)
        //        //HttpContext.Request.Cookies["sresu"];
        //    if (Request.Cookies["SSOJWT"] != null)      //check cookie exist
        //    {
        //        var _token = Request.Cookies["SSOJWT"]; //.Value;   //get token from cookie   _token2;// 
        //        //var handler = new JwtSecurityTokenHandler();
        //        //var tokenS = handler.ReadToken(_token) as JwtSecurityToken;

        //        //var _userid = tokenS.Claims.First(claim => claim.Type == "sub").Value;
        //        //var _noPekerja = tokenS.Claims.First(claim => claim.Type == "employeeNumber").Value;
        //        //ModelUserDTO _userSemak = LoginProcess.MtdSemakUserFromLoginFromJWT(_userid, _noPekerja);
        //        //if (_userSemak.RESULTSET == "0")
        //        //{
        //        //    _msg = _userSemak.RESULTTEXT;
        //        //    ViewBag.Msg = _msg;
        //        //    if (_msg != "")
        //        //    {
        //        //        return RedirectToAction("Logout");
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (_result.ROLE == "PENTADBIR" || _result.ROLE == "PEMOHON" || _result.ROLE == "PENASIHATAKAD" || _result.ROLE == "PANELPENILAI" || _result.ROLE == "MODERATOR" || _result.ROLE == "BENDAHARI" || _result.ROLE == "JKFAKULTI" || _result.ROLE == "SENAT")
        //        //    {
        //        //        MtdGetsaveJWTOnSession(_userSemak, _token, new JWTModel(_token));
        //        //    }
        //        //    else
        //        //    {
        //        //        return RedirectToAction("Logout");
        //        //    }
        //        //}

        //        if (HttpContext.Session.GetString("_token") != null)               //if token exist but user goes login page
        //        {
        //            var expired_jwt = System.Convert.ToDateTime(HttpContext.Session.GetString("_expiredJWT").ToString());
        //            if (DateTime.Now < expired_jwt)         //if still valid
        //            {
        //                return RedirectToAction("Dashboard", "Home");
        //            }
        //            else    //if token already expired
        //            {
        //                return RedirectToAction("Logout");
        //            }
        //        }
        //        else   //not from my2 and token 
        //        {
        //            return RedirectToAction("MtdGetVerifyUserByCookies");
        //        }
        //    }
        //    else
        //    {
        //        //return View("Logout");
        //        //return RedirectToAction("LoginPage");
        //        return RedirectToAction("Dashboard", "Home");
        //    }
        //}

        //private bool MtdGetsaveJWTOnSession(ModelUserDTO _userSemak, string _token, JWTModel jwtmodel)
        //{
        //    MtdGetCreateOldStyleSession(_userSemak);
        //    // string _link2 = @"LocalApps?key=" + Encrypt.EncryptString4url((_userSemak.USERNAME + "~~" + _userSemak.NOPEKERJA), _encryptCode);
        //    //Session["_uR66gG88"] = _link2;
        //    HttpContext.Session.SetString("_token", _token);                                                       // save token
        //    HttpContext.Session.SetString("_expiredJWT", DateTime.Now.AddMinutes(240).ToString());                // save datetime expired
        //    //FormsAuthentication.SetAuthCookie(token, false);
        //    return true;
        //}

        private void MtdGetCreateOldStyleSession(ModelUserDTO _result)
        {
            //var log = NLog.LogManager.GetCurrentClassLogger(); 
            //log.Info("MtdGetCreateOldStyleSession _result.USERNAME ~ " + _result.USERNAME);
            //log.Info("MtdGetCreateOldStyleSession _result.NOPEKERJA ~ " + _result.NOPEKERJA);
            //log.Info("MtdGetCreateOldStyleSession _result.REALNAME ~ " + _result.REALNAME);
            //log.Info("MtdGetCreateOldStyleSession _result.EMAIL ~ " + _result.EMAIL);
            //log.Info("MtdGetCreateOldStyleSession _result.KOD_PTJ ~ " + _result.KOD_PTJ);
            //log.Info("MtdGetCreateOldStyleSession _result.KOD_USER ~ " + _result.KOD_USER);
            //log.Info("MtdGetCreateOldStyleSession _result.SPR_NOKP ~ " + _result.SPR_NOKP);
            //log.Info("MtdGetCreateOldStyleSession _result.HRSTAFFK ~ " + _result.HRSTAFFK);
#pragma warning disable CS8604 // Possible null reference argument.
            //string functions = MtdGetFunctionListString(_result.USERNAME, _screenCodeFunction, _result.HRSTAFFK, _result.NOPEKERJA);
            //log.Info("MtdGetCreateOldStyleSession functions ~ " + functions);

            HttpContext.Session.SetString("_nopekerja", _result.NOPEKERJA);
            //HttpContext.Session.SetString("_nopekerjaEnc", _result.NOPEKERJA != null && _result.NOPEKERJA != "" ? EncryptHr.NewEncrypt(_result.NOPEKERJA, _encryptCode) : "-");
            HttpContext.Session.SetString("_PENGGUNA_NAME", _result.REALNAME);
            HttpContext.Session.SetString("_countRecord", _result.COUNTRECORD);
            HttpContext.Session.SetString("_email", _result.EMAIL);
            HttpContext.Session.SetString("_kodPtj", _result.KOD_PTJ);
            //HttpContext.Session.SetString("_nokpEnc", EncryptHr.NewEncrypt(_result.NOKP, _encryptCode));
            //HttpContext.Session.SetString("_gdOk#yb", _result.KOD_USER);
            //HttpContext.Session.SetString("_pelajarId", _result.SPR_NOKP);
            //HttpContext.Session.SetString("_pelajarIdEnc", "");
            //if (_result.SPR_NOKP != null && _result.SPR_NOKP.Length > 0)
            //{
            //    HttpContext.Session.SetString("_pelajarIdEnc", EncryptHr.NewEncrypt(_result.SPR_NOKP, _encryptCode));
            //    HttpContext.Session.SetString("_nopekerjaEnc", EncryptHr.NewEncrypt(_result.SPR_NOKP, _encryptCode));
            //}
#pragma warning restore CS8604 // Possible null reference argument.
            HttpContext.Session.SetString("_userId", _result.USERNAME);
            //HttpContext.Session.SetString("_userIdEnc", EncryptHr.NewEncrypt(_result.USERNAME, _encryptCode));
            HttpContext.Session.SetString("_hrStafFk", _result.HRSTAFFK);
            //HttpContext.Session.SetString("_hrStafFkEnc", EncryptHr.NewEncrypt(_result.HRSTAFFK, _encryptCode));
            //HttpContext.Session.SetString("_functions", functions);
            HttpContext.Session.SetString("_hg%6ds", "");
            //if (functions.Contains("DEVELOPER"))
            //{
            //    HttpContext.Session.SetString("_hg%6ds", "ik&&h65NN");
            //}

            if (_result.ROLE != null)
            {
                HttpContext.Session.SetString("_hrApelCRole", _result.ROLE);
            }
            if (_result.NAMA_PERANAN != null)
            {
                HttpContext.Session.SetString("_namaperanan", _result.NAMA_PERANAN);
            }
            HttpContext.Session.SetString("_olTju7YY8", "");

        }

        private void MtdGetReLoginFromLocal(string _user, string _passwd)
        {
            string _link = "";
            if (_user != null && _passwd != null && _user.Length > 0 && _passwd.Length > 0)
            {
                //_link = EncryptHr.NewEncrypt(_user, _encryptCode) + "~~~~" + EncryptHr.NewEncrypt(_passwd, _encryptCode);
                //bool _readCard = LoginProcess.MtdGetReLoginFromLocal();
                //if (_readCard)
                //{
                //    HttpContext.Session.SetString("_uR67gG89", _link);
                //}
            }
            else
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                HttpContext.Session.SetString("_uR67gG89", null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
        }

        //private IEnumerable<string> MtdGetFunctionList(string _username, string _screenCodeFunction, string _stafFk, string _noPekerja)
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
        //private string MtdGetFunctionListString(string _username, string _screenCodeFunction, string _stafFk, string _noPekerja)
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

        //    bool _tpFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06001");
        //    if (_tpFakulti)
        //    {
        //        _newList.Add("TP06001");
        //    }
        //    bool _ppFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06002");
        //    if (_ppFakulti)
        //    {
        //        _newList.Add("PP06002");
        //    }
        //    bool _psmFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "06004");
        //    if (_ppFakulti)
        //    {
        //        _newList.Add("HM06S11B01");
        //    }
        //    bool _pghFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "03001");
        //    if (_pghFakulti)
        //    {
        //        _newList.Add("HM22S29B01");
        //    }
        //    bool _kjbFakulti = LoginProcess.MtdGetCheckApprovalStructure(_stafFk, "04001");
        //    if (_kjbFakulti)
        //    {
        //        _newList.Add("HM22S29B02");
        //    }

        //    string _balik = "";
        //    foreach (var _item in _newList)
        //    {
        //        _balik += (_item + ":");
        //    }
        //    return _balik;
        //}

        //public IActionResult CapaiGambar(string id)
        //{
        //    //var log = NLog.LogManager.GetCurrentClassLogger();
        //    //log.Info("CapaiGambar id ~ " + id);
        //    string _kod = HttpContext.Session.GetString("_gdOk#yb") ?? "-";

        //    var _host = HttpContext.Request.Host;
        //    if (id != null && id != "" && id != "-")
        //    {
        //        string _stafPk = EncryptHr.NewDecrypt(id, _encryptCode);
        //        //log.Info("CapaiGambar _noPekerja ~ " + _noPekerja);
        //        ModelUserDTO _photo = new();
        //        //_photo.HRSTAFFK = _stafPk;
        //        //_photo = LoginProcess.MtdGetPhotoStaf(_photo);

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
        //        //string wwwPath = this.Environment.WebRootPath;
        //        //string contentPath = this.Environment.ContentRootPath;
        //        var _path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //        return File(_path, "image/jpeg");
        //    }
        //}

        //public IActionResult CapaiGambarStudent(string id)
        //{
        //    //var log = NLog.LogManager.GetCurrentClassLogger();
        //    //log.Info("CapaiGambar id ~ " + id);
        //    string _kod = HttpContext.Session.GetString("_gdOk#yb") ?? "-";

        //    var _host = HttpContext.Request.Host;
        //    if (id != null && id != "" && id != "-")
        //    {
        //        //string _stafPk = EncryptHr.NewDecrypt(id, _encryptCode);
        //        string _nokp = EncryptHr.NewDecrypt(id, _encryptCode);
        //        //log.Info("CapaiGambar _noPekerja ~ " + _noPekerja);
        //        ModelUserDTO _photo = new();
        //        _photo.NOKP = _nokp;
        //        _photo = LoginDBPelajar.MtdGetPhotoStudent(_nokp);

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
        //        //string wwwPath = this.Environment.WebRootPath;
        //        //string contentPath = this.Environment.ContentRootPath;
        //        var _path = Path.Combine(_host.ToString(), _pic); //validate the path for security or use other means to generate the path.
        //        return File(_path, "image/jpeg");
        //    }
        //}

        // GET: ScreenEncryptDecrypt
        public IActionResult ScreenEncryptDecrypt()
        {
            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
            string _nopekerja = HttpContext.Session.GetString("_nopekerja") ?? "0";
            if (_ePUserId != "")
            {
                HttpContext.Session.SetString("_titleTop", "Sample");
                HttpContext.Session.SetString("_titleSmall", "Encrypt-Decrypt");
                ModelParameterHr _cariData = new();
                return View(_cariData);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                // return RedirectToAction("Logout", "Login");
            }
        }

        // GET AJAX : MtdGetEncryptCode
        [HttpPost]
//        public JsonResult MtdGetEncryptCode([FromBody] ModelParameterHr _data)
//        {
//            _data.RESULTSET = "0";
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            if (_eUserIdIn != "")
//            {
//                _data.ViewField = "";
//#pragma warning disable CS8604 // Possible null reference argument.
//                //string _viewField = EncryptHr.NewEncrypt(_data.Key, _encryptCode);
//                string _viewField = EncryptHr.NewEncrypt(_data.Key, _encryptCode);
//#pragma warning restore CS8604 // Possible null reference argument.
//                _data.ViewField = _viewField;
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }

        // GET AJAX : MtdGetDecryptCode
        [HttpPost]
//        public JsonResult MtdGetDecryptCode([FromBody] ModelParameterHr _data)
//        {
//            _data.RESULTSET = "0";
//            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
//            if (_eUserIdIn != "")
//            {
//                _data.ViewField = "";
//#pragma warning disable CS8604 // Possible null reference argument.
//                //string _viewField = EncryptHr.DecryptString4url(_data.Value, _encryptCode);
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

        // GET AJAX : MtdGetEncryptCode8bit
        [HttpPost]
//        public JsonResult MtdGetEncryptCode8bit([FromBody] ModelParameterHr _data)
//        {
//            _data.RESULTSET = "0";
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            if (_eUserIdIn != "")
//            {
//                _data.ViewField = "";
//#pragma warning disable CS8604 // Possible null reference argument.
//                string _viewField = EncryptHr.OldEncryptString4url(_data.Key, PublicConstant.OldEncryptCode());
//#pragma warning restore CS8604 // Possible null reference argument.
//                _data.ViewField = _viewField;
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }


        // GET AJAX : MtdGetDecryptCode8bit
        [HttpPost]
//        public JsonResult MtdGetDecryptCode8bit([FromBody] ModelParameterHr _data)
//        {
//            _data.RESULTSET = "0";
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            if (_eUserIdIn != "")
//            {
//                _data.ViewField = "";
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
        [HttpPost]
//        public JsonResult MtdGetPublicEncryptCode16bit([FromBody] ModelParameterHr _data)
//        {
//            _data.RESULTSET = "0";
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//            string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            if (_eUserIdIn != "")
//            {
//                _data.ViewField = "";
//#pragma warning disable CS8604 // Possible null reference argument.
//                string _viewField = EncryptHr.NewEncrypt(_data.Key, PublicConstant.New2022EncryptCode());
//#pragma warning restore CS8604 // Possible null reference argument.
//                _data.ViewField = _viewField;
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }


        // GET AJAX : MtdGetPublicDecryptCode16bit
        [HttpPost]
        public JsonResult MtdGetPublicDecryptCode16bit([FromBody] ModelParameterHr _data)
        {
            _data.RESULTSET = "0";
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string _eUserIdIn = "";
           //string _eUserIdIn = HttpContext.Session.GetString("_nopekerjaEnc") != null ? EncryptHr.NewDecrypt(HttpContext.Session.GetString("_nopekerjaEnc").ToString(), _encryptCode) : "";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                     if (_eUserIdIn != "")
            {
                _data.ViewField = "";
#pragma warning disable CS8604 // Possible null reference argument.
                //string _viewField = EncryptHr.NewDecrypt(_data.Value, PublicConstant.New2022EncryptCode());
                string _viewField = "";
#pragma warning restore CS8604 // Possible null reference argument.
                _data.Text = _viewField;
                return Json(_data);
            }
            else
            {
                return Json(_data);
            }
        }

    }
}
