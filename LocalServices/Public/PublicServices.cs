//using Dapper;
//using Net6HrPublicLibrary.Model;
//using APELC.LocalServices.Public;
//using APELC.PublicShared;
using APELC.LocalShared;
using APELC.Models;
//using Oracle.ManagedDataAccess.Client;

//namespace APELC.LocalServices.Public
//{
//    public class PublicServices
//    {
//        static string _encryptCode = SecurityConstants.EncryptCode();
//        static readonly string ConnOraHrDs = SecurityConstants.ConnUtmDbDs();
//        internal static bool MtdTestConnection()
//        {
//            bool _return = true;
//            try
//            {
//                //using (var dbConn = new OracleConnection(ConnOraHrDs))
//                //{
//                //    int _xx = dbConn.QueryFirstOrDefault<int>("SELECT 7 FROM DUAL ");
//                //    if (_xx > 0)
//                //    {
//                //        _return = true;
//                //    }
//                //    else { _return = false; }
//                //}
//            }
//            catch (Exception e)
//            {
//                _return = false;
//                var log = NLog.LogManager.GetCurrentClassLogger();
//                log.Info("MtdTestConnection e ~ " + e);
//            }
//            return _return;

//        }

//        internal static IEnumerable<ModelParameterHr> MtdGetFakultiByCampusList(string _kodKampus)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListFakultiByCampus(_kodKampus).ToList();
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
//                ViewField = row.ViewField
//            };
//        }
//        private static ModelParameterHr MtdPindahParameterWithKey(ParameterHrModel row)
//        {
//            return new ModelParameterHr()
//            {
//                Key = row.Key,
//                ViewField = row.Key + " - " + row.ViewField
//            };
//        }


//        internal static IEnumerable<ModelParameterHr> ListSmuParameterKeyKodByKumpulan(int _kumpulan)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListSmuParameterKeyKodByKumpulan(_kumpulan).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> MtdGetCajKlinikList(int _klinikFk)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.MtdGetCajKlinikList(_klinikFk);
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            _list.Add(new ModelParameterHr()
//            {
//                Key = "1020~~0",
//                ViewField = "Lain-Lain"
//            });
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListSmuParameterWithPKnKod(int _paramPk, string _kod)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListSmuParameterEnglishWithCode(_paramPk, _kod).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListTarafJawatanSub()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListTarafJawatan().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelParameterHr> ListApprovalStructureStafFkByCodeJabatan(string _kod, string _kodFakulti)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListApprovalStructureStafFkByCodeJabatan(_kod, _kodFakulti).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }


//        internal static IEnumerable<ModelParameterHr> ListFakulti2018()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListFakulti2018().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelParameterHr> ListApprovalStructureStafFkPPTPByCodeJabatan(string _kodFakulti)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListApprovalStructureStafFkPPTPByCodeJabatan(_kodFakulti).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListJawatanAll()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListJawatanAll().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListTarafJawatan()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListTarafJawatan().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListTahunAll()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListTahunAll().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListBahagianAll(string fakulti)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListBahagianAll(fakulti).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListHrUnitByKodFakulti(string kod)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListHrUnitByKodFakulti(kod).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ItemListPerananPengguna(string _kod)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ItemListPerananPengguna(_kod).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static ModelHrStaffPeribadi MtdGetHrStafMaklumatByNoPekerja(string _nopekerja)
//        {
//            ModelHrStaffPeribadi _backData = new ModelHrStaffPeribadi()
//            {
//                NO_PEKERJA = _nopekerja
//            };
//            HrStaffMaklumatPeribadiModel _getData = PublicOracleServices.MtdGetHrStafMaklumatByNoPekerja(_nopekerja);
//            if (_getData != null)
//            {
//                _backData = MtdHrStaffPindahFromLibrary(_getData);
//            }
//            return _backData;
//        }

//        internal static ModelHrStaffPeribadi MtdGetHrStafMaklumatByNoKP(string _nokp)
//        {
//            ModelHrStaffPeribadi _backData = new ModelHrStaffPeribadi()
//            {
//                NO_KP_BARU = _nokp
//            };
//            HrStaffMaklumatPeribadiModel _getData = PublicOracleServices.MtdGetHrStafMaklumatByNoKP(_nokp);
//            if (_getData != null)
//            {
//                _backData = MtdHrStaffPindahFromLibrary(_getData);
//            }
//            return _backData;
//        }

//        internal static ModelHrStaffPeribadi MtdGetHrStafMaklumatByStafFk(int stafFk)
//        {
//            ModelHrStaffPeribadi _backData = new ModelHrStaffPeribadi()
//            {
//                STAF_PK = stafFk,
//                STAF_PK_ENC = EncryptHr.NewEncrypt(stafFk.ToString(), _encryptCode)
//            };
//            HrStaffMaklumatPeribadiModel _getData = PublicOracleServices.MtdGetHrStafMaklumatByStafFk(stafFk);
//            if (_getData != null)
//            {
//                _backData = MtdHrStaffPindahFromLibrary(_getData);
//            }
//            return _backData;
//        }

//        private static ModelHrStaffPeribadi MtdHrStaffPindahFromLibrary(HrStaffMaklumatPeribadiModel _data)
//        {
//            //var log = NLog.LogManager.GetCurrentClassLogger();
//            //log.Info("MtdPindahFromLibrary  _data.STAF_PK ~ " + _data.STAF_PK);
//            string _stafPkEnc = EncryptHr.NewEncrypt(_data.STAF_PK.ToString(), _encryptCode);
//            string _nokerjaEnc = EncryptHr.NewEncrypt(_data.NO_PEKERJA.ToString(), _encryptCode);
//            //log.Info("MtdPindahFromLibrary  _data.STAF_PK ~ " + _data.STAF_PK);
//            return new ModelHrStaffPeribadi()
//            {
//                STAF_PK = _data.STAF_PK,
//                STAF_PK_ENC = _stafPkEnc,
//                UMUR_PILIHAN_PENCEN_FK = _data.UMUR_PILIHAN_PENCEN_FK,
//                TKH_PENCEN = _data.TKH_PENCEN,
//                PAPAR_TKHPENCEN = _data.PAPAR_TKHPENCEN,
//                TKH_MULA_JPA = _data.TKH_MULA_JPA,
//                TKH_MULA_UTM = _data.TKH_MULA_UTM,
//                TKH_LANTIKAN_TERKINI = _data.TKH_LANTIKAN_TERKINI,
//                TKH_MULA_KONTRAK = _data.TKH_MULA_KONTRAK,
//                TKH_TAMAT_KONTRAK = _data.TKH_TAMAT_KONTRAK,
//                TKH_SAH_JAWATAN = _data.TKH_SAH_JAWATAN,
//                PAPAR_TKHSAHJAWATAN = _data.PAPAR_TKHSAHJAWATAN,
//                GELARAN_PROFESSIONAL = _data.GELARAN_PROFESSIONAL,
//                NO_PEKERJA = _data.NO_PEKERJA,
//                NO_PEKERJA_ENC = _nokerjaEnc,
//                TKH_HENTI = _data.TKH_HENTI,
//                TKH_MATI = _data.TKH_MATI,
//                KOD_PTJ = _data.KOD_PTJ,
//                KOD_JAWATAN = _data.KOD_JAWATAN,
//                KOD_PTJ_ASAL = _data.KOD_PTJ_ASAL,
//                KOD_JAWATAN_GILIRAN = _data.KOD_JAWATAN_GILIRAN,
//                TKH_KURSUS_BTN = _data.TKH_KURSUS_BTN,
//                STATUS_ASSTATK = _data.STATUS_ASSTATK,
//                STATUS_AKTIF = _data.STATUS_AKTIF,
//                ASTARAFK = _data.ASTARAFK,
//                STATUS_BTN = _data.STATUS_BTN,
//                TKH_LAPOR_DIRI = _data.TKH_LAPOR_DIRI,
//                NO_FAIL = _data.NO_FAIL,
//                KOD_JAWATAN_HAKIKI = _data.KOD_JAWATAN_HAKIKI,
//                PUSAT_KOS = _data.PUSAT_KOS,
//                KOD_PTJ_PENEMPATAN = _data.KOD_PTJ_PENEMPATAN,
//                TKH_LANTIK_TETAP = _data.TKH_LANTIK_TETAP,
//                PAPAR_TKHLANTIKTETAP = _data.PAPAR_TKHLANTIKTETAP,
//                TKH_LANTIKAN_AKADEMIK = _data.TKH_LANTIKAN_AKADEMIK,
//                PAPAR_TKHLANTIKANAKADEMIK = _data.PAPAR_TKHLANTIKANAKADEMIK,
//                TARAF_JAWATAN_SUB_FK = _data.TARAF_JAWATAN_SUB_FK,
//                MAKLUMAT_PERIBADI_PK = _data.MAKLUMAT_PERIBADI_PK,
//                ALAMAT_TETAP_FK = _data.ALAMAT_TETAP_FK,
//                ALAMAT_MENYURAT_FK = _data.ALAMAT_MENYURAT_FK,
//                ALAMAT_PEJABAT_FK = _data.ALAMAT_PEJABAT_FK,
//                AGAMA = _data.AGAMA,
//                BANGSA_FK = _data.BANGSA_FK,
//                KEWARGANEGARAAN = _data.KEWARGANEGARAAN,
//                KOD_NEGERI_LAHIR = _data.KOD_NEGERI_LAHIR,
//                STATUS_KAHWIN = _data.STATUS_KAHWIN,
//                KOD_WARGANEGARA = _data.KOD_WARGANEGARA,
//                JANTINA = _data.JANTINA,
//                NO_KP_LAMA = _data.NO_KP_LAMA,
//                NO_KP_BARU = _data.NO_KP_BARU,
//                TEMPAT_LAHIR = _data.TEMPAT_LAHIR,
//                NAMA = _data.NAMA,
//                WARNA_KP = _data.WARNA_KP,
//                NO_OKU = _data.NO_OKU,
//                TKH_LAHIR = _data.TKH_LAHIR,
//                PAPAR_TKHLAHIR = _data.PAPAR_TKHLAHIR,
//                JAWATAN_DESC = _data.JAWATAN_DESC,
//                JAWATAN_AKADEMIK = _data.JAWATAN_AKADEMIK,
//                JAWATAN_GRED = _data.JAWATAN_GRED,
//                FAKULTI_DESC = _data.FAKULTI_DESC,
//                PHOTOSTAF = _data.PHOTOSTAF,
//                LPPT_DATA = _data.LPPT_DATA,
//                RESULTSET = _data.RESULTSET,
//                EMAIL_RASMI = _data.EMAIL_RASMI,
//                NO_HP = _data.NO_HP,
//                KAMPUS_DESC = _data.KAMPUS_DESC,
//                KAMPUS_KOD = _data.KAMPUS_KOD
//            };
//        }
//        internal static IEnumerable<ModelParameterHr> ListDDDescCaj()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = new List<ParameterHrModel>(); // PublicOracleServices.ListDDDescCaj().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static ModelKadKedatangan MtdGetNoKerjaCutiSokongLulus(string noPekerjaPemohon)
//        {
//            ModelKadKedatangan _backData = new ModelKadKedatangan()
//            {
//                KAD_NOKERJA = noPekerjaPemohon
//            };
//            KadKedatanganModel _getData = PublicOracleServices.MtdGetNoKerjaCutiSokongLulus(noPekerjaPemohon);
//            if (_getData != null)
//            {
//                _backData = MtdPindahFromLibraryAsasCuti(_getData);
//            }
//            return _backData;
//        }

//        private static ModelKadKedatangan MtdPindahFromLibraryAsasCuti(KadKedatanganModel _getData)
//        {
//            ModelKadKedatangan _return = new ModelKadKedatangan()
//            {
//                KAD_NOKERJA = _getData.KAD_NOKERJA,
//                KAD_NOKERJA_ENC = _getData.KAD_NOKERJA_ENC,
//                KAD_PEGSOKONG = _getData.KAD_PEGSOKONG,
//                KAD_PEGLULUS = _getData.KAD_PEGLULUS,
//                RESULTSET = _getData.RESULTSET
//            };
//            return _return;
//        }

//        internal static IEnumerable<ModelParameterHr> ItemJenisPerkhidmatan()
//        {
//            List<ParameterHrModel> _dataLib = PublicConstant.ItemJenisPerkhidmatan;
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelParameterHr> ItemListKampus()
//        {
//            List<ParameterHrModel> _dataLib = PublicConstant.ItemListKampus;
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }
//        internal static IEnumerable<ModelParameterHr> ListAdmParameterKeyKodByKumpulan(int _kumpulan)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListAdmParameterKeyKodByKumpulan(_kumpulan).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static IEnumerable<ModelParameterHr> ListHrParameterKeyKodByKumpulan(int _kumpulan)
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ItemListHrParameter(_kumpulan).ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }


//        internal static IEnumerable<ModelParameterHr> ListCountryAll()
//        {

//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListCountryAll().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static int MtdGetBilStafAktifFakulti(string _kodFakulti)
//        {
//            return PublicOracleServices.MtdGetBilStafAktifFakulti(_kodFakulti);
//        }

//        internal static IEnumerable<ModelParameterHr> ListHrmsKodCuti()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListHrmsKodCuti().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelParameterHr> ListKodEssential()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListKodEssential().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelParameterHr> ListFakultiAll()
//        {
//            IEnumerable<ParameterHrModel> _dataLib = PublicOracleServices.ListFakultiAll().ToList();
//            List<ModelParameterHr> _list = new List<ModelParameterHr>();
//            foreach (var row in _dataLib)
//            {
//                _list.Add(MtdPindahParameter(row));
//            }
//            return _list;
//        }

//        internal static List<ModelHrStaffPeribadi> MtdGetCarianStaffListSearch(string? _kampus, string? _fakulti, string? _jabatan, string? _jawatan, string? _nopekerja, string? _nokp, string? _nama, string? _userPtj)
//        {
//            string? _unit = "";

//            IEnumerable<HrStaffMaklumatPeribadiModel> _dataList = PublicOracleServices.MtdGetCarianStaffListSearch(_kampus, _fakulti, _jabatan, _unit, _jawatan, _nopekerja, _nokp, _nama, _userPtj);
//            List<ModelHrStaffPeribadi> _list = new();
//            foreach (var row in _dataList)
//            {
//                _list.Add(MtdHrStaffPindahFromLibrary(row));
//            }
//            return _list;
//        }

//    }
//}
