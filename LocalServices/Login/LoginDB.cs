using Dapper;
using APELC.Model;
using APELC.LocalShared;
using APELC.LocalServices;
using Net6HrPublicLibrary.PublicServices.Login;
////using Oracle.ManagedDataAccess.Client;
using System.Net;
using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data.Odbc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using ServiceStack.Model;
using Net6HrPublicLibrary.Model;
using System.Data;
using System.Collections.Generic;
//using System.Data.IDbTransaction;


namespace APELC.LocalServices.Login
{
    public interface IDbTransaction : IDisposable;
    public class LoginDB
    {


        //        myConnectionString = "server=127.0.0.1;uid=root;" +
        //    "pwd=12345;database=test";

        //try
        //{
        //    dbconn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        //    dbconn.Open();
        //}
    //catch (MySql.Data.MySqlClient.MySqlException ex)
    //{
    //    MessageBox.Show(ex.Message);
    //}



public class InMemoryDBContext : DbContext
        {
            public InMemoryDBContext(DbContextOptions<InMemoryDBContext> options) : base(options)
            { }

            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<JenisAPEL>().HasMany(c => c.Products).WithOne(a => a.JenisAPEL).HasForeignKey(a => a.Idjenis_APEL_param);

            //    modelBuilder.Seed();
            //}

            //public DbSet<JenisAPEL> JenisAPEL { get; set; }

            //public DbSet<KatPengguna> Categories { get; set; }
        }

       // internal static IEnumerable<ModelParameterAPELC> DB_MtdGetDropdownListkatPenggunaDaftarAkaun(IEnumerable<ModelParameterAPELC> _katpenggunadaftarakaun, IEnumerable<ModelParameterAPELC> ConnMySQLUpnm)
        //{

           // ModelParameterAPELC _sqlpenggunadaftarakaun = LoginSQL.SQL_MtdListKatPenggunaDaftarAkaun();
            //ModelParameterAPELC _result = new()
            //{
               // RESULTSET = "0"
            //};

            //try
           // {
               // using (var dbConn = new MySqlConnection(IEnumerable<ModelParameterAPELC> ConnMySQLUpnm))
               // {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                   // ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlpenggunadaftarakaun);

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                   // if (_hasil != null && _hasil.Value == "1")
                   // {
                     //   _result = _hasil;
                      //  _result.RESULTSET = "2";
                   // }

                //}
            //}
            //catch (Exception e)
           // {
               // var log = NLog.LogManager.GetCurrentClassLogger();
               // log.Info("Public MtdGetDropdownList e ~ " + e);
           // }
           // string _resultsetpda=_result.ToString();
            

           // return _resultsetpda;
       // }

        internal static string DB_MtdGetDropdownListjenisApel(string _jenisapel, string ConnMySQLUpnm)
        {

            string _sqljenisapel =  LoginSQL.SQL_MtdListJenisAPEL();
            ModelParameterAPELC _result = new()
            {
                RESULTSET = "0"
            };

            try
            {
                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqljenisapel);

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (_hasil != null && _hasil.Value == "1")
                    {
                        _result = _hasil;
                        _result.RESULTSET = "2";
                    }

                }
            }
            catch (Exception e)
            {
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Info("Public MtdGetDropdownList e ~ " + e);
            }
            string _resultsetja = _result.ToString();


            return _resultsetja;
        }

        //internal static string DB_MtdGetDropdownListkatPerananSuperuserDaftarAkaun(string _katperanansuperuserdaftarakaun, string ConnMySQLUpnm)
       // {

          //  string _sqlperanansuperuserdaftarakaun = LoginSQL.SQL_MtdListKatPerananSuperuserDaftarAkaun();
         //   ModelParameterAPELC _result = new()
          //  {
               // RESULTSET = "0"
          //  };

           // try
           // {
              //  using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
               // {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                  //  ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperanansuperuserdaftarakaun);

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                  //  if (_hasil != null && _hasil.Value == "1")
                   // {
                   //     _result = _hasil;
                   //     _result.RESULTSET = "2";
                   // }

               // }
           // }
           // catch (Exception e)
            //{
             //   var log = NLog.LogManager.GetCurrentClassLogger();
              //  log.Info("Public MtdGetDropdownList e ~ " + e);
           // }
          //  string _resultsetprsda = _result.ToString();


           // return _resultsetprsda;
        //}

//        internal static string DB_MtdGetDropdownListkatPerananPelajarDaftarAkaun(string _katperananpelajardaftarakaun,string ConnMySQLUpnm)
//        {

//            string _sqlperananpelajardaftarakaun = LoginSQL.SQL_MtdListKatPerananPelajarDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananpelajardaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprpda = _result.ToString();


//            return _resultsetprpda;
//        }

//        internal static string DB_MtdGetDropdownListkatPerananLuarUPNMDaftarAkaun(string _katperananluarupnmdaftarakaun, string ConnMySQLUpnm)
//        {

//            string _sqlperananluarupnmdaftarakaun = LoginSQL.SQL_MtdListKatPerananLuarUPNMDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananluarupnmdaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprlda = _result.ToString();


//            return _resultsetprlda;
//        }

//        internal static string DB_MtdGetDropdownListkatPerananUrusetiaAPELDaftarAkaun(string _katperananurusetiaapeldaftarakaun, string ConnMySQLUpnm)
//        {

//            string _sqlperananurusetiaapeldaftarakaun = LoginSQL.SQL_MtdListKatPerananUrusetiaAPELDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananurusetiaapeldaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprada = _result.ToString();


//            return _resultsetprada;
//        }

//        internal static string DB_MtdGetDropdownListkatPerananUrusetiaFakultiDaftarAkaun(string _katperananurusetiafakultidaftarakaun, string ConnMySQLUpnm)
//        {

//            string _sqlperananurusetiafakultidaftarakaun = LoginSQL.SQL_MtdListKatPerananUrusetiaFakultiDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananurusetiafakultidaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprfda = _result.ToString();


//            return _resultsetprfda;
//        }

//        internal static string DB_MtdGetDropdownListkatPerananBendahariDaftarAkaun(string _katperananbendaharidaftarakaun, string ConnMySQLUpnm)
//        {

//            string _sqlperananbendaharidaftarakaun = LoginSQL.SQL_MtdListKatPerananBendahariDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananbendaharidaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprbda = _result.ToString();


//            return _resultsetprbda;
//        }

//        internal static string DB_MtdGetDropdownListkatPerananPenasihatAkadDaftarAkaun(string _katperananpenasihatakaddaftarakaun, string ConnMySQLUpnm)
//        {

//            string _sqlperananpenasihatakaddaftarakaun = LoginSQL.SQL_MtdListKatPerananPenasihatAkadDaftarAkaun();
//            ModelParameterAPELC _result = new()
//            {
//                RESULTSET = "0"
//            };

//            try
//            {
//                using (var dbConn = new MySqlConnection(ConnMySQLUpnm))
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sqlperananpenasihatakaddaftarakaun);

//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    if (_hasil != null && _hasil.Value == "1")
//                    {
//                        _result = _hasil;
//                        _result.RESULTSET = "2";
//                    }

//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("Public MtdGetDropdownList e ~ " + e);
//            }
//            string _resultsetprpada = _result.ToString();


//            return _resultsetprpada;
//        }
        // Semakan Info Wujud Super User dari Info Staf_FK di Table Peranan UPNM

        //        public static IEnumerable<string> DB_PenggunaApelCUPNM(string _id_pengguna, string _kata_laluan)
        //        {
        //            string _sql = LoginSQL.SQL_MtdGetPenggunaApelCUPNM();
        //            PenggunaApelCMain _result = new();
        //            _result.RESULTSET = "0";

        //            try
        //            {
        //                using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLUpnm))
        //                {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                    PenggunaApelCMain _hasil = dbConn.QueryFirstOrDefault<PenggunaApelCMain>(_sql, new());
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                    if (_hasil != null && _hasil.Value == "1")
        //                    {
        //                        _result = _hasil;
        //                        _result.RESULTSET = "2";
        //                    }

        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                var log = NLog.LogManager.GetCurrentClassLogger();
        //                log.Info("Public MtdGetApelCPengguna e ~ " + e);
        //            }
        //            return (IEnumerable<string>)_result;
        //        }

        //        public static string DB_MtdSemakUserFromMyUPNMDecrypt(string _key)
        //        {
        //            string _returnString = "";
        //            var _sql = @"SELECT ID_PENGGUNA.DECRYPT(HEXTORAW(:KOD)) AS USER_GROUP FROM APELC_PENGGUNA_UPNM";
        //            UserIdModel _result = new UserIdModel();
        //            _result.RESULTSET = "0";
        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLUpnm))
        //            {
        //                UserIdModel _hasil = dbConn.QueryFirstOrDefault<UserIdModel>(_sql, new { KOD = _key });
        //                if (_hasil != null)
        //                {
        //                    _returnString = _hasil.USER_GROUP;
        //                }
        //            }
        //#pragma warning disable CS8603 // Possible null reference return.
        //            return _returnString;
        //#pragma warning restore CS8603 // Possible null reference return.
        //        }



        // Simpan rekod dan daftar akaun baru

        //        public static IEnumerable<string> DB_MtdGetPenggunaSimpan(string _id_pengguna, string _kata_laluan)
        //        {
        //            string _sql = LoginSQL.SQL_MtdGetPenggunaSimpan();
        //            PenggunaApelCMain _result = new();
        //            _result.RESULTSET = "0";

        //            try
        //            {
        //                using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLUpnm))
        //                {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                    PenggunaApelCMain _hasil = dbConn.QueryFirstOrDefault<PenggunaApelCMain>(_sql, new());
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                    if (_hasil != null && _hasil.Value == "1")
        //                    {
        //                        _result = _hasil;
        //                        _result.RESULTSET = "2";
        //                    }

        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                var log = NLog.LogManager.GetCurrentClassLogger();
        //                log.Info("Public MtdGetApelCPengguna e ~ " + e);
        //            }
        //            return (IEnumerable<string>)_result;
        //        }

        // Semakan Info Wujud Super User dari Info Staf_FK di Table Peranan UPNM
        //        public static ModelParameterAPELC DB_MtdSuperUserWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdSuperUserWujud();
        //            ModelParameterAPELC _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterAPELC _hasil = dbConn.QueryFirstOrDefault<ModelParameterAPELC>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Pentadbir APELC dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPentadbirAPELCWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPentadbirAPELCWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Bendahari dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdBendahariWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdBendahariWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Peranan dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPemohonWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPemohonWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }


        // Semakan Info Wujud Pengawas Ujian Cabaran dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPengawasUjianCbrnWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPengawasUjianCbrnWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Panel Penilai dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPanelPenilaiWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPanelPenilaiWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Panel Penilai dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdModeratorWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdModeratorWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Penasihat Akademik dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPenasihatAkademikWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPenasihatAkademikWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }


        // Semakan Info Wujud Penggubal dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPenggubalWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPenggubalWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Penilai Instrumen dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdPenilaiInstrumenWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdPenilaiInstrumenWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud JK Fakulti dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdJKFakultiTimbDekanWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdJKFakultiTimbDekanWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }

        // Semakan Info Wujud Senat dari Info Staf_FK di Table Peranan ACL
        //        public static ModelParameterHr DB_MtdSenatWujud(string? _stafFk, string? _peranan)
        //        {
        //            string _sql = ParameterSQL.SQL_MtdSenatWujud();
        //            ModelParameterHr _result = new();
        //            _result.RESULTSET = "0";

        //            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //            {
        //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                if (_hasil != null && _hasil.Value == "1")
        //                {
        //                    _result = _hasil;
        //                    _result.RESULTSET = "2";
        //                }
        //            }
        //            return _result;
        //        }



    }
}
