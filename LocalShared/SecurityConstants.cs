using Microsoft.AspNetCore.Mvc.Rendering;
using APELC.PublicShared;
using APELC.Model;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace APELC.LocalShared
{
	public class SecurityConstants
	{
        public static string SCREEN_CARIAN = "_Sc66%KLgHyr";
        public static string SCREEN_CARIAN_CONST = "_Scon%KLgHyr";

        //Constants to represent Access Right Level on Record Level
        public static int CAPAIAN_SEMUA = 1;
        public static int CAPAIAN_PTJ = 2;
        public static int CAPAIAN_DIRISENDIRI = 3;
        public static int CAPAIAN_UNIT = 4;
        public static int CAPAIAN_SUB_UNIT = 5;
        public static int CAPAIAN_KAMPUS = 6;
        public static int CAPAIAN_MULTI_PTJ = 8;
        public static int CAPAIAN_CROSS_KAMPUS = 9;

        public static string CAPAIAN2016_SEMUA = "A";
        public static string CAPAIAN2016_UNIVERSITI = "B";
        public static string CAPAIAN2016_MULTI_UNIVERSITI = "C";
        public static string CAPAIAN2016_KAMPUS = "D";
        public static string CAPAIAN2016_MULTI_KAMPUS = "E";
        public static string CAPAIAN2016_PTJ = "F";
        public static string CAPAIAN2016_MULTI_PTJ = "G";
        public static string CAPAIAN2016_JAB = "H";
        public static string CAPAIAN2016_MULTI_JAB = "I";
        public static string CAPAIAN2016_UNIT = "J";
        public static string CAPAIAN2016_MULTI_UNIT = "K";
        public static string CAPAIAN2016_DIRISENDIRI = "L";

        public static string EncryptCode()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["MySetting:EncryptCode"];
        }

        //public static string ConnOraRujuk()
        //{
        //    return ConnMySQLUpnmDbDs();
        //}

        //public static string ConnMySQLUpnmDbDs()
        //{
        //    //return PublicConstant.ConnMySQLUpnmDbDs();
        //}


        public readonly List<ModelParameterAPEL> ItemListKelulusanKP = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "BARU", Key = "B" },
            new ModelParameterAPEL { ViewField = "SOKONG", Key = "S" },
            new ModelParameterAPEL { ViewField = "DILULUSKAN", Key = "L" },
            new ModelParameterAPEL { ViewField = "TIDAK DILULUSKAN", Key = "TL" },
            new ModelParameterAPEL { ViewField = "DIBATALKAN", Key = "BT" }
        };


        public readonly List<SelectListItem> ItemListPleaseSelect = new List<SelectListItem>()
        {
            new SelectListItem { Text = "- Please Select -", Value = "" }
        };

        public readonly List<SelectListItem> ItemListDaftarYN = new List<SelectListItem>()
        {
            new SelectListItem { Text = "YA", Value = "1" },
            new SelectListItem { Text = "TIDAK", Value = "2" }
        };

//        internal static string MtdGetSideLocation()
//        {
//#pragma warning disable SYSLIB0014 // Type or member is obsolete
//            var webClient = new WebClient();
//#pragma warning restore SYSLIB0014 // Type or member is obsolete
//            var _json = webClient.DownloadString(@"C:/Log/LokasiEncrypt.txt");
//           //string _ipLokasi = EncryptHr.NewDecrypt(_json, EncryptCode());
//            string _first = _ipLokasi + ".";
//            Random rnd = new Random();
//            int dice = rnd.Next(46, 254);
//            string _back = _first + dice.ToString();
//            return _back;
//        }
        public static List<ModelParameterAPEL> MtdGetDDStatusList = new()
        {
            new ModelParameterAPEL { Key = "344", ViewField = "DRAFT" },
            new ModelParameterAPEL { Key = "345", ViewField = "SUBMITTED" },
            new ModelParameterAPEL { Key = "355", ViewField = "DELETED" },
            new ModelParameterAPEL { Key = "1076", ViewField = "VERIFIED" },
            new ModelParameterAPEL { Key = "1077", ViewField = "NOT VERIFIED" },
            new ModelParameterAPEL { Key = "753", ViewField = "PAY" },
            new ModelParameterAPEL { Key = "349", ViewField = "APPROVED" },
            new ModelParameterAPEL { Key = "350", ViewField = "DISAPPROVED" },
            new ModelParameterAPEL { Key = "1022", ViewField = "CORRECTION" }
        };

        public readonly List<ModelParameterAPEL> ItemListYaTidak = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "YA", Key = "Y" },
            new ModelParameterAPEL { ViewField = "TIDAK", Key = "T" }
        };

        public readonly List<ModelParameterAPEL> ItemListYaBukan = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "YA", Key = "1" },
            new ModelParameterAPEL { ViewField = "BUKAN", Key = "2" }
        };

        public readonly List<ModelParameterAPEL> ItemListYaBukanYN = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "YA", Key = "Y" },
            new ModelParameterAPEL { ViewField = "BUKAN", Key = "N" }
        };

        public readonly List<ModelParameterAPEL> ItemListStatusDapatRawatanKllinik = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "DAFTAR", Key = "4940" },
            new ModelParameterAPEL { ViewField = "RAWATAN", Key = "4941" },
            new ModelParameterAPEL { ViewField = "CETAKAN", Key = "4942" },
            new ModelParameterAPEL { ViewField = "BATAL", Key = "4943" },
            new ModelParameterAPEL { ViewField = "SELESAI", Key = "4935" }
        };

        internal readonly List<ModelParameterAPEL> ItemListKodGred = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "ALL", Key = "ALL" },
            new ModelParameterAPEL { ViewField = ">= Gred 41 ", Key = "> '40'" },
            new ModelParameterAPEL { ViewField = "<= Gred 40 ", Key = "< '41'" },
        };

        public readonly List<ModelParameterAPEL> ItemList_PleaseSelect = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "- Please Select -", Key = "" }
        };

        //public static string MtdGetMinitToJamText(int _minit)
        //{
        //    string _text = "0 Jam 00 Minit";
        //    if (_minit > 0)
        //    {
        //        int _bilJam = NumberHelper.ToIntiger((_minit / 60).ToString().Split('.')[0]);
        //        int _bilMin = _minit % 60;
        //        string _paparJam = _bilJam.ToString("00");
        //        string _paparMin = _bilMin.ToString("00");
        //        _text = _paparJam + " Jam " + _paparMin + " Minit  ";
        //    }
        //    return _text;
        //}
        public static string MtdGetVerifyCodeApprovalSecture(int _paramPk)
        {
            string _kod = "";
            switch (_paramPk)
            {
                case 4615:
                    _kod = "99043"; break;
                case 4616:
                    _kod = "99044"; break;
                case 4617:
                    _kod = "99045"; break;
                case 4618:
                    _kod = "99046"; break;
                case 4619:
                    _kod = "99047"; break;
                case 4620:
                    _kod = "99048"; break;
                case 4621:
                    _kod = "99049"; break;
                case 4622:
                    _kod = "99050"; break;
                case 4623:
                    _kod = "99051"; break;
                case 4624:
                    _kod = "99052"; break;
                case 4625:
                    _kod = "99053"; break;
                case 4626:
                    _kod = "99054"; break;
                case 4627:
                    _kod = "99055"; break;
            }
            return _kod;
        }
        public static string MtdGetApproveCodeApprovalSecture(int _paramPk)
        {
            string _kod = "";
            switch (_paramPk)
            {
                case 4615:
                    _kod = "99056"; break;
                case 4616:
                    _kod = "99057"; break;
                case 4617:
                    _kod = "99058"; break;
                case 4618:
                    _kod = "99059"; break;
                case 4619:
                    _kod = "99060"; break;
                case 4620:
                    _kod = "99061"; break;
                case 4621:
                    _kod = "99062"; break;
                case 4622:
                    _kod = "99063"; break;
                case 4623:
                    _kod = "99064"; break;
                case 4624:
                    _kod = "99065"; break;
                case 4625:
                    _kod = "99066"; break;
                case 4626:
                    _kod = "99067"; break;
                case 4627:
                    _kod = "99068"; break;
            }
            return _kod;
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations
                                .EmailAddressAttribute()
                                .IsValid(emailAddress);
        }

        public List<ModelParameterAPEL> ItemListBulan = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "01 - Januari", Key = "01" },
            new ModelParameterAPEL { ViewField = "02 - Februari", Key = "02" },
            new ModelParameterAPEL { ViewField = "03 - Mac", Key = "03" },
            new ModelParameterAPEL { ViewField = "04 - April", Key = "04" },
            new ModelParameterAPEL { ViewField = "05 - Mei", Key = "05" },
            new ModelParameterAPEL { ViewField = "06 - Jun", Key = "06" },
            new ModelParameterAPEL { ViewField = "07 - Julai", Key = "07" },
            new ModelParameterAPEL { ViewField = "08 - Ogos", Key = "08" },
            new ModelParameterAPEL { ViewField = "09 - September", Key = "09" },
            new ModelParameterAPEL { ViewField = "10 - Oktober", Key = "10" },
            new ModelParameterAPEL { ViewField = "11 - November", Key = "11" },
            new ModelParameterAPEL { ViewField = "12 - Disember", Key = "12" }
        };

        internal readonly List<ModelParameterAPEL> ItemListKampus = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "JOHOR BAHRU", Key = "J" },
            new ModelParameterAPEL { ViewField = "KUALA LUMPUR", Key = "K" },
            new ModelParameterAPEL { ViewField = "HTP-PAGOH", Key = "P" }
        };

        internal readonly List<ModelParameterAPEL> ItemListKampusFk = new List<ModelParameterAPEL>()
        {
            new ModelParameterAPEL { ViewField = "JOHOR BAHRU", Key = "416" },
            new ModelParameterAPEL { ViewField = "KUALA LUMPUR", Key = "417" },
            new ModelParameterAPEL { ViewField = "PAGOH", Key = "1510" }
        };

        internal static string GetFirstValueOf(string _text, int _maxLength)
        {
            if (string.IsNullOrEmpty(_text))
                return _text;
            return _text.Substring(0, Math.Min(_text.Length, _maxLength));
        }

        public static string StudSchema()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["MySetting:StudSchema"];
        }

        public static string AttachmentPath()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["MySetting:AttachmentPathSecurity"];
        }

    }
}
