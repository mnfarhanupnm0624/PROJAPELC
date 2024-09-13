using Dapper;
using APELC.Model;
using APELC.LocalShared;
////using Oracle.ManagedDataAccess.Client;

namespace APELC.LocalServices.Senarai
{
    public class SenaraiDB
    {
        static readonly string ConnMySQLHrUpnm = LocalConstant.ConnMySQLUpnmDbDs();
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        // Get Senarai Aduan Kes Siasatan Pemohon
        //public static IEnumerable<ModelHrPemohon> DB_MtdGetPemohonList(string? _noaduan, string? _katPemohon, string? _cKampus, string? _cStsAduan, string? _cKatAduan, string? _cTkhMula, string? _cTkhTamat, string userAll, int stafPk)
        //{
        //    List<ModelHrPemohon> list = null;
        //    try
        //    {
        //        using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //        {
        //            IEnumerable<ModelHrPemohon> _getList = dbConn.Query<ModelHrPemohon>(SenaraiSql.SQL_MtdGetPemohonList(_noaduan, _katPemohon, _cKampus, _cStsAduan, _cKatAduan, _cTkhMula, _cTkhTamat, userAll, stafPk));
        //            if (_getList != null)
        //            {
        //                list = _getList.ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("SenaraiDB DB_MtdGetPemohonList try catch ex.Message ~ " + ex.Message);
        //    }

        //    return list;
        //}
    }
}
