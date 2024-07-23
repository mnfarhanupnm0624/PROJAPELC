using Dapper;
using APELC.PublicShared;
using APELC.LocalServices.Senarai;
using APELC.LocalShared;
using APELC.Model;
////using Oracle.ManagedDataAccess.Client;

namespace APELC.LocalServices.ApelCDB
{
    public class ApelCDB
    {
        static readonly string ConnMySQLHrUpnm = PublicConstant.ConnUpnmDbDs();
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        //internal static ModelHrStaffMaklumatPeribadi DB_MtdGetMaklumatAsas(int _stafPk)
        //{
        //    ModelHrStaffMaklumatPeribadi _data = new();
        //    try
        //    {
        //        using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //        {
        //            _data = dbConn.QueryFirstOrDefault<ModelHrStaffMaklumatPeribadi>(ApelCSQL.DB_MtdGetMaklumatAsas(), new { STAF_PK = _stafPk });
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

        //internal static ModelHrPemohonMaklumat DB_MtdGetApelPemohon(int _ApelCPk)
        //{
        //    ModelHrPemohonMaklumat _data = new();
        //    try
        //    {
        //        using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //        {
        //            _data = dbConn.QueryFirstOrDefault<ModelHrPemohonMaklumat>(ApelCSQL.SQL_MtdGetApelPemohon(), new { ADUAN_PK = _ApelCPk });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var log = NLog.LogManager.GetCurrentClassLogger();
        //        log.Info("DB_MtdGetApelPemohon e ~ " + e);
        //    }

        //    return _data;
        //}

        public static IEnumerable<ModelHrStafPenyiasat> DB_MtdGetStafPenyiasatList(int ApelCPk)
        {
            List<ModelHrStafPenyiasat> list = null;
            try
            {
                //using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
                //{
                //    IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(SelenggaraSQL.SQL_MtdGetStafPenyiasatList(), new { Aduan_PK = ApelCPk });
                //    if (_getList != null)
                //    {
                //        list = _getList.ToList();
                //    }
                //}
            }
            catch (Exception ex)
            {
                //var log = NLog.LogManager.GetCurrentClassLogger();
                //log.Info("SenaraiDB DB_MtdGetStafPenyiasatList try catch ex.Message ~ " + ex.Message);
            }

            return list;
        }

        //public static ModelMaklumatPeribadiPelajar MtdGetDataPelajarByNokp(string _nokp)
        //{
        //    ModelMaklumatPeribadiPelajar _return = new ModelMaklumatPeribadiPelajar();
        //    _return.RESULTSET = "0";
        //    _return.RESULTSET_TEXT = "BEGIN GET STUDENT RECORD";
        //    try
        //    {
        //        string _sQl = ApelCSQL.SQL_GetStudentRecord(_nokp);
        //        //var log = NLog.LogManager.GetCurrentClassLogger();
        //        //log.Info("MtdGetDataPelajarByNokp _sQl ~ " + _sQl);

        //        using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
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
        //    using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //    {
        //        return dbConn.Query<ParameterHrModel>(SelenggaraSQL.SQL_ListKatAduan());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> DB_ListKatPemohon()
        //{
        //    using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //    {
        //        return dbConn.Query<ParameterHrModel>(SelenggaraSQL.SQL_ListKatPemohon());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> DB_ListStsAduan()
        //{
        //    using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //    {
        //        return dbConn.Query<ParameterHrModel>(SelenggaraSQL.SQL_ListStsAduan());
        //    }
        //}

        //public static IEnumerable<ParameterHrModel> ListJawatanAll(string _kodjwtn)
        //{
        //    using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnm))
        //    {
        //        return dbConn.Query<ParameterHrModel>(SelenggaraSQL.SqlJawatanList(_kodjwtn));
        //    }
        //}
        // End: 
        // DDL
    }
}
