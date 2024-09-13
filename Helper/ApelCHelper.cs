using APELC.LocalServices.Login;
using APELC.Model;
using System.Xml.Linq;

namespace APELC.Helper
{
    public class ApelCHelper
    {
        public SessionModel GetApelCInfo(SessionModel apelcInfo)
        {
            SessionModel _data = new();
            var apelcmohon = new SessionModel();

            if (apelcInfo.PENGGUNA_UPNM_PK_ENC != null)
            {
                _data = LoginProcess.MtdGetMaklumatPenggunaSession(apelcInfo.PENGGUNA_UPNM_PK_ENC);
                if (_data != null)
                {
                    apelcInfo.PENGGUNA_UPNM_PK_ENC = _data.Pengguna.PENGGUNA_UPNM_PK_ENC;
                    apelcInfo.ID_PENGGUNA = _data.Pengguna.ID_PENGGUNA;
                    apelcInfo.KATA_LALUAN_PENGGUNA = _data.Pengguna.KATA_LALUAN_PENGGUNA;
                    apelcInfo.JENIS_MODUL_PENGGUNA_UPNM_FK = _data.Pengguna.JENIS_MODUL_PENGGUNA_UPNM_FK;
                    apelcInfo.SESSION_TIMEOUT = _data.Pengguna.SESSION_TIMEOUT;
                    apelcInfo.STATUS_AKTIF_PENGGUNA_UPNM_FK = _data.Pengguna.STATUS_AKTIF_PENGGUNA_UPNM_FK;
                }
            }
            return apelcInfo;
        }
    }
}
