using Dapper;
using APELC.Model;
using APELC.LocalShared;
using APELC.LocalServices.Login;
////using Oracle.ManagedDataAccess.Client;
//namespace MySql.Data.MySqlClient.MySqlConnection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using APELC.PublicShared;
using APELC.LocalServices.Senarai;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;


namespace APELC.LocalServices.Login
{
//    public class LoginDB
//    {
//        static readonly string ConnMySQLHrUpnm = PublicConstant.ConnUpnmDbDs();
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();

////        myConnectionString = "server=127.0.0.1;uid=root;" +
////    "pwd=12345;database=test";

////try
////{
////    conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
////    conn.Open();
////}
////catch (MySql.Data.MySqlClient.MySqlException ex)
////{
////    MessageBox.Show(ex.Message);
////}

//        public static ModelUserDTO MtdGetPhotoStaf(ModelUserDTO photo,MySqlConnection mySqlConnection)
//        {
//            //var _sql = "SELECT GAMBAR as PHOTO, '2' as RESULTSET FROM HR_GAMBAR WHERE NO_PEKERJA2 = :NOPEKERJA ";
//            var _sql = LoginSQL.SQL_GetPhoto();
//            ModelUserDTO _result = new();
//            _result.RESULTSET = "0";
//            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
//            {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                ModelUserDTO _hasil = dbConn.QueryFirstOrDefault<ModelUserDTO>(_sql, new { HRSTAFFK = photo.HRSTAFFK });
//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                if (_hasil != null && _hasil.RESULTSET == "2")
//                {
//                    _result = _hasil;
//                    _result.RESULTSET = "2";
//                }
//            }
//            return _result;
//        }

//        // Semakan Info Wujud Super User dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdSuperUserWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdSuperUserWujud();
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

//        // Semakan Info Wujud Pentadbir APELC dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPentadbirAPELCWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPentadbirAPELCWujud();
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

//        // Semakan Info Wujud Bendahari dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdBendahariWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdBendahariWujud();
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

//        // Semakan Info Wujud Pemohon dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPemohonWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPemohonWujud();
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


//        // Semakan Info Wujud Pengawas Ujian Cabaran dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPengawasUjianCbrnWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPengawasUjianCbrnWujud();
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

//        // Semakan Info Wujud Panel Penilai dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPanelPenilaiWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPanelPenilaiWujud();
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

//        // Semakan Info Wujud Panel Penilai dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdModeratorWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdModeratorWujud();
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

//        // Semakan Info Wujud Penasihat Akademik dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPenasihatAkademikWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPenasihatAkademikWujud();
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


//        // Semakan Info Wujud Penggubal dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPenggubalWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPenggubalWujud();
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

//        // Semakan Info Wujud Penilai Instrumen dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdPenilaiInstrumenWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdPenilaiInstrumenWujud();
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

//        // Semakan Info Wujud JK Fakulti dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdJKFakultiWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdJKFakultiWujud();
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

//        // Semakan Info Wujud Senat dari Info Staf_FK di Table Peranan ACL
//        public static ModelParameterHr DB_MtdSenatWujud(string? _stafFk, string? _peranan)
//        {
//            string _sql = LoginSQL.SQL_MtdSenatWujud();
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



//    }
}
