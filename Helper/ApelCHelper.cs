using APELC.LocalServices.Login;
using APELC.Model;
using System.Xml.Linq;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace APELC.Helper
{
    public class ApelCHelper
    {
        //public SessionModel GetApelCInfo(SessionModel apelcInfo)
        //{
        //    return GetApelCInfo(apelcInfo);
        //}

        public SessionModel GetApelCInfo(SessionModel _pengguna)
        {
            SessionModel _data = new();

            if (new SessionModel().PENGGUNA_UPNM_PK_ENC != null)
            {
                SessionModel UserSessionInfo = LoginProcess.MtdGetMaklumatPenggunaSession(_pengguna);
                _data = UserSessionInfo;
                if (_data != null)
                {
                    UserSessionInfo.PENGGUNA_UPNM_PK_ENC = _data.Pengguna.PENGGUNA_UPNM_PK_ENC;
                    UserSessionInfo.ID_PENGGUNA = _data.Pengguna.ID_PENGGUNA;
                    UserSessionInfo.KATA_LALUAN_PENGGUNA = _data.Pengguna.KATA_LALUAN_PENGGUNA;
                    UserSessionInfo.JENIS_MODUL_PENGGUNA_UPNM_FK = _data.Pengguna.JENIS_MODUL_PENGGUNA_UPNM_FK;
                    UserSessionInfo.SESSION_TIMEOUT = _data.Pengguna.SESSION_TIMEOUT;
                    UserSessionInfo.STATUS_AKTIF_PENGGUNA_UPNM_FK = _data.Pengguna.STATUS_AKTIF_PENGGUNA_UPNM_FK;
                }
            }
            return _data;
        }

    }
}
