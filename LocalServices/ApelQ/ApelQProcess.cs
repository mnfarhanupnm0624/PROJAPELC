//using Microsoft.VisualBasic;
//using Net6HrPublicLibrary.Model;
////using Net6HrPublicLibrary.PublicShared;
//using APEL.LocalServices.Login;
//using APEL.LocalShared;
//using APEL.Models;
//using System.Security.Policy;
////using System.Web.Mvc;
//using System.Xml.Linq;

//namespace APEL.LocalServices.Siasatan
//{
//    public class SiasatanProcess
//    {
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();
//        readonly static string _attachmentPath = SecurityConstants.AttachmentPath();

//        // Begin: Get Siasatan Session
//        //internal static CarianSiasatanMain MtdGetMaklumatAduanSession(string _aduanPkEnc)
//        //{
//        //    CarianSiasatanMain _data = new();
//        //    int _aduanPk = _aduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_aduanPkEnc, _encryptCode)) : 0;
//        //    ModelHrPengadu session = SiasatanDB.DB_MtdGetApelSession(_aduanPk);
//        //    if (session != null)
//        //    {
//        //        session = MtdHrSessionPindahFromLibrary(session);
//        //        _data.Pengadu = session;
//        //    }
//        //    return _data;
//        //}

//        private static ModelHrPengadu MtdHrSessionPindahFromLibrary(ModelHrPengadu _data)
//        {
//            string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_PK.ToString(), _encryptCode);
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_PK.ToString(), _encryptCode);

//            return new ModelHrPengadu()
//            {
//                ADUAN_PK_ENC = _aduanPkEnc,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                ADUAN_PK = _data.ADUAN_PK,
//                SIASATAN_PK = _data.SIASATAN_PK,
//                REPORT_NO = _data.REPORT_NO,
//                STATUS_SSTN = _data.STATUS_SSTN,
//                STS_RNGKSAN = _data.STS_RNGKSAN,
//                STS_LAPORAN = _data.STS_LAPORAN,
//            };
//        }
//        // End: Get Siasatan Session

//        // Method Get Info Staf UTM
//        internal static ModelHrStaffPeribadi Process_GetMaklumatAsasStaf(int _stafPk)
//        {
//            ModelHrStaffPeribadi _info = new();
//            _info = SiasatanDB.DB_MtdGetMaklumatAsas(_stafPk);
//            if (_info != null)
//            {
//                if (_info.NO_TEL_BIMBIT != null)
//                { _info.NO_HP = _info.NO_TEL_BIMBIT; }
//                else { _info.NO_HP = _info.NO_TEL_PEJABAT; }
//            }

//            return _info;
//        }

//        internal static CarianSiasatanMain MtdGetMaklumatAduanPengadu(string _aduanPkEnc)
//        {
//            CarianSiasatanMain _data = new();
//            int _aduanPk = _aduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_aduanPkEnc, _encryptCode)) : 0;
//            ModelHrPengadu pengadu = SiasatanDB.DB_MtdGetApelPengadu(_aduanPk);
//            if (pengadu != null)
//            {
//                if (pengadu.COMPLAINER_FK != null) // Staff
//                {
//                    int _stafPk = int.Parse(pengadu.COMPLAINER_FK);
//                    ModelHrStaffPeribadi _staf = Process_GetMaklumatAsasStaf(_stafPk);
//                    if (_staf != null)
//                    {
//                        _data.stafPeribadi = _staf;
//                    }
//                }
//                if (pengadu.COMPLAINER_NO_KP != null) // Pelajar
//                {
//                    ModelPelajarPeribadi _pelajar = SiasatanDB.MtdGetDataPelajarByNokp(pengadu.COMPLAINER_NO_KP);
//                    if (_pelajar != null)
//                    {
//                        _data.pelajarInfo = _pelajar;
//                    }
//                }
//                if (pengadu.MAKLUMAT_PERIBADI_FK != null)
//                {
//                    ModelHrVisitorPeribadi _visitor = SiasatanDB.DB_GetDataVisitorByPk(pengadu.MAKLUMAT_PERIBADI_FK);
//                    if (_visitor != null)
//                    {
//                        _data.visitorinfo = _visitor;
//                    }
//                }

//                pengadu.LABEL_STATUS = LabelStatusDesc(pengadu.STATUS_FK);
//                string[] n = pengadu.LABEL_STATUS.Split('~');
//                pengadu.STATUS_DESC = n[1];

//                pengadu = MtdHrPengaduPindahFromLibrary(pengadu);
//                _data.Pengadu = pengadu;
//            }
//            return _data;
//        }

//        internal static CarianSiasatanMain MtdGetMaklumatAduanPenerima(string _aduanPkEnc)
//        {
//            CarianSiasatanMain _data = new();
//            int _aduanPk = _aduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_aduanPkEnc, _encryptCode)) : 0;
//            ModelHrPengadu aduaninfo = SiasatanDB.DB_MtdGetApelPenerima(_aduanPk);
//            if (aduaninfo != null)
//            {
//                // Penerima Aduan
//                if (aduaninfo.ACCEPTER_FK != null) // Staff
//                {
//                    int _stafPk = int.Parse(aduaninfo.ACCEPTER_FK);
//                    ModelHrStaffPeribadi _staf = Process_GetMaklumatAsasStaf(_stafPk);
//                    if (_staf != null)
//                    {
//                        _data.stafPeribadi2 = _staf;
//                    }
//                }

//                // Get Info untuk Print PDF
//                // Pengadu
//                if (aduaninfo.COMPLAINER_FK != null) // Staff
//                {
//                    int _stafPk = int.Parse(aduaninfo.COMPLAINER_FK);
//                    ModelHrStaffPeribadi _staf = Process_GetMaklumatAsasStaf(_stafPk);
//                    if (_staf != null)
//                    {
//                        _data.stafPeribadi = _staf;
//                    }
//                }
//                if (aduaninfo.COMPLAINER_NO_KP != null) // Pelajar
//                {
//                    ModelPelajarPeribadi pelajar = SiasatanDB.MtdGetDataPelajarByNokp(aduaninfo.COMPLAINER_NO_KP);
//                    if (pelajar != null)
//                    {
//                        _data.pelajarInfo = pelajar;
//                    }
//                }
//                if (aduaninfo.MAKLUMAT_PERIBADI_FK != null) // Pelawat
//                {
//                    ModelHrVisitorPeribadi _visitor = SiasatanDB.DB_GetDataVisitorByPk(aduaninfo.MAKLUMAT_PERIBADI_FK);
//                    if (_visitor != null)
//                    {
//                        _data.visitorinfo = _visitor;
//                    }
//                }

//                aduaninfo = MtdHrPengaduPindahFromLibrary(aduaninfo);
//                _data.Pengadu = aduaninfo;
//            }
//            return _data;
//        }

//        private static ModelHrPengadu MtdHrPengaduPindahFromLibrary(ModelHrPengadu _data)
//        {
//            string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_PK.ToString(), _encryptCode);
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_PK.ToString(), _encryptCode);
//            string _complainpkEnc = _data.COMPLAINER_FK != null ? EncryptHr.NewEncrypt(_data.COMPLAINER_FK.ToString(), _encryptCode) : "";
//            string _complainStudentEnc = _data.COMPLAINER_NO_KP != null ? EncryptHr.NewEncrypt(_data.COMPLAINER_NO_KP.ToString(), _encryptCode) : "";

//            return new ModelHrPengadu()
//            {
//                ADUAN_PK_ENC = _aduanPkEnc,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                COMPLAINER_FK_ENC = _complainpkEnc,
//                COMPLAINER_NO_KP_ENC = _complainStudentEnc,
//                ADUAN_PK = _data.ADUAN_PK,
//                SIASATAN_PK = _data.SIASATAN_PK,
//                REPORT_NO = _data.REPORT_NO,
//                TKH_ADUAN = _data.TKH_ADUAN,
//                DATE_TKH_ADUAN = _data.DATE_TKH_ADUAN,
//                MASA_TKH_ADUAN = _data.MASA_TKH_ADUAN,
//                NAMA_PENGADU = _data.NAMA_PENGADU,
//                NO_ID = _data.NO_ID,
//                NO_KP = _data.NO_KP,
//                STATUS_DESC = _data.STATUS_DESC,
//                LABEL_STATUS = _data.LABEL_STATUS,
//                KATEGORI_KES_DESC = _data.KATEGORI_KES_DESC,
//                CATATAN_ADUAN = _data.CATATAN_ADUAN,
//                BAHASA = _data.BAHASA,
//                KAMPUS_DESC = _data.KAMPUS_DESC,
//                ZON = _data.ZON,
//                LOCATION_RECORD = _data.LOCATION_RECORD,
//                UNIT = _data.UNIT,
//                CATEGORY_PENGADU = _data.CATEGORY_PENGADU,
//                SUB_CATEGORY_PENGADU = _data.SUB_CATEGORY_PENGADU,
//                TAJUK_RNGKSN = _data.TAJUK_RNGKSN,
//                AKBT_KJDN = _data.AKBT_KJDN,
//                RMSN_AWAL = _data.RMSN_AWAL,

//            };
//        }

//        // Get Senarai Staf Penyiasat
//        internal static List<ModelHrStafPenyiasat> MtdGetStafPenyiasatList(string _siasatanPkEnc)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrStafPenyiasat> _dataList = SiasatanDB.DB_MtdGetStafPenyiasatList(_siasatanPk);
//            List<ModelHrStafPenyiasat> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.CNAMA != null)
//                    {
//                        string[] _staf = row.CNAMA.Split('~');
//                        row.NAMA = _staf[0];
//                        row.NO_PEKERJA = _staf[1];
//                        row.NO_KP_BARU = _staf[2];
//                        row.JAWATAN_DESC = _staf[3];
//                        row.KAMPUS_DESC = _staf[4];
//                        row.STATUS_AKTIF = _staf[5];
//                    }
//                    row.LABEL_STATUS = LabelStatusAktifDesc(row.STATUS_AKTIF);
//                    string[] n = row.LABEL_STATUS.Split('~');
//                    row.LABEL_STATUS = n[0];
//                    row.STATUSAKTIF_DESC = n[1];

//                    _list.Add(MtdHrStaffPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        // Carian Staf Penyiasat - Daftar
//        internal static List<ModelHrStafPenyiasat> MtdGetStafPnystList(string _cSiasatanPkEnc, string? _cNoPekerja, string? _cNama, string? _cKampus, string? _cJawatan, string? _cStsAktif, string userAll)
//        {
//            int _siasatanPk = _cSiasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_cSiasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrStafPenyiasat> _dataList = SiasatanDB.DB_MtdGetStafPnystList(_siasatanPk, _cNoPekerja, _cNama, _cKampus, _cJawatan, _cStsAktif, userAll);
//            List<ModelHrStafPenyiasat> _list = new();
//            int no = 0;
//            foreach (var row in _dataList)
//            {
//                no++;
//                row.LABEL_STATUS = LabelStatusAktifDesc(row.STATUS_AKTIF);
//                string[] n = row.LABEL_STATUS.Split('~');
//                row.LABEL_STATUS = n[0];
//                row.STATUSAKTIF_DESC = n[1];

//                _list.Add(MtdHrStaffPindahFromLibrary(row));
//            }
//            return _list;
//        }

//        private static ModelHrStafPenyiasat MtdHrStaffPindahFromLibrary(ModelHrStafPenyiasat _data)
//        {
//            string _stafPkEnc = EncryptHr.NewEncrypt(_data.STAF_PP_FK.ToString(), _encryptCode);
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_PK.ToString(), _encryptCode);

//            return new ModelHrStafPenyiasat()
//            {
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                STAF_PP_FK_ENC = _stafPkEnc,
//                ADUAN_PK = _data.ADUAN_PK,
//                SIASATAN_PK = _data.SIASATAN_PK,
//                STAF_PP_FK = _data.STAF_PP_FK,
//                NAMA = _data.NAMA,
//                NO_PEKERJA = _data.NO_PEKERJA,
//                NO_KP_BARU = _data.NO_KP_BARU,
//                JAWATAN_DESC = _data.JAWATAN_DESC,
//                KAMPUS_DESC = _data.KAMPUS_DESC,
//                STATUS_AKTIF = _data.STATUS_AKTIF,
//                KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
//                LABEL_STATUS = _data.LABEL_STATUS,
//                STATUSAKTIF_DESC = _data.STATUSAKTIF_DESC,
//                DATE_TKH_PNYST = _data.DATE_TKH_PNYST,
//                MASA_TKH_PNYST = _data.MASA_TKH_PNYST,
//                REPORT_NO = _data.REPORT_NO,
//                BTRN_ULSN = _data.BTRN_ULSN,
//                DATE_CIPTA = _data.DATE_CIPTA,
//                MASA_CIPTA = _data.MASA_CIPTA,
//                DATE_KEMASKINI = _data.DATE_KEMASKINI,
//                MASA_KEMASKINI = _data.MASA_KEMASKINI,
//            };
//        }

//        // -- Insert and Get Value Kod Kategori - C
//        internal static ModelHrStafPenyiasat MtdInsertStafPenyiasat(ModelHrStafPenyiasat _dataIn)
//        {
//            try
//            {
//                ModelParameterHr _kod = SiasatanDB.DB_MtdGetKodKategori(_dataIn.KATEGORI_KOD_FK);
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.STAF_PP_FK = _dataIn.STAF_PP_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.STAF_PP_FK_ENC, _encryptCode)) : 0;
//                int _insert = 0;

//                ModelParameterHr _semak = SiasatanDB.DB_MtdGetKodKategoriSemakSediada(_kod.ViewField, _dataIn.SIASATAN_PK);
//                if (_semak != null)
//                {
//                    int _no = _semak.Nombor + 1;
//                    _dataIn.KOD_PRNN_PNYST = _kod.ViewField + _no.ToString();
//                    _insert = SiasatanDB.MtdGetHrPenyiasatInsert(_dataIn);
//                }
//                else
//                {
//                    int _no = 1;
//                    _dataIn.KOD_PRNN_PNYST = _kod.ViewField + _no.ToString();
//                    _insert = SiasatanDB.MtdGetHrPenyiasatInsert(_dataIn);
//                }
//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//                else
//                {
//                    _dataIn.RESULTSET = "-1";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertStafPenyiasat. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // -- Insert and Get Value Kod Peranan - A1(Pengadu) dan A2-AN(Saksi)
//        internal static ModelHrRakaman MtdInsertPrckpnDesc(ModelHrRakaman _dataIn)
//        {
//            try
//            {
//                int _insert = 0;
//                _dataIn.RESULTSET = "-1";
//                _dataIn.SIASATAN_FK = _dataIn.CSIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CSIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.KOD_PRNN_PNYST_FK = _dataIn.CKOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CKOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                _dataIn.DAFTAR_PNYST_PK = SiasatanDB.DB_MtdGetDaftarPnystPk(_dataIn.SIASATAN_FK, _dataIn.KOD_PRNN_PNYST_FK);

//                if (_dataIn.CKTGRI_PRCKPN_FK == "1010") // Pengadu
//                {
//                    string _kod = SiasatanDB.DB_GetKodPerananPrckpn(_dataIn.CKATEGORI_BRG_FK.ToString());
//                    ModelParameterHr _wujud = SiasatanDB.DB_MtdGetKodPerananSemakSediada(_kod + "1", _dataIn.SIASATAN_FK);
//                    if (_wujud != null) { _dataIn.RESULTSET = "1"; }
//                    else
//                    {
//                        _insert = SiasatanDB.MtdRakamanPrckpnInsert(_dataIn);
//                    }
//                }
//                else // Saksi
//                {
//                    _insert = SiasatanDB.MtdRakamanPrckpnInsert(_dataIn);
//                }

//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertPrckpnDesc. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // Get Senarai Rakaman Percakapan
//        internal static List<ModelHrRakaman> MtdGetPeribadiPrckpnList(string? _siasatanPkEnc, string? _kodBrgFk)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrRakaman> _dataList = SiasatanDB.DB_MtdGetPeribadiPrckpnList(_siasatanPk, _kodBrgFk);
//            List<ModelHrRakaman> _list = new();

//            _list = MtdHrRakamanGetInfo(_dataList);

//            return _list;
//        }

//        // Get Senarai Rakaman Percakapan Pengadu/Saksi
//        internal static List<ModelHrRakaman> MtdGetPrckpnList(string? _siasatanPkEnc, string? _kodPeranan, string? _kodBrgFk)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrRakaman> _dataList = SiasatanDB.DB_MtdGetPrckpnList(_siasatanPk, _kodPeranan, _kodBrgFk);
//            List<ModelHrRakaman> _list = new();

//            _list = MtdHrRakamanGetInfo(_dataList);

//            return _list;
//        }

//        private static List<ModelHrRakaman> MtdHrRakamanGetInfo(IEnumerable<ModelHrRakaman> _dataList)
//        {
//            List<ModelHrRakaman> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.KTGRI_PRCKPN_FK == 1010) // Get Info Pengadu - KTGRI_PRCKPN_FK = 1010
//                    {
//                        if (row.COMPLAINER_FK != null) // Staf
//                        {
//                            if (row.INFO_PENGADU_STAF != null)
//                            {
//                                string[] _staf = row.INFO_PENGADU_STAF.Split('~');
//                                row.NAMA_PENGADU = _staf[0];
//                                row.NO_ID = _staf[1];
//                                row.NO_KP = _staf[2];
//                                row.KATEGORI_SAKSI = "01";
//                            }
//                        }
//                        if (row.COMPLAINER_NO_KP != null) // Pelajar
//                        {
//                            string? _pelajarinfo = SiasatanDB.DB_GetInfoPelajarDetails(row.COMPLAINER_NO_KP); // Pelajar
//                            if (_pelajarinfo != null)
//                            {
//                                string[] _student = _pelajarinfo.Split('~');
//                                row.NAMA_PENGADU = _student[0];
//                                row.NO_ID = _student[1];
//                                row.NO_KP = _student[2];
//                                row.KATEGORI_SAKSI = "02";
//                            }
//                        }
//                        // Lain-Lain
//                    }
//                    else   // Get Info Saksi - KTGRI_PRCKPN_FK = 1011
//                    {
//                        if (row.INFO_SAKSI_STAF != null)  // Get info Staf di HR_INV_PRCKPN_STAF_LAIN2
//                        {
//                            string[] _saksistaf = row.INFO_SAKSI_STAF.Split('~');  // Staf
//                            row.NAMA_PENGADU = _saksistaf[0];
//                            row.NO_ID = _saksistaf[1];
//                            row.NO_KP = _saksistaf[2];
//                            row.KATEGORI_SAKSI = "421";
//                        }
//                        else if (row.NO_KP_PELAJAR != null) // Get info Pelajar di HR_INV_PRCKPN_PLJR
//                        {
//                            string? _pelajarinfo = SiasatanDB.DB_GetInfoPelajarDetails(row.NO_KP_PELAJAR); // Pelajar
//                            if (_pelajarinfo != null)
//                            {
//                                string[] _student = _pelajarinfo.Split('~');
//                                row.NAMA_PENGADU = _student[0];
//                                row.NO_ID = _student[1];
//                                row.NO_KP = _student[2];
//                                row.KATEGORI_SAKSI = "422";
//                            }
//                        }
//                    }
//                    _list.Add(MtdHrRakamanPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        private static ModelHrRakaman MtdHrRakamanPindahFromLibrary(ModelHrRakaman _data)
//        {
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_FK.ToString(), _encryptCode);
//            string _peribadiPkEnc = _data.PERIBADI_PRCKPN_PK > 0 ? EncryptHr.NewEncrypt(_data.PERIBADI_PRCKPN_PK.ToString(), _encryptCode) : "";
//            string _perincianPrbdiPkEnc = _data.PERINCIAN_PRCKPN_PK > 0 ? EncryptHr.NewEncrypt(_data.PERINCIAN_PRCKPN_PK.ToString(), _encryptCode) : "";

//            return new ModelHrRakaman()
//            {
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                PERIBADI_PRCKPN_PK_ENC = _peribadiPkEnc,
//                PERINCIAN_PRCKPN_PK_ENC = _perincianPrbdiPkEnc,
//                PERIBADI_PRCKPN_PK = _data.PERIBADI_PRCKPN_PK,
//                KOD_PRNN_PRCKPN = _data.KOD_PRNN_PRCKPN,
//                STATUS_PRCKPN = _data.STATUS_PRCKPN,
//                KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
//                DATE_TKHCIPTA_RKMN = _data.DATE_TKHCIPTA_RKMN,
//                MASA_TKHCIPTA_RKMN = _data.MASA_TKHCIPTA_RKMN,
//                NAMA_PENGADU = _data.NAMA_PENGADU,
//                NO_ID = _data.NO_ID,
//                NO_KP = _data.NO_KP,
//                KOD_BRG_PRCKPN = _data.KOD_BRG_PRCKPN,
//                DATE_TKHCIPTA_RKMN_PRNCN = _data.DATE_TKHCIPTA_RKMN_PRNCN,
//                MASA_TKHCIPTA_RKMN_PRNCN = _data.MASA_TKHCIPTA_RKMN_PRNCN,
//                DATE_TKHKEMASKINI_RKMN_PRNCN = _data.DATE_TKHKEMASKINI_RKMN_PRNCN,
//                MASA_TKHKEMASKINI_RKMN_PRNCN = _data.MASA_TKHKEMASKINI_RKMN_PRNCN,
//                KATEGORI_SAKSI = _data.KATEGORI_SAKSI,
//                KATEGORI_BRG_FK = _data.KATEGORI_BRG_FK,
//            };
//        }

//        internal static ModelHrRakaman MtdGetInfoPeribadiPrckpn(string? _pribadiPrckpnPkEnc)
//        {
//            ModelHrRakaman info = new();
//            try
//            {
//                int peribadiPrckpnPk = _pribadiPrckpnPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_pribadiPrckpnPkEnc, _encryptCode)) : 0;
//                info = SiasatanDB.DB_GetInfoPeribadiPrckpn(peribadiPrckpnPk);
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdGetInfoPeribadiPrckpn. More info: " + e);
//            }
//            return info;
//        }

//        internal static ModelHrStaffPeribadi MtdGetInfoStafDetails(string? _noKp)
//        {
//            ModelHrStaffPeribadi peribadi = new();
//            try
//            {
//                peribadi = SiasatanDB.DB_GetInfoStafDetails(_noKp);
//                if (peribadi != null)
//                {
//                    // Get Info No Telefon
//                    if (peribadi.NO_TEL_BIMBIT != null)
//                    { peribadi.NO_HP = peribadi.NO_TEL_BIMBIT; }
//                    else if (peribadi.NO_TEL_PEJABAT != null)
//                    {
//                        peribadi.NO_HP = peribadi.NO_TEL_PEJABAT;
//                    }
//                    else { peribadi.NO_HP = "TIADA"; }

//                    // Get Info Email
//                    if (peribadi.EMAIL_RASMI != null)
//                    { }
//                    else if (peribadi.EMAIL_KEDUA != null)
//                    {
//                        peribadi.EMAIL_RASMI = peribadi.EMAIL_KEDUA;
//                    }
//                    else { peribadi.EMAIL_RASMI = "TIADA"; }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdGetInfoStafDetails. More info: " + e);
//            }
//            return peribadi;
//        }

//        internal static CarianRakaman MtdGetPerincianPrbdiDetails(string? _perincianPrbdiPkEnc)
//        {
//            CarianRakaman info = new();
//            ModelHrRakaman butiran = new();
//            try
//            {
//                int _perincianPrbdiPk = _perincianPrbdiPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_perincianPrbdiPkEnc, _encryptCode)) : 0;
//                butiran = SiasatanDB.DB_GetPerincianPrbdiDetails(_perincianPrbdiPk);
//                if (butiran != null)
//                {
//                    butiran.CHECKED = null;
//                    butiran.CHECKED2 = null;

//                    butiran.PRCKPN_TERJEMAHAN_PK_ENC = butiran.PRCKPN_TERJEMAHAN_PK > 0 ? EncryptHr.NewEncrypt(butiran.PRCKPN_TERJEMAHAN_PK.ToString(), _encryptCode) : "";
//                    if (butiran.TEKS_PENGESAHAN != null && butiran.TEKS_PENGESAHAN == "Y") { butiran.CHECKED = "checked"; }
//                    if (butiran.SUMPAH_AKUR_JANJI_FK != null && butiran.SUMPAH_AKUR_JANJI_FK == "Y") { butiran.CHECKED2 = "checked"; }

//                    if (butiran.KATEGORI_PENTERJEMAH_FK == "421" && butiran.PENTERJEMAH_FK != null) // Staff
//                    {
//                        int _stafPk = int.Parse(butiran.PENTERJEMAH_FK);
//                        ModelHrStaffPeribadi peribadi = SiasatanDB.DB_MtdGetMaklumatAsas(_stafPk);
//                        if (peribadi != null)
//                        {
//                            if (peribadi.NO_TEL_BIMBIT != null)
//                            { peribadi.NO_HP = peribadi.NO_TEL_BIMBIT; }
//                            else { peribadi.NO_HP = peribadi.NO_TEL_PEJABAT; }

//                            info.stafPeribadi = peribadi;
//                        }
//                    }
//                    if (butiran.KATEGORI_PENTERJEMAH_FK == "422" && butiran.NO_KP != null) // Pelajar
//                    {
//                        ModelPelajarPeribadi pelajar = SiasatanDB.MtdGetDataPelajarByNokp(butiran.NO_KP);
//                        if (pelajar != null)
//                        {
//                            info.pelajarInfo = pelajar;
//                        }
//                    }

//                    info.butiran = butiran;
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdGetPerincianPrbdiDetails. More info: " + e);
//            }
//            return info;
//        }

//        // Diari Penyiasat
//        internal static List<ModelHrStafPenyiasat> MtdGetDiariPenyiasatList(string _cSiasatanPkEnc)
//        {
//            int _siasatanPk = _cSiasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_cSiasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrStafPenyiasat> _dataList = SiasatanDB.DB_MtdGetDiariPenyiasatList(_siasatanPk);
//            List<ModelHrStafPenyiasat> _list = new();
//            int no = 0;
//            foreach (var row in _dataList)
//            {
//                no++;
//                row.LABEL_STATUS = LabelStatusAktifDesc(row.STATUS_AKTIF);
//                string[] n = row.LABEL_STATUS.Split('~');
//                row.LABEL_STATUS = n[0];
//                row.STATUSAKTIF_DESC = n[1];

//                _list.Add(MtdHrStaffPindahFromLibrary(row));
//            }
//            return _list;
//        }

//        internal static CarianLampiran MtdGetLampiranDoc(string _lampiranPkEnc)
//        {
//            int _lampiranPk = _lampiranPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_lampiranPkEnc, _encryptCode)) : 0;

//            ModelHrLampiran lampiran = SiasatanDB.DB_GetLampiranDoc(_lampiranPk);
//            CarianLampiran _dataReturn = new()
//            {
//                FAIL = lampiran.FAIL,
//                NAMA_FAIL = lampiran.NAMA_FAIL,
//            };
//            return _dataReturn;
//        }
//        internal static CarianLampiran MtdGetInvLampiranDoc(string _lampiranPkEnc)
//        {
//            int _lampiranPk = _lampiranPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_lampiranPkEnc, _encryptCode)) : 0;

//            ModelHrLampiran lampiran = SiasatanDB.DB_GetInvLampiranDoc(_lampiranPk);
//            CarianLampiran _dataReturn = new()
//            {
//                PATH = lampiran.PATH,
//                NAMA_FAIL = lampiran.NAMA_FAIL,
//            };
//            return _dataReturn;
//        }

//        private static ModelHrPolis MtdHrPolisPindahFromLibrary(ModelHrPolis _data)
//        {
//            string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_FK.ToString(), _encryptCode);
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_FK.ToString(), _encryptCode);

//            return new ModelHrPolis()
//            {
//                ADUAN_PK_ENC = _aduanPkEnc,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                FAIL = _data.FAIL,
//                NO_LPRN_POLIS = _data.NO_LPRN_POLIS,
//                DATE_TKH_LPRN = _data.DATE_TKH_LPRN,
//                MASA_TKH_LPRN = _data.MASA_TKH_LPRN,
//                HARI_TKH_LPRN = _data.HARI_TKH_LPRN,
//                KOD_RUJUKAN = _data.KOD_RUJUKAN,
//                RESULTSET = _data.RESULTSET
//            };
//        }

//        // Get Info Penterjemah
//        internal static CarianRakaman MtdGetPenterjemah(string? _katTerjemah, string? _noId)
//        {
//            CarianRakaman _data = new();
//            if (_noId != null)
//            {
//                if (_katTerjemah == "421") // Staff
//                {
//                    int _stafPk = int.Parse(_noId);
//                    ModelHrStaffPeribadi peribadi = SiasatanDB.DB_MtdGetMaklumatAsas(_stafPk);
//                    if (peribadi != null)
//                    {
//                        if (peribadi.NO_TEL_BIMBIT != null)
//                        { peribadi.NO_HP = peribadi.NO_TEL_BIMBIT; }
//                        else { peribadi.NO_HP = peribadi.NO_TEL_PEJABAT; }

//                        _data.stafPeribadi = peribadi;
//                    }
//                }
//                if (_katTerjemah == "422") // Pelajar
//                {
//                    ModelPelajarPeribadi pelajar = SiasatanDB.MtdGetDataPelajarByNokp(_noId);
//                    if (pelajar != null)
//                    {
//                        _data.pelajarInfo = pelajar;
//                    }
//                }
//            }
//            return _data;
//        }

//        internal static CarianSiasatan MtdGetInitialEvent(string _siasatanPkEnc)
//        {
//            CarianSiasatan _data = new();
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            ModelHrPengadu _event = SiasatanDB.DB_MtdGetInitialEvent(_siasatanPk);
//            if (_event != null)
//            {
//                _event = MtdHrPengaduPindahFromLibrary(_event);
//                _data._eventInfo = _event;
//            }
//            return _data;
//        }

//        // -- Kemaskini Info Keterangan Percakapan Perincian Peribadi
//        internal static ModelHrRakaman MtdUpdateKeteranganPS(ModelHrRakaman _dataIn)
//        {
//            try
//            {
//                _dataIn.SIASATAN_FK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.PERINCIAN_PRCKPN_PK = _dataIn.PERINCIAN_PRCKPN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.PERINCIAN_PRCKPN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.KOD_PRNN_PNYST_FK = _dataIn.CKOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CKOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                _dataIn.DAFTAR_PNYST_PK = SiasatanDB.DB_MtdGetDaftarPnystPk(_dataIn.SIASATAN_FK, _dataIn.KOD_PRNN_PNYST_FK);
//                _dataIn.PRCKPN_TERJEMAHAN_PK = _dataIn.PRCKPN_TERJEMAHAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.PRCKPN_TERJEMAHAN_PK_ENC, _encryptCode)) : 0; // Terjemah

//                int _update = 0;
//                string _checkDeklarasi = "N";
//                string _checkSign = "N";
//                if (_dataIn.CHECKED == "true" && _dataIn.CHECKED2 == "true")
//                {
//                    _checkDeklarasi = "Y";
//                    _checkSign = "Y";
//                    _dataIn.CHECKED = _checkDeklarasi;
//                    _dataIn.CHECKED2 = _checkSign;

//                    _update = SiasatanDB.DB_MtdUpdateKeteranganPS(_dataIn);
//                    if (_update > 0)
//                    {
//                        _dataIn.RESULTSET = "2";
//                    }
//                }
//                else
//                {
//                    _dataIn.RESULTSET = "-1";
//                }

//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdUpdateKeteranganPS. More info: " + e);
//            }

//            return _dataIn;
//        }

//        internal static ModelParameterHr MtdGetKodBorangPS(string? _peribadiPrckpnkEnc, string? _kodPeranan)
//        {
//            int _peribadiPrckpnPk = _peribadiPrckpnkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_peribadiPrckpnkEnc, _encryptCode)) : 0;
//            ParameterHrModel _dataLib = SiasatanDB.DB_MtdGetKodBorangPS(_peribadiPrckpnPk, _kodPeranan);
//            ModelParameterHr _data = new();
//            int _no = 1;

//            if (_dataLib != null)
//            {
//                _dataLib.Nombor = _dataLib.Nombor + _no;
//                _dataLib.ViewField = _kodPeranan + "." + _dataLib.Nombor;

//                _data = MtdPindahParameter(_dataLib);
//            }
//            else
//            {
//                _data.ViewField = _kodPeranan + "." + _no;
//            }

//            return _data;
//        }

//        // -- Insert Kod Borang Baru Pengadu/Saksi
//        internal static ModelHrRakaman MtdInsertPerincianKodBrg(ModelHrRakaman _dataIn)
//        {
//            try
//            {
//                int _insert = 0;
//                _dataIn.RESULTSET = "-1";
//                _dataIn.SIASATAN_FK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.PERIBADI_PRCKPN_PK = _dataIn.PERIBADI_PRCKPN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.PERIBADI_PRCKPN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.KOD_PRNN_PNYST_FK = _dataIn.CKOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CKOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                _dataIn.DAFTAR_PNYST_PK = SiasatanDB.DB_MtdGetDaftarPnystPk(_dataIn.SIASATAN_FK, _dataIn.KOD_PRNN_PNYST_FK);

//                _insert = SiasatanDB.InsertPerincianKodBrg(_dataIn);
//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ InsertPerincianKodBrg. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // -- Kemaskini Info Ringkasan Kejadian
//        internal static ModelHrPengadu MtdUpdateInitialEvent(ModelHrPengadu _dataIn)
//        {
//            try
//            {
//                ModelHrPengadu _data = new();
//                int _update, _update2 = 0;

//                _dataIn.RESULTSET = "-1";
//                _dataIn.STATUS_FK = "345";
//                _dataIn.KATEGORI_PENGESAHAN_FK = "5127";
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;

//                _update = SiasatanDB.DB_MtdUpdateInitialEvent(_dataIn);
//                _update2 = SiasatanDB.DB_MtdInsertPengesahan(_dataIn);

//                if (_update > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                    if (_update2 > 0)
//                    {
//                        _dataIn.RESULTSET = "2";
//                    }
//                    else
//                    {
//                        _dataIn.RESULTSET = "-1";
//                    }
//                }

//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdUpdateInitialEvent. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // -- Kemaskini Lulus Ringkasan Kejadian
//        internal static ModelHrPengadu MtdInsertInitialApprove(ModelHrPengadu _dataIn)
//        {
//            try
//            {
//                ModelHrPengadu _data = new();
//                int _insert = 0;
//                _dataIn.RESULTSET = "-1";
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;

//                //Get Pk Approval Pk
//                int _approvalPk = SiasatanDB.DB_MtdGetApprovalRolePk(_dataIn.PENCIPTA_FK); // KATEGORI_PENGESAHAN_FK
//                _dataIn.APPROVAL_ROLE_PK = _approvalPk;

//                _insert = SiasatanDB.DB_MtdInsertInitialApprove(_dataIn);

//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertInitialApprove. More info: " + e);
//            }
//            return _dataIn;
//        }

//        // -- Kemaskini Status Pembetulan Ringkasan Kejadian
//        internal static ModelHrPengadu MtdInsertInitialCorrection(ModelHrPengadu _dataIn)
//        {
//            try
//            {
//                ModelHrPengadu _data = new();
//                int _update = 0;
//                _dataIn.RESULTSET = "-1";
//                _dataIn.STATUS_FK = "5142";
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;

//                int _sts_rngksn = SiasatanDB.DB_MtdSahWujud(_dataIn.SIASATAN_PK, _dataIn.STATUS_FK, "5127"); // CORRECTION, KATEGORI_PENGESAHAN_FK
//                if (_sts_rngksn > 0)
//                {
//                    _dataIn.INV_PENGESAHAN_PK = _sts_rngksn;
//                    _update = SiasatanDB.DB_MtdUpdatePengesahan(_dataIn);
//                }
//                else
//                {
//                    //Get Pk Approval Pk
//                    int _approvalPk = SiasatanDB.DB_MtdGetApprovalRolePk(_dataIn.PENCIPTA_FK); // KATEGORI_PENGESAHAN_FK
//                    _dataIn.APPROVAL_ROLE_PK = _approvalPk;
//                    _update = SiasatanDB.DB_MtdInsertPengesahan(_dataIn);
//                }

//                if (_update > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }

//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdUpdateInitialEvent. More info: " + e);
//            }

//            return _dataIn;
//        }

//        internal static bool MtdInsertLampiran(ModelHrLampiran _lampiran)
//        {
//            return SiasatanDB.DB_MtdInsertLampiran(_lampiran);
//        }
//        internal static bool MtdUpdateLampiran(ModelHrLampiran _lampiran)
//        {
//            return SiasatanDB.DB_MtdUpdateLampiran(_lampiran);
//        }

//        // Get Senarai Lampiran 
//        internal static List<ModelHrLampiran> MtdGetLampiranList(string? _siasatanPkEnc, string _kategoriKod)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrLampiran> _dataList = SiasatanDB.DB_GetLampiranList(_siasatanPk, _kategoriKod);
//            List<ModelHrLampiran> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    row.FILE = null;
//                    var _fileNameNoSiriUpload = row.NAMA_FAIL;
//                    if (_fileNameNoSiriUpload != null)
//                    {
//                        if (row.PATH != null)
//                        {
//                            string _pathAttach = _attachmentPath + row.PATH;
//                            var path = Path.Combine((_pathAttach), _fileNameNoSiriUpload);
//                            FileInfo file = new FileInfo(path);
//                            if (file.Exists)
//                            {
//                                if (file.Extension == ".mp4")
//                                {
//                                    row.FILE_EXT = "bi bi-filetype-mp4";
//                                }
//                                if (file.Extension == ".mp3")
//                                {
//                                    row.FILE_EXT = "bi bi-filetype-mp3";
//                                }
//                                if (file.Extension == ".pdf")
//                                {
//                                    row.FILE_EXT = "bi bi-file-pdf red-color";
//                                }
//                                if (file.Extension == ".jpeg")
//                                {
//                                    row.FILE_EXT = "bi bi-file-earmark-image green-color";
//                                }
//                                if (file.Extension == ".png")
//                                {
//                                    row.FILE_EXT = "bi bi-file-earmark-image green-color";
//                                }
//                            }
//                        }
//                    }

//                    _list.Add(MtdHrLampiranPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        // Get Senarai Lampiran Dokumen Lain
//        internal static List<ModelHrLampiran> MtdGetLampiranDocList(string? _aduanPkEnc)
//        {
//            int _aduanPk = _aduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_aduanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrLampiran> _dataList = SiasatanDB.DB_GetLampiranDocList(_aduanPk);
//            List<ModelHrLampiran> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    _list.Add(MtdHrLampiranPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        private static ModelHrLampiran MtdHrLampiranPindahFromLibrary(ModelHrLampiran _data)
//        {
//            string _siasatanPkEnc = _data.SIASATAN_FK > 0 ? EncryptHr.NewEncrypt(_data.SIASATAN_FK.ToString(), _encryptCode) : "";
//            string _invlampiranPkEnc = _data.INV_LAMPIRAN_PK > 0 ? EncryptHr.NewEncrypt(_data.INV_LAMPIRAN_PK.ToString(), _encryptCode) : "";
//            string _lampiranPkEnc = _data.LAMPIRAN_PK > 0 ? EncryptHr.NewEncrypt(_data.LAMPIRAN_PK.ToString(), _encryptCode) : "";

//            return new ModelHrLampiran()
//            {
//                INV_LAMPIRAN_PK_ENC = _invlampiranPkEnc,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                LAMPIRAN_PK_ENC = _lampiranPkEnc,
//                KOD_LMPRN_RKMN = _data.KOD_LMPRN_RKMN,
//                TAJUK = _data.TAJUK,
//                NAMA_FAIL = _data.NAMA_FAIL,
//                KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
//                KATEGORI_KOD_FK = _data.KATEGORI_KOD_FK,
//                DATE_TKHCIPTA = _data.DATE_TKHCIPTA,
//                MASA_TKHCIPTA = _data.MASA_TKHCIPTA,
//                DATE_TKHKEMASKINI = _data.DATE_TKHKEMASKINI,
//                MASA_TKHKEMASKINI = _data.MASA_TKHKEMASKINI,
//                CTTN_LMPRN = _data.CTTN_LMPRN,
//                URL = _data.URL,
//                RAKAMAN_PRCKPN_FK = _data.RAKAMAN_PRCKPN_FK,
//                FAIL = _data.FAIL,
//                LAMPIRAN_PK = _data.LAMPIRAN_PK,
//                FILE = _data.FILE,
//                FILE_EXT = _data.FILE_EXT,
//                PATH = _data.PATH,
//            };
//        }

//        internal static ModelHrLampiran MtdGetLampiran(string _lampiranPkEnc)
//        {
//            ModelHrLampiran _data = new();
//            int _lampiranPk = _lampiranPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_lampiranPkEnc, _encryptCode)) : 0;
//            _data = SiasatanDB.DB_GetLampiran(_lampiranPk);
//            if (_data != null)
//            {
//                _data = MtdHrLampiranPindahFromLibrary(_data);
//            }
//            return _data;
//        }

//        internal static ModelParameterHr MtdGetPerananOkt(string? _siasatanPkEnc, string? _katBrg)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            ParameterHrModel _dataLib = SiasatanDB.DB_GetPerananOkt(_siasatanPk, _katBrg);
//            string _kod = SiasatanDB.DB_GetKodPerananPrckpn(_katBrg);
//            ModelParameterHr _data = new();
//            int _no = 1;
//            if (_dataLib != null)
//            {
//                _dataLib.Nombor = _dataLib.Nombor + _no;
//                _dataLib.ViewField = _kod + _dataLib.Nombor;

//                _data = MtdPindahParameter(_dataLib);
//            }
//            else
//            {
//                _data.ViewField = _kod + _no;
//            }

//            return _data;
//        }

//        // -- Insert and Get Value Kod Peranan - B1(OKT)
//        internal static ModelHrRakaman MtdInsertPrckpnOktDesc(ModelHrRakaman _dataIn)
//        {
//            try
//            {
//                int _insert = 0;
//                _dataIn.SIASATAN_FK = _dataIn.CSIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CSIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.KOD_PRNN_PNYST_FK = _dataIn.CKOD_PRNN_PNYST_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.CKOD_PRNN_PNYST_FK_ENC, _encryptCode)) : 0;
//                _dataIn.DAFTAR_PNYST_PK = SiasatanDB.DB_MtdGetDaftarPnystPk(_dataIn.SIASATAN_FK, _dataIn.KOD_PRNN_PNYST_FK);

//                _insert = SiasatanDB.MtdRakamanPrckpnInsert(_dataIn);
//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertPrckpnOktDesc. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // -- Kemaskini  Keterangan Percakapan Perincian Peribadi
//        internal static ModelHrRakaman MtdUpdateStatus(ModelHrRakaman _dataIn)
//        {
//            try
//            {
//                _dataIn.PERINCIAN_PRCKPN_PK = _dataIn.PERINCIAN_PRCKPN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.PERINCIAN_PRCKPN_PK_ENC, _encryptCode)) : 0;
//                int _update = 0;

//                _update = SiasatanDB.DB_UpdateStatus(_dataIn);
//                if (_update > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }

//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdUpdateKeteranganPS. More info: " + e);
//            }

//            return _dataIn;
//        }

//        internal static ModelParameterHr MtdGetPerananInvLampiran(string? _siasatanPkEnc, string? _katBrg)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            ParameterHrModel _dataLib = SiasatanDB.DB_GetPerananInvLampiran(_siasatanPk, _katBrg);
//            ModelParameterHr _kod = SiasatanDB.DB_MtdGetKodKategori(_katBrg);
//            ModelParameterHr _data = new();
//            int _no = 1;
//            if (_dataLib != null)
//            {
//                _dataLib.Nombor = _dataLib.Nombor + _no;
//                _dataLib.Key = _katBrg;
//                _dataLib.ViewField = _kod.ViewField + _dataLib.Nombor;

//                _data = MtdPindahParameter(_dataLib);
//            }
//            else
//            {
//                _data.Key = _katBrg;
//                _data.ViewField = _kod.ViewField + _no;
//            }

//            return _data;
//        }

//        internal static int MtdGetRngksnStafEmel(string _siasatanPkEnc)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            //Get Pencipta Pk
//            int _penciptaPk = SiasatanDB.DB_GetRngksnStafEmel(_siasatanPk, "345", "5127"); // PENCIPTA Pengesahan Ringkasan Kejadian
//            int _data = _penciptaPk;
//            return _data;
//        }

//        internal static ModelHrStaffPeribadi MtdGetMaklumatStaf(int _stafPk)
//        {
//            ModelHrStaffPeribadi _staf = new();
//            _staf = Process_GetMaklumatAsasStaf(_stafPk);
//            return _staf;
//        }

//        // Get Senarai Staf Penyiasat
//        internal static List<ModelHrStafPenyiasat> MtdGetPengesahanList(string _siasatanPkEnc, string _katpengesahan)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrStafPenyiasat> _dataList = SiasatanDB.DB_GetPengesahanList(_siasatanPk, _katpengesahan);
//            List<ModelHrStafPenyiasat> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.CNAMA != null)
//                    {
//                        string[] _staf = row.CNAMA.Split('~');
//                        row.NAMA = _staf[0];
//                        row.NO_PEKERJA = _staf[1];
//                        row.NO_KP_BARU = _staf[2];
//                        row.JAWATAN_DESC = _staf[3];
//                        row.KAMPUS_DESC = _staf[4];
//                        row.STATUS_AKTIF = _staf[5];
//                    }
//                    row.LABEL_STATUS = LabelStatusDesc(row.STATUS_FK);
//                    string[] n = row.LABEL_STATUS.Split('~');
//                    row.LABEL_STATUS = n[0];
//                    row.STATUSAKTIF_DESC = n[1];

//                    _list.Add(MtdHrStaffPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        // -- Insert Mesej rujuk kepada No Aduan
//        internal static ModelHrMessage MtdInsertMessage(ModelHrMessage _dataIn)
//        {
//            try
//            {
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.PENGGUNA_FK = _dataIn.PENGGUNA_FK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.PENGGUNA_FK_ENC, _encryptCode)) : 0;

//                _dataIn.RESULTSET = "-1";
//                int _insert = 0;

//                _insert = SiasatanDB.DB_InsertMessage(_dataIn);
//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertMessage. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // Get Senarai Staf Penyiasat
//        internal static List<ModelHrMessage> MtdGetMessageList(string _siasatanPkEnc)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrMessage> _dataList = SiasatanDB.DB_GetMessageList(_siasatanPk);
//            List<ModelHrMessage> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.KOD_PRNN_PNYST == null)
//                    {
//                        row.KOD_PRNN_PNYST = "Peg.Kanan";
//                    }
//                    _list.Add(MtdHrMessagePindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        // -- Kemaskini Status Tamat Siasatan
//        internal static ModelHrPengadu MtdUpdateTamatSiasatan(ModelHrPengadu _dataIn)
//        {
//            try
//            {
//                ModelHrPengadu _data = new();
//                int _update = 0;
//                _dataIn.RESULTSET = "-1";
//                _dataIn.SIASATAN_PK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.STATUS_SSTN = "N";

//                ModelHrPengadu _event = SiasatanDB.DB_MtdGetInitialEvent(_dataIn.SIASATAN_PK);
//                if (_event.TAJUK_RNGKSN != null && _event.AKBT_KJDN != null && _event.RMSN_AWAL != null)
//                {
//                    _update = SiasatanDB.DB_UpdateTamatSiasatan(_dataIn);
//                    if (_update > 0)
//                    {
//                        _dataIn.RESULTSET = "2";
//                    }
//                }
//                else { _dataIn.RESULTSET = "0"; }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdUpdateTamatSiasatan. More info: " + e);
//            }
//            return _dataIn;
//        }

//        internal static bool GetKodPrnnPnyst(string? _siasatanPkEnc, string? _perincianPrbdiPkEnc, string _pencipta)
//        {
//            int _perincianPrbiPk = _perincianPrbdiPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_perincianPrbdiPkEnc, _encryptCode)) : 0;
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            int _pnyst = SiasatanDB.DB_MtdGetDaftarPnystPk(_siasatanPk, int.Parse(_pencipta));

//            bool _semak = SiasatanDB.DB_GetKodPrnnPnyst(_perincianPrbiPk, _pnyst);
//            return _semak;
//        }

//        // -- Insert Catatan Siasatan
//        internal static ModelHrCatatan MtdInsertCatatanSstn(ModelHrCatatan _dataIn)
//        {
//            try
//            {
//                _dataIn.SIASATAN_FK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.KOD_PRNN_PNYST_FK = SiasatanDB.DB_MtdGetDaftarPnystPk(_dataIn.SIASATAN_FK, _dataIn.PENCIPTA_FK);
//                _dataIn.RESULTSET = "-1";
//                int _insert = 0;
//                if (_dataIn.TKH_CTTN != null && _dataIn.MASA != null)
//                {
//                    _dataIn.TKH_CTTN = UbahTarikhInsert(_dataIn.TKH_CTTN);
//                    DateTime d = DateTime.Parse(_dataIn.MASA);
//                    string _24hour = d.ToString("HH:mm");
//                    _dataIn.TKH_CTTN = _dataIn.TKH_CTTN + ' ' + _24hour;
//                    DateTime oDate = Convert.ToDateTime(_dataIn.TKH_CTTN);

//                    _dataIn.TKH_CTTN_DATETIME = oDate;
//                    _dataIn.MASA = _24hour;

//                    _insert = SiasatanDB.DB_InsertCatatanSstn(_dataIn);

//                    if (_insert > 0)
//                    {
//                        _dataIn.RESULTSET = "2";
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertCatatanSstn. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // Get List Catatan Siasatan
//        internal static List<ModelHrCatatan> MtdGetCatatanSstnList(string _cSiasatanPkEnc)
//        {
//            int _siasatanPk = _cSiasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_cSiasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrCatatan> _dataList = SiasatanDB.DB_GetCatatanSstnList(_siasatanPk);
//            List<ModelHrCatatan> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    _list.Add(MtdHrCatatanPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        private static ModelHrCatatan MtdHrCatatanPindahFromLibrary(ModelHrCatatan _data)
//        {
//            string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_FK.ToString(), _encryptCode);

//            return new ModelHrCatatan()
//            {
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                BLOK = _data.BLOK,
//                KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
//                DATE_TKH_CTTN = _data.DATE_TKH_CTTN,
//                MASA_TKH_CTTN = _data.MASA_TKH_CTTN
//            };
//        }

//        // -- Insert Catatan Siasatan
//        internal static CarianLaporan MtdInsertLaporanSstn(CarianLaporan _dataIn)
//        {
//            try
//            {
//                _dataIn.SIASATAN_FK = _dataIn.SIASATAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.SIASATAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.LPRN_SSTN_PK = _dataIn.LPRN_SSTN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.LPRN_SSTN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.RESULTSET = "-1";
//                int _insert = 0, _insert2 = 0;

//                _insert = SiasatanDB.DB_InsertLaporanSstn(_dataIn);

//                ModelHrPengadu _data = new()
//                {
//                    PENCIPTA_FK = _dataIn.PENCIPTA_FK,
//                    SIASATAN_PK = _dataIn.SIASATAN_FK,
//                    STATUS_FK = "345",
//                    KATEGORI_PENGESAHAN_FK = "5128"
//                };
//                _insert2 = SiasatanDB.DB_MtdInsertPengesahan(_data);

//                if (_insert > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                    if (_insert2 > 0)
//                    {
//                        _dataIn.RESULTSET = "2";
//                    }
//                    else
//                    {
//                        _dataIn.RESULTSET = "-1";
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdInsertCatatanSstn. More info: " + e);
//            }

//            return _dataIn;
//        }

//        internal static CarianLaporan MtdGetLaporanSstn(string _siasatanPkEnc)
//        {
//            CarianLaporan _data = new();
//            int _siasatanFk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            ModelHrLaporan _info = SiasatanDB.DB_GetLaporanSstn(_siasatanFk);
//            if (_info != null)
//            {
//                _info = MtdHrLaporanPindahFromLibrary(_info);
//                _data._laporan = _info;
//            }
//            return _data;
//        }

//        private static ModelHrLaporan MtdHrLaporanPindahFromLibrary(ModelHrLaporan _data)
//        {
//            string _laporanSstnPkEnc = EncryptHr.NewEncrypt(_data.LPRN_SSTN_PK.ToString(), _encryptCode);
//            string _rumusanPkEnc = _data.RUMUSAN_PK > 0 ? EncryptHr.NewEncrypt(_data.RUMUSAN_PK.ToString(), _encryptCode) : "";

//            return new ModelHrLaporan()
//            {
//                LPRN_SSTN_PK_ENC = _laporanSstnPkEnc,
//                RUMUSAN_PK_ENC = _rumusanPkEnc,
//                LPRN_SSTN_PK = _data.LPRN_SSTN_PK,
//                TAJUK_KES = _data.TAJUK_KES,
//                BTIRN_LPRN = _data.BTIRN_LPRN,
//                PERKARA = _data.PERKARA,
//                TARIKH = _data.TARIKH,
//                RESULTSET = _data.RESULTSET,
//                RUMUSAN_PK = _data.RUMUSAN_PK
//            };
//        }

//        // Get Senarai Rumusan Siasatan
//        internal static List<ModelHrLaporan> MtdGetRumusanList(string? _lprnSstnPkEnc)
//        {
//            int _lprnSstnPk = _lprnSstnPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_lprnSstnPkEnc, _encryptCode)) : 0;
//            IEnumerable<ModelHrLaporan> _dataList = SiasatanDB.DB_GetRumusanList(_lprnSstnPk);
//            List<ModelHrLaporan> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    _list.Add(MtdHrLaporanPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        // Begin: Action Guide
//        // dari Value Status FK - HR_INV_SIASATAN, HR_INV_PENGESAHAN
//        internal static string LabelStatusDesc(string? _statuscode)
//        {
//            string status = "";
//            try
//            {
//                if (_statuscode == "345")
//                {
//                    status = " badge-warning~Hantar"; // Hantar
//                }
//                else if (_statuscode == "349")
//                {
//                    status = " badge-success~Diluluskan"; // Lulus
//                }
//                else if (_statuscode == "378")
//                {
//                    status = " badge-warning~Siasatan Lanjut"; // Siasatan Lanjut
//                }
//                else if (_statuscode == "380")
//                {
//                    status = " badge-success~Selesai"; // Selesai
//                }
//                else if (_statuscode == "381")
//                {
//                    status = " badge-primary~Batal"; // Batal
//                }
//                else if (_statuscode == "5142")
//                {
//                    status = " badge-danger~Pembetulan"; // Pembetulan
//                }
//                else { status = " badge-secondary~Tiada"; } // TIADA
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SenaraiProcess. LabelStatus. More info: " + e);
//            }
//            return status;
//        }

//        // -- Hapus Rumusan Siasatan
//        internal static ModelHrLaporan MtdHapusRumusanSstn(ModelHrLaporan _dataIn)
//        {
//            try
//            {
//                _dataIn.RUMUSAN_PK = _dataIn.RUMUSAN_PK_ENC != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_dataIn.RUMUSAN_PK_ENC, _encryptCode)) : 0;
//                _dataIn.RESULTSET = "-1";
//                int _hapus = 0;

//                _hapus = SiasatanDB.DB_HapusRumusanSstn(_dataIn);

//                if (_hapus > 0)
//                {
//                    _dataIn.RESULTSET = "2";
//                }
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess ~ MtdHapusRumusanSstn. More info: " + e);
//            }

//            return _dataIn;
//        }

//        // dari Value Status AKTIF - HR_STAF
//        internal static string LabelStatusAktifDesc(string? _statuscode)
//        {
//            string status = "";
//            try
//            {
//                if (_statuscode == "Y") // Aktif
//                {
//                    status = " badge-success~Aktif";
//                }
//                else if (_statuscode == "N") // Tidak Aktif
//                {
//                    status = " badge-danger~Tidak Aktif";
//                }
//                else { status = " badge-secondary~Tiada"; } // TIADA
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SiasatanProcess. LabelStatusAktifDesc. More info: " + e);
//            }
//            return status;
//        }

//        public static string UbahTarikhInsert(string tarikh)
//        {
//            string TKH_ASAL = tarikh;
//            try
//            {
//                // untuk dapat data dari table, tidak boleh untuk insert
//                string[] value = tarikh.Split('/');
//                value[2] = value[2].Substring(0, 4);
//                string[] bulan = new string[] { "", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
//                TKH_ASAL = value[0] + "-" + bulan[Convert.ToInt32(value[1]) * 1] + "-" + value[2];
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: MvcHrPreServices WaranProcess UbahTarikhInsert. Tarikh: " + tarikh + " .More info: " + e);
//            }
//            return TKH_ASAL;
//        }

//        // End: Action Guide

//        // Begin:
//        // DDL - Kategori Aduan
//        internal static IEnumerable<ModelParameterHr> ListKatAduan()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListKatAduan().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Kategori Pengadu
//        internal static IEnumerable<ModelParameterHr> ListKatPengadu()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListKatPengadu().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Status Aduan
//        internal static IEnumerable<ModelParameterHr> ListStsAduan()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListStsAduan().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Senarai Jawatan KP
//        internal static IEnumerable<ModelParameterHr> ListJawatanAll(string _kodjwtn)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.ListJawatanAll(_kodjwtn).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Status Aduan
//        internal static IEnumerable<ModelParameterHr> ListStsAktif()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListStsAktif().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Kategori Percakapan
//        internal static IEnumerable<ModelParameterHr> ListKatPrckpn()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListKatPrckpn().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Kategori Peranan
//        internal static IEnumerable<ModelParameterHr> MtdGetPerananByKatPrckpnList(string _siasatanPkEnc, int _katPrckpn, string? _katBrg)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_GetPerananByKatPrckpnList(_siasatanPk, _katPrckpn).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            string _kod = SiasatanDB.DB_GetKodPerananPrckpn(_katBrg);
//            int _no = 1;
//            if (_katPrckpn != 1010)
//            {
//                if (_dataLib != null)
//                {
//                    foreach (var row in _dataLib)
//                    {
//                        row.Nombor = row.Nombor + _no;
//                        row.Key = _kod + row.Nombor + _no;
//                        row.ViewField = _kod + row.Nombor;
//                        _list.Add(MtdPindahParameter(row));
//                    }
//                }
//                else
//                {
//                    ParameterHrModel _data = new();
//                    _data.Key = _kod + _no;
//                    _data.ViewField = _kod + _no;
//                    _list.Add(MtdPindahParameter(_data));
//                }
//            }
//            else
//            {
//                ParameterHrModel _data = new();
//                _data.Key = _kod + _no;
//                _data.ViewField = _kod + _no;
//                _list.Add(MtdPindahParameter(_data));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> MtdGetSaksiList(string? _katsaksi, string? _select, string? _name)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = new List<ParameterHrModel>();
//            if (_katsaksi == "421")
//            {
//                _dataLib = SiasatanDB.DB_GetSaksiList(_katsaksi, _select, _name).ToList();
//            }
//            if (_katsaksi == "422")
//            {
//                _dataLib = SiasatanDB.DB_GetSaksiStudentList(_katsaksi, _select, _name).ToList();
//            }
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        // DDL - Kategori Kod
//        internal static IEnumerable<ModelParameterHr> ListKategoriKod(string? _katkod)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListKategoriKod(_katkod).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        // DDL - Kategori Kod Peranan
//        internal static IEnumerable<ModelParameterHr> MtdGetKodPerananList(string _siasatanPkEnc, string? _kodperanan)
//        {
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_GetKodPerananList(_siasatanPk, _kodperanan).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        // DDL - Kategori Kod Borang Percakapan
//        internal static IEnumerable<ModelParameterHr> MtdGetKodPerananPerincianList(string? _peribadiPrckpnPk)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_GetKodPerananPerincianList(_peribadiPrckpnPk).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        // DDL - Kategori Kampus
//        internal static IEnumerable<ModelParameterHr> ItemListKampusFk()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_ListKampusFk().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Kategori Zon
//        internal static IEnumerable<ModelParameterHr> MtdGetKodZonAjax(string? _kampus)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_GetKodZonList(_kampus).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        // DDL - Kategori Blok
//        internal static IEnumerable<ModelParameterHr> MtdGetKodBlokAjax(string? _kampus, string? _zon)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = SiasatanDB.DB_GetKodBlokList(_kampus, _zon).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        private static ModelParameterHr MtdPindahParameter(ParameterHrModel row)
//        {
//            return new ModelParameterHr()
//            {
//                Key = row.Key,
//                ViewField = row.ViewField,
//                Text = row.Tahun
//            };
//        }

//        // End: DDL

//        // Chat
//        internal static CarianMessage MtdGetMaklumatPenyiasatChat(string _siasatanPkEnc, string? _stafPk, string userAll)
//        {
//            CarianMessage _data = new();
//            int _siasatanPk = _siasatanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_siasatanPkEnc, _encryptCode)) : 0;
//            ModelHrMessage pegChat = SiasatanDB.DB_GetMaklumatPenyiasatChat(_siasatanPk, _stafPk, userAll);
//            if (pegChat != null)
//            {
//                pegChat = MtdHrMessagePindahFromLibrary(pegChat);
//                _data.pegChat = pegChat;
//            }
//            return _data;
//        }

//        private static ModelHrMessage MtdHrMessagePindahFromLibrary(ModelHrMessage _data)
//        {
//            return new ModelHrMessage()
//            {
//                SIASATAN_PK = _data.SIASATAN_PK,
//                KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
//                REPORT_NO = _data.REPORT_NO,
//                ID_PENGGUNA = _data.ID_PENGGUNA,
//                CATATAN = _data.CATATAN,
//                DATE_CIPTA = _data.DATE_CIPTA,
//                MASA_CIPTA = _data.MASA_CIPTA,
//            };
//        }
//    }
//}
