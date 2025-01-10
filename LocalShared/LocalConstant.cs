using APELC.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System;
using System.IO;
using System.Data.Odbc;
//using MySqlConnector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Web;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System.Net;
using JetBrains.Annotations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql;
using APELC.Data;
using MySql.Data.MySqlClient;



namespace APELC.LocalShared
{
    public class LocalConstant
    {
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
#pragma warning disable CS8603 // Possible null reference return.
            return config["MySetting:EncryptCode"];
#pragma warning restore CS8603 // Possible null reference return.
        }

        //public static string ConnOraRujuk()
        //{
        //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    return config["MySetting:UpnmdbRujuk"];
        //}

        public static string ConnMySQLUpnmDbDs()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //string _userEnc = config["MySetting:UpnmdbDSUser"];
            //string _user = EncryptHr.DecryptString4url(_userEnc, "admSmis");
            //string _pawdEnc = config["MySetting:UpnmdbDSPawd"];
            //string _pawd = EncryptHr.DecryptString4url(_pawdEnc, "admSmis");
            //string _ipAddEnc = config["MySetting:UtmdbIPAdd"];
            //string _ipAdd = EncryptHr.DecryptString4url(_ipAddEnc, "admSmis");
            //string _serviceEnc = config["MySetting:UtmdbService"];
            //string _service = EncryptHr.DecryptString4url(_serviceEnc, "admSmis");
            //string _text = @"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + _ipAdd + @")(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + _service + @")));User Id=" + _user + ";Password=" + _pawd;

            //MySqlConnection conn = new MySqlConnection();
            //conn.ConnectionString = ("server=172.16.2.247;uid=apelUPNM;pwd=AP3L@MySQL;database=apelc");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string _text = config[key: "connectionString:UPNMDBMYSQL"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return _text;
#pragma warning restore CS8603 // Possible null reference return.
        }


        public static string MtdRIChar(string fileName)
        {
            string _data = fileName.Replace("'", "");
            _data = _data.Replace("--", "");
            _data = _data.Replace(";", "");
            _data = _data.Replace("=", "");
            _data = _data.Replace("<", "");
            _data = _data.Replace("[", "");
            _data = _data.Replace("{", "");
            return _data;
        }

        public static string MtdRemoveSpecialCharacter(string fileName)
        {
            string _data = fileName.Replace("/", "");
            _data = _data.Replace("&", "");
            _data = _data.Replace("+", "");
            _data = _data.Replace("%", "");
            _data = _data.Replace("#", "");
            _data = _data.Replace("$", "");
            _data = _data.Replace("!", "");
            _data = _data.Replace("'", "");
            _data = _data.Replace("\"", "");
            _data = _data.Replace("\\", "");
            _data = _data.Replace("<", "");
            _data = _data.Replace(">", "");
            _data = _data.Replace("{", "");
            _data = _data.Replace("}", "");
            _data = _data.Replace("[", "");
            _data = _data.Replace("]", "");
            _data = _data.Replace("(", "");
            _data = _data.Replace(")", "");
            _data = _data.Replace("|", "");
            _data = _data.Replace("?", "");
            _data = _data.Replace(":", "");
            _data = _data.Replace(";", "");
            _data = _data.Replace(",", "");
            _data = _data.Replace(" ", "");
            return _data;
        }
        public static string MtdRemoveSpecialCharacter5space(string fileName)
        {
            string _data = fileName.Replace("/", "");
            _data = _data.Replace("&", "");
            _data = _data.Replace("+", "");
            _data = _data.Replace("%", "");
            _data = _data.Replace("#", "");
            _data = _data.Replace("$", "");
            _data = _data.Replace("!", "");
            _data = _data.Replace("'", "");
            _data = _data.Replace("\"", "");
            _data = _data.Replace("\\", "");
            _data = _data.Replace("<", "");
            _data = _data.Replace(">", "");
            _data = _data.Replace("{", "");
            _data = _data.Replace("}", "");
            _data = _data.Replace("[", "");
            _data = _data.Replace("]", "");
            _data = _data.Replace("(", "");
            _data = _data.Replace(")", "");
            _data = _data.Replace("|", "");
            _data = _data.Replace("?", "");
            _data = _data.Replace(":", "");
            _data = _data.Replace(";", "");
            _data = _data.Replace(",", "");
            return _data;
        }


        public static string[] SplitAyat(string text01, string _splitBy)
        {
            string[] stringSeparators = new string[] { _splitBy };
            string[] result;
            result = text01.Split(stringSeparators, StringSplitOptions.None);
            return result;
        }

        public static string MtdRefomateTime(string _time)
        {
            string[] _text = SplitAyat(_time, ":");
            string _hour = _text[0].Length == 1 ? ("0" + _text[0]) : _text[0];
            string _mint = _text[1].Length == 1 ? ("0" + _text[1]) : _text[1];
            return (_hour + ":" + _mint);
        }


        //public static List<ParameterHrModel> ItemListHPhoneCode = new List<ParameterHrModel>()
        //{
        //    new ParameterHrModel { NAMA_PARAMETER = "010", KOD = "+6010" },
        //    new ParameterHrModel { NAMA_PARAMETER = "011", KOD = "+6011" },
        //    new ParameterHrModel { NAMA_PARAMETER = "012", KOD = "+6012" },
        //    new ParameterHrModel { NAMA_PARAMETER = "013", KOD = "+6013" },
        //    new ParameterHrModel { NAMA_PARAMETER = "014", KOD = "+6014" },
        //    new ParameterHrModel { NAMA_PARAMETER = "015", KOD = "+6015" },
        //    new ParameterHrModel { NAMA_PARAMETER = "016", KOD = "+6016" },
        //    new ParameterHrModel { NAMA_PARAMETER = "017", KOD = "+6017" },
        //    new ParameterHrModel { NAMA_PARAMETER = "018", KOD = "+6018" },
        //    new ParameterHrModel { NAMA_PARAMETER = "019", KOD = "+6019" }
        //};

        //public static List<ParameterHrModel> ItemListVisitorPurpose = new List<ParameterHrModel>()
        //{
        //    new ParameterHrModel { NAMA_PARAMETER = "OFFICIAL BUSINESS ONLY", KOD = "4637" },
        //    new ParameterHrModel { NAMA_PARAMETER = "OTHERS", KOD = "4640" }
        //};
        ////new ParameterHrModel { NAMA_PARAMETER = "PERSONAL AFFAIRS", KOD = "4638" },
        ////new ParameterHrModel { NAMA_PARAMETER = "PARENTS/RELATIVES VISITING STUDENTS/STAFFS ON CAMPUS", KOD = "4639" },

        //public static List<ParameterHrModel> ItemListVisitorType = new List<ParameterHrModel>()
        //{
        //    new ParameterHrModel { NAMA_PARAMETER = "VISITOR", KOD = "4641" },
        //    new ParameterHrModel { NAMA_PARAMETER = "CONTRACT WORKER", KOD = "4642" }
        //};

        //public static List<ParameterHrModel> ItemJenisPerkhidmatan = new List<ParameterHrModel>()
        //{
        //    new ParameterHrModel { NAMA_PARAMETER = "A - Akademik Profesional", KOD = "A" },
        //    new ParameterHrModel { NAMA_PARAMETER = "B - Akademik bukan Profesional", KOD = "B" },
        //    new ParameterHrModel { NAMA_PARAMETER = "C - Pengawai Am Profesional", KOD = "C" },
        //    new ParameterHrModel { NAMA_PARAMETER = "D - Pengawai Am bukan Profesional", KOD = "D" },
        //    new ParameterHrModel { NAMA_PARAMETER = "E - Kakitangan Sokongan", KOD = "E" },
        //    new ParameterHrModel { NAMA_PARAMETER = "F - Profesional", KOD = "F" }
        //};

        //public static List<ModelParameterAPELC> ItemListKampus = new List<ModelParameterAPELC>()
        //{
            //new ParameterHrModel { NAMA_PARAMETER = "Johor Bahru", KOD = "J" },
            //new ModelParameterAPELC { NAMA_PARAMETER = "Sungai Besi", KOD = "S" },
            //new ParameterHrModel { NAMA_PARAMETER = "Pagoh", KOD = "P" }
        //};

        public static string? GetLastItem(string? _text)
        {
            string _last = "";
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (_text != null)
            {
                string[] _array = SplitAyat(_text.ToString(), ".");
                _last = _array[_array.Length - 1].ToLower();
            }
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            return _last;
        }
    }
}
