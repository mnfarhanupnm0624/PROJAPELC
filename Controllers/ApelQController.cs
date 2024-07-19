//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
////using Net6HrPublicLibrary.PublicShared;
//using APEL.Helper;
//using APEL.LocalServices.Siasatan;
//using APEL.LocalServices.Status;
//using APEL.LocalShared;
//using APEL.Models;
//using Newtonsoft.Json;
//using System.IO;
//using System.Net.Mail;
//using System.Text.RegularExpressions;
//using System.Xml.Linq;

//namespace APEL.Controllers
//{
//    public class ApelQController : Controller
//    {
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();
//        readonly static string _attachmentPath = SecurityConstants.AttachmentPath();

//        public IActionResult Index()
//        {
//            MtdGetDashboardSession("Siasatan - Index", "", "");
//            ModelUserDTO _data = new();
//            return View(_data);
//        }
//        public IActionResult BackToDashboard()
//        {
//            return Redirect("/Home/Dashboard");
//        }

//        private void MtdGetDashboardSession(string id, string id2, string id3)
//        {
//            HttpContext.Session.SetString("_titleTop", "KESELAMATAN UTM");
//            HttpContext.Session.SetString("_titleSmall", id);
//            HttpContext.Session.SetString("_titleSmallSub", id2);
//            HttpContext.Session.SetString("_titleSmallSubDtl", id3);
//        }

//        public IActionResult UnderConstraction()
//        {
//            MtdGetDashboardSession("Under Constraction", "", "");
//            ModelUserDTO _data = new();
//            return View(_data);
//        }

//        private bool saveAduanOnSession(CarianSiasatanMain aduanModel)
//        {
//            var aduanHelper = new ApelCHelper();
//            HttpContext.Session.SetString("ApelUser", JsonConvert.SerializeObject(aduanHelper.GetApelInfo(aduanModel)));
//            return true;
//        }

//        public string[] StoreSession(string[] str)
//        {
//            string _sts_ringkasan = ApelUser.GetApel(HttpContext.Session).STS_RNGKSAN ?? "";
//            string _statusSstn = ApelUser.GetApel(HttpContext.Session).STATUS_SSTN ?? "";
//            if (_sts_ringkasan != "APPROVED") // Status Ringkasan Kejadian
//            {
//                str[4] = str[5] = str[6] = str[7] = str[8] = str[9] = str[11] = str[12] = "text-secondary";
//            }
//            if (_statusSstn != "N") // Status Laporan Siasatan
//            {
//                str[12] = "text-secondary";
//            }

//            string[] str2 = new string[13];
//            try
//            {
//                HttpContext.Session.SetString("_butiranaduan", str[0]);
//                HttpContext.Session.SetString("_ringkasankjdn", str[2]);
//                HttpContext.Session.SetString("_diaripnyst", str[3]);
//                HttpContext.Session.SetString("_frmpengadusaksi", str[4]);
//                HttpContext.Session.SetString("_attchpengadusaksi", str[5]);
//                HttpContext.Session.SetString("_attchokt", str[6]);
//                HttpContext.Session.SetString("_frmokt", str[7]);
//                HttpContext.Session.SetString("_attchdoclain", str[8]);
//                HttpContext.Session.SetString("_attchbrgbukti", str[9]);
//                HttpContext.Session.SetString("_minitsstn", str[10]);
//                HttpContext.Session.SetString("_cttsstn", str[11]);
//                HttpContext.Session.SetString("_rmsnsstn", str[12]);
//            }
//            catch (Exception e)
//            {
//                //var log = NLog.LogManager.GetCurrentClassLogger();
//                //log.Info("SiasatanController StoreSession ~ " + e);
//            }
//            return str2;
//        }

//        // BEGIN: Kes Siasatan - Sub Topic
//        // GET PAGE : LaporanAduan
//        public IActionResult LaporanAduan(CarianSiasatanMain _dataIn)
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Laporan Aduan", "");
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.CADUAN_PK_ENC != null)
//                {
//                    CarianSiasatanMain _data = new();

//                    //save on session
//                    bool _aduanSes = saveAduanOnSession(_dataIn);
//                    if (_aduanSes == true)
//                    {
//                        string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//                        string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//                        _data = SiasatanProcess.MtdGetMaklumatAduanPenerima(_dataIn.CADUAN_PK_ENC);
//                        if (_data != null)
//                        {
//                            _data.CADUAN_PK_ENC = _aduanPkEnc;
//                            _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                        }

//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));

//                        return View(_data);
//                    }
//                    else
//                    {
//                        return RedirectToAction("SiasatanSkrinList", "Senarai");
//                    }
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        //GET VIEW : PartialPegView
//        [HttpPost]
//        public IActionResult PartialPegView([FromBody] CarianSiasatanMain _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                CarianSiasatanMain _data = new();
//                if (_dataIn.CADUAN_PK_ENC != null)
//                {
//                    _data = SiasatanProcess.MtdGetMaklumatAduanPengadu(_dataIn.CADUAN_PK_ENC);
//                    if (_data.stafPeribadi.NO_PEKERJA != null)
//                    {
//                        TempData["STAF"] = "Ya";
//                    }
//                    else if (_data.pelajarInfo.SPR_NOKP != null)
//                    {
//                        TempData["PELAJAR"] = "Ya";
//                    }
//                    else
//                    {
//                        TempData["VISITOR"] = "Ya";
//                    }
//                    return View("PartialPegView", _data);
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : Daftar
//        public IActionResult Daftar()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Daftar Penyiasat", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";
//            string _role = HttpContext.Session.GetString("_hrSiasatanRole") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianSiasatanMain _data = new();

//                    if (_role == "PNYST_KANAN")
//                    {
//                        List<ModelHrStafPenyiasat> _newList = new();
//                        _newList = SiasatanProcess.MtdGetStafPenyiasatList(_siasatanPkEnc);
//                        _data.ListCarianStaf = _newList.ToList();
//                        _data.CADUAN_PK_ENC = _aduanPkEnc;
//                        _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                        return View(_data);
//                    }
//                    else
//                    {
//                        return RedirectToAction("LaporanAduan", "Siasatan");
//                    }
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        //public IActionResult LaporanPolis()
//        //{
//        //    MtdGetDashboardSession("Senarai Siasatan", "Laporan Polis", "");

//        //    string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//        //    string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//        //    string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//        //    if (_eUserIdIn != "")
//        //    {
//        //        if (_aduanPkEnc != "")
//        //        {
//        //            CarianSiasatan _data = new();
//        //            _data = SiasatanProcess.MtdGetLaporanPolis(_aduanPkEnc, _siasatanPkEnc);
//        //            return View(_data);
//        //        }
//        //        else
//        //        {
//        //            return RedirectToAction("LaporanAduan", "Siasatan");
//        //        }
//        //    }
//        //    else
//        //    {
//        //        return Redirect("/Home/Index");
//        //    }
//        //}

//        public FileResult ViewAttachFile(string id)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            if (_ePUserId == "" || _ePUserId == null)
//            {
//                return File("", "application/pdf");
//            }
//            else
//            {
//                CarianLampiran _data = new();
//                _data = SiasatanProcess.MtdGetInvLampiranDoc(id);
//                if (_data.PATH != null && _data.NAMA_FAIL != null)
//                {
//                    string nfsFilePath = _attachmentPath + _data.PATH;
//                    string filePath = nfsFilePath;
//                    string fileName = _data.NAMA_FAIL;

//                    var path = Path.Combine((filePath), fileName);
//                    FileInfo file = new FileInfo(path);

//                    Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
//                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + fileName);
//                    if (file.Extension == ".pdf")
//                    {
//                        return File(fileBytes, "application/pdf");
//                    }
//                    if (file.Extension == ".jpeg")
//                    {
//                        return File(fileBytes, "image/jpeg");
//                    }
//                    if (file.Extension == ".png")
//                    {
//                        return File(fileBytes, "image/png");
//                    }
//                    if (file.Extension == ".mp3")
//                    {
//                        return File(fileBytes, "audio/mp3");
//                    }
//                    if (file.Extension == ".mp4")
//                    {
//                        return File(fileBytes, "video/mp4");
//                    }
//                }
//                return File("", "application/pdf");
//            }

//        }

//        public FileResult CapaiPDF(string id)
//        {
//            var _host = HttpContext.Request.Host;
//            if (id != null && id != "" && id != "-")
//            {
//                CarianLampiran _data = new();
//                _data = SiasatanProcess.MtdGetLampiranDoc(id);
//                Response.Headers.Add("Content-Disposition", "inline; filename=" + "TiadaFail");

//                if (_data.FAIL != null && _data.FAIL.Length > 0)
//                {
//                    Response.Headers.Add("Content-Disposition", "inline; filename=" + _data.NAMA_FAIL);
//                    return File(_data.FAIL, "application/pdf");
//                }
//                else
//                {
//                    string _pdf = @"/pdf/no_image.pdf";
//                    var path = Path.Combine(_host.ToString(), _pdf); //validate the path for security or use other means to generate the path.
//                    return File(path, "application/pdf");
//                }
//            }
//            else
//            {
//                string _pdf = @"/pdf/no_image.pdf";
//                var path = Path.Combine(_host.ToString(), _pdf); //validate the path for security or use other means to generate the path.
//                return File(path, "application/pdf");
//            }
//        }

//        // GET PAGE : InitialEvent
//        public IActionResult InitialEvent()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Ringkasan Kejadian", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianSiasatan _data = new();
//                    CarianSiasatanMain _session = new();

//                    //save on session
//                    _session.CADUAN_PK_ENC = _aduanPkEnc;
//                    bool _aduanSes = saveAduanOnSession(_session);
//                    if (_aduanSes == true)
//                    {
//                        _data = SiasatanProcess.MtdGetInitialEvent(_siasatanPkEnc);
//                        _data.ListCarianStaf = SiasatanProcess.MtdGetPengesahanList(_siasatanPkEnc, "5127");
//                        if (_data.ListCarianStaf.Count > 0)
//                        {
//                            ModelHrStafPenyiasat? lastItem = _data.ListCarianStaf.LastOrDefault();
//                            if (lastItem != null)
//                            {
//                                if (lastItem.STATUSAKTIF_DESC == "Hantar")
//                                {
//                                    TempData["Status"] = "disabled";
//                                }
//                            }
//                        }
//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                        return View(_data);
//                    }
//                    else
//                    {
//                        return RedirectToAction("LaporanAduan", "Siasatan");
//                    }
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : DiariPenyiasat
//        public IActionResult DiariPenyiasat()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Diari Penyiasat", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            CarianSiasatan _data = new();
//            CarianSiasatanMain _session = new();

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    //save on session
//                    _session.CADUAN_PK_ENC = _aduanPkEnc;
//                    bool _aduanSes = saveAduanOnSession(_session);
//                    if (_aduanSes == true)
//                    {
//                        _data.ListCarianStaf = SiasatanProcess.MtdGetDiariPenyiasatList(_siasatanPkEnc);
//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    }
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : CatatanDiari
//        public IActionResult CatatanDiari()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Catatan Diari Siasatan", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                CarianCatatan _data = new();
//                CarianSiasatanMain _session = new();

//                if (_siasatanPkEnc != "")
//                {
//                    //save on session
//                    _session.CADUAN_PK_ENC = _aduanPkEnc;
//                    bool _aduanSes = saveAduanOnSession(_session);
//                    if (_aduanSes == true)
//                    {
//                        _data.ListCatatanSstn = SiasatanProcess.MtdGetCatatanSstnList(_siasatanPkEnc);
//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    }
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : FrmPengaduSaksi
//        public IActionResult FrmPengaduSaksi()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Rakaman Pengadu/Saksi", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianRakaman _data = new();
//                    List<ModelHrRakaman> _newList = new();
//                    _newList = SiasatanProcess.MtdGetPeribadiPrckpnList(_siasatanPkEnc, "1014"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER)  
//                    _data.ListRkmn = _newList.ToList();
//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;

//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : FrmPercakapanOkt
//        public IActionResult FrmPercakapanOkt()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Borang Percakapan OKT", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianRakaman _data = new();
//                    List<ModelHrRakaman> _newList = new();
//                    _newList = SiasatanProcess.MtdGetPeribadiPrckpnList(_siasatanPkEnc, "1015"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
//                    _data.ListRkmn = _newList.ToList();
//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : AttachPengaduSaksi
//        public IActionResult AttachPengaduSaksi()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Lampiran Rakaman Pengadu/Saksi", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    string _pathAttach = _attachmentPath;

//                    CarianLampiran _data = new();
//                    List<ModelHrLampiran> _newList = new();
//                    _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1014"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
//                    _data.ListLampiran = _newList.ToList();

//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : AttachOkt
//        public IActionResult AttachOkt()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Lampiran Rakaman OKT", "");
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    string _pathAttach = _attachmentPath;

//                    CarianLampiran _data = new();
//                    List<ModelHrLampiran> _newList = new();
//                    _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1015"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
//                    _data.ListLampiran = _newList.ToList();

//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : AttachDocLain
//        public IActionResult AttachDocLain()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Lampiran Dokumen Lain", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianLampiran _data = new();
//                    List<ModelHrLampiran> _newList = new();
//                    List<ModelHrLampiran> _newList2 = new();

//                    _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1017"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
//                    _newList2 = SiasatanProcess.MtdGetLampiranDocList(_aduanPkEnc); // Get File PDF by ADUAN_FK

//                    _newList.AddRange(_newList2);
//                    _data.ListLampiran = _newList.ToList();

//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : AttachEvidence
//        public IActionResult AttachEvidence()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Lampiran Barang Bukti", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    CarianLampiran _data = new();
//                    List<ModelHrLampiran> _newList = new();
//                    _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1018"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
//                    _data.ListLampiran = _newList.ToList();

//                    _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        //  END: Kes Siasatan - Sub Topic 

//        // GET PAGE : LaporanSiasatan
//        public IActionResult LaporanSiasatan()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Laporan Siasatan", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                CarianLaporan _data = new();
//                CarianSiasatanMain _session = new();
//                if (_siasatanPkEnc != "")
//                {
//                    //save on session
//                    _session.CADUAN_PK_ENC = _aduanPkEnc;
//                    bool _aduanSes = saveAduanOnSession(_session);
//                    if (_aduanSes == true)
//                    {
//                        _data = SiasatanProcess.MtdGetLaporanSstn(_siasatanPkEnc);
//                        if (_data._laporan != null)
//                        {
//                            _data.ListLaporanSstn = SiasatanProcess.MtdGetRumusanList(_data._laporan.LPRN_SSTN_PK_ENC);
//                        }
//                        _data.ListCarianStaf = SiasatanProcess.MtdGetPengesahanList(_siasatanPkEnc, "5128");
//                        if (_data.ListCarianStaf.Count > 0)
//                        {
//                            ModelHrStafPenyiasat? lastItem = _data.ListCarianStaf.LastOrDefault();
//                            if (lastItem != null)
//                            {
//                                if (lastItem.STATUSAKTIF_DESC == "Hantar")
//                                {
//                                    TempData["Status"] = "disabled";
//                                }
//                            }
//                        }
//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    }
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET AJAX : MtdGetSenaraiStafPnystCapai
//        [HttpPost]
//        public JsonResult MtdGetSenaraiStafPnystCapai([FromBody] ModelHrStafPenyiasat _carian)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string? _userPtjLogin = HttpContext.Session.GetString("_kodPtj") ?? "";
//            List<ModelHrStafPenyiasat> _newList = new();
//            if (_ePUserId != "")
//            {
//                string? _userAll = "KP"; // Dapatkan Semua Staf Gred KP
//                if (_carian.CSIASATAN_PK_ENC != null)
//                {
//                    _newList = SiasatanProcess.MtdGetStafPnystList(_carian.CSIASATAN_PK_ENC, _carian.CNOPEKERJA, _carian.CNAMA, _carian.CKAMPUS, _carian.CJAWATAN, _carian.CSTSAKTIF, _userAll);
//                }
//                return Json(_newList);
//            }
//            else
//            {
//                return Json(_newList);
//            }
//        }

//        // GET AJAX : MtdInsertStafPenyiasat
//        [HttpPost]
//        public JsonResult MtdInsertStafPenyiasat([FromBody] ModelHrStafPenyiasat _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrStafPenyiasat _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    _insert = SiasatanProcess.MtdInsertStafPenyiasat(_dataIn);
//                }
//                return Json(_insert);
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }

//        // GET AJAX : MtdInsertPrckpnDesc
//        [HttpPost]
//        public JsonResult MtdInsertPrckpnDesc([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrRakaman _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.CSIASATAN_PK_ENC != null)
//                {
//                    if (_dataIn.CKOD_PRNN_PRCKPN != null)
//                    {
//                        _insert = SiasatanProcess.MtdInsertPrckpnDesc(_dataIn);
//                    }
//                    else
//                    {
//                        _insert.RESULTSET = "-1";
//                    }
//                }
//                return Json(_insert);
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }

//        // GET PAGE : PerananRkmnPSList
//        public IActionResult PerananRkmnPSList(ModelHrRakaman _dataIn)
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Rakaman Pengadu/Saksi", "Perincian");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    if (_dataIn.PERIBADI_PRCKPN_PK_ENC != null)
//                    {
//                        CarianRakaman _data = new();
//                        List<ModelHrRakaman> _newList = new();
//                        ModelHrRakaman _getinfo = SiasatanProcess.MtdGetInfoPeribadiPrckpn(_dataIn.PERIBADI_PRCKPN_PK_ENC);
//                        _newList = SiasatanProcess.MtdGetPrckpnList(_dataIn.CSIASATAN_PK_ENC, _getinfo.KOD_PRNN_PRCKPN, _getinfo.KATEGORI_BRG_FK);
//                        _data.ListRkmn = _newList.ToList();

//                        if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "01") // Staff - Saksi,Pengadu
//                        {
//                            _data.stafPeribadi = SiasatanProcess.MtdGetInfoStafDetails(_dataIn.NO_KP);
//                            TempData["PS"] = "STAF";
//                        }
//                        if (_dataIn.KATEGORI_SAKSI == "422" || _dataIn.KATEGORI_SAKSI == "02") // Pelajar - Saksi,Pengadu
//                        {
//                            //_data.pelajarInfo = SiasatanDB.MtdGetDataPelajarByNokp(_dataIn.NO_KP);
//                            TempData["PS"] = "PELAJAR";
//                        }
//                        if (_dataIn.KATEGORI_SAKSI == "01" || _dataIn.KATEGORI_SAKSI == "02") { TempData["Kategori"] = "Pengadu"; }
//                        if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "422") { TempData["Kategori"] = "Saksi"; }

//                        _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                        _data.CPERIBADI_PRCKPN_PK_ENC = _dataIn.PERIBADI_PRCKPN_PK_ENC;
//                        _data.CKOD_PRNN_PRCKPN = _getinfo.KOD_PRNN_PRCKPN;
//                        _data.KATEGORI_SAKSI = _dataIn.KATEGORI_SAKSI;
//                        _data.NO_KP = _dataIn.NO_KP;

//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                        return View(_data);
//                    }
//                    else
//                    {
//                        return RedirectToAction("FrmPengaduSaksi", "Siasatan");
//                    }
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : PerincianRkmnPS
//        public IActionResult PerincianRkmnPS(ModelHrRakaman _dataIn)
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Rakaman Pengadu/Saksi", "Perincian");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.PERINCIAN_PRCKPN_PK_ENC != null)
//                {
//                    string _hidden = "hidden";
//                    TempData["hidden"] = _hidden;

//                    CarianRakaman _data = new();
//                    _data = SiasatanProcess.MtdGetPerincianPrbdiDetails(_dataIn.PERINCIAN_PRCKPN_PK_ENC);
//                    if (_data.butiran.PENTERJEMAH_FK != null) // Staf, Visitor PK
//                    {
//                        _hidden = "";
//                        TempData["hidden"] = _hidden;
//                        TempData["T"] = "STAF";
//                    }
//                    if (_data.butiran.NO_KP != null) // Pelajar
//                    {
//                        _hidden = "";
//                        TempData["hidden"] = _hidden;
//                        TempData["T"] = "PELAJAR";
//                    }

//                    bool _pnyst = SiasatanProcess.GetKodPrnnPnyst(_dataIn.CSIASATAN_PK_ENC, _dataIn.PERINCIAN_PRCKPN_PK_ENC, _penciptaFk);
//                    if (_pnyst == true)
//                    {
//                        _data.RESULTSET = "Ya";
//                    }

//                    if (_dataIn.KATEGORI_SAKSI == "01" || _dataIn.KATEGORI_SAKSI == "02") { TempData["Kategori"] = "Pengadu"; }
//                    if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "422") { TempData["Kategori"] = "Saksi"; }

//                    // Pass Value dari Page PerananRkmnPSList
//                    _data.CSIASATAN_PK_ENC = _dataIn.CSIASATAN_PK_ENC;
//                    _data.CPERIBADI_PRCKPN_PK_ENC = _dataIn.PERIBADI_PRCKPN_PK_ENC;
//                    _data.CPERINCIAN_PRCKPN_PK_ENC = _dataIn.PERINCIAN_PRCKPN_PK_ENC;
//                    _data.NO_KP = _dataIn.NO_KP;
//                    _data.KATEGORI_SAKSI = _dataIn.KATEGORI_SAKSI;

//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("PerananRkmnPSList", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : PrintPDF - Butiran Aduan
//        public IActionResult PrintPDF(string id)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (id != null && id != "")
//                {
//                    CarianSiasatanMain _data = new();
//                    _data = SiasatanProcess.MtdGetMaklumatAduanPenerima(id);

//                    if (_data.stafPeribadi.NO_PEKERJA != null)
//                    {
//                        TempData["PS"] = "STAF";
//                    }
//                    else if (_data.pelajarInfo.SPR_NOKP != null)
//                    {
//                        TempData["PS"] = "PELAJAR";
//                    }
//                    else
//                    {
//                        TempData["PS"] = "PELAWAT";
//                    }
//                    return View("PrintPDFButiranAduan", _data);
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : PrintPDFEvent - Ringkasan Kejadian
//        public IActionResult PrintPDFEvent(ModelHrPengadu _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    CarianSiasatan _data = new();
//                    _data = SiasatanProcess.MtdGetInitialEvent(_dataIn.SIASATAN_PK_ENC);
//                    return View("PrintPDFInitialEvent", _data);
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET AJAX : MtdUpdateKeteranganPS
//        [HttpPost]
//        public JsonResult MtdUpdateKeteranganPS([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrRakaman _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.PERINCIAN_PRCKPN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdUpdateKeteranganPS(_dataIn);
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        // GET AJAX : MtdUpdateInitialEvent
//        [HttpPost]
//        public JsonResult MtdUpdateInitialEvent([FromBody] ModelHrPengadu _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrPengadu _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdUpdateInitialEvent(_dataIn);
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        // GET AJAX : MtdInsertInitialApprove
//        [HttpPost]
//        public JsonResult MtdInsertInitialApprove([FromBody] ModelHrPengadu _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrPengadu _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdInsertInitialApprove(_dataIn);
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        // GET AJAX : MtdInsertInitialCorrection
//        [HttpPost]
//        public JsonResult MtdInsertInitialCorrection([FromBody] ModelHrPengadu _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrPengadu _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdInsertInitialCorrection(_dataIn);
//                    if (_update.RESULTSET == "2")
//                    {
//                        string _emailTo = "";
//                        string _emailMessage = "";
//                        string _senderName = "Admin apel @ UTM";
//                        string _emailFrom = "noreply-utmhr@utm.my";
//                        string _emailFromPassword = "";
//                        string _emailSubject = "Status Ringkasan Kejadian : Pembetulan";
//                        if (_dataIn.EMAIL != null && _dataIn.BTRN_ULSN != null)
//                        {
//                            _emailTo = _dataIn.EMAIL;
//                            _emailMessage = _dataIn.BTRN_ULSN;
//                        }
//                        string _emailCc = "mohamadnoorfarhan@utm.my, syedanwar@utm.my";

//                        //string status = EmailHelper.f_SendMail(_senderName, _emailFrom, _emailFromPassword, _emailTo, _emailCc, _emailSubject, _emailMessage);
//                        //if (status == "OK")
//                        //{
//                        //    _update.EMAIL = _dataIn.EMAIL;
//                        //    _update.RESULTSET = "3";
//                        //}
//                    }
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        // GET AJAX : MtdGetLampiran
//        [HttpPost]
//        public JsonResult MtdGetLampiran([FromBody] ModelHrLampiran _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLampiran _data = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.INV_LAMPIRAN_PK_ENC != null)
//                {
//                    _data = SiasatanProcess.MtdGetLampiran(_dataIn.INV_LAMPIRAN_PK_ENC);
//                }
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }

//        // GET AJAX : MtdGetRngksnStafEmel
//        [HttpPost]
//        public JsonResult MtdGetRngksnStafEmel([FromBody] ModelHrPengadu _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrStaffPeribadi _data = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    int _pencipta_rngksn = SiasatanProcess.MtdGetRngksnStafEmel(_dataIn.SIASATAN_PK_ENC);
//                    _data = SiasatanProcess.MtdGetMaklumatStaf(_pencipta_rngksn);
//                }
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }

//        // GET AJAX : MtdUpdateTamatSiasatan
//        [HttpPost]
//        public JsonResult MtdUpdateTamatSiasatan([FromBody] ModelHrPengadu _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrPengadu _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdUpdateTamatSiasatan(_dataIn);
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        //OnChange
//        //GET AJAX : MtdGetPerananByKatPrckpnListAjax
//        [HttpPost]
//        public JsonResult MtdGetPerananByKatPrckpnListAjax([FromBody] ModelHrRakaman _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                if (_cari.SIASATAN_PK_ENC != null)
//                {
//                    List<ModelParameterHr> _return = SiasatanProcess.MtdGetPerananByKatPrckpnList(_cari.SIASATAN_PK_ENC, _cari.KTGRI_PRCKPN_FK, _cari.KATEGORI_BRG_FK).ToList();
//                    return Json(_return);
//                }
//                else
//                {
//                    List<ModelParameterHr> _return = new();
//                    return Json(_return);
//                }
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        public JsonResult GetSaksiAjax([FromBody] ModelHrRakaman _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                string? _katsaksi = _cari.KATEGORI_SAKSI;
//                List<ModelParameterHr> _return = SiasatanProcess.MtdGetSaksiList(_katsaksi, _cari.SELECT, _cari.NAME).ToList();
//                return Json(_return);
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        //GET AJAX : MtdGetKodPerananPSAjax
//        [HttpPost]
//        public JsonResult MtdGetKodPerananPSAjax([FromBody] ModelHrLampiran _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                if (_cari.SIASATAN_PK_ENC != null)
//                {
//                    List<ModelParameterHr> _return = SiasatanProcess.MtdGetKodPerananList(_cari.SIASATAN_PK_ENC, _cari.KATEGORI_KOD_FK).ToList();
//                    return Json(_return);
//                }
//                else
//                {
//                    List<ModelParameterHr> _return = new();
//                    return Json(_return);
//                }
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        //GET AJAX : MtdGetKodPerananPerincianPSAjax
//        [HttpPost]
//        public JsonResult MtdGetKodPerananPerincianPSAjax([FromBody] ModelHrLampiran _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                List<ModelParameterHr> _return = SiasatanProcess.MtdGetKodPerananPerincianList(_cari.PERIBADI_PRCKPN_FK).ToList();
//                return Json(_return);
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        //GET AJAX : MtdGetKodZonAjax
//        [HttpPost]
//        public JsonResult MtdGetKodZonAjax([FromBody] ModelHrCatatan _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                List<ModelParameterHr> _return = SiasatanProcess.MtdGetKodZonAjax(_cari.KAMPUS).ToList();
//                return Json(_return);
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        //GET AJAX : MtdGetKodBlokAjax
//        [HttpPost]
//        public JsonResult MtdGetKodBlokAjax([FromBody] ModelHrCatatan _cari)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                List<ModelParameterHr> _return = SiasatanProcess.MtdGetKodBlokAjax(_cari.KAMPUS, _cari.KOD_ZON).ToList();
//                return Json(_return);
//            }
//            else
//            {
//                List<ModelParameterHr> _return = new();
//                return Json(_return);
//            }
//        }

//        // End: Onchange

//        //GET VIEW : MtdGetPenterjemah
//        [HttpPost]
//        public IActionResult MtdGetPenterjemah([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";

//            CarianRakaman _data = new();
//            if (_eUserIdIn != "")
//            {
//                _data = SiasatanProcess.MtdGetPenterjemah(_dataIn.KATEGORI_PENTERJEMAH, _dataIn.NO_ID);
//                if (_dataIn.KATEGORI_PENTERJEMAH != "421") // Staf
//                {
//                    TempData["T"] = "STAF";
//                }
//                else if (_dataIn.KATEGORI_PENTERJEMAH != "422") // Pelajar
//                {
//                    TempData["T"] = "PELAJAR";
//                }
//                return Json(_data);
//            }
//            else
//            {
//                return Json(_data);
//            }
//        }

//        //GET AJAX : MtdGetKodBorangPSAjax
//        [HttpPost]
//        public JsonResult MtdGetKodBorangPSAjax([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.PERIBADI_PRCKPN_PK_ENC != null)
//                {
//                    ModelParameterHr _return = SiasatanProcess.MtdGetKodBorangPS(_dataIn.PERIBADI_PRCKPN_PK_ENC, _dataIn.KOD_PRNN_PRCKPN);
//                    return Json(_return);
//                }
//                else
//                {
//                    ModelParameterHr _return = new();
//                    return Json(_return);
//                }
//            }
//            else
//            {
//                ModelParameterHr _return = new();
//                return Json(_return);
//            }
//        }

//        // GET AJAX : MtdInsertPerincianKodBrg
//        [HttpPost]
//        public JsonResult MtdInsertPerincianKodBrg([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            ModelHrRakaman _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_siasatanPkEnc != "")
//                {
//                    _dataIn.SIASATAN_PK_ENC = _siasatanPkEnc;
//                    if (_dataIn.KOD_BRG_PRCKPN != null)
//                    {
//                        _insert = SiasatanProcess.MtdInsertPerincianKodBrg(_dataIn);
//                    }
//                    else
//                    {
//                        _insert.RESULTSET = "-1";
//                    }
//                    return Json(_insert);
//                }
//                else
//                {
//                    return Json(_insert);
//                }
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }

//        //Begin: OKT
//        //GET AJAX : MtdGetPerananOktAjax
//        [HttpPost]
//        public JsonResult MtdGetPerananOktAjax([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    ModelParameterHr _return = SiasatanProcess.MtdGetPerananOkt(_dataIn.SIASATAN_PK_ENC, _dataIn.KATEGORI_BRG_FK);
//                    return Json(_return);
//                }
//                else
//                {
//                    ModelParameterHr _return = new();
//                    return Json(_return);
//                }
//            }
//            else
//            {
//                ModelParameterHr _return = new();
//                return Json(_return);
//            }
//        }

//        // GET AJAX : MtdInsertPrckpnOktDesc
//        [HttpPost]
//        public JsonResult MtdInsertPrckpnOktDesc([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrRakaman _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.CSIASATAN_PK_ENC != null)
//                {
//                    if (_dataIn.CKOD_PRNN_PRCKPN != null)
//                    {
//                        _insert = SiasatanProcess.MtdInsertPrckpnOktDesc(_dataIn);
//                    }
//                    else
//                    {
//                        _insert.RESULTSET = "-1";
//                    }
//                }
//                return Json(_insert);
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }

//        // GET PAGE : PerananRkmnPSList
//        public IActionResult PerananRkmnOktList(ModelHrRakaman _dataIn)
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Rakaman OKT", "Perincian");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    if (_dataIn.PERIBADI_PRCKPN_PK_ENC != null)
//                    {
//                        CarianRakaman _data = new();
//                        List<ModelHrRakaman> _newList = new();
//                        ModelHrRakaman _getinfo = SiasatanProcess.MtdGetInfoPeribadiPrckpn(_dataIn.PERIBADI_PRCKPN_PK_ENC);
//                        _newList = SiasatanProcess.MtdGetPrckpnList(_dataIn.CSIASATAN_PK_ENC, _getinfo.KOD_PRNN_PRCKPN, _getinfo.KATEGORI_BRG_FK);
//                        _data.ListRkmn = _newList.ToList();
//                        if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "01") // Staff
//                        {
//                            _data.stafPeribadi = SiasatanProcess.MtdGetInfoStafDetails(_dataIn.NO_KP);
//                            TempData["PS"] = "STAF";
//                        }
//                        if (_dataIn.KATEGORI_SAKSI == "422" || _dataIn.KATEGORI_SAKSI == "02") // Pelajar
//                        {
//                            //_data.pelajarInfo = SiasatanDB.MtdGetDataPelajarByNokp(_dataIn.NO_KP);
//                            TempData["PS"] = "PELAJAR";
//                        }
//                        if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "422") { TempData["Kategori"] = "OKT"; }

//                        _data.CSIASATAN_PK_ENC = _siasatanPkEnc;
//                        _data.CPERIBADI_PRCKPN_PK_ENC = _dataIn.PERIBADI_PRCKPN_PK_ENC;
//                        _data.CKOD_PRNN_PRCKPN = _getinfo.KOD_PRNN_PRCKPN;
//                        _data.KATEGORI_SAKSI = _dataIn.KATEGORI_SAKSI;
//                        _data.NO_KP = _dataIn.NO_KP;

//                        StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                        return View(_data);
//                    }
//                    else
//                    {
//                        return RedirectToAction("FrmPercakapanOkt", "Siasatan");
//                    }
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET PAGE : PerincianRkmnOkt
//        public IActionResult PerincianRkmnOkt(ModelHrRakaman _dataIn)
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Rakaman OKT", "Perincian");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";

//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.PERINCIAN_PRCKPN_PK_ENC != null)
//                {
//                    string _hidden = "hidden";
//                    TempData["hidden"] = _hidden;

//                    CarianRakaman _data = new();
//                    _data = SiasatanProcess.MtdGetPerincianPrbdiDetails(_dataIn.PERINCIAN_PRCKPN_PK_ENC);
//                    if (_data.butiran.PENTERJEMAH_FK != null) // Staf, Visitor PK
//                    {
//                        _hidden = "";
//                        TempData["hidden"] = _hidden;
//                        TempData["T"] = "STAF";
//                    }
//                    if (_data.butiran.NO_KP != null) // Pelajar
//                    {
//                        _hidden = "";
//                        TempData["hidden"] = _hidden;
//                        TempData["T"] = "PELAJAR";
//                    }

//                    bool _pnyst = SiasatanProcess.GetKodPrnnPnyst(_dataIn.CSIASATAN_PK_ENC, _dataIn.PERINCIAN_PRCKPN_PK_ENC, _penciptaFk);
//                    if (_pnyst == true)
//                    {
//                        _data.RESULTSET = "Ya";
//                    }

//                    if (_dataIn.KATEGORI_SAKSI == "421" || _dataIn.KATEGORI_SAKSI == "422") { TempData["Kategori"] = "OKT"; }

//                    // Pass Value dari Page PerananRkmnPSList
//                    _data.CSIASATAN_PK_ENC = _dataIn.CSIASATAN_PK_ENC;
//                    _data.CPERIBADI_PRCKPN_PK_ENC = _dataIn.PERIBADI_PRCKPN_PK_ENC;
//                    _data.CPERINCIAN_PRCKPN_PK_ENC = _dataIn.PERINCIAN_PRCKPN_PK_ENC;
//                    _data.NO_KP = _dataIn.NO_KP;
//                    _data.KATEGORI_SAKSI = _dataIn.KATEGORI_SAKSI;

//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("PerananRkmnOktList", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        //End: OKT

//        // GET AJAX : MtdUpdateStatus
//        [HttpPost]
//        public JsonResult MtdUpdateStatus([FromBody] ModelHrRakaman _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrRakaman _update = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_dataIn.PERINCIAN_PRCKPN_PK_ENC != null)
//                {
//                    _update = SiasatanProcess.MtdUpdateStatus(_dataIn);
//                }
//                return Json(_update);
//            }
//            else
//            {
//                return Json(_update);
//            }
//        }

//        //GET AJAX : MtdGetPerananInvLampiranAjax
//        [HttpPost]
//        public JsonResult MtdGetPerananInvLampiranAjax([FromBody] ModelHrLampiran _dataIn)
//        {
//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            if (_eUserIdIn != "")
//            {
//                if (_dataIn.SIASATAN_PK_ENC != null)
//                {
//                    ModelParameterHr _return = SiasatanProcess.MtdGetPerananInvLampiran(_dataIn.SIASATAN_PK_ENC, _dataIn.KATEGORI_KOD_FK);
//                    return Json(_return);
//                }
//                else
//                {
//                    ModelParameterHr _return = new();
//                    return Json(_return);
//                }
//            }
//            else
//            {
//                ModelParameterHr _return = new();
//                return Json(_return);
//            }
//        }

//        // GET AJAX : MtdInsertCatatanSstn
//        [HttpPost]
//        public JsonResult MtdInsertCatatanSstn([FromBody] ModelHrCatatan _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";
//            ModelHrCatatan _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_siasatanPkEnc != "")
//                {
//                    _dataIn.SIASATAN_PK_ENC = _siasatanPkEnc;
//                    _insert = SiasatanProcess.MtdInsertCatatanSstn(_dataIn);
//                }
//                return Json(_insert);
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }

//        // GET AJAX : MtdGetCatatanSstnList
//        public JsonResult MtdGetCatatanSstnList([FromBody] ModelHrCatatan _carian)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";
//            List<ModelHrCatatan> _newList = new();
//            if (_ePUserId != "")
//            {
//                if (_siasatanPkEnc != null)
//                {
//                    _newList = SiasatanProcess.MtdGetCatatanSstnList(_siasatanPkEnc);
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                }
//                return Json(_newList);
//            }
//            else
//            {
//                return Json(_newList);
//            }
//        }

//        public JsonResult MtdSubmitLaporan([FromBody] CarianLaporan _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";
//            CarianLaporan _insert = new();

//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_siasatanPkEnc != "")
//                {
//                    _dataIn.SIASATAN_PK_ENC = _siasatanPkEnc;
//                    _insert = SiasatanProcess.MtdInsertLaporanSstn(_dataIn);
//                }
//                return Json(_insert);
//            }

//            return Json(_insert);
//        }

//        public JsonResult MtdHapusRumusan([FromBody] ModelHrLaporan _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLaporan _hapus = new();

//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                _hapus = SiasatanProcess.MtdHapusRumusanSstn(_dataIn);
//                return Json(_hapus);
//            }

//            return Json(_hapus);
//        }

//        // Begin: Upload File
//        // GET : MtdAddAttachment
//        public ActionResult MtdAddAttachment(ModelHrLampiran _attach, IFormFile file)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLampiran _data = new();
//            if (_ePUserId != "")
//            {
//                _attach.PENCIPTA_FK = int.Parse(_penciptaFk);

//                if (file != null)
//                {
//                    // Get Value Daftar Pnyst
//                    //_attach.SIASATAN_PK = _attach.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                    //_attach.KOD_PRNN_PNYST_FK = _attach.KOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.KOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                    //_attach.DAFTAR_PNYST_PK = _attach.KOD_PRNN_PNYST_FK > 0 ? SiasatanDB.DB_MtdGetDaftarPnystPk(_attach.SIASATAN_PK, _attach.KOD_PRNN_PNYST_FK) : 0;

//                    // Get Value File
//                    string _folder = "Audio";
//                    var fileName0 = Path.GetFileName(file.FileName);
//                    string fileNameWoExt = Path.GetFileNameWithoutExtension(fileName0);
//                    var fileName = Regex.Replace(fileNameWoExt, "[^\\w'']", "_");
//                    var fileNameExt = Path.GetExtension(file.FileName);
//                    var _fileNameNoSiriUpload = _attach.KATEGORI_KOD_FK + "-" + _attach.RAKAMAN_PRCKPN_FK + "-" + _attach.DAFTAR_PNYST_PK + "-" + _attach.NO_REPORT + fileNameExt;   // 1014_1-1-JB000032022 - kategoriKod, perincianPeribadiPk, daftarpnyst, noreport

//                    // Get Value Path
//                    string _pathAttach = _attachmentPath + "/" + _attach.NO_REPORT + "/" + _folder + "/";
//                    string _savePath = "/" + _attach.NO_REPORT + "/" + _folder + "/";
//                    bool exists = Directory.Exists(_pathAttach);
//                    if (!exists)
//                        Directory.CreateDirectory(_pathAttach);

//                    var path = Path.Combine((_pathAttach), _fileNameNoSiriUpload);

//                    ModelHrLampiran _lampiran = new()
//                    {
//                        PENCIPTA_FK = _attach.PENCIPTA_FK,
//                        SIASATAN_PK = _attach.SIASATAN_PK,
//                        KATEGORI_KOD_FK = _attach.KATEGORI_KOD_FK,
//                        PATH = _savePath,
//                        KOD_LMPRN_RKMN = _attach.KOD_LMPRN_RKMN,
//                        DAFTAR_PNYST_PK = _attach.DAFTAR_PNYST_PK,
//                        TAJUK = _attach.TAJUK,
//                        RAKAMAN_PRCKPN_FK = _attach.RAKAMAN_PRCKPN_FK,
//                        NAMA_FAIL = _fileNameNoSiriUpload,
//                        CTTN_LMPRN = _attach.CTTN_LMPRN,
//                        URL = _attach.URL,
//                    };

//                    bool _result = SiasatanProcess.MtdInsertLampiran(_lampiran);
//                    if (_result == true)
//                    {
//                        using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
//                        {
//                            file.CopyTo(fileStream);
//                        }
//                        _lampiran.RESULTSET = "2";
//                    }
//                    else
//                    {
//                        _lampiran.RESULTSET = "-1";
//                    }
//                    return Json(_lampiran);
//                }
//                return Json(_data);
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET : MtdUpdateAttachment
//        public ActionResult MtdUpdateAttachment(ModelHrLampiran _attach, IFormFile file)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLampiran _data = new();
//            _data.RESULTSET = "-1";

//            if (_ePUserId != "")
//            {
//                _attach.PENCIPTA_FK = int.Parse(_penciptaFk);

//                if (_attach.SIASATAN_PK_ENC != null)
//                {
//                    if (file != null)
//                    {
//                        // Get Value Daftar Pnyst, Inv Lampiran Pk
//                        //_attach.SIASATAN_PK = _attach.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                        //_attach.INV_LAMPIRAN_PK = _attach.INV_LAMPIRAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.INV_LAMPIRAN_PK_ENC, _encryptCode)) : 0;
//                        //_attach.KOD_PRNN_PNYST_FK = _attach.KOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.KOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                        //_attach.DAFTAR_PNYST_PK = _attach.KOD_PRNN_PNYST_FK > 0 ? SiasatanDB.DB_MtdGetDaftarPnystPk(_attach.SIASATAN_PK, _attach.KOD_PRNN_PNYST_FK) : 0;

//                        // Get Value File
//                        var fileName0 = Path.GetFileName(file.FileName);
//                        string fileNameWoExt = Path.GetFileNameWithoutExtension(fileName0);
//                        var fileName = Regex.Replace(fileNameWoExt, "[^\\w'']", "_");
//                        var fileNameExt = Path.GetExtension(file.FileName);
//                        var _fileNameNoSiriUpload = _attach.KATEGORI_KOD_FK + "-" + _attach.RAKAMAN_PRCKPN_FK + "-" + _attach.DAFTAR_PNYST_PK + "-" + _attach.NO_REPORT + fileNameExt;   // 1014_1-1-JB000032022 - kategoriKod, perincianPeribadiPk, daftarpnyst, noreport

//                        // Get Value Path
//                        if (_attach.PATH != null)
//                        {
//                            string _pathAttach = _attachmentPath + _attach.PATH;
//                            bool exists = Directory.Exists(_pathAttach);
//                            if (!exists)
//                                Directory.CreateDirectory(_pathAttach);

//                            var path = Path.Combine((_pathAttach), _fileNameNoSiriUpload);

//                            ModelHrLampiran _lampiran = new()
//                            {
//                                PENCIPTA_FK = _attach.PENCIPTA_FK,
//                                INV_LAMPIRAN_PK = _attach.INV_LAMPIRAN_PK,
//                                KATEGORI_KOD_FK = _attach.KATEGORI_KOD_FK,
//                                PATH = _attach.PATH,
//                                DAFTAR_PNYST_PK = _attach.DAFTAR_PNYST_PK,
//                                TAJUK = _attach.TAJUK,
//                                NAMA_FAIL = _fileNameNoSiriUpload,
//                                CTTN_LMPRN = _attach.CTTN_LMPRN,
//                                URL = _attach.URL,
//                            };


//                            bool _result = SiasatanProcess.MtdUpdateLampiran(_lampiran);
//                            if (_result == true)
//                            {
//                                using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
//                                {
//                                    file.CopyTo(fileStream);
//                                }
//                                _lampiran.RESULTSET = "2";
//                            }
//                            else
//                            {
//                                _lampiran.RESULTSET = "-1";
//                            }
//                            return Json(_lampiran);
//                        }
//                    }
//                    return Json(_data);
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET : MtdAddAttachmentDocLain
//        public ActionResult MtdAddAttachmentDoc(ModelHrLampiran _attach, IFormFile file)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLampiran _data = new();
//            if (_ePUserId != "")
//            {
//                _attach.PENCIPTA_FK = int.Parse(_penciptaFk);

//                if (file != null)
//                {
//                    // Get Value Daftar Pnyst
//                    //_attach.SIASATAN_PK = _attach.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                    //_attach.KOD_PRNN_PNYST_FK = _attach.KOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.KOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                    //_attach.DAFTAR_PNYST_PK = _attach.KOD_PRNN_PNYST_FK > 0 ? SiasatanDB.DB_MtdGetDaftarPnystPk(_attach.SIASATAN_PK, _attach.KOD_PRNN_PNYST_FK) : 0;

//                    // Get Value File
//                    string _folder = "Gambar";
//                    var fileName0 = Path.GetFileName(file.FileName);
//                    string fileNameWoExt = Path.GetFileNameWithoutExtension(fileName0);
//                    var fileName = Regex.Replace(fileNameWoExt, "[^\\w'']", "_");
//                    var fileNameExt = Path.GetExtension(file.FileName);
//                    var _fileNameNoSiriUpload = _attach.KATEGORI_KOD_FK + "-" + _attach.KOD_LMPRN_RKMN + "-" + _attach.DAFTAR_PNYST_PK + "-" + _attach.NO_REPORT + fileNameExt;   // 1014_1-1-JB000032022 - kategoriKod, perincianPeribadiPk, daftarpnyst, noreport

//                    // Create folder 
//                    if (fileNameExt == ".pdf")
//                    {
//                        _folder = "Fail";
//                    }
//                    if (fileNameExt == ".mp3")
//                    {
//                        _folder = "Audio";
//                    }
//                    if (fileNameExt == ".mp4")
//                    {
//                        _folder = "Video";
//                    }

//                    // Get Value Path
//                    string _pathAttach = _attachmentPath + "/" + _attach.NO_REPORT + "/" + _folder + "/";
//                    string _savePath = "/" + _attach.NO_REPORT + "/" + _folder + "/";
//                    bool exists = Directory.Exists(_pathAttach);
//                    if (!exists)
//                        Directory.CreateDirectory(_pathAttach);

//                    var path = Path.Combine((_pathAttach), _fileNameNoSiriUpload);

//                    ModelHrLampiran _lampiran = new()
//                    {
//                        PENCIPTA_FK = _attach.PENCIPTA_FK,
//                        SIASATAN_PK = _attach.SIASATAN_PK,
//                        KATEGORI_KOD_FK = _attach.KATEGORI_KOD_FK,
//                        PATH = _savePath,
//                        KOD_LMPRN_RKMN = _attach.KOD_LMPRN_RKMN,
//                        DAFTAR_PNYST_PK = _attach.DAFTAR_PNYST_PK,
//                        TAJUK = _attach.TAJUK,
//                        RAKAMAN_PRCKPN_FK = _attach.RAKAMAN_PRCKPN_FK,
//                        NAMA_FAIL = _fileNameNoSiriUpload,
//                        CTTN_LMPRN = _attach.CTTN_LMPRN,
//                        URL = _attach.URL,
//                    };

//                    bool _result = SiasatanProcess.MtdInsertLampiran(_lampiran);
//                    if (_result == true)
//                    {
//                        using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
//                        {
//                            file.CopyTo(fileStream);
//                        }
//                        _lampiran.RESULTSET = "2";
//                    }
//                    else
//                    {
//                        _lampiran.RESULTSET = "-1";
//                    }
//                    return Json(_lampiran);
//                }
//                return Json(_data);
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET : MtdUpdateAttachmentDoc
//        public ActionResult MtdUpdateAttachmentDoc(ModelHrLampiran _attach, IFormFile file)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            ModelHrLampiran _data = new();
//            _data.RESULTSET = "-1";

//            if (_ePUserId != "")
//            {
//                _attach.PENCIPTA_FK = int.Parse(_penciptaFk);

//                if (_attach.SIASATAN_PK_ENC != null)
//                {
//                    if (file != null)
//                    {
//                        // Get Value Daftar Pnyst, Inv Lampiran Pk
//                        //_attach.SIASATAN_PK = _attach.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                        //_attach.INV_LAMPIRAN_PK = _attach.INV_LAMPIRAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.INV_LAMPIRAN_PK_ENC, _encryptCode)) : 0;
//                        //_attach.KOD_PRNN_PNYST_FK = _attach.KOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_attach.KOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                        //_attach.DAFTAR_PNYST_PK = _attach.KOD_PRNN_PNYST_FK > 0 ? SiasatanDB.DB_MtdGetDaftarPnystPk(_attach.SIASATAN_PK, _attach.KOD_PRNN_PNYST_FK) : 0;

//                        // Get Value File
//                        var fileName0 = Path.GetFileName(file.FileName);
//                        string fileNameWoExt = Path.GetFileNameWithoutExtension(fileName0);
//                        var fileName = Regex.Replace(fileNameWoExt, "[^\\w'']", "_");
//                        var fileNameExt = Path.GetExtension(file.FileName);
//                        var _fileNameNoSiriUpload = _attach.KATEGORI_KOD_FK + "-" + _attach.KOD_LMPRN_RKMN + "-" + _attach.DAFTAR_PNYST_PK + "-" + _attach.NO_REPORT + fileNameExt;   // 1014_1-1-JB000032022 - kategoriKod, perincianPeribadiPk, daftarpnyst, noreport

//                        if (_attach.PATH != null)
//                        {
//                            string _pathAttach = _attachmentPath + _attach.PATH;
//                            bool exists = Directory.Exists(_pathAttach);
//                            if (!exists)
//                                Directory.CreateDirectory(_pathAttach);

//                            var path = Path.Combine((_pathAttach), _fileNameNoSiriUpload);

//                            ModelHrLampiran _lampiran = new()
//                            {
//                                PENCIPTA_FK = _attach.PENCIPTA_FK,
//                                INV_LAMPIRAN_PK = _attach.INV_LAMPIRAN_PK,
//                                KATEGORI_KOD_FK = _attach.KATEGORI_KOD_FK,
//                                PATH = _attach.PATH,
//                                DAFTAR_PNYST_PK = _attach.DAFTAR_PNYST_PK,
//                                TAJUK = _attach.TAJUK,
//                                NAMA_FAIL = _fileNameNoSiriUpload,
//                                CTTN_LMPRN = _attach.CTTN_LMPRN,
//                                URL = _attach.URL,
//                            };

//                            bool _result = SiasatanProcess.MtdUpdateLampiran(_lampiran);
//                            if (_result == true)
//                            {
//                                using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
//                                {
//                                    file.CopyTo(fileStream);
//                                }
//                                _lampiran.RESULTSET = "2";
//                            }
//                            else
//                            {
//                                _lampiran.RESULTSET = "-1";
//                            }
//                            return Json(_lampiran);
//                        }
//                    }
//                    return Json(_data);
//                }
//                else
//                {
//                    return RedirectToAction("SiasatanSkrinList", "Senarai");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // End: Upload File

//        // GET PAGE : Chat
//        public IActionResult Chat()
//        {
//            MtdGetDashboardSession("Senarai Siasatan", "Chat", "");

//            string _eUserIdIn = HttpContext.Session.GetString("_userId") ?? "";
//            string _role = HttpContext.Session.GetString("_hrSiasatanRole") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            string _aduanPkEnc = ApelUser.GetApel(HttpContext.Session).ADUAN_PK_ENC ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";
//            string _userAll = "";

//            if (_eUserIdIn != "")
//            {
//                if (_siasatanPkEnc != "")
//                {
//                    if (_role == "PNYST_KANAN")
//                    {
//                        _userAll = "ALL"; // Dapatkan Semua Senarai Siasatan
//                    }
//                    CarianMessage _data = new();
//                    _data = SiasatanProcess.MtdGetMaklumatPenyiasatChat(_siasatanPkEnc, _penciptaFk, _userAll);
//                    if (_data != null)
//                    {
//                        _data.CNAMA = _eUserIdIn + " (Peg.Kanan)";
//                        _data.CNOADUAN = _data.pegChat.REPORT_NO;
//                        if (_userAll != "ALL")
//                        {
//                            _data.CNAMA = _eUserIdIn + " (" + _data.pegChat.KOD_PRNN_PNYST + ") ";
//                        }
//                        List<ModelHrMessage> _newList = new();
//                        _newList = SiasatanProcess.MtdGetMessageList(_siasatanPkEnc);
//                        _data.ListMessage = _newList.ToList();
//                    }
//                    StoreSession(StatusProcess.SemakSemuaMenu(_aduanPkEnc, _siasatanPkEnc));
//                    return View(_data);
//                }
//                else
//                {
//                    return RedirectToAction("LaporanAduan", "Siasatan");
//                }
//            }
//            else
//            {
//                return Redirect("/Home/Index");
//            }
//        }

//        // GET AJAX : MtdInsertMessage
//        [HttpPost]
//        public JsonResult MtdInsertMessage([FromBody] ModelHrMessage _dataIn)
//        {
//            string _ePUserId = HttpContext.Session.GetString("_userId") ?? "";
//            string _penciptaFk = HttpContext.Session.GetString("_hrStafFk") ?? "";
//            string _siasatanPkEnc = ApelUser.GetApel(HttpContext.Session).SIASATAN_PK_ENC ?? "";

//            ModelHrMessage _insert = new();
//            if (_ePUserId != "")
//            {
//                _dataIn.PENCIPTA_FK = int.Parse(_penciptaFk);
//                if (_siasatanPkEnc != "")
//                {
//                    _dataIn.SIASATAN_PK_ENC = _siasatanPkEnc;
//                    _insert = SiasatanProcess.MtdInsertMessage(_dataIn);
//                    return Json(_insert);
//                }
//                else
//                {
//                    return Json(_insert);
//                }
//            }
//            else
//            {
//                return Json(_insert);
//            }
//        }
//    }
//}
