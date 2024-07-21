//using Dapper;
using Net6HrPublicLibrary.Model;
//using Net6HrPublicLibrary.PublicShared;
using APELC.LocalShared;
using APELC.Models;
//using Oracle.ManagedDataAccess.Client;

namespace APELC.LocalServices.Senarai
{
    public class SenaraiDB
    {
        //static readonly string ConnOraHr = PublicConstant.ConnUtmDbDs();
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        // Get Senarai Aduan Kes Siasatan Pengadu
        //public static IEnumerable<ModelHrPengadu> DB_MtdGetPengaduList(string? _noaduan, string? _katPengadu, string? _cKampus, string? _cStsAduan, string? _cKatAduan, string? _cTkhMula, string? _cTkhTamat, string userAll, int stafPk)
        //{
        //    List<ModelHrPengadu> list = null;
        //    try
        //    {
        //        using (var dbConn = new OracleConnection(ConnOraHr))
        //        {
        //            IEnumerable<ModelHrPengadu> _getList = dbConn.Query<ModelHrPengadu>(SenaraiSql.SQL_MtdGetPengaduList(_noaduan, _katPengadu, _cKampus, _cStsAduan, _cKatAduan, _cTkhMula, _cTkhTamat, userAll, stafPk));
        //            if (_getList != null)
        //            {
        //                list = _getList.ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("SenaraiDB DB_MtdGetPengaduList try catch ex.Message ~ " + ex.Message);
        //    }

        //    return list;
        //}
    }
}
