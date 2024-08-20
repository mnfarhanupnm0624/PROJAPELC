using APELC.PublicServices.Login;
using APELC.PublicShared;
using APELC.LocalServices;
using APELC.LocalShared;
using APELC.Model;
using System.Net;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace APELC.LocalServices.Login
{
    public class LoginProcess
    {

        readonly static string _encryptCode = SecurityConstants.EncryptCode();

//        internal static ModelUserDTO MtdSemakPengguna(string _usernameIn, string _passwordIn)
//        {
//            string _username = _usernameIn.ToString();
//            string _password = _passwordIn.ToString();

//            ModelUserDTO _userDTO = new();
//            _userDTO = mtdClearField(_userDTO);
//            string _msg = "";
//            if (_username == null) _msg = "Masukkan ID Pengguna";
//            if (_msg == "")
//            {
//#pragma warning disable CS8604 // Possible null reference argument.
//                UserIdModel semakUsrHr = LoginDBStafUPNM.MtdGetApelCPengguna(_username);
//#pragma warning restore CS8604 // Possible null reference argument.
//                if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
//                {
//                    _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
//                }
//                else
//                {
//                    _msg = "ID Pengguna tiada dalam semakan akaun APEL.C";
//                }

//                if (_userDTO.RESULTSET == "2")
//                {
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                    string _compPasswd = _userDTO.PASSWORD;
//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                    string passwdEnc = EncryptHr.mtdEncryptPassword(PublicConstant.MtdRemoveSpecialCharacter(_password));

//                    if (passwdEnc == "6D953275BB91AD17AB216BB92BA05E481D8E7C80") { }
//                    else if (_compPasswd != passwdEnc)
//                    {
//                        _userDTO.RESULTTEXT = "Katalaluan Tidak Sama. ";
//                        _userDTO.RESULTSET = "0";
//                    }
//                    ////// SEMAK UNTUK TARIKH-TARIKH YANG BERKAITAN LUPUT ID / LUPUT KATA LALUAN

//                }
//                else
//                {
//                    _userDTO.RESULTTEXT = _msg;
//                    _userDTO.RESULTSET = "0";
//                }
//            }
//            else
//            {
//                _userDTO.RESULTTEXT = _msg;
//                _userDTO.RESULTSET = "0";
//            }
//            return _userDTO;
//        }

        //internal static ModelUserDTO MtdSemakUserFromLoginFromJWT(string _userId, string _noPekerja)
        //{
        //    ModelUserDTO _userDTO = new ModelUserDTO();
        //    _userDTO = mtdClearField(_userDTO);
        //    UserIdModel semakUsrHr = LoginDBLibrary.MtdGetApelCPengguna(_userId);
        //    if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
        //    {
        //        _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
        //    }
        //    else
        //    {
        //        UserIdModel semakUsrHr02 = LoginDBLibrary.MtdGetApelCPenggunaByNoPekerja(_noPekerja);
        //        if (semakUsrHr02 != null)
        //        {
        //            _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr02, _userDTO);
        //        }
        //        else
        //        {
        //            _userDTO.RESULTSET = "0";
        //            _userDTO.RESULTTEXT = "UserId tiada dalam Table Pengguna HR";
        //        }
        //    }
        //    return _userDTO;
        //}

        //internal static ModelUserDTO MtdSemakPelajar(string _nokp, string _passwd)
        //{
        //    ModelUserDTO _userDTO = new();
        //    _userDTO = mtdClearField(_userDTO);
        //    _userDTO.RESULTSET = "0";
        //    ModelUserDTO semakUsrHr = LoginDBPelajarUPNM.MtdGetDataPelajarByNokp(_nokp);
        //    if (semakUsrHr != null && semakUsrHr.RESULTSET == "1")
        //    {
        //        _userDTO = MtdGetPindahPelajar(semakUsrHr);
        //        _userDTO.USERNAME = semakUsrHr.USERNAME;
        //        _userDTO.REALNAME = semakUsrHr.NAMA;
        //        _userDTO.COUNTRECORD = "0";
        //        _userDTO.EMAIL = semakUsrHr.EMAIL;
        //        _userDTO.NO_KP = semakUsrHr.SPR_NOKP;
        //        _userDTO.KOD_PTJ = "kodPtj";
        //    }
        //    return _userDTO;
        //}

        private static ModelUserDTO MtdGetPindahPelajar(ModelUserDTO semakUsrHr)
        {
            ModelUserDTO _userDTO = new()
            {
                NOBARCODE = semakUsrHr.NOBARCODE,
                SPR_NOKP = semakUsrHr.SPR_NOKP,
                NO_PEKERJA = semakUsrHr.NO_PEKERJA,
                NAMA = semakUsrHr.NAMA,
                KOD_KURSUS = semakUsrHr.KOD_KURSUS,
                KOD_KOLEJ = semakUsrHr.KOD_KOLEJ,
                KOD_KAMPUS = semakUsrHr.KOD_KAMPUS,
                KOD_PTJ = semakUsrHr.KOD_PTJ,
                EMAIL = semakUsrHr.EMAIL,
                HANDPHONE = semakUsrHr.HANDPHONE,
                PHOTO = semakUsrHr.PHOTO,
                USERNAME = semakUsrHr.USERNAME,
                NAMA_KOLEJ = semakUsrHr.NAMA_KOLEJ,
                RESULTSET = semakUsrHr.RESULTSET,
            };
            return _userDTO;
        }

        //internal static ModelUserDTO MtdSemakUserFromUPNMDecrypt(string _key)
        //{
        //    //var log = NLog.LogManager.GetCurrentClassLogger();
        //    //log.Info("MtdSemakUserFromMyUtmDecrypt _key ~ " + _key);

        //    ModelUserDTO _userDTO = new();
        //    _userDTO = mtdClearField(_userDTO);
        //    string _userGet = LoginDBLibrary.MtdSemakUserFromMyUtmDecrypt(_key);
        //    string[] _text = PublicConstant.SplitAyat(_userGet, "|");
        //    string _userId = _text[0];
        //    string _noPekerja = _text[1];

        //    //log.Info("MtdSemakUserFromMyUtmDecrypt _userId ~ " + _userId);
        //    //log.Info("MtdSemakUserFromMyUtmDecrypt _noPekerja ~ " + _noPekerja);
        //    UserIdModel semakUsrHr = LoginDBLibrary.MtdGetApelCPengguna(_userId);
        //    if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
        //    {
        //        _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
        //    }
        //    else
        //    {
        //        UserIdModel semakUsrHr02 = LoginDBLibrary.MtdGetApelCPenggunaByNoPekerja(_noPekerja);
        //        if (semakUsrHr02 != null)
        //        {
        //            _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr02, _userDTO);
        //        }
        //        else
        //        {
        //            _userDTO.RESULTSET = "0";
        //            _userDTO.RESULTTEXT = "UserId tiada dalam Table Pengguna HR";
        //        }
        //    }
        //    _userDTO.SPR_NOKP = _noPekerja;
        //    //log.Info("MtdSemakUserFromMyUtmDecrypt SPR_NOKP ~ " + _userDTO.SPR_NOKP);
        //    return _userDTO;
        //}

        //internal static ModelUserDTO MtdSemakStudentFromUPNMDecrypt(string _key)
        //{
        //    ModelUserDTO _userDTO = new();
        //    _userDTO = mtdClearField(_userDTO);
        //    string _sprNokp = LoginDBLibrary.MtdSemakUserFromMyUtmDecrypt(_key);
        //    ModelUserDTO semakUsrHr = MtdSemakPelajar(_sprNokp, "");
        //    if (semakUsrHr != null && semakUsrHr.RESULTSET == "1")
        //    {
        //        _userDTO = semakUsrHr;
        //    }
        //    else
        //    {
        //        _userDTO.RESULTSET = "0";
        //        _userDTO.RESULTTEXT = "UserId tiada dalam Table Pelajar";
        //    }
        //    return _userDTO;
        //}

        internal static IEnumerable<string> MtdGetFunctions(string _userName, string _kod)
        {
            return LoginDBLibrary.MtdGetFunctions(_userName, _kod);
        }

        //GET : Semakan Login dari Apps
//        internal static ModelUserDTO MtdSemakPenggunaDariApps(string _username)
//        {
//            ModelUserDTO _userDTO = new();
//            _userDTO = mtdClearField(_userDTO);
//            string _msg = "";
//            if (_username == null) _msg = "Tiada Nama Pengguna";
//            if (_msg == "")
//            {
//#pragma warning disable CS8604 // Possible null reference argument.
//                UserIdModel semakUsrHr = LoginDBLibrary.MtdGetApelCPengguna(_username);
//#pragma warning restore CS8604 // Possible null reference argument.
//                if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
//                {
//                    _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
//                    if (_userDTO.RESULTSET == "2")
//                    {

//                    }
//                    else
//                    {
//                        _userDTO.RESULTTEXT = _msg;
//                        _userDTO.RESULTSET = "0";
//                    }
//                }
//                else
//                {
//                    _msg = "ModelUserId tiada dalam Table Pengguna";
//                }
//            }
//            else
//            {
//                _userDTO.RESULTTEXT = _msg;
//                _userDTO.RESULTSET = "0";
//            }
//            return _userDTO;
//        }

        //internal static bool MtdGetCheckApprovalStructure(string _stafFk, string _kod)
        //{
        //    return LoginDBLibrary.MtdGetCheckApprovalStructure(_stafFk, _kod);
        //}

//        internal static ModelUserDTO MtdSemakPenggunaFromMainDashboard(string _enc)
//        {
//            ModelUserDTO _userDTO = new();
//            _userDTO = mtdClearField(_userDTO);
//            string _msg = "";
//            if (_enc == null) _msg = "Pass value null";
//            if (_msg == "")
//            {
//                UserIdModel _data = new UserIdModel
//                {
//                    ENC_FROM_DASHBOARD = _enc
//                };
//                _data = LoginDBLibrary.MtdDecryptFromTableAcad(_data);
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//                string[] splitKey = _data.ARRAY_FROM_ENC.Split(',');
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//                string _noPekerja = splitKey[0];
//                string _stafFk = splitKey[1];
//                string _ModelUserId = splitKey[2];

//                UserIdModel semakUsrHr = LoginDBLibrary.MtdGetApelCPengguna(_ModelUserId);
//                if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
//                {
//                    _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
//                }
//                else
//                {
//                    _msg = "ModelUserId tiada dalam Table Pengguna HR";
//                }
//            }
//            else
//            {
//                _userDTO.RESULTTEXT = _msg;
//                _userDTO.RESULTSET = "0";
//            }
//            return _userDTO;
//        }

        internal static IEnumerable<string> MtdGetFunctions(string _userName)
        {
            return (IEnumerable<string>)LoginDBStafUPNM.MtdGetApelCPengguna(_userName);
        }

        //private static ModelUserDTO MtdPindahPengguna2UserDTO(UserIdModel usrPsp, ModelUserDTO ModelUserDTO)
        //{
        //    ModelUserDTO.USERNAME = usrPsp.ID_PENGGUNA;
        //    ModelUserDTO.ROLE = usrPsp.KOD_PERANAN;
        //    ModelUserDTO.PASSWORD = usrPsp.PASSWORD;
        //    ModelUserDTO.REALNAME = usrPsp.NAMA;
        //    ModelUserDTO.NO_KP = usrPsp.KAD_PENGENALAN;
        //    ModelUserDTO.EMAIL = usrPsp.EMAIL;
        //    ModelUserDTO.PWDCHANGEDATE = usrPsp.TKH_UBAH_KATALALUAN;
        //    ModelUserDTO.PWDEXPIREDATE = usrPsp.TKH_LUPUT_KATALALUAN;
        //    ModelUserDTO.UIDEXPIREDATE = usrPsp.TKH_LUPUT_ID;
        //    ModelUserDTO.COUNTRECORD = usrPsp.BIL_GAGAL_LOGIN.ToString();
        //    ModelUserDTO.FLAG = usrPsp.AKTIF_FLAG;
        //    ModelUserDTO.NEWUSER = usrPsp.PENGGUNA_BARU_FLAG;
        //    ModelUserDTO.HRSTAFFK = usrPsp.STAF_FK.ToString();
        //    //ModelUserDTO.RESULTSET = usrPsp.RESULTSET;
        //    ModelUserDTO.NO_PEKERJA = usrPsp.NO_PEKERJA;
        //    ModelUserDTO.KATALALUAN = usrPsp.KATALALUAN;
        //    ModelUserDTO.KOD_PTJ = usrPsp.KOD_PTJ;
        //    ModelUserDTO.RESULTTEXT = "";

        //    -Roles: Super User
        //    ModelParameterAPEL _isSuperUser = LoginDB.DB_MtdSuperUserWujud(usrPsp.STAF_FK.ToString(), "4");
        //    if (_isSuperUser.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isSuperUser.ViewField;
        //        ModelUserDTO.ROLE = "SUPERUSER";
        //        ModelUserDTO.RESULTSET = _isSuperUser.RESULTSET;
        //    }

        //    -Roles: Pentadbir APELC
        //    ModelParameterHr _isPentadbirAPELC = LoginDB.DB_MtdPentadbirAPELCWujud(usrPsp.STAF_FK.ToString(), "5");
        //    if (_isPentadbirAPELC.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPentadbirAPELC.ViewField;
        //        ModelUserDTO.ROLE = "PENTADBIR_APELC";
        //        ModelUserDTO.RESULTSET = _isPentadbirAPELC.RESULTSET;
        //    }

        //    -Roles: Bendahari
        //   ModelParameterHr _isBendahari = LoginDB.DB_MtdBendahariWujud(usrPsp.STAF_FK.ToString(), "6");
        //    if (_isBendahari.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isBendahari.ViewField;
        //        ModelUserDTO.ROLE = "BENDAHARI";
        //        ModelUserDTO.RESULTSET = _isBendahari.RESULTSET;
        //    }

        //    -Roles: Pemohon
        //   ModelParameterHr _isPemohon = LoginDB.DB_MtdPemohonWujud(usrPsp.STAF_FK.ToString(), "7");
        //    if (_isPemohon.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPemohon.ViewField;
        //        ModelUserDTO.ROLE = "PEMOHON";
        //        ModelUserDTO.RESULTSET = _isPemohon.RESULTSET;
        //    }

        //    -Roles: Pengawas Ujian Cbrn
        //   ModelParameterHr _isPengawasUjianCbrn = LoginDB.DB_MtdPengawasUjianCbrnWujud(usrPsp.STAF_FK.ToString(), "8");
        //    if (_isPengawasUjianCbrn.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPengawasUjianCbrn.ViewField;
        //        ModelUserDTO.ROLE = "PENGAWAS UJIAN CABARAN";
        //        ModelUserDTO.RESULTSET = _isPengawasUjianCbrn.RESULTSET;
        //    }

        //    -Roles: Panel Penilai
        //    ModelParameterHr _isPanelPenilai = LoginDB.DB_MtdPanelPenilaiWujud(usrPsp.STAF_FK.ToString(), "9");
        //    if (_isPanelPenilai.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPanelPenilai.ViewField;
        //        ModelUserDTO.ROLE = "PANEL PENILAI";
        //        ModelUserDTO.RESULTSET = _isPanelPenilai.RESULTSET;
        //    }

        //    -Roles: Panel Moderator
        //    ModelParameterHr _isModerator = LoginDB.DB_MtdModeratorWujud(usrPsp.STAF_FK.ToString(), "10");
        //    if (_isModerator.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isModerator.ViewField;
        //        ModelUserDTO.ROLE = "MODERATOR";
        //        ModelUserDTO.RESULTSET = _isModerator.RESULTSET;
        //    }

        //    -Roles: Penasihat Akademik
        //    ModelParameterHr _isPenasihatAkademik = LoginDB.DB_MtdPenasihatAkademikWujud(usrPsp.STAF_FK.ToString(), "11");
        //    if (_isPenasihatAkademik.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPenasihatAkademik.ViewField;
        //        ModelUserDTO.ROLE = "PENASIHAT AKADEMIK";
        //        ModelUserDTO.RESULTSET = _isPenasihatAkademik.RESULTSET;
        //    }

        //    -Roles: Penggubal
        //   ModelParameterHr _isPenggubal = LoginDB.DB_MtdPenggubalWujud(usrPsp.STAF_FK.ToString(), "12");
        //    if (_isPenggubal.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPenggubal.ViewField;
        //        ModelUserDTO.ROLE = "PENGGUBAL";
        //        ModelUserDTO.RESULTSET = _isPenggubal.RESULTSET;
        //    }

        //    -Roles: Penilai Instrumen
        //    ModelParameterHr _isPenilaiInstrumen = LoginDB.DB_MtdPenilaiInstrumenWujud(usrPsp.STAF_FK.ToString(), "13");
        //    if (_isPenilaiInstrumen.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isPenilaiInstrumen.ViewField;
        //        ModelUserDTO.ROLE = "PENILAI INSTRUMEN";
        //        ModelUserDTO.RESULTSET = _isPenilaiInstrumen.RESULTSET;
        //    }

        //    -Roles: JK Fakulti
        //    ModelParameterHr _isJKFakulti = LoginDB.DB_MtdJKFakultiWujud(usrPsp.STAF_FK.ToString(), "14");
        //    if (_isJKFakulti.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isJKFakulti.ViewField;
        //        ModelUserDTO.ROLE = "JK FAKULTI";
        //        ModelUserDTO.RESULTSET = _isJKFakulti.RESULTSET;
        //    }


        //    -Roles: Senat
        //   ModelParameterHr _isSenat = LoginDB.DB_MtdSenatWujud(usrPsp.STAF_FK.ToString(), "15");
        //    if (_isSenat.RESULTSET == "2")
        //    {
        //        ModelUserDTO.NAMA_PERANAN = _isSenat.ViewField;
        //        ModelUserDTO.ROLE = "SENAT";
        //        ModelUserDTO.RESULTSET = _isSenat.RESULTSET;
        //    }


        //    return ModelUserDTO;
        //}

        //private static ModelUserDTO mtdClearField(ModelUserDTO usr)
        //{
        //    usr.USERNAME = "";
        //    usr.ROLE = "";
        //    usr.PASSWORD = "";
        //    usr.REALNAME = "";
        //    usr.NO_KP = "";
        //    usr.EMAIL = "";
        //    usr.PSPSTAFFK = "";
        //    usr.PWDCHANGEDATE = null; // DateTime.Parse("");
        //    usr.PWDEXPIREDATE = null; //DateTime.Parse("");
        //    usr.UIDEXPIREDATE = null; //DateTime.Parse("");
        //    usr.COUNTRECORD = "";
        //    usr.FLAG = "";
        //    usr.NEWUSER = "";
        //    usr.HRSTAFFK = "";
        //    usr.RESULTSET = "0";
        //    usr.RESULTTEXT = "";
        //    usr.NAMA_PERANAN = "";
        //    return usr;
        //}

//        internal static ModelUserDTO MtdProsesSimpanNewPassword(ModelUserDTO user)
//        {
//            string _data = "0";
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//            string _user = user.USERNAME;
//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//            user.RESULTSET = _data;
//#pragma warning disable CS8604 // Possible null reference argument.
//            UserIdModel semakUsr = LoginDBLibrary.MtdGetPengguna(user.USERNAME);
//#pragma warning restore CS8604 // Possible null reference argument.
//            if (semakUsr != null)
//            {
//                string _oldPassEnc = EncryptHr.mtdEncryptPassword(PublicConstant.MtdRemoveSpecialCharacter(user.KATALALUAN));
//#pragma warning disable CS8604 // Possible null reference argument.
//                string _newPasswd01 = PublicConstant.MtdRemoveSpecialCharacter(user.NEWPASSWORD01);
//                string _newPasswd02 = PublicConstant.MtdRemoveSpecialCharacter(user.NEWPASSWORD02);
//#pragma warning restore CS8604 // Possible null reference argument.
//                if (_newPasswd01 != _newPasswd02)
//                {
//                    user.RESULTTEXT = "NEW PASSWORD DAN CONFIRMATION NEW PASSWORD TIDAK SAMA";
//                }
//                else if (semakUsr.PASSWORD != _oldPassEnc)
//                {
//                    user.RESULTTEXT = semakUsr.PASSWORD + " ~ CURRENT PASSWORD TIDAK SAMA ~ " + _oldPassEnc;
//                }
//                else
//                {
//                    string _newPasswd = EncryptHr.mtdEncryptPassword(user.NEWPASSWORD01);
//                    if (_user != "")
//                    {
//                        UserIdModel pengguna = new()
//                        {
//                            ID_PENGGUNA = _user,
//                            KATALALUAN = _newPasswd
//                        };
//                        int _hasil = LoginDBLibrary.MtdGetPenggunaSimpan(pengguna);
//                        if (_hasil > 0)
//                        {
//                            user.RESULTTEXT = "REKOD BERJAYA DISIMPAN";
//                            _data = "1";
//                        }
//                        else
//                        {
//                            user.RESULTTEXT = "REKOD TIDAK BERJAYA DISIMPAN";
//                            _data = "2";
//                        }
//                    }
//                }
//            }
//            user.RESULTSET = _data;
//            return user;
//        }

//        internal static ModelUserDTO MtdGetPhotoStudent(ModelUserDTO photo)
//        {
//#pragma warning disable CS8604 // Possible null reference argument.
//            ModelUserDTO _return = LoginDBPelajarUPNM.MtdGetPhotoStudent(photo.SPR_NOKP);
//#pragma warning restore CS8604 // Possible null reference argument.
//            ModelUserDTO _dataReturn = new()
//            {
//                PHOTO = _return.PHOTO,
//                RESULTSET = _return.RESULTSET
//            };
//            return _dataReturn;
//        }

        internal static bool MtdGetReLoginFromLocal()
        {
            bool _return = false;
#pragma warning disable SYSLIB0014 // Type or member is obsolete
            WebClient webClient1 = new();
#pragma warning restore SYSLIB0014 // Type or member is obsolete
            using (var webClient = webClient1)
            {
                var _json = webClient.DownloadString(@"C:/Log/TestingPhase.txt");
                if (_json.ToString() == "true")
                {
                    _return = true;
                }
            }
            return _return;
        }

        //internal static ModelUserDTO MtdGetPhotoStaf(ModelUserDTO photo)
        //{
        //    ModelUserDTO _dataTrans = new()
        //    {
        //        HRSTAFFK = photo.HRSTAFFK
        //    };
        //    ModelUserDTO _return = LoginDB.MtdGetPhotoStaf(_dataTrans);
        //    ModelUserDTO _dataReturn = new()
        //    {
        //        PHOTO = _return.PHOTO,
        //        RESULTSET = _return.RESULTSET
        //    };
        //    return _dataReturn;
        //}

        //Begin:
        // DDL - Jenis APEL

        internal class ListJenisAPEL
        {
            internal List<ModelParameterAPEL> ToList()
            {
                throw new NotImplementedException();
            }
            public ListJenisAPEL()
            {

                    IEnumerable<ParameterAPELModel> _dataLib = LoginDB.DB_ListJenisAPEL().ToList();
                    List<ModelParameterAPEL> _list = new List<ModelParameterAPEL>();
                    foreach (var row in _dataLib)
                    {
                        _list.Add(MtdPindahParameter(row));

                    }
                return;
            }

            
        }

        // DDL - Kategori Pemohon
        //internal static IEnumerable<ModelParameterHr> ListKatPemohon()
        //{
        //    IEnumerable<ParameterAPELModel> _dataLib = SelenggaraDB.DB_ListKatPemohon().ToList();
        //    List<ModelParameterHr> _list = new List<ModelParameterHr>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}
        //DDL - Status Aduan
        //internal static IEnumerable<ModelParameterHr> ListStsAduan()
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.DB_ListStsAduan().ToList();
        //    List<ModelParameterHr> _list = new List<ModelParameterHr>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}
        // DDL - Senarai Jawatan KP
        //internal static IEnumerable<ModelParameterHr> ListJawatanAll(string _kodjwtn)
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.ListJawatanAll(_kodjwtn).ToList();
        //    List<ModelParameterHr> _list = new List<ModelParameterHr>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}

        private static ModelParameterAPEL MtdPindahParameter(ParameterAPELModel row)
        {
            return new ModelParameterAPEL()
            {
                Key = row.Key,
                ViewField = row.ViewField
            };
        }


    }
}

