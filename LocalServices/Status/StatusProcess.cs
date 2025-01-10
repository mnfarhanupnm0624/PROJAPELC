using APELC.LocalServices.ApelC;
using APELC.LocalShared;
using APELC.Model;
using System.Xml.Linq;

namespace APELC.LocalServices.Status
{
    public class StatusProcess
    {
        readonly static string _encryptCode = SecurityConstantsLocal.EncryptCode();

        //public static string[] SemakSemuaMenu(string _aduanPkEnc, string _siasatanPkEnc)
        //{
        //    string[] str = new string[13];
        //    str[0] = str[1] = str[2] = str[3] = str[4] = str[5] = str[6] = str[7] = str[8] =
        //    str[9] = str[10] = str[11] = str[12] = "text-secondary";

        //    if (_aduanPkEnc != null)
        //    {
        //        try
        //        {
        //            CarianApelCMain _data = SiasatanProcess.MtdGetMaklumatAduanPenerima(_aduanPkEnc);
        //            if (_data != null)
        //            {
        //                str[0] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess ButiranAduan e ~ " + e);
        //        }
        //        //try
        //        //{
        //        //    CarianSiasatan _data2 = SiasatanProcess.MtdGetLaporanPolis(_aduanPkEnc, _siasatanPkEnc);
        //        //    if (_data2 != null)
        //        //    {
        //        //        if (_data2.info.NO_LPRN_POLIS != null)
        //        //        {
        //        //            str[1] = "text-success";
        //        //        }
        //        //    }
        //        //}
        //        //catch (Exception e)
        //        //{
        //        //    var log = NLog.LogManager.GetCurrentClassLogger();
        //        //    log.Info("SemakSemuaMenu StatusProcess LaporanPolis e ~ " + e);
        //        //}
        //        try
        //        {
        //            CarianSiasatan _data3 = SiasatanProcess.MtdGetInitialEvent(_siasatanPkEnc);
        //            if (_data3 != null)
        //            {
        //                if (_data3._eventInfo.TAJUK_RNGKSN != null && _data3._eventInfo.AKBT_KJDN != null && _data3._eventInfo.RMSN_AWAL != null)
        //                {
        //                    str[2] = "text-success";
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Ringkasan Kejadian e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianSiasatan _data4 = new();
        //            _data4.ListCarianStaf = SiasatanProcess.MtdGetDiariPenyiasatList(_siasatanPkEnc);
        //            if (_data4.ListCarianStaf.Count() > 0)
        //            {
        //                str[3] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Diari Penyiasat e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianRakaman _data5 = new();
        //            List<ModelHrRakaman> _newList = new();
        //            _newList = SiasatanProcess.MtdGetPrckpnList(_siasatanPkEnc, "", "1014"); // KodBorangRakaman, KumpulanFk = 161 (smuparameter) 
        //            _data5.ListRkmn = _newList.ToList();
        //            if (_data5.ListRkmn.Count() > 0)
        //            {
        //                str[4] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Borang Rakaman Peranan/Saksi e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianLampiran _data6 = new();
        //            List<ModelHrLampiran> _newList = new();
        //            _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1014"); // KodBorangRakaman, KumpulanFk = 161 (smuparameter) 
        //            _data6.ListLampiran = _newList.ToList();
        //            if (_data6.ListLampiran.Count() > 0)
        //            {
        //                str[5] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Lampiran Rakaman Peranan/Saksi e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianLampiran _data7 = new();
        //            List<ModelHrLampiran> _newList = new();
        //            _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1015"); // KodBorangRakaman, KumpulanFk = 161 (smuparameter) 
        //            _data7.ListLampiran = _newList.ToList();
        //            if (_data7.ListLampiran.Count() > 0)
        //            {
        //                str[6] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Lampiran OKT e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianRakaman _data8 = new();
        //            List<ModelHrRakaman> _newList = new();
        //            _newList = SiasatanProcess.MtdGetPrckpnList(_siasatanPkEnc, "", "1015"); // KodBorangRakaman, KumpulanFk = 161 (smuparameter) 
        //            _data8.ListRkmn = _newList.ToList();
        //            if (_data8.ListRkmn.Count() > 0)
        //            {
        //                str[7] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Borang Rakaman OKT e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianLampiran _data9 = new();
        //            List<ModelHrLampiran> _newList = new();
        //            List<ModelHrLampiran> _newList2 = new();

        //            _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1017"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
        //            _newList2 = SiasatanProcess.MtdGetLampiranDocList(_aduanPkEnc); // Get File PDF by ADUAN_FK

        //            _newList.AddRange(_newList2);
        //            _data9.ListLampiran = _newList.ToList();

        //            if (_data9.ListLampiran.Count() > 0)
        //            {
        //                str[8] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Lampiran Dokumen Lain e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianLampiran _data10 = new();
        //            List<ModelHrLampiran> _newList = new();
        //            _newList = SiasatanProcess.MtdGetLampiranList(_siasatanPkEnc, "1018"); // Kod Borang Rakaman, KUMPULAN_FK = 161 (SMU_PARAMETER) 
        //            _data10.ListLampiran = _newList.ToList();
        //            if (_data10.ListLampiran.Count() > 0)
        //            {
        //                str[9] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Lampiran Barang Bukti e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianMessage _data11 = new();
        //            List<ModelHrMessage> _newList = new();
        //            _newList = SiasatanProcess.MtdGetMessageList(_siasatanPkEnc);
        //            _data11.ListMessage = _newList.ToList();
        //            if (_data11.ListMessage.Count() > 0)
        //            {
        //                str[10] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Minit Siasatan e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianCatatan _data12 = new();
        //            List<ModelHrCatatan> _newList = new();
        //            _newList = SiasatanProcess.MtdGetCatatanSstnList(_siasatanPkEnc);
        //            _data12.ListCatatanSstn = _newList.ToList();
        //            if (_data12.ListCatatanSstn.Count() > 0)
        //            {
        //                str[11] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Catatan Siasatan e ~ " + e);
        //        }

        //        try
        //        {
        //            CarianLaporan _data13 = new();
        //            List<ModelHrLaporan> _newList = new();
        //            _data13 = SiasatanProcess.MtdGetLaporanSstn(_siasatanPkEnc);
        //            if (_data13._laporan != null)
        //            {
        //                _data13.ListLaporanSstn = SiasatanProcess.MtdGetRumusanList(_data13._laporan.LPRN_SSTN_PK_ENC);
        //            }
        //            if (_data13.ListLaporanSstn.Count() > 0)
        //            {
        //                str[12] = "text-success";
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            var log = NLog.LogManager.GetCurrentClassLogger();
        //            log.Info("SemakSemuaMenu StatusProcess Rumusan Siasatan e ~ " + e);
        //        }
        //    }

        //    return str;
        //}
    }
}
