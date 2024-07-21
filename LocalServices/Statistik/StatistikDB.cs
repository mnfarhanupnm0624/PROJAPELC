//using Dapper;
using Net6HrPublicLibrary.Model;
//using Net6HrPublicLibrary.PublicShared;
using APELC.LocalServices.Senarai;
using APELC.LocalShared;
using APELC.Models;
//using Oracle.ManagedDataAccess.Client;

namespace APELC.LocalServices.Aduan
{
    public class StatistikDB
    {
        //static readonly string ConnOraHr = PublicConstant.ConnUtmDbDs();
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        //internal static ModelHrStaffMaklumatPeribadi DB_MtdGetMaklumatAsas(int _stafPk)
        //{
        //    ModelHrStaffMaklumatPeribadi _data = new();
        //    try
        //    {
        //        using (var dbConn = new OracleConnection(ConnOraHr))
        //        {
        //            _data = dbConn.QueryFirstOrDefault<ModelHrStaffMaklumatPeribadi>(AduanSQL.DB_MtdGetMaklumatAsas(), new { STAF_PK = _stafPk });
        //            if (_data != null)
        //            {
        //                _data.STAF_PK_ENC = EncryptHr.NewEncrypt(_data.STAF_PK.ToString(), _encryptCode);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("DB_MtdGetMaklumatAsas e ~ " + e);
        //    }

        //    return _data;
        //}

        //internal static ModelHrPengaduMaklumat DB_MtdGetApelPengadu(int _aduanPk)
        //{
        //    ModelHrPengaduMaklumat _data = new();
        //    try
        //    {
        //        using (var dbConn = new OracleConnection(ConnOraHr))
        //        {
        //            _data = dbConn.QueryFirstOrDefault<ModelHrPengaduMaklumat>(AduanSQL.SQL_MtdGetApelPengadu(), new { ADUAN_PK = _aduanPk });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("DB_MtdGetApelPengadu e ~ " + e);
        //    }

        //    return _data;
        //}

        public static IEnumerable<ModelHrStafPenyiasat> DB_MtdGetStafPenyiasatList(int AduanPk)
        {
            List<ModelHrStafPenyiasat> list = null;
            //try
            //{
            //    using (var dbConn = new OracleConnection(ConnOraHr))
            //    {
            //        IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(AduanSQL.SQL_MtdGetStafPenyiasatList(), new { Aduan_PK = AduanPk });
            //        if (_getList != null)
            //        {
            //            list = _getList.ToList();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var log = NLog.LogManager.GetCurrentClassLogger();
            //    log.Info("SenaraiDB DB_MtdGetStafPenyiasatList try catch ex.Message ~ " + ex.Message);
            //}

            return list;
        }

        //public static ModelMaklumatPeribadiPelajar MtdGetDataPelajarByNokp(string _nokp)
        //{
        //    ModelMaklumatPeribadiPelajar _return = new ModelMaklumatPeribadiPelajar();
        //    _return.RESULTSET = "0";
        //    _return.RESULTSET_TEXT = "BEGIN GET STUDENT RECORD";
        //    try
        //    {
        //        string _sQl = AduanSQL.SQL_GetStudentRecord(_nokp);
        //        //var log = NLog.LogManager.GetCurrentClassLogger();
        //        //log.Info("MtdGetDataPelajarByNokp _sQl ~ " + _sQl);

        //        using (var dbConn = new OracleConnection(ConnOraHr))
        //        {
        //            ModelMaklumatPeribadiPelajar _data = dbConn.QueryFirstOrDefault<ModelMaklumatPeribadiPelajar>(_sQl);
        //            if (_data != null && _data.RESULTSET == "1")
        //            {
        //                _return = _data;
        //                //_return.PHOTOSTUDENT = MtdGetStudenBhoto(_return.GRK_NOKP);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("MtdGetDataPelajarByNokp _nokp ~ " + _nokp);
        //        log.Info("MtdGetDataPelajarByNokp try catch ex.Message ~ " + ex.Message);
        //    }
        //    return _return;
        //}

        // Begin:
        // DDL
        //public static IEnumerable<ParameterHrModel> DB_ListKatAduan()
        //{
        //    using (var dbConn = new OracleConnection(ConnOraHr))
        //    {
        //        return dbConn.Query<ParameterHrModel>(AduanSQL.SQL_ListKatAduan());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> DB_ListKatPengadu()
        //{
        //    using (var dbConn = new OracleConnection(ConnOraHr))
        //    {
        //        return dbConn.Query<ParameterHrModel>(AduanSQL.SQL_ListKatPengadu());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> DB_ListStsAduan()
        //{
        //    using (var dbConn = new OracleConnection(ConnOraHr))
        //    {
        //        return dbConn.Query<ParameterHrModel>(AduanSQL.SQL_ListStsAduan());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> ListJawatanAll(string _kodjwtn)
        //{
        //    using (var dbConn = new OracleConnection(ConnOraHr))
        //    {
        //        return dbConn.Query<ParameterHrModel>(AduanSQL.SqlJawatanList(_kodjwtn));
        //    }
        //}
        // End: 
        // DDL
    }
}
