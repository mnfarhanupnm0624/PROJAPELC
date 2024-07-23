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


namespace APELC.LocalServices.Login
{
    public class LoginDB
    {
        static readonly string ConnMySQLHrUpnm = PublicConstant.ConnUpnmDbDs();
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

//        myConnectionString = "server=127.0.0.1;uid=root;" +
//    "pwd=12345;database=test";

//try
//{
//    conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
//    conn.Open();
//}
//catch (MySql.Data.MySqlClient.MySqlException ex)
//{
//    MessageBox.Show(ex.Message);
//}

        public static UserDTOModel MtdGetPhotoStaf(UserDTOModel photo,MySqlConnection mySqlConnection)
        {
            //var _sql = "SELECT GAMBAR as PHOTO, '2' as RESULTSET FROM HR_GAMBAR WHERE NO_PEKERJA2 = :NOPEKERJA ";
            var _sql = LoginSQL.SQL_GetPhoto();
            UserDTOModel _result = new();
            _result.RESULTSET = "0";
            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
            {
                UserDTOModel _hasil = dbConn.QueryFirstOrDefault<UserDTOModel>(_sql, new { HRSTAFFK = photo.HRSTAFFK });
                if (_hasil != null && _hasil.RESULTSET == "2")
                {
                    _result = _hasil;
                    _result.RESULTSET = "2";
                }
            }
            return _result;
        }

        // Semakan Info Wujud Pengarah dari Info Staf_FK di Modul ACL
        public static ModelParameterHr DB_MtdPngrhWujud(string? _stafFk, string? _peranan)
        {
            string _sql = LoginSQL.SQL_MtdPngrhWujud();
            ModelParameterHr _result = new();
            _result.RESULTSET = "0";

            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
            {
                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
                if (_hasil != null && _hasil.Value == "1")
                {
                    _result = _hasil;
                    _result.RESULTSET = "2";
                }
            }
            return _result;
        }

        // Semakan Info Wujud Penyiasat dari Info Staf_FK di Modul ACL
        public static ModelParameterHr DB_MtdPnystKnnWujud(string? _stafFk, string? _peranan)
        {
            string _sql = LoginSQL.SQL_MtdPnystKnnWujud();
            ModelParameterHr _result = new();
            _result.RESULTSET = "0";

            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
            {
                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
                if (_hasil != null && _hasil.Value == "1")
                {
                    _result = _hasil;
                    _result.RESULTSET = "2";
                }
            }
            return _result;
        }

        // Semakan Info Wujud Penyiasat dari Info Staf_FK di Modul ACL
        public static ModelParameterHr DB_MtdPnystWujud(string? _stafFk, string? _peranan)
        {
            string _sql = LoginSQL.SQL_MtdPnystWujud();
            ModelParameterHr _result = new();
            _result.RESULTSET = "0";

            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
            {
                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk, PERANAN_FK = _peranan });
                if (_hasil != null && _hasil.Value == "1")
                {
                    _result = _hasil;
                    _result.RESULTSET = "2";
                }
            }
            return _result;
        }

        // Semakan Info Wujud Pembantu Penyiasat dari Info Staf_FK di HR_INV_DAFTAR_PNYST
        public static bool DB_MtdBntuPnystWujud(string? _stafFk)
        {
            string _sql = LoginSQL.SQL_MtdBntuPnystWujud();
            bool _result = false;

            using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
            {
                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafFk });
                if (_hasil != null && _hasil.Value == "1")
                {
                    _result = true;
                }
            }
            return _result;
        }


    }
}
