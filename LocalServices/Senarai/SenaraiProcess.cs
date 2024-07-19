//using Net6HrPublicLibrary.Model;
//using Net6HrPublicLibrary.PublicServices.Login;
//using Net6HrPublicLibrary.PublicServices.Public;
////using Net6HrPublicLibrary.PublicShared;
//using APEL.LocalServices.Siasatan;
//using APEL.LocalShared;
//using APEL.Models;

//namespace APEL.LocalServices.Senarai
//{
//    public class SenaraiProcess
//    {
//        readonly static string _encryptCode = SecurityConstants.EncryptCode();

//        internal static List<ModelHrPengadu> MtdGetPengaduList(string? _noaduan, string? _katPengadu, string? _cKampus, string? _cStsAduan, string? _cKatAduan, string? _cTkhMula, string? _cTkhTamat, string userAll, string userPnyst)
//        {
//            //int _stafPk = userPnyst != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(userPnyst, _encryptCode)) : 0;
//            //IEnumerable<ModelHrPengadu> _dataList = SenaraiDB.DB_MtdGetPengaduList(_noaduan, _katPengadu, _cKampus, _cStsAduan, _cKatAduan, _cTkhMula, _cTkhTamat, userAll, _stafPk);
//            List<ModelHrPengadu> _list = new();
//            if (_dataList != null)
//            {
//                foreach (var row in _dataList)
//                {
//                    if (row.COMPLAINER_FK != null) // Staff
//                    {
//                        if (row.INFO_PENGADU_STAF != null)
//                        {
//                            string[] _staf = row.INFO_PENGADU_STAF.Split('~');
//                            row.NAMA_PENGADU = _staf[0];
//                            row.NO_ID = _staf[1];
//                            row.NO_KP = _staf[2];
//                        }
//                        else
//                        {
//                            row.NAMA_PENGADU = "TIADA";
//                        }
//                    }
//                    if (row.MAKLUMAT_PERIBADI_FK != null) // Lain-Lain
//                    {
//                        string? _lain = row.INFO_LAIN;
//                        if (_lain != null && _lain.Length > 1)
//                        {
//                            row.NAMA_PENGADU = MtdGetSplitAyat(_lain, 0);
//                            row.NO_KP = MtdGetSplitAyat(_lain, 1);
//                            row.NO_ID = "-";
//                        }
//                        else
//                        {
//                            row.NAMA_PENGADU = "TIADA";
//                        }
//                    }

//                    row.LABEL_STATUS = LabelStatusDesc(row.STATUS_FK);
//                    string[] n = row.LABEL_STATUS.Split('~');
//                    row.LABEL_STATUS = n[0];
//                    row.STATUS_DESC = n[1];

//                    _list.Add(MtdHrPengaduPindahFromLibrary(row));
//                }
//            }
//            return _list;
//        }

//        private static ModelHrPengadu MtdHrPengaduPindahFromLibrary(ModelHrPengadu _data)
//        {
//            //string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_PK.ToString(), _encryptCode);
//            //string _siasatanPkEnc = EncryptHr.NewEncrypt(_data.SIASATAN_PK.ToString(), _encryptCode);
//            //string? _pelajarinfo = _data.COMPLAINER_NO_KP != null ? SiasatanDB.DB_GetInfoPelajarDetails(_data.COMPLAINER_NO_KP) : null; // Pelajar
//            if (_pelajarinfo != null)
//            {
//                string[] _student = _pelajarinfo.Split('~');
//                _data.NAMA_PENGADU = _student[0];
//                _data.NO_ID = _student[1];
//                _data.NO_KP = _student[2];
//            }

//            return new ModelHrPengadu()
//            {
//                ADUAN_PK = _data.ADUAN_PK,
//                ADUAN_PK_ENC = _aduanPkEnc,
//                SIASATAN_PK = _data.SIASATAN_PK,
//                SIASATAN_PK_ENC = _siasatanPkEnc,
//                REPORT_NO = _data.REPORT_NO,
//                TKH_ADUAN = _data.TKH_ADUAN,
//                DATE_TKH_ADUAN = _data.DATE_TKH_ADUAN,
//                MASA_TKH_ADUAN = _data.MASA_TKH_ADUAN,
//                NAMA_PENGADU = _data.NAMA_PENGADU,
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
