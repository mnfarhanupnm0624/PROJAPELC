using Microsoft.AspNetCore.Mvc;
using Net6HrPublicLibrary.PublicShared;
using Net6HrPublicLibrary.Model;
using APELC.LocalServices.Senarai;
using APELC.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.IQueryable;
using System.Diagnostics;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace APELC.Controllers
{
    public class SenaraiController : Controller
    {
        public IActionResult Index()
        {
            MtdGetDashboardSession("Senarai Permohonan - Index", "");
            ModelUserDTO _data = new();
            return View(_data);
        }

        public IActionResult BackToDashboard()
        {
            return Redirect("/Home/Dashboard");
        }

        private void MtdGetDashboardSession(string id, string id2)
        {
            HttpContext.Session.SetString("_titleTop", "APEL.C UPNM");
            HttpContext.Session.SetString("_titleSmall", id);
            HttpContext.Session.SetString("_titleSmallSub", id2);
        }

        public IActionResult UnderConstraction()
        {
            MtdGetDashboardSession("Under Constraction", "");
            ModelUserDTO _data = new();
            return View(_data);
        }

        // GET : SiasatanSkrinList
        public IActionResult SenaraiAksesKawalanAPELC()
        {
            MtdGetDashboardSession("Senarai Permohonan ", "");

            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
            string _stafFkEnc = HttpContext.Session.GetString("_hrStafFkEnc") ?? "";
            string _role = HttpContext.Session.GetString("_hrApelCRole") ?? "";
            string _userAll = "";
            string _userPnyst = "";
            if (_eUserIdIn != "")
            {
                if (_role == "PNYST_KANAN") {
                    _userAll = "ALL"; // Dapatkan Semua Senarai Permohonan 
                }
                else if (_role == "PNYST")
                {
                    _userPnyst = _stafFkEnc;
                }
                CarianPerananApelCMain _data = new();
                List<ModelParameterAPELC> _newList = new();
                //_newList = SenaraiProcess.MtdGetPemohonList("", "", "", "", "", "", "", _userAll, _userPnyst);
                //_data.ListCarianPeranan = _newList.ToList();

                // Clear Session
                HttpContext.Session.Remove("ApelCUser");
                HttpContext.Session.SetString("_butiranpermohonanapelc", "");
                HttpContext.Session.SetString("_ringkasankjdn", "");
                HttpContext.Session.SetString("_diaripnyst", "");
                HttpContext.Session.SetString("_frmPemohonsaksi", "");
                HttpContext.Session.SetString("_attchPemohonsaksi", "");
                HttpContext.Session.SetString("_attchokt", "");
                HttpContext.Session.SetString("_frmokt", "");
                HttpContext.Session.SetString("_attchdoclain", "");
                HttpContext.Session.SetString("_attchbrgbukti", "");
                HttpContext.Session.SetString("_minitsstn", "");
				HttpContext.Session.SetString("_cttsstn", "");
                HttpContext.Session.SetString("_rmsnsstn", "");

                return View(_data);
            }
            else
            {
                return Redirect("/Login/LoginPageApelC");
            }
        }

        [HttpPost]
        public JsonResult MtdGetSenaraiSiasatanCapai([FromBody] ModelParameterAPELC _carian)
        {
            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
            string _stafFkEnc = HttpContext.Session.GetString("_hrStafFkEnc") ?? "";
            string _role = HttpContext.Session.GetString("_hrApelCRole") ?? "";
            string _userAll = "";
            string _userPnyst = "";

            List<ModelParameterAPELC> _newList = null;
            if (_ePUserId != "")
            {
                if (_role == "PNYST_KANAN")
                {
                    _userAll = "ALL"; // Dapatkan Semua Senarai Permohonan 
                }
                else if (_role == "PNYST")
                {
                    _userPnyst = _stafFkEnc;
                }
                //_newList = SenaraiProcess.MtdGetPemohonList(_carian.CNOADUAN, _carian.CKATEGORIPemohon, _carian.CKAMPUS, _carian.CSTSPemohon, _carian.CKATEGORIADUAN, _carian.CTKH_ADUANMULA, _carian.CTKH_ADUANTAMAT, _userAll, _userPnyst);
                return Json(_newList);
            }
            else
            {
                return Json(_newList);
            }
        }
    }
}
