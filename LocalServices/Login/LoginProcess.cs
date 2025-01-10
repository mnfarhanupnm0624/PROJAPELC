using Dapper;
using APELC.LocalShared;
using APELC.LocalServices;
using APELC.Model;
using APELC.Helper;
using System.Net;
using System.Collections.Specialized;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Data.Odbc;
using System.Text;
using MySql.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.CodeAnalysis.Editing;
using Net6HrPublicLibrary.PublicServices.Login;
using Net6HrPublicLibrary.PublicShared;
using Net6HrPublicLibrary.Model;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using APELC.LocalServices.Selenggara;

namespace APELC.LocalServices.Login
{
   // public static System.Collections.Generic.List<TSource> ToList<TSource>(this System.Collections.Generic.IEnumerable<TSource> source);
    public class LoginProcess
    {
        
        internal static bool MtdTestConnection()
        {
            bool _return = true;
            try
            {
                //using (var dbConn = new MySql.Data.MySqlClient.MySqlConnection(ConnMySQLHrUpnmDs))
                //{
                //    int _xx = dbConn.QueryFirstOrDefault<int>("SELECT 7 FROM DUAL ");
                //    if (_xx > 0)
                //    {
                //        _return = true;
                //    }
                //    else { _return = false; }
                //}
            }
            catch (Exception e)
            {
                _return = false;
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Info("MtdTestConnection e ~ " + e);
            }
            return _return;

        }
        //DDL Process
        //internal static string MtdGetDropdownListkatPenggunaDaftarAkaun(string _katpenggunadaftarakaun, string ConnMySQLUpnm)
        //{
        // string _functionListskatpenggunadaftarakaun = LoginDB.DB_MtdGetDropdownListkatPenggunaDaftarAkaun(_katpenggunadaftarakaun, ConnMySQLUpnm);
        // string _functionListkatpenggunadaftarakaun = _functionListskatpenggunadaftarakaun;
        // List<string> _newList = _functionListskatpenggunadaftarakaun.Split(',').ToList();
        // foreach (var _item in _functionListskatpenggunadaftarakaun)
        //{
        // _newList.Add(_item.ToString());
        //}
        //_functionListskatpenggunadaftarakaun = _newList.ToString();
        // return _functionListkatpenggunadaftarakaun;
        // }

        //        internal static string MtdGetDropdownListjenisApel(string _jenisapel, string ConnMySQLUpnm)
        //        {
        //            string _functionListsjenisapel = LoginDB.DB_MtdGetDropdownListjenisApel(_jenisapel, ConnMySQLUpnm);
        //            string _functionListjenisapel = _functionListsjenisapel;
        //            List<string> _newList = _functionListsjenisapel.Split(',').ToList();
        //            foreach (var _item in _functionListsjenisapel)
        //            {
        //                _newList.Add(_item.ToString());
        //            }
        //            _functionListsjenisapel = _newList.ToString();
        //#pragma warning disable CS8603 // Possible null reference return.
        //            return _functionListjenisapel;
        //#pragma warning restore CS8603 // Possible null reference return.
        //        }

        // internal class MtdGetDropdownListjenisApel
        // {
        //   public MtdGetDropdownListjenisApel(IEnumerable<ModelParameterAPELC> _jenisapel)
        //  {
        //  IEnumerable<ModelParameterAPELC> _functionListsjenisapel = (IEnumerable<ModelParameterAPELC>)LoginDB.DB_MtdGetDropdownListjenisApel(_jenisapel).ToList();
        // List<ModelParameterAPELC> _list = new List<ModelParameterAPELC>();
        //  foreach (var row in _functionListsjenisapel)
        //  {
        //      _list.Add(MtdPindahParameter(row));
        //  }
        //  return _list;
        // }
        // }

        //internal static string MtdGetDropdownListjenisApel(string _jenisapel, string ConnMySQLUpnm)
        //{
        // string _functionListsjenisapel =LoginDB.DB_MtdGetDropdownListjenisApel(_jenisapel, ConnMySQLUpnm);
        // string _functionListjenisapel = _functionListsjenisapel;
        // List<string> _newList = _functionListsjenisapel.Split(',').ToList();
        // foreach (var row in _functionListsjenisapel)
        // {
        // _newList.Add(MtdPindahParameter(row));
        //}
        //  return _list;
        //}

        internal static string MtdGetDropdownListjenisApel(string _jenisapel, string ConnMySQLUpnm)
        {
         string _functionListsjenisapel =LoginDB.DB_MtdGetDropdownListjenisApel(_jenisapel, ConnMySQLUpnm);
         string _functionListjenisapel = _functionListsjenisapel;
         List<string> _newList = _functionListsjenisapel.Split(',').ToList();
         foreach (var _item in _functionListsjenisapel)
         {
                _newList.Add(_item.ToString());
          }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            _functionListsjenisapel = _newList.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return _functionListjenisapel;
        }



        //internal static string MtdGetDropdownListkatPerananSuperuserDaftarAkaun(string _katperanansuperuserdaftarakaun, string ConnMySQLUpnm)
        //{
            //string _functionListskatperanansuperuserdaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananSuperuserDaftarAkaun(_katperanansuperuserdaftarakaun, ConnMySQLUpnm);
           // string _functionListkatperanansuperuserdaftarakaun = _functionListskatperanansuperuserdaftarakaun;
           // List<string> _newList = _functionListskatperanansuperuserdaftarakaun.Split(',').ToList();
            //foreach (var _item in _functionListskatperanansuperuserdaftarakaun)
            //{
               // _newList.Add(_item.ToString());
            //}
            //_functionListskatperanansuperuserdaftarakaun = _newList.ToString();
            //return _functionListkatperanansuperuserdaftarakaun;
        //}


//        internal static string MtdGetDropdownListkatPerananPelajarDaftarAkaun(string _katperananpelajardaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananpelajardaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananPelajarDaftarAkaun(_katperananpelajardaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananpelajardaftarakaun = _functionListskatperananpelajardaftarakaun;
//            List<string> _newList = _functionListskatperananpelajardaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananpelajardaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananpelajardaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananpelajardaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }

//        internal static string MtdGetDropdownListkatPerananLuarUPNMDaftarAkaun(string _katperananluarupnmdaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananluarupnmdaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananLuarUPNMDaftarAkaun(_katperananluarupnmdaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananluarupnmdaftarakaun = _functionListskatperananluarupnmdaftarakaun;
//            List<string> _newList = _functionListskatperananluarupnmdaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananluarupnmdaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananluarupnmdaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananluarupnmdaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }

//        internal static string MtdGetDropdownListkatPerananUrusetiaAPELDaftarAkaun(string _katperananurusetiaapeldaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananurusetiaapeldaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananUrusetiaAPELDaftarAkaun(_katperananurusetiaapeldaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananurusetiaapeldaftarakaun = _functionListskatperananurusetiaapeldaftarakaun;
//            List<string> _newList = _functionListskatperananurusetiaapeldaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananurusetiaapeldaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananurusetiaapeldaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananurusetiaapeldaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }

//        internal static string MtdGetDropdownListkatPerananUrusetiaFakultiDaftarAkaun(string _katperananurusetiafakultidaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananurusetiafakultidaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananUrusetiaFakultiDaftarAkaun(_katperananurusetiafakultidaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananururusetiafakultidaftarakaun = _functionListskatperananurusetiafakultidaftarakaun;
//            List<string> _newList = _functionListskatperananurusetiafakultidaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananurusetiafakultidaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananurusetiafakultidaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananururusetiafakultidaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }

//        internal static string MtdGetDropdownListkatPerananBendahariDaftarAkaun(string _katperananbendaharidaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananbendaharidaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananBendahariDaftarAkaun(_katperananbendaharidaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananbendaharidaftarakaun = _functionListskatperananbendaharidaftarakaun;
//            List<string> _newList = _functionListskatperananbendaharidaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananbendaharidaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananbendaharidaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananbendaharidaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }

//        internal static string MtdGetDropdownListkatPerananPenasihatAkadDaftarAkaun(string _katperananpenasihatakaddaftarakaun,string ConnMySQLUpnm)
//        {
//            string _functionListskatperananpenasihatakaddaftarakaun = LoginDB.DB_MtdGetDropdownListkatPerananPenasihatAkadDaftarAkaun(_katperananpenasihatakaddaftarakaun, ConnMySQLUpnm);
//            string _functionListkatperananpenasihatakaddaftarakaun = _functionListskatperananpenasihatakaddaftarakaun;
//            List<string> _newList = _functionListskatperananpenasihatakaddaftarakaun.Split(',').ToList();
//            foreach (var _item in _functionListskatperananpenasihatakaddaftarakaun)
//            {
//                _newList.Add(_item.ToString());
//            }
//            _functionListskatperananpenasihatakaddaftarakaun = _newList.ToString();
//#pragma warning disable CS8603 // Possible null reference return.
//            return _functionListkatperananpenasihatakaddaftarakaun;
//#pragma warning restore CS8603 // Possible null reference return.
//        }
        //        internal static PenggunaApelCMain MtdSemakPengguna(string _usernameIn, string _passwordIn)
        //        {
        //            string _username = _usernameIn.ToString();
        //            string _password = _passwordIn.ToString();

        //            PenggunaApelCMain _userDTO = new();
        //            //_userDTO = mtdClearField(_userDTO);
        //            string _msg = "";
        //            if (_username == null) _msg = "Masukkan ID Pengguna";
        //            if (_msg == "")
        //            {
        //#pragma warning disable CS8604 // Possible null reference argument.
        //                PenggunaApelCMain semakUsrHr = (PenggunaApelCMain)LoginDB.DB_PenggunaApelCUPNM(_username, _password);
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
        //                    string _compPasswd = _userDTO.KATA_LALUAN_PENGGUNA;
        //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //                    string passwdEnc = EncryptLocal.mtdEncryptPassword(LocalConstant.MtdRemoveSpecialCharacter(_password));

        //                    if (passwdEnc == "6D953275BB91AD17AB216BB92BA05E481D8E7C80") { }
        //                    else if (_compPasswd != passwdEnc)
        //                    {
        //                        _userDTO.RESULTSET_TEXT = "Katalaluan Tidak Sama. ";
        //                        _userDTO.RESULTSET = "0";
        //                    }
        //                    ////// SEMAK UNTUK TARIKH-TARIKH YANG BERKAITAN LUPUT ID / LUPUT KATA LALUAN

        //                }
        //                else
        //                {
        //                    _userDTO.RESULTSET_TEXT = _msg;
        //                    _userDTO.RESULTSET = "0";
        //                }
        //            }
        //            else
        //            {
        //                _userDTO.RESULTSET_TEXT = _msg;
        //                _userDTO.RESULTSET = "0";
        //            }
        //            return _userDTO;
        //        }

        //        internal static SessionModel MtdSemakUserFromLoginFromJWT(string _userId, string _pwd)
        //        {
        //            SessionModel _userDTO = new SessionModel();
        //            _userDTO = mtdClearField(_userDTO);
        //            SessionModel semakUsrHr = (SessionModel)LoginDB.DB_PenggunaApelCUPNM(_userId, _pwd);
        //            if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
        //            {
        //                _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
        //            }
        //            else
        //            {
        //                SessionModel semakUsrHr02 = LoginDB.DB_PenggunaApelCUPNM(_userId, _pwd);
        //                if (semakUsrHr02 != null)
        //                {
        //                    _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr02, _userDTO);
        //                }
        //                else
        //                {
        //                    _userDTO.RESULTSET = "0";
        //                    _userDTO.RESULTSET_TEXT = "UserId tiada dalam Table Pengguna APELC";
        //                }
        //            }
        //            return _userDTO;
        //        }

        internal static SessionModel MtdGetMaklumatPenggunaSession(SessionModel semakPengguna)
        {
            SessionModel _pengguna = new()
            {
                PENGGUNA_UPNM_PK_ENC = semakPengguna.PENGGUNA_UPNM_PK_ENC,
                ID_PENGGUNA = semakPengguna.ID_PENGGUNA,
                KATA_LALUAN_PENGGUNA = semakPengguna.KATA_LALUAN_PENGGUNA,
                JENIS_MODUL_PENGGUNA_UPNM_FK = semakPengguna.JENIS_MODUL_PENGGUNA_UPNM_FK,
                BIL_GAGAL_LOGIN = semakPengguna.BIL_GAGAL_LOGIN,
                SESSION_TIMEOUT = semakPengguna.SESSION_TIMEOUT,
                RESULTSET = semakPengguna.RESULTSET,
            };
            return _pengguna;
        }

        //internal static ModelUserDTO MtdSemakUserFromAPELCUPNMDecrypt(string _key)
        //{
        //    var log = NLog.LogManager.GetCurrentClassLogger();
        //    log.Info("MtdSemakUserFromMyUtmDecrypt _key ~ " + _key);

        //    ModelUserDTO _userDTO = new();
        //    _userDTO = mtdClearField(_userDTO);
        //    string _userGet = LoginDB.DB_MtdSemakUserFromMyUPNMDecrypt(_key);
        //    string[] _text = LocalConstant.SplitAyat(_userGet, "|");
        //    string _userId = _text[0];
        //    string _pwd = _text[1];

        //    //log.Info("MtdSemakUserFromMyUtmDecrypt _userId ~ " + _userId);
        //    //log.Info("MtdSemakUserFromMyUtmDecrypt _noPekerja ~ " + _noPekerja);
        //    UserIdModel semakUsrHr = LoginDB.DB_PenggunaApelCUPNM(_userId,_pwd);
        //    if (semakUsrHr != null && semakUsrHr.RESULTSET == "2")
        //    {
        //        _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr, _userDTO);
        //    }
        //    else
        //    {
        //        UserIdModel semakUsrHr02 = LoginDB.MtdGetApelCPenggunaByNoPekerja(_noPekerja);
        //        if (semakUsrHr02 != null)
        //        {
        //            _userDTO = MtdPindahPengguna2UserDTO(semakUsrHr02, _userDTO);
        //        }
        //        else
        //        {
        //            _userDTO.RESULTSET = "0";
        //            _userDTO.RESULTTEXT = "UserId tiada dalam Table Pengguna APELC";
        //        }
        //    }
        //    _userDTO.SPR_NOKP = _noPekerja;
        //    //log.Info("MtdSemakUserFromMyUtmDecrypt SPR_NOKP ~ " + _userDTO.SPR_NOKP);
        //    return _userDTO;
        //}

        

        //internal IEnumerable<string> MtdGetFunctions(string _id_pengguna, string _kata_laluan)
        //{
        //    return LoginDB.DB_PenggunaApelCUPNM(_id_pengguna, _kata_laluan);
        //}

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
        //                UserIdModel semakUsrHr = LoginDB.MtdGetApelCPengguna(_username);
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
        //    return LoginDB.MtdGetCheckApprovalStructure(_stafFk, _kod);
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
        //                _data = LoginDB.MtdDecryptFromTableAcad(_data);
        //#pragma warning disable CS8602 // Dereference of a possibly null reference.
        //                string[] splitKey = _data.ARRAY_FROM_ENC.Split(',');
        //#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //                string _noPekerja = splitKey[0];
        //                string _stafFk = splitKey[1];
        //                string _ModelUserId = splitKey[2];

        //                UserIdModel semakUsrHr = LoginDB.MtdGetApelCPengguna(_ModelUserId);
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

        internal static IEnumerable<string> MtdGetFunctions(string _username)
        {
            return (IEnumerable<string>)LoginDBStafUPNM.MtdGetApelCPenggunaStaf(_username);
        }

        private static PenggunaApelCMain MtdPindahPengguna2UserDTO(PenggunaApelCMain usrPsp, PenggunaApelCMain UserDTO)
        {
            
            UserDTO.ID_PENGGUNA = usrPsp.ID_PENGGUNA;
            //UserDTO.ROLE = usrPsp.KOD_PERANAN;
            UserDTO.KATA_LALUAN_PENGGUNA = usrPsp.KATA_LALUAN_PENGGUNA;
            //UserDTO.REALNAME = usrPsp.NAMA;
            //UserDTO.NO_KP = usrPsp.KAD_PENGENALAN;
            //UserDTO.EMAIL = usrPsp.EMAIL;
            UserDTO.TKH_KEMASKINI_PENGGUNA_UPNM = usrPsp.TKH_KEMASKINI_PENGGUNA_UPNM;
            UserDTO.BIL_GAGAL_LOGIN = usrPsp.BIL_GAGAL_LOGIN.ToString();
            UserDTO.STATUS_AKTIF_PENGGUNA_UPNM_FK = usrPsp.STATUS_AKTIF_PENGGUNA_UPNM_FK;
            //UserDTO.NEWUSER = usrPsp.PENGGUNA_BARU_FLAG;
            //UserDTO.HRSTAFFK = usrPsp.STAF_FK.ToString();
            UserDTO.RESULTSET = usrPsp.RESULTSET;
            //UserDTO.NO_PEKERJA = usrPsp.NO_PEKERJA;
            //UserDTO.KATALALUAN = usrPsp.KATALALUAN;
            //UserDTO.KOD_PTJ = usrPsp.KOD_PTJ;
            UserDTO.RESULTSET_TEXT = "";

            //-Roles: Super User
            //ModelParameterAPELC _isSuperUser = LoginDB.DB_MtdSuperUserWujud(usrPsp.STAF_FK.ToString(), "4");
            //if (_isSuperUser.RESULTSET == "2")
            //{
            //    PenggunaApelCMain.JENIS_PERANAN_FK = _isSuperUser.NAMA_PARAMETER;
            //    PenggunaApelCMain.JENIS_PERANAN_FK = "PENGGUNA SUPER";
            //    PenggunaApelCMain.RESULTSET = _isSuperUser.RESULTSET;
            //}

            //-Roles: Pentadbir APELC
            //ModelParameterAPELC _isPentadbirAPELC = LoginDB.DB_MtdPentadbirAPELCWujud(usrPsp.STAF_FK.ToString(), "5");
            //if (_isPentadbirAPELC.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPentadbirAPELC.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PENTADBIR_APELC";
            //    ModelUserDTO.RESULTSET = _isPentadbirAPELC.RESULTSET;
            //}

            //-Roles: Bendahari
            //ModelParameterAPELC _isBendahari = LoginDB.DB_MtdBendahariWujud(usrPsp.STAF_FK.ToString(), "6");
            //if (_isBendahari.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isBendahari.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "BENDAHARI";
            //    ModelUserDTO.RESULTSET = _isBendahari.RESULTSET;
            //}

            //-Roles: Pemohon
            //ModelParameterAPELC _isPemohon = LoginDB.DB_MtdPemohonWujud(usrPsp.STAF_FK.ToString(), "7");
            //if (_isPemohon.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPemohon.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "Peranan";
            //    ModelUserDTO.RESULTSET = _isPemohon.RESULTSET;
            //}

            //-Roles: Pengawas Ujian Cbrn
            //ModelParameterAPELC _isPengawasUjianCbrn = LoginDB.DB_MtdPengawasUjianCbrnWujud(usrPsp.STAF_FK.ToString(), "8");
            //if (_isPengawasUjianCbrn.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPengawasUjianCbrn.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PENGAWAS UJIAN CABARAN";
            //    ModelUserDTO.RESULTSET = _isPengawasUjianCbrn.RESULTSET;
            //}

            //-Roles: Panel Penilai
            //ModelParameterAPELC _isPanelPenilai = LoginDB.DB_MtdPanelPenilaiWujud(usrPsp.STAF_FK.ToString(), "9");
            //if (_isPanelPenilai.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPanelPenilai.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PANEL PENILAI";
            //    ModelUserDTO.RESULTSET = _isPanelPenilai.RESULTSET;
            //}

            //-Roles: Panel Moderator
            //ModelParameterAPELC _isModerator = LoginDB.DB_MtdModeratorWujud(usrPsp.STAF_FK.ToString(), "10");
            //if (_isModerator.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isModerator.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "MODERATOR";
            //    ModelUserDTO.RESULTSET = _isModerator.RESULTSET;
            //}

            //-Roles: Penasihat Akademik
            //ModelParameterAPELC _isPenasihatAkademik = LoginDB.DB_MtdPenasihatAkademikWujud(usrPsp.STAF_FK.ToString(), "11");
            //if (_isPenasihatAkademik.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPenasihatAkademik.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PENASIHAT AKADEMIK";
            //    ModelUserDTO.RESULTSET = _isPenasihatAkademik.RESULTSET;
            //}

            //-Roles: Penggubal Set Ujian Cabaran
            //ModelParameterAPELC _isPenggubal = LoginDB.DB_MtdPenggubalWujud(usrPsp.STAF_FK.ToString(), "12");
            //if (_isPenggubal.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPenggubal.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PENGGUBAL";
            //    ModelUserDTO.RESULTSET = _isPenggubal.RESULTSET;
            //}

            //-Roles: Penilai Instrumen
            //ModelParameterAPELC _isPenilaiInstrumen = LoginDB.DB_MtdPenilaiInstrumenWujud(usrPsp.STAF_FK.ToString(), "13");
            //if (_isPenilaiInstrumen.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isPenilaiInstrumen.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "PENILAI INSTRUMEN";
            //    ModelUserDTO.RESULTSET = _isPenilaiInstrumen.RESULTSET;
            //}

            //-Roles: JK Fakulti
            //ModelParameterAPELC _isJKFakulti = LoginDB.DB_MtdJKFakultiWujud(usrPsp.STAF_FK.ToString(), "14");
            //if (_isJKFakulti.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isJKFakulti.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "JK FAKULTI";
            //    ModelUserDTO.RESULTSET = _isJKFakulti.RESULTSET;
            //}


            //-Roles: Senat
            //ModelParameterAPELC _isSenat = LoginDB.DB_MtdSenatWujud(usrPsp.STAF_FK.ToString(), "15");
            //if (_isSenat.RESULTSET == "2")
            //{
            //    ModelUserDTO.NAMA_PERANAN = _isSenat.NAMA_PARAMETER;
            //    ModelUserDTO.ROLE = "SENAT";
            //    ModelUserDTO.RESULTSET = _isSenat.RESULTSET;
            //}


            return UserDTO;
        }

        private static SessionModel mtdClearField(SessionModel usr)
        {
            usr.ID_PENGGUNA = "";
            //usr.ROLE = "";
            usr.KATA_LALUAN_PENGGUNA = "";
            //usr.REALNAME = "";
            //usr.NO_KP = "";
            //usr.EMAIL = "";
            //usr.PSPSTAFFK = "";
            /*usr.PWDCHANGEDATE = null;*/ // DateTime.Parse("");
            /*usr.PWDEXPIREDATE = null;*/ //DateTime.Parse("");
            /*usr.UIDEXPIREDATE = null;*/ //DateTime.Parse("");
            usr.BIL_GAGAL_LOGIN = "";
            usr.FLAG = "";
            //usr.NEWUSER = "";
            //usr.HRSTAFFK = "";
            usr.RESULTSET = "0";
            usr.RESULTSET_TEXT = "";
            //usr.NAMA_PERANAN = "";
            return usr;
        }

//        internal static PenggunaApelCMain MtdProsesSimpanNewPassword(PenggunaApelCMain user)
//        {
//            string _data = "0";
//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//            string _user = user.ID_PENGGUNA;
//            string _password = user.KATA_LALUAN_PENGGUNA;
//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//            user.RESULTSET = _data;
//#pragma warning disable CS8604 // Possible null reference argument.
//            PenggunaApelCMain semakUsr = (PenggunaApelCMain)LoginDB.DB_PenggunaApelCUPNM(user.ID_PENGGUNA, user.KATA_LALUAN_PENGGUNA);
//#pragma warning restore CS8604 // Possible null reference argument.
//            if (semakUsr != null)
//            {
//                string _oldPassEnc = EncryptLocal.mtdEncryptPassword(LocalConstant.MtdRemoveSpecialCharacter(user.KATA_LALUAN_PENGGUNA));
//#pragma warning disable CS8604 // Possible null reference argument.
//                string _newPasswd01 = LocalConstant.MtdRemoveSpecialCharacter(user.KATA_LALUAN_PENGGUNA_NEW_PWD);
//                string _newPasswd02 = LocalConstant.MtdRemoveSpecialCharacter(user.KATA_LALUAN_PENGGUNA_CONFIRM_PWD);
//#pragma warning restore CS8604 // Possible null reference argument.
//                if (_newPasswd01 != _newPasswd02)
//                {
//                    user.RESULTSET_TEXT = "KATA LALUAN BARU DAN PENGESAHAN KATA LALUAN BARU TIDAK SAMA";
//                }
//                else if (semakUsr.KATA_LALUAN_PENGGUNA != _oldPassEnc)
//                {
//                    user.RESULTSET_TEXT = semakUsr.KATA_LALUAN_PENGGUNA + " ~ KATA LALUAN SEMASA TIDAK SAMA ~ " + _oldPassEnc;
//                }
//                else
//                {
//                    string _newPasswd = EncryptLocal.mtdEncryptPassword(user.KATA_LALUAN_PENGGUNA_NEW_PWD);
//                    if (_user != "")
//                    {
//                        PenggunaApelCMain pengguna = new()
//                        {
//                            ID_PENGGUNA = _user,
//                            KATA_LALUAN_PENGGUNA = _newPasswd
//                        };
//                        int _hasil = LoginDB.DB_MtdGetPenggunaSimpan(pengguna);
//                        if (_hasil > 0)
//                        {
//                            user.RESULTSET_TEXT = "REKOD BERJAYA DISIMPAN";
//                            _data = "1";
//                        }
//                        else
//                        {
//                            user.RESULTSET_TEXT = "REKOD TIDAK BERJAYA DISIMPAN";
//                            _data = "2";
//                        }
//                    }
//                }
//            }
//            user.RESULTSET = _data;
//            return user;
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

        //private static ModelParameterAPELC MtdPindahParameter(ModelParameterAPELC row)
        //{

            //return new ModelParameterAPELC()
            //{
               // KOD = row.KOD,
                //NAMA_PARAMETER = row.NAMA_PARAMETER
            //};
        //}

        
    }
}

