﻿using Net6HrPublicLibrary.Model;
using Net6HrPublicLibrary.PublicServices.Login;
using Net6HrPublicLibrary.PublicShared;
using APELC.LocalServices.Public;
using APELC.Model;
using APELC.LocalServices.Senarai;
using APELC.LocalShared;


namespace APELC.LocalServices.Selenggara
{
    public class SelenggaraProcess
    {
        readonly static string _encryptCode = SecurityConstants.EncryptCode();

        //internal static CarianAduanMain MtdGetMaklumatAduanPemohon(string _aduanPkEnc)
        //{
        //    CarianAduanMain _data = new();
        //    int _aduanPk = _aduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_aduanPkEnc, _encryptCode)) : 0;
        //    ModelHrPemohonMaklumat Pemohon = AduanDB.DB_MtdGetApelPemohon(_aduanPk);
        //    if (Pemohon != null)
        //    {
        //        if (Pemohon.COMPLAINER_FK != null) // Staff
        //        {
        //            int _stafPk = int.Parse(Pemohon.COMPLAINER_FK);
        //            ModelHrStaffMaklumatPeribadi peribadi = AduanDB.DB_MtdGetMaklumatAsas(_stafPk);
        //            if (peribadi != null)
        //            {
        //                if (peribadi.NO_TEL_BIMBIT != null)
        //                { peribadi.NO_HP = peribadi.NO_TEL_BIMBIT; }
        //                else { peribadi.NO_HP = peribadi.NO_TEL_PEJABAT; }

        //                _data.stafPeribadi = peribadi;
        //            }
        //        }
        //        if (Pemohon.COMPLAINER_NO_KP != null) // Pelajar
        //        {
        //            ModelMaklumatPeribadiPelajar pelajar = AduanDB.MtdGetDataPelajarByNokp(Pemohon.COMPLAINER_NO_KP);
        //            if (pelajar != null)
        //            {
        //                _data.pelajarInfo = pelajar;
        //            }
        //        }

        //        Pemohon.LABEL_STATUS = LabelStatusDesc(Pemohon.STATUS_FK);
        //        string[] n = Pemohon.LABEL_STATUS.Split('~');
        //        Pemohon.STATUS_DESC = n[1];

        //        Pemohon = MtdHrPemohonPindahFromLibrary(Pemohon);
        //        _data.Pemohon = Pemohon;
        //    }
        //    return _data;
        //}

        //private static ModelHrPemohonMaklumat MtdHrPemohonPindahFromLibrary(ModelHrPemohonMaklumat _data)
        //{
        //    string _aduanPkEnc = EncryptHr.NewEncrypt(_data.ADUAN_PK.ToString(), _encryptCode);
        //    string _AduanPkEnc = EncryptHr.NewEncrypt(_data.Aduan_PK.ToString(), _encryptCode);
        //    string _complainpkEnc = "";
        //    string _complainStudentEnc = "";
        //    if (_data.COMPLAINER_FK != null)
        //    {
        //        _complainpkEnc = EncryptHr.NewEncrypt(_data.COMPLAINER_FK.ToString(), _encryptCode);
        //    }
        //    if (_data.COMPLAINER_NO_KP != null)
        //    {
        //        _complainStudentEnc = EncryptHr.NewEncrypt(_data.COMPLAINER_NO_KP.ToString(), _encryptCode);
        //    }

        //    return new ModelHrPemohonMaklumat()
        //    {
        //        ADUAN_PK = _data.ADUAN_PK,
        //        MOHON_PK_ENC = _aduanPkEnc,
        //        Aduan_PK = _data.Aduan_PK,
        //        MOHON_PK_ENC = _AduanPkEnc,
        //        MOHON_NO = _data.MOHON_NO,
        //        TKH_ADUAN = _data.TKH_ADUAN,
        //        DATE_TKH_ADUAN = _data.DATE_TKH_ADUAN,
        //        MASA_TKH_ADUAN = _data.MASA_TKH_ADUAN,
        //        NAMA_PEMOHON = _data.NAMA_PEMOHON,
        //        NO_ID = _data.NO_ID,
        //        NO_KP = _data.NO_KP,
        //        STATUS_DESC = _data.STATUS_DESC,
        //        LABEL_STATUS = _data.LABEL_STATUS,
        //        COMPLAINER_FK_ENC = _complainpkEnc,
        //        COMPLAINER_NO_KP_ENC = _complainStudentEnc,
        //        KATEGORI_KES_DESC = _data.KATEGORI_KES_DESC,
        //    };
        //}

        // Get Senarai Staf Penyiasat
        //internal static List<ModelHrStafPenyiasat> MtdGetStafPenyiasatList(string _AduanPkEnc)
        //{
        //    int _AduanPk = _AduanPkEnc != null ? NumberHelper.ToIntiger(EncryptHr.NewDecrypt(_AduanPkEnc, _encryptCode)) : 0;
        //    IEnumerable<ModelHrStafPenyiasat> _dataList = SelenggaraDB.DB_MtdGetStafPenyiasatList(_AduanPk);
        //    List<ModelHrStafPenyiasat> _list = new();

        //    foreach (var row in _dataList)
        //    {
        //        if (row.CNAMA != null)
        //        {
        //            string[] _staf = row.CNAMA.Split('~');
        //            row.NAMA = _staf[0];
        //            row.NO_PEKERJA = _staf[1];
        //            row.NO_KP_BARU = _staf[2];
        //            row.JAWATAN_DESC = _staf[3];
        //            row.KAMPUS_DESC = _staf[4];
        //            row.STATUS_AKTIF = _staf[5];
        //        }
        //        row.LABEL_STATUS = LabelStatusAktifDesc(row.STATUS_AKTIF);
        //        string[] n = row.LABEL_STATUS.Split('~');
        //        row.LABEL_STATUS = n[0];
        //        row.STATUSAKTIF_DESC = n[1];

        //        _list.Add(MtdHrStaffPindahFromLibrary(row));
        //    }
        //    return _list;
        //}

        //private static ModelHrStafPenyiasat MtdHrStaffPindahFromLibrary(ModelHrStafPenyiasat _data)
        //{
        //    return new ModelHrStafPenyiasat()
        //    {

        //        ADUAN_PK = _data.ADUAN_PK,
        //        STAF_PP_FK = _data.STAF_PP_FK,
        //        NAMA = _data.NAMA,
        //        NO_PEKERJA = _data.NO_PEKERJA,
        //        NO_KP_BARU = _data.NO_KP_BARU,
        //        JAWATAN_DESC = _data.JAWATAN_DESC,
        //        KAMPUS_DESC = _data.KAMPUS_DESC,
        //        STATUS_AKTIF = _data.STATUS_AKTIF,
        //        KOD_PRNN_PNYST = _data.KOD_PRNN_PNYST,
        //        LABEL_STATUS = _data.LABEL_STATUS,
        //        STATUSAKTIF_DESC = _data.STATUSAKTIF_DESC,
        //        DATE_TKH_PNYST = _data.DATE_TKH_PNYST,
        //        MASA_TKH_PNYST = _data.MASA_TKH_PNYST
        //    };
        //}

        // Begin: Action Guide
        // dari Value Status FK - HR_BK_TINDAKAN
        internal static string LabelStatusDesc(string? _statuscode)
        {
            string status = "";
            try
            {
                if (_statuscode == "378")
                {
                    status = " badge-warning~Aduan Lanjut";
                } // Aduan Lanjut
                else if (_statuscode == "380")
                {
                    status = " badge-success~Selesai";
                } // Selesai
                else if (_statuscode == "381")
                {
                    status = " badge-primary~Batal";
                } // Batal
                else { status = " badge-secondary~Tiada"; } // TIADA
            }
            catch (Exception e)
            {
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Info(DateTime.Now + " Error: SenaraiProcess. LabelStatus. More info: " + e);
            }
            return status;
        }

        // dari Value Status AKTIF - HR_STAF
        internal static string LabelStatusAktifDesc(string? _statuscode)
        {
            string status = "";
            try
            {
                if (_statuscode == "Y") // Aktif
                {
                    status = " btn-success~Aktif";
                }
                else if (_statuscode == "N") // Tidak Aktif
                {
                    status = " btn-danger~Tidak Aktif";
                }
                else { status = " btn-secondary~Tiada"; } // TIADA
            }
            catch (Exception e)
            {
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Info(DateTime.Now + " Error: AduanProcess. LabelStatusAktifDesc. More info: " + e);
            }
            return status;
        }

        // End: Action Guide

        // Begin:
        // DDL - Kategori Aduan
        //internal static IEnumerable<ModelParameterAPEL> ListKatAduan()
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.DB_ListKatAduan().ToList();
        //    List<ModelParameterAPEL> _list = new List<ModelParameterAPEL>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}
        //// DDL - Kategori Pemohon
        //internal static IEnumerable<ModelParameterAPEL> ListKatPemohon()
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.DB_ListKatPemohon().ToList();
        //    List<ModelParameterAPEL> _list = new List<ModelParameterAPEL>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}
        // DDL - Status Aduan
        //internal static IEnumerable<ModelParameterAPEL> ListStsAduan()
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.DB_ListStsAduan().ToList();
        //    List<ModelParameterAPEL> _list = new List<ModelParameterAPEL>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}
        //// DDL - Senarai Jawatan KP
        //internal static IEnumerable<ModelParameterAPEL> ListJawatanAll(string _kodjwtn)
        //{
        //    IEnumerable<ParameterHrModel> _dataLib = SelenggaraDB.ListJawatanAll(_kodjwtn).ToList();
        //    List<ModelParameterAPEL> _list = new List<ModelParameterAPEL>();
        //    foreach (var row in _dataLib)
        //    {
        //        _list.Add(MtdPindahParameter(row));
        //    }
        //    return _list;
        //}


        private static ModelParameterAPEL MtdPindahParameter(ParameterHrModel row)
        {
            return new ModelParameterAPEL()
            {
                Key = row.Key,
                ViewField = row.ViewField
            };
        }

        // End: DDL
    }
}
