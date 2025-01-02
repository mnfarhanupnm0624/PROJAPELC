using APELC.LocalServices.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APELC.LocalServices.ApelC;
using APELC.LocalServices.Selenggara;
using APELC.LocalServices.DashboardStatistik;
using APELC.LocalServices.Senarai;
using APELC.LocalServices.Status;
using APELC.LocalShared;
using APELC.Model;
using System.Net;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Net.Http.Json;
using System.Data.Entity;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace APELC.Model
{
    public class ModelParameterAPELC
    {
        //RUJUK KEPADA TABLE APELC_PARAMETER

        public string? SelectListjenisApel { get; set; }

        public string? SelectListjenisApel_EN { get; set; }
        //public SelectList OrderTemplates { get; set; }
        public string? PARAM_PK_ENC { set; get; }
        public string? KUMPULAN_FK { get; set; }
        public string? KOD { set; get; }
        //public string? NAMA_PARAMETER { set; get; }

        //public SelectList OrderTemplates { get; set; }
        public string? NAMA_PARAMETER { set; get; }
        //public IEnumerable<SelectListItem> NAMA_PARAMETER { get; set; }
        public string? NAMA_PARAMETER_EN { set; get; }
        public string? STATUS_AKTIF { set; get; }
        public string? Value { set; get; }
        public string? Text { set; get; }
        public string? Key { set; get; }
        public int Nombor { set; get; }
        public string? RESULTSET { set; get; }
        public string? OldText01 { set; get; }
        public string? OldText01Enc { set; get; }
        public string? OldText02 { set; get; }
        public string? OldText02Enc { set; get; }
        public string? PublicText01 { set; get; }
        public string? PublicText01Enc { set; get; }
        public string? PublicText02 { set; get; }
        public string? PublicText02Enc { set; get; }

        public DateTime TKH_HAPUS { get; set; }

        // this.context = context;



        // DDL List      
        List<string> _functionListjenisApel = new List<string>().ToList();
        //_functionListjenisApel.Add(new SelectList
       // {
        //Key = "Key",
        //Value = "Value"
        //});
        //ViewBag.ListjenisApelSelect = _functionListjenisApel.Add(new SelectList{ Key = "Key",Value = "Value"});
        //public List<ModelParameterAPELC> MySelectListjenisApel = new LoginProcess.MtdGetDropdownListjenisApel().ToList();

        /*string[] fruits = { "apple", "passionfruit", "banana", "mango",
                      "orange", "blueberry", "grape", "strawberry" };

        List<int> lengths = fruits.Select(fruit => fruit.Length).ToList();*/

        //internal IEnumerable<ModelParameterAPELC> ToList()
        //{
        //  throw new NotImplementedException();
        //}

        //public List<ModelParameterAPELC> ListkatPenggunaDaftarAkaun = new LoginProcess.MtdGetDropdownListkatPenggunaDaftarAkaun().ToList();
        //public List<ModelParameterAPELC> ListKatPenggunaUPNM = new LoginProcess.MtdGetDropdownList().ToList();
        //public List<ModelParameterAPELC> ListJenisAPEL = PublicServices.ListBahagianAll("").ToList();
        //public List<ModelParameterHr> ListKampus = new SecurityConstants().ItemListKampus;
        //public List<ModelParameterHr> ListKatAduan = StatistikProcess.ListKatAduan().ToList();
        //public List<ModelParameterHr> ListKatPemohon = StatistikProcess.ListKatPemohon().ToList();
        //public List<ModelParameterHr> ListStsAduan = StatistikProcess.ListStsAduan().ToList();
        //public List<ModelParameterHr> ListJawatanAll = StatistikProcess.ListJawatanAll("KP").ToList();
    }
}
