//using Dapper;
//using Net6HrPublicLibrary.Model;
//using Net6HrPublicLibrary.PublicShared;
//using APEL.LocalServices.Login;
//using APEL.LocalShared;
//using APEL.Models;
//using Oracle.ManagedDataAccess.Client;
//using System.IO;
//using System.Xml.Linq;

//namespace APEL.LocalServices.Siasatan
//{
//    public class SiasatanDB
//    {
//        static readonly string ConnOraHr = PublicConstant.ConnUtmDbDs();
//        static readonly string ConnOraAkademik = PublicConstant.ConnUtmDbAkademik();
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();

//        internal static ModelHrPengadu DB_MtdGetApelSession(int _aduanPk)
//        {
//            ModelHrPengadu _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrPengadu>(SiasatanSQL.SQL_MtdGetApelSession(), new { ADUAN_PK = _aduanPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetApelSession e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrStaffPeribadi DB_MtdGetMaklumatAsas(int _stafPk)
//        {
//            ModelHrStaffPeribadi _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrStaffPeribadi>(SiasatanSQL.SQL_MtdGetMaklumatAsasByStafPk(), new { STAF_PK = _stafPk });
//                    if (_data != null)
//                    {
//                        _data.STAF_PK_ENC = EncryptHr.NewEncrypt(_data.STAF_PK.ToString(), _encryptCode);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetMaklumatAsas e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrPengadu DB_MtdGetApelPengadu(int _aduanPk)
//        {
//            ModelHrPengadu _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrPengadu>(SiasatanSQL.SQL_MtdGetApel(), new { ADUAN_PK = _aduanPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetApelPengadu e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrPengadu DB_MtdGetApelPenerima(int _aduanPk)
//        {
//            ModelHrPengadu _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrPengadu>(SiasatanSQL.SQL_MtdGetApel(), new { ADUAN_PK = _aduanPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetApelPenerima e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrVisitorPeribadi DB_GetDataVisitorByPk(string? _visitorPk)
//        {
//            ModelHrVisitorPeribadi _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrVisitorPeribadi>(SiasatanSQL.SQL_GetDataVisitorByPk(), new { MAKLUMAT_PERIBADI_FK = _visitorPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetDataVisitorByPk e ~ " + e);
//            }

//            return _data;
//        }

//        public static IEnumerable<ModelHrStafPenyiasat> DB_MtdGetStafPenyiasatList(int siasatanPk)
//        {
//            List<ModelHrStafPenyiasat> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(SiasatanSQL.SQL_MtdGetStafPenyiasatList(), new { SIASATAN_PK = siasatanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SenaraiDB DB_MtdGetStafPenyiasatList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static ModelPelajarPeribadi MtdGetDataPelajarByNokp(string? _nokp)
//        {
//            ModelPelajarPeribadi _return = new ModelPelajarPeribadi();
//            _return.RESULTSET = "0";
//            _return.RESULTSET_TEXT = "BEGIN GET STUDENT RECORD";
//            try
//            {
//                string _sQl = SiasatanSQL.SQL_GetStudentRecord(_nokp);

//                using (var dbConn = new OracleConnection(ConnOraAkademik))
//                {
//                    ModelPelajarPeribadi _data = dbConn.QueryFirstOrDefault<ModelPelajarPeribadi>(_sQl);
//                    if (_data != null && _data.RESULTSET == "1")
//                    {
//                        _return = _data;
//                        //_return.PHOTOSTUDENT = MtdGetStudenBhoto(_return.GRK_NOKP);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("MtdGetDataPelajarByNokp _nokp ~ " + _nokp);
//                log.Info("MtdGetDataPelajarByNokp try catch ex.Message ~ " + ex.Message);
//            }
//            return _return;
//        }

//        // Get Senarai Staf - Daftar Penyiasat
//        public static IEnumerable<ModelHrStafPenyiasat> DB_MtdGetStafPnystList(int siasatanPk, string? _cNoPekerja, string? _cNama, string? _cKampus, string? _cJawatan, string? _cStsAktif, string userAll)
//        {
//            List<ModelHrStafPenyiasat> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(SiasatanSQL.SQL_MtdGetStafPnystList(_cNoPekerja, _cNama, _cKampus, _cJawatan, _cStsAktif, userAll), new { SIASATAN_PK = siasatanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdGetStafPnystList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static ModelParameterHr DB_MtdGetKodKategori(string? kodkategoriFk)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.QueryFirstOrDefault<ModelParameterHr>(SiasatanSQL.SQL_MtdGetKodKategori(), new { PARAM_PK = kodkategoriFk });
//            }
//        }

//        public static ModelParameterHr DB_MtdGetKodKategoriSemakSediada(string? kod, int siasatanPk)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.QueryFirstOrDefault<ModelParameterHr>(SiasatanSQL.SQL_MtdGetKodKategoriSemakSediada(), new { KOD = kod, SIASATAN_PK = siasatanPk });
//            }
//        }

//        public static int MtdGetHrPenyiasatInsert(ModelHrStafPenyiasat _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_GetHrPenyiasatInsert(), new
//                {
//                    SIASATAN_FK = _dataIn.SIASATAN_PK,
//                    STAF_PP_FK = _dataIn.STAF_PP_FK,
//                    KATEGORI_KOD_FK = _dataIn.KATEGORI_KOD_FK,
//                    KOD_PRNN_PNYST = _dataIn.KOD_PRNN_PNYST,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static ModelParameterHr DB_MtdGetKodPerananSemakSediada(string? kodPeranan, int siasatanPk)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.QueryFirstOrDefault<ModelParameterHr>(SiasatanSQL.SQL_MtdGetKodPerananSemakSediada(), new
//                {
//                    KOD_PRNN_PRCKPN = kodPeranan,
//                    SIASATAN_FK = siasatanPk
//                });
//            }
//        }

//        internal static int MtdRakamanPrckpnInsert(ModelHrRakaman _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0, _insert2 = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    var query_result = dbConn.Query<int>(SiasatanSQL.SQLGetNextValSEQ("SEQ_HR_INV_PERIBADI_PRCKPN"));
//                    int _temp = query_result.FirstOrDefault();
//                    _dataIn.PERIBADI_PRCKPN_PK = (int)_temp;

//                    _insert = MtdInsertPeribadiPrckpn(_dataIn);

//                    if (_dataIn.CKTGRI_PRCKPN_FK == "1011" || _dataIn.CKATEGORI_BRG_FK == 1015) // Saksi dan Kategori OKT
//                    { 
//                        if (_dataIn.KATEGORI_SAKSI == "421") // Staf
//                        {
//                            _insert2 = MtdInsertPerincianStaf(_dataIn);
//                        }
//                        if (_dataIn.KATEGORI_SAKSI == "422") // Student
//                        {
//                            _insert2 = MtdInsertPerincianStudent(_dataIn);
//                        }
//                    }

//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                        if (_insert2 > 0)
//                        {
//                            _data = 1;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB MtdRakamanPrckpnInsert try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdInsertPeribadiPrckpn(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertPeribadiPrckpn(), new
//                {
//                    PERIBADI_PRCKPN_PK = _dataIn.PERIBADI_PRCKPN_PK,
//                    SIASATAN_FK = _dataIn.SIASATAN_FK,
//                    KOD_PRNN_PNYST_FK = _dataIn.DAFTAR_PNYST_PK,
//                    KATEGORI_BRG_FK = _dataIn.CKATEGORI_BRG_FK,
//                    KTGRI_PRCKPN_FK = _dataIn.CKTGRI_PRCKPN_FK,
//                    KOD_PRNN_PRCKPN = _dataIn.CKOD_PRNN_PRCKPN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdInsertPerincianStaf(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertSaksiPrckpnStaf(), new
//                {
//                    PERIBADI_PRCKPN_FK = _dataIn.PERIBADI_PRCKPN_PK,
//                    STAF_LAIN2_SAKSI_OKT_FK = _dataIn.NO_ID,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdInsertPerincianStudent(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertSaksiPrckpnStudent(), new
//                {
//                    PERIBADI_PRCKPN_FK = _dataIn.PERIBADI_PRCKPN_PK,
//                    NO_KP = _dataIn.NO_ID,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdInsertPerincianPrckpn(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertPerincianPrckpn(), new
//                {
//                    PERIBADI_PRCKPN_FK = _dataIn.PERIBADI_PRCKPN_PK,
//                    KOD_PRNN_PNYST_FK = _dataIn.DAFTAR_PNYST_PK,
//                    STATUS_PRCKPN = "DRAF",
//                    KOD_BRG_PRCKPN = _dataIn.KOD_BRG_PRCKPN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static int DB_MtdGetDaftarPnystPk(int _siasatanPk, int _stafPk)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.QueryFirstOrDefault<int>(SiasatanSQL.DB_MtdGetDaftarPnystPk(), new
//                {
//                    SIASATAN_FK = _siasatanPk,
//                    STAF_PP_FK = _stafPk
//                });
//            }
//        }

//        public static IEnumerable<ModelHrRakaman> DB_MtdGetPeribadiPrckpnList(int siasatanPk, string? _kodBrgFk)
//        {
//            List<ModelHrRakaman> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrRakaman> _getList = dbConn.Query<ModelHrRakaman>(SiasatanSQL.SQL_MtdGetPeribadiPrckpnList(), new { SIASATAN_PK = siasatanPk, KATEGORI_BRG_FK = _kodBrgFk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SenaraiDB DB_MtdGetPrckpnPengaduSaksiList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static IEnumerable<ModelHrRakaman> DB_MtdGetPrckpnList(int siasatanPk, string? _kodPeranan, string? _kodBrgFk)
//        {
//            List<ModelHrRakaman> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrRakaman> _getList = dbConn.Query<ModelHrRakaman>(SiasatanSQL.SQL_MtdGetPrckpnList(_kodPeranan), new { SIASATAN_PK = siasatanPk, KATEGORI_BRG_FK = _kodBrgFk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SenaraiDB DB_MtdGetPrckpnPengaduSaksiList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static ModelHrRakaman DB_GetInfoPeribadiPrckpn(int _peribadiPrckpnpk)
//        {
//            ModelHrRakaman _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrRakaman>(SiasatanSQL.SQL_GetInfoPeribadiPrckpn(), new { PERIBADI_PRCKPN_PK = _peribadiPrckpnpk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetInfoPeribadiPrckpn e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrStaffPeribadi DB_GetInfoStafDetails(string? _noKp)
//        {
//            ModelHrStaffPeribadi _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrStaffPeribadi>(SiasatanSQL.SQL_GetInfoStafDetails(), new { NO_KP = _noKp });
//                    if (_data != null)
//                    {
//                        _data.STAF_PK_ENC = EncryptHr.NewEncrypt(_data.STAF_PK.ToString(), _encryptCode);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetInfoStafDetails e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrRakaman DB_GetPerincianPrbdiDetails(int _perincianPrbdiPk)
//        {
//            ModelHrRakaman _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrRakaman>(SiasatanSQL.SQL_GetPerincianPrbdiDetails(), new { PERINCIAN_PRCKPN_PK = _perincianPrbdiPk });
//                    if (_data != null)
//                    {
//                        _data.PERINCIAN_PRCKPN_PK_ENC = EncryptHr.NewEncrypt(_data.PERINCIAN_PRCKPN_PK.ToString(), _encryptCode);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetPerincianPrbdiDetails e ~ " + e);
//            }

//            return _data;
//        }

//        internal static string DB_GetInfoPelajarDetails(string? _noKp)
//        {
//            string _data = "";
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraAkademik))
//                {
//                    _data = dbConn.QueryFirstOrDefault<string>(SiasatanSQL.SQL_GetInfoPelajarDetails(), new { NO_KP = _noKp });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetInfoStafDetails e ~ " + e);
//            }

//            return _data;
//        }

//        // Get Diari Penyiasat
//        public static IEnumerable<ModelHrStafPenyiasat> DB_MtdGetDiariPenyiasatList(int siasatanPk)
//        {
//            List<ModelHrStafPenyiasat> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(SiasatanSQL.SQL_MtdGetDiariPenyiasatList(), new { SIASATAN_PK = siasatanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdGetDiariPenyiasatList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static ModelHrLampiran DB_GetLampiranDoc(int _lampiranPk)
//        {
//            ModelHrLampiran _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrLampiran>(SiasatanSQL.SQL_GetLampiranDocList(), new { LAMPIRAN_PK = _lampiranPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetLaporanPolis e ~ " + e);
//            }

//            return _data;
//        }
//        internal static ModelHrLampiran DB_GetInvLampiranDoc(int _lampiranPk)
//        {
//            ModelHrLampiran _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrLampiran>(SiasatanSQL.SQL_GetLampiran(), new { INV_LAMPIRAN_PK = _lampiranPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetInvLampiranDoc e ~ " + e);
//            }

//            return _data;
//        }

//        internal static ModelHrPengadu DB_MtdGetInitialEvent(int _siasatanPk)
//        {
//            ModelHrPengadu _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrPengadu>(SiasatanSQL.SQL_MtdGetInitialEvent(), new { SIASATAN_PK = _siasatanPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdGetInitialEvent e ~ " + e);
//            }

//            return _data;
//        }

//        internal static int DB_MtdUpdateKeteranganPS(ModelHrRakaman _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _update = 0, _insert = 0, _hapus = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _update = MtdUpdateKeteranganPS(_dataIn);

//                    if (_dataIn.KATEGORI_PENTERJEMAH != "")
//                    {
//                        if (_dataIn.PRCKPN_TERJEMAHAN_PK > 0)
//                        {
//                            _hapus = MtdHapusPrckpnTerjemah(_dataIn);
//                        }
//                        if (_dataIn.KATEGORI_PENTERJEMAH == "421") // Staf
//                        {
//                            _insert = MtdInsertPerincianTerjemahStaf(_dataIn);
//                        }
//                        if (_dataIn.KATEGORI_PENTERJEMAH == "422") // Pelajar
//                        {
//                            _insert = MtdInsertPerincianTerjemahPelajar(_dataIn);
//                        }
//                    }

//                    if (_update > 0)
//                    {
//                        _data = 1;
//                        if (_insert > 0)
//                        {
//                            _data = 1;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdUpdateKeteranganPS try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdUpdateKeteranganPS(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_UpdateKeteranganPS(), new
//                {
//                    KOD_PRNN_PNYST_FK = _dataIn.DAFTAR_PNYST_PK,
//                    PERINCIAN_PRCKPN_PK = _dataIn.PERINCIAN_PRCKPN_PK,
//                    CHECKED = _dataIn.CHECKED,
//                    CHECKED2 = _dataIn.CHECKED2,
//                    SOALAN_JAWAPAN = _dataIn.SOALAN_JAWAPAN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static int MtdInsertPerincianTerjemahStaf(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertPerincianTerjemahStaf(), new
//                {
//                    PERINCIAN_PRCKPN_FK = _dataIn.PERINCIAN_PRCKPN_PK,
//                    PENTERJEMAH_FK = _dataIn.NO_ID,
//                    KATEGORI_PENTERJEMAH_FK = _dataIn.KATEGORI_PENTERJEMAH,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdInsertPerincianTerjemahPelajar(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertPerincianTerjemahPelajar(), new
//                {
//                    PERINCIAN_PRCKPN_FK = _dataIn.PERINCIAN_PRCKPN_PK,
//                    NO_KP = _dataIn.NO_ID,
//                    KATEGORI_PENTERJEMAH_FK = _dataIn.KATEGORI_PENTERJEMAH,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdHapusPrckpnTerjemah(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_HapusPrckpnTerjemah(), new
//                {
//                    PRCKPN_TERJEMAHAN_PK = _dataIn.PRCKPN_TERJEMAHAN_PK,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static ParameterHrModel DB_MtdGetKodBorangPS(int _peribadiPrckpnPk, string? _kodPeranan)
//        {
//            ParameterHrModel _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ParameterHrModel>(SiasatanSQL.SQL_MtdGetKodBorangPS(_kodPeranan), new { PERIBADI_PRCKPN_FK = _peribadiPrckpnPk });
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdGetKodBorangPS try catch ex.Message ~ " + ex.Message);
//            }

//            return _data;
//        }

//        internal static int InsertPerincianKodBrg(ModelHrRakaman _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _insert = InsertPerincianKodBrgLib(_dataIn);
//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB InsertPerincianKodBrg try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int InsertPerincianKodBrgLib(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertPerincianPrckpn(), new
//                {
//                    PERIBADI_PRCKPN_FK = _dataIn.PERIBADI_PRCKPN_PK,
//                    KOD_PRNN_PNYST_FK = _dataIn.DAFTAR_PNYST_PK,
//                    STATUS_PRCKPN = "DRAF",
//                    KOD_BRG_PRCKPN = _dataIn.KOD_BRG_PRCKPN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        internal static int DB_MtdUpdateInitialEvent(ModelHrPengadu _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _update = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _update = MtdUpdateInitialEvent(_dataIn);

//                    if (_update > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdUpdateInitialEvent try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdUpdateInitialEvent(ModelHrPengadu _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_MtdUpdateInitialEvent(), new
//                {
//                    SIASATAN_PK = _dataIn.SIASATAN_PK,
//                    TAJUK_RNGKSN = _dataIn.TAJUK_RNGKSN,
//                    AKBT_KJDN = _dataIn.AKBT_KJDN,
//                    RMSN_AWAL = _dataIn.RMSN_AWAL,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        internal static bool DB_MtdInsertLampiran(ModelHrLampiran _lampiran)
//        {
//            bool _return = false;
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    int _result = dbConn.Execute(SiasatanSQL.SQL_MtdInsertLampiran(), new
//                    {
//                        PENCIPTA_FK = _lampiran.PENCIPTA_FK,
//                        SIASATAN_FK = _lampiran.SIASATAN_PK,
//                        KATEGORI_KOD_FK = _lampiran.KATEGORI_KOD_FK,
//                        PATH = _lampiran.PATH,
//                        KOD_LMPRN_RKMN = _lampiran.KOD_LMPRN_RKMN,
//                        KOD_PRNN_PNYST_FK = _lampiran.DAFTAR_PNYST_PK,
//                        TAJUK = _lampiran.TAJUK,
//                        RAKAMAN_PRCKPN_FK = _lampiran.RAKAMAN_PRCKPN_FK,
//                        NAMA_FAIL = _lampiran.NAMA_FAIL,
//                        CTTN_LMPRN = _lampiran.CTTN_LMPRN,
//                        URL = _lampiran.URL,
//                    });

//                    if (_result > 0)
//                    {
//                        _return = true;
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdInsertLampiran e ~ " + e);
//            }
//            return _return;
//        }

//        public static IEnumerable<ModelHrLampiran> DB_GetLampiranList(int siasatanPk, string _kategoriKod)
//        {
//            List<ModelHrLampiran> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrLampiran> _getList = dbConn.Query<ModelHrLampiran>(SiasatanSQL.SQL_GetLampiranList(), new { SIASATAN_PK = siasatanPk, KATEGORI_KOD_FK = _kategoriKod });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetLampiranList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static ModelHrLampiran DB_GetLampiran(int _lampiranPk)
//        {
//            ModelHrLampiran _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrLampiran>(SiasatanSQL.SQL_GetLampiran(), new { INV_LAMPIRAN_PK = _lampiranPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetLampiranPS e ~ " + e);
//            }

//            return _data;
//        }

//        internal static bool DB_MtdUpdateLampiran(ModelHrLampiran _lampiran)
//        {
//            bool _return = false;
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    int _result = dbConn.Execute(SiasatanSQL.SQL_MtdUpdateLampiran(), new
//                    {
//                        PENCIPTA_FK = _lampiran.PENCIPTA_FK,
//                        INV_LAMPIRAN_PK = _lampiran.INV_LAMPIRAN_PK,
//                        PATH = _lampiran.PATH,
//                        KOD_PRNN_PNYST_FK = _lampiran.DAFTAR_PNYST_PK,
//                        TAJUK = _lampiran.TAJUK,
//                        NAMA_FAIL = _lampiran.NAMA_FAIL,
//                        CTTN_LMPRN = _lampiran.CTTN_LMPRN,
//                        URL = _lampiran.URL,
//                    });

//                    if (_result > 0)
//                    {
//                        _return = true;
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_MtdInsertLampiran e ~ " + e);
//            }
//            return _return;
//        }

//        internal static string DB_GetKodPerananPrckpn(string? _katBrg)
//        {
//            string _data = "";
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<string>(SiasatanSQL.SQL_GetKodPerananPrckpn(), new { PARAM_PK = _katBrg });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetKodPerananPrckpn e ~ " + e);
//            }

//            return _data;
//        }

//        public static ParameterHrModel DB_GetPerananOkt(int _siasatanPk, string? _katBrg)
//        {
//            ParameterHrModel _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ParameterHrModel>(SiasatanSQL.SQL_GetPerananOkt(), new { SIASATAN_FK = _siasatanPk, KATEGORI_BRG_FK = _katBrg });
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetPerananOkt try catch ex.Message ~ " + ex.Message);
//            }

//            return _data;
//        }

//        internal static int DB_UpdateStatus(ModelHrRakaman _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _update = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _update = MtdUpdateStatus(_dataIn);
//                    if (_update > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_UpdateStatus try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdUpdateStatus(ModelHrRakaman _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                string _sql;
//                if (_dataIn.STATUS_PRCKPN == "BATAL")
//                {
//                    _sql = SiasatanSQL.SQL_HapusStatus();
//                    _dataIn.STATUS_PRCKPN = "BATAL";
//                }
//                else
//                {
//                    _sql = SiasatanSQL.SQL_UpdateStatus();
//                    _dataIn.STATUS_PRCKPN = "SELESAI";
//                }
//                return dbConn.Execute(_sql, new
//                {
//                    PERINCIAN_PRCKPN_PK = _dataIn.PERINCIAN_PRCKPN_PK,
//                    STATUS_PRCKPN = _dataIn.STATUS_PRCKPN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static IEnumerable<ModelHrLampiran> DB_GetLampiranDocList(int aduanPk)
//        {
//            List<ModelHrLampiran> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrLampiran> _getList = dbConn.Query<ModelHrLampiran>(SiasatanSQL.SQL_GetLampiranDocList(), new { ADUAN_FK = aduanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetLampiranDocList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static ParameterHrModel DB_GetPerananInvLampiran(int _siasatanPk, string? _katBrg)
//        {
//            ParameterHrModel _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ParameterHrModel>(SiasatanSQL.SQL_GetPerananInvLampiran(), new { SIASATAN_FK = _siasatanPk, KATEGORI_KOD_FK = _katBrg });
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetPerananInvLampiran try catch ex.Message ~ " + ex.Message);
//            }

//            return _data;
//        }



//        internal static int DB_MtdUpdatePengesahan(ModelHrPengadu _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _update = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _update = MtdUpdatePengesahan(_dataIn);

//                    if (_update > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdUpdatePengesahan try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdUpdatePengesahan(ModelHrPengadu _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_MtdUpdatePengesahan(), new
//                {
//                    BTRN_ULSN = _dataIn.BTRN_ULSN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK,
//                    INV_PENGESAHAN_PK = _dataIn.INV_PENGESAHAN_PK
//                });
//            }
//        }

//        internal static int DB_MtdInsertPengesahan(ModelHrPengadu _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _insert = MtdInsertPengesahan(_dataIn);

//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdInsertPengesahan try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdInsertPengesahan(ModelHrPengadu _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_MtdInsertPengesahan(), new
//                {
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK,
//                    SIASATAN_FK = _dataIn.SIASATAN_PK,
//                    KOD_APPROVAL_FK = _dataIn.APPROVAL_ROLE_PK,
//                    STATUS_FK = _dataIn.STATUS_FK,
//                    BTRN_ULSN = _dataIn.BTRN_ULSN,
//                    PENGESAH_STATUS = "N",
//                    KATEGORI_PENGESAHAN_FK = _dataIn.KATEGORI_PENGESAHAN_FK
//                });
//            }
//        }


//        internal static int DB_MtdInsertInitialApprove(ModelHrPengadu _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _insert = MtdInsertInitialApprove(_dataIn);

//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_MtdInsertInitialApprove try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdInsertInitialApprove(ModelHrPengadu _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_MtdInsertInitialApprove(), new
//                {
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK,
//                    SIASATAN_FK = _dataIn.SIASATAN_PK,
//                    KOD_APPROVAL_FK = _dataIn.APPROVAL_ROLE_PK,
//                    PENGESAH_STATUS = "Y",
//                    STATUS_FK = "349",
//                    KATEGORI_PENGESAHAN_FK = "5127"
//                });
//            }
//        }

//        public static IEnumerable<ModelHrStafPenyiasat> DB_GetPengesahanList(int siasatanPk, string _katpengesahan)
//        {
//            List<ModelHrStafPenyiasat> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrStafPenyiasat> _getList = dbConn.Query<ModelHrStafPenyiasat>(SiasatanSQL.SQL_GetPengesahanList(),
//                        new { SIASATAN_PK = siasatanPk, KATEGORI_PENGESAHAN_FK = _katpengesahan });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SenaraiDB DB_GetPengesahanList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static int DB_InsertMessage(ModelHrMessage _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _insert = InsertMessage(_dataIn);
//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_InsertMessage try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int InsertMessage(ModelHrMessage _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertMessage(), new
//                {
//                    SIASATAN_FK = _dataIn.SIASATAN_PK,
//                    PENGGUNA_FK = _dataIn.PENGGUNA_FK,
//                    CATATAN = _dataIn.CATATAN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        public static IEnumerable<ModelHrMessage> DB_GetMessageList(int siasatanPk)
//        {
//            List<ModelHrMessage> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrMessage> _getList = dbConn.Query<ModelHrMessage>(SiasatanSQL.SQL_GetMessageList(), new { SIASATAN_PK = siasatanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SenaraiDB DB_GetMessageList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static int DB_UpdateTamatSiasatan(ModelHrPengadu _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _update = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _update = MtdUpdateTamatSiasatan(_dataIn);

//                    if (_update > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_UpdateTamatSiasatan try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdUpdateTamatSiasatan(ModelHrPengadu _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_UpdateTamatSiasatan(), new
//                {
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK,
//                    SIASATAN_PK = _dataIn.SIASATAN_PK,
//                    STATUS_SSTN = _dataIn.STATUS_SSTN,
//                });
//            }
//        }

//        // Semakan Info Kod Peranan Pnyst
//        public static bool DB_GetKodPrnnPnyst(int _perincianPrbiPk, int _pencipta)
//        {
//            string _sql = SiasatanSQL.SQL_GetKodPrnnPnyst();
//            bool _result = false;

//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql,
//                    new
//                    {
//                        PERINCIAN_PRCKPN_PK = _perincianPrbiPk,
//                        KOD_PRNN_PNYST_FK = _pencipta
//                    });
//                if (_hasil != null && _hasil.Value == "1")
//                {
//                    _result = true;
//                }
//            }
//            return _result;
//        }

//        public static int DB_InsertCatatanSstn(ModelHrCatatan _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertCatatanSstn(), new
//                {
//                    SIASATAN_FK = _dataIn.SIASATAN_FK,
//                    KOD_PRNN_PNYST_FK = _dataIn.KOD_PRNN_PNYST_FK,
//                    TKH_CTTN_DATETIME = _dataIn.TKH_CTTN_DATETIME,
//                    MASA = _dataIn.MASA,
//                    BLOK_FK = _dataIn.BLOK_FK,
//                    CTTN_LOKASI = _dataIn.CTTN_LOKASI,
//                    BUTIRAN_SSTN = _dataIn.BUTIRAN_SSTN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        // Get Senarai Catatan Siasatan
//        public static IEnumerable<ModelHrCatatan> DB_GetCatatanSstnList(int siasatanPk)
//        {
//            List<ModelHrCatatan> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrCatatan> _getList = dbConn.Query<ModelHrCatatan>(SiasatanSQL.SQL_MtdGetCatatanList(), new { SIASATAN_PK = siasatanPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetCatatanSstnList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static int DB_InsertLaporanSstn(CarianLaporan _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _insert = 0, _insert2 = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    if (_dataIn.LPRN_SSTN_PK > 0)
//                    {
//                        _insert = MtdUpdateInvLprnSstn(_dataIn);
//                    }
//                    else
//                    {
//                        var query_result = dbConn.Query<int>(SiasatanSQL.SQLGetNextValSEQ("SEQ_HR_INV_LPRN_SSTN"));
//                        int _temp = query_result.FirstOrDefault();
//                        _dataIn.LPRN_SSTN_PK = (int)_temp;

//                        _insert = MtdInsertInvLprnSstn(_dataIn);
//                    }

//                    //Loop and insert records.
//                    if (_dataIn.ListLaporanSstn.Count() > 0)
//                    {
//                        foreach (ModelHrLaporan row in _dataIn.ListLaporanSstn)
//                        {
//                            if (row.PERKARA != null && row.TARIKH != null && row.RESULTSET == "")
//                            {
//                                row.PENCIPTA_FK = _dataIn.PENCIPTA_FK;
//                                row.LPRN_SSTN_PK = _dataIn.LPRN_SSTN_PK;
//                                row.TARIKH = SiasatanProcess.UbahTarikhInsert(row.TARIKH);
//                                _insert2 = DB_InsertRumusanSstn(row);
//                            }
//                        }
//                    }

//                    if (_insert > 0)
//                    {
//                        _data = 1;
//                        if (_insert2 > 0)
//                        {
//                            _data = 1;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB MtdRakamanPrckpnInsert try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdInsertInvLprnSstn(CarianLaporan _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertInvLprnSstn(), new
//                {
//                    LPRN_SSTN_PK = _dataIn.LPRN_SSTN_PK,
//                    SIASATAN_FK = _dataIn.SIASATAN_FK,
//                    TAJUK_KES = _dataIn.TAJUK_KES,
//                    BTIRN_LPRN = _dataIn.BTIRN_LPRN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int DB_InsertRumusanSstn(ModelHrLaporan _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_InsertInvRumusan(), new
//                {
//                    LPRN_SSTN_FK = _dataIn.LPRN_SSTN_PK,
//                    PERKARA = _dataIn.PERKARA,
//                    TARIKH = _dataIn.TARIKH,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }
//        public static int MtdUpdateInvLprnSstn(CarianLaporan _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_UpdateInvLprnSstn(), new
//                {
//                    LPRN_SSTN_PK = _dataIn.LPRN_SSTN_PK,
//                    TAJUK_KES = _dataIn.TAJUK_KES,
//                    BTIRN_LPRN = _dataIn.BTIRN_LPRN,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        internal static ModelHrLaporan DB_GetLaporanSstn(int _siasatanFk)
//        {
//            ModelHrLaporan _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrLaporan>(SiasatanSQL.SQL_GetLaporanSstn(), new { SIASATAN_FK = _siasatanFk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetLaporanSstn e ~ " + e);
//            }

//            return _data;
//        }

//        public static IEnumerable<ModelHrLaporan> DB_GetRumusanList(int lprnSstnPk)
//        {
//            List<ModelHrLaporan> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ModelHrLaporan> _getList = dbConn.Query<ModelHrLaporan>(SiasatanSQL.SQL_GetRumusanList(),
//                        new { LPRN_SSTN_FK = lprnSstnPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetRumusanList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        internal static int DB_HapusRumusanSstn(ModelHrLaporan _dataIn)
//        {
//            int _data = 0;
//            try
//            {
//                int _hapus = 0;
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _hapus = MtdHapusRumusanSstn(_dataIn);

//                    if (_hapus > 0)
//                    {
//                        _data = 1;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_HapusRumusanSstn try catch ex.Message ~ " + ex.Message);
//            }
//            return _data;
//        }

//        public static int MtdHapusRumusanSstn(ModelHrLaporan _dataIn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Execute(SiasatanSQL.SQL_HapusRumusanSstn(), new
//                {
//                    RUMUSAN_PK = _dataIn.RUMUSAN_PK,
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK
//                });
//            }
//        }

//        // Begin:
//        // DDL
//        public static IEnumerable<ParameterHrModel> DB_ListKatAduan()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListKatAduan());
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListKatPengadu()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListKatPengadu());
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListStsAduan()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListStsAduan());
//            }
//        }

//        public static IEnumerable<ParameterHrModel> ListJawatanAll(string _kodjwtn)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SqlJawatanList(_kodjwtn));
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListStsAktif()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListStsAktif());
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListKatPrckpn()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListKatPrckpn());
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_GetPerananByKatPrckpnList(int _siasatanPk, int _katPrckpn)
//        {
//            List<ParameterHrModel> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ParameterHrModel> _getList = dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetPerananByKatPrckpnList(), new { SIASATAN_FK = _siasatanPk, KTGRI_PRCKPN_FK = _katPrckpn });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetPerananByKatPrckpnList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        // Staf
//        public static IEnumerable<ParameterHrModel> DB_GetSaksiList(string? _katsaksi, string? _select, string? _name)
//        {
//            List<ParameterHrModel> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ParameterHrModel> _getList = dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetSaksiList(_katsaksi, _select, _name));
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetSaksiList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        // Student
//        public static IEnumerable<ParameterHrModel> DB_GetSaksiStudentList(string? _katsaksi, string? _select, string? _name)
//        {
//            List<ParameterHrModel> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraAkademik))
//                {
//                    IEnumerable<ParameterHrModel> _getList = dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetStudentVWSenarai(_katsaksi, _select, _name));
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetSaksiStudentList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListKategoriKod(string? _katKod)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_MtdGetKodKategori(), new { PARAM_PK = _katKod });
//            }
//        }

//        public static IEnumerable<ParameterHrModel> DB_GetKodPerananList(int _siasatanPk, string? _kodperanan)
//        {
//            List<ParameterHrModel> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ParameterHrModel> _getList = dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetKodPerananList(_kodperanan), new { SIASATAN_FK = _siasatanPk, KATEGORI_BRG_FK = _kodperanan });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetKodPerananList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static IEnumerable<ParameterHrModel> DB_GetKodPerananPerincianList(string? _peribadiPrckpnPk)
//        {
//            List<ParameterHrModel> list = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    IEnumerable<ParameterHrModel> _getList = dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetKodPerananPerincianList(), new { PERIBADI_PRCKPN_FK = _peribadiPrckpnPk });
//                    if (_getList != null)
//                    {
//                        list = _getList.ToList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("SiasatanDB DB_GetKodPerananPerincianList try catch ex.Message ~ " + ex.Message);
//            }

//            return list;
//        }

//        public static IEnumerable<ParameterHrModel> DB_ListKampusFk()
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_ListKampusFk());
//            }
//        }
//        public static IEnumerable<ParameterHrModel> DB_GetKodZonList(string? _kampus)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetKodZonList(), new { KOD_KAMPUS = _kampus });
//            }
//        }
//        public static IEnumerable<ParameterHrModel> DB_GetKodBlokList(string? _kampus, string? _zon)
//        {
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                return dbConn.Query<ParameterHrModel>(SiasatanSQL.SQL_GetKodBlokList(), new { KOD_KAMPUS = _kampus, KOD_ZON = _zon });
//            }
//        }

//        public static int DB_MtdSahWujud(int _siasatanPk, string? _status, string _sts_rngksn)
//        {
//            string _sql = SiasatanSQL.SQL_MtdSahWujud();
//            int _result = 0;
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { SIASATAN_FK = _siasatanPk, STATUS_FK = _status, KATEGORI_PENGESAHAN_FK = _sts_rngksn });
//                if (_hasil != null && _hasil.Value == "1" && _hasil.Key != null)
//                {
//                    _result = int.Parse(_hasil.Key);
//                }
//            }
//            return _result;
//        }

//        public static int DB_MtdGetApprovalRolePk(int _stafPk)
//        {
//            string _sql = SiasatanSQL.SQL_GetApprovalRolePk();
//            int _result = 0;
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { STAF_FK = _stafPk });
//                if (_hasil != null && _hasil.Value == "1" && _hasil.Key != null)
//                {
//                    _result = int.Parse(_hasil.Key);
//                }
//            }
//            return _result;
//        }

//        public static int DB_GetRngksnStafEmel(int _siasatanPk, string? _status, string _sts_rngksn)
//        {
//            string _sql = SiasatanSQL.SQL_GetRngksnStafEmel();
//            int _result = 0;
//            using (var dbConn = new OracleConnection(ConnOraHr))
//            {
//                ModelParameterHr _hasil = dbConn.QueryFirstOrDefault<ModelParameterHr>(_sql, new { SIASATAN_FK = _siasatanPk, STATUS_FK = _status, KATEGORI_PENGESAHAN_FK = _sts_rngksn });
//                if (_hasil != null && _hasil.Value == "1" && _hasil.Key != null)
//                {
//                    _result = int.Parse(_hasil.Key);
//                }
//            }
//            return _result;
//        }

//        // End: 
//        // DDL

//        // Chat
//        internal static ModelHrMessage DB_GetMaklumatPenyiasatChat(int _siasatanPk, string? stafPk, string userAll)
//        {
//            ModelHrMessage _data = new();
//            try
//            {
//                using (var dbConn = new OracleConnection(ConnOraHr))
//                {
//                    _data = dbConn.QueryFirstOrDefault<ModelHrMessage>(SiasatanSQL.SQL_GetMaklumatPenyiasatChat(userAll), new { SIASATAN_PK = _siasatanPk, STAF_PK = stafPk });
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("DB_GetMaklumatPenyiasatChat e ~ " + e);
//            }

//            return _data;
//        }
//    }
//}
