//using Net6HrPublicLibrary.Model;
//using Net6HrPublicLibrary.PublicServices.Login;
//using Net6HrPublicLibrary.PublicServices.Public;
////using Net6HrPublicLibrary.PublicShared;
//using APELC.LocalServices.ApelC;
//using APELC.LocalShared;
//using APELC.Models;

//namespace APELC.LocalServices.Senarai
//{
//    public class SenaraiProcess
//    {
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();

//        internal static List<ModelHrPemohon> MtdGetPemohonList(string? _noaduan, string? _katPemohon, string? _cKampus, string? _cStsAduan, string? _cKatAduan, string? _cTkhMula, string? _cTkhTamat, string userAll, string userPnyst)
//        {
//            //int _stafPk = userPnyst != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(userPnyst, _encryptCode)) : 0;
//            //IEnumerable<ModelHrPemohon> _dataList = SenaraiDB.DB_MtdGetPemohonList(_noaduan, _katPemohon, _cKampus, _cStsAduan, _cKatAduan, _cTkhMula, _cTkhTamat, userAll, _stafPk);
//            List<ModelHrPemohon> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.COMPLAINER_FK != null) // Staff
//                    {
//                        if (row.INFO_Pemohon_STAF != null)
//                        {
//                            string[] _staf = row.INFO_Pemohon_STAF.Split('~');
//                            row.NAMA_PEMOHON = _staf[0];
//                            row.NO_ID = _staf[1];
//                            row.NO_KP = _staf[2];
//                        }
//                        else
//                        {
//                            row.NAMA_PEMOHON = "TIADA";
//                        }
//                    }
//                    if (row.MAKLUMAT_PERIBADI_FK != null) // Lain-Lain
//                    {
//                        string? _lain = row.INFO_LAIN;
//                        if (_lain != null && _lain.Length > 1)
//                        {
//                            row.NAMA_PEMOHON = MtdGetSplitAyat(_lain, 0);
//                            row.NO_KP = MtdGetSplitAyat(_lain, 1);
//                            row.NO_ID = "-";
//                        }
//                        else
//                        {
//                            row.NAMA_PEMOHON = "TIADA";
//                        }
//                    }

//                    row.LABEL_STATUS = LabelStatusDesc(row.STATUS_FK);
//                    string[] n = row.LABEL_STATUS.Split('~');
//                    row.LABEL_STATUS = n[0];
//                    row.STATUS_DESC = n[1];

//                    _list.Add(MtdHrPemohonPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        private static ModelHrPemohon MtdHrPemohonPindahFromLibrary(ModelHrPemohon _data)
//        {
//            //string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_PK.ToString(), _encryptCode);
//            //string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.ApelC_PK.ToString(), _encryptCode);
//            //string? _pelajarinfo = _data.COMPLAINER_NO_KP != null ? SiasatanDB.DB_GetInfoPelajarDetails(_data.COMPLAINER_NO_KP) : null; // Pelajar
//            if (_pelajarinfo != null)
//            {
//                string[] _student = _pelajarinfo.Split('~');
//                _data.NAMA_PEMOHON = _student[0];
//                _data.NO_ID = _student[1];
//                _data.NO_KP = _student[2];
//            }

//            return new ModelHrPemohon()
//            {
//                ADUAN_PK = _data.ADUAN_PK,
//                MOHON_PK_ENC = _aduanPkEnc,
//                SIASATAN_PK = _data.ApelC_PK,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                MOHON_NO = _data.MOHON_NO,
//                TKH_ADUAN = _data.TKH_ADUAN,
//                DATE_TKH_ADUAN = _data.DATE_TKH_ADUAN,
//                MASA_TKH_ADUAN = _data.MASA_TKH_ADUAN,
//                NAMA_PEMOHON = _data.NAMA_PEMOHON,
//                NO_ID = _data.NO_ID,
//                NO_KP = _data.NO_KP,
//                STATUS_DESC = _data.STATUS_DESC,
//                LABEL_STATUS = _data.LABEL_STATUS
//            };
//        }

//        public static string MtdGetSplitAyat(string _ayat, int _lokasi)
//        {
//            string _return = "";
//            if (_ayat != null)
//            {
//                string[] _ayat01 = PublicConstant.SplitAyat(_ayat, "~");
//                _return = _ayat01[_lokasi];
//            }
//            return _return;
//        }

//        internal static string LabelStatusDesc(string? _statuscode)
//        {
//            string status = "";
//            try
//            {
//                if (_statuscode == "378")
//                {
//                    status = " badge-warning~Siasatan Lanjut";
//                } // Siasatan Lanjut
//                else if (_statuscode == "380")
//                {
//                    status = " badge-success~Selesai";
//                } // Selesai
//                else if (_statuscode == "381")
//                {
//                    status = " badge-primary~Batal";
//                } // Batal
//                else { status = " badge-secondary~Tiada"; } // TIADA
//            }
//            catch (Exception e)
//            {
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info(DateTime.Now + " Error: SenaraiProcess. LabelStatus. More info: " + e);
//            }
//            return status;
//        }
//    }
//}
