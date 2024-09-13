using APELC.LocalServices.Login;
using APELC.Model;
using System.Xml.Linq;
using APELC.LocalServices.Public;
using APELC.LocalServices.Login;
using APELC.LocalShared;

namespace APELC.Model
{
    public class SessionModel
    {
        public string? RESULTSET { set; get; }
        public bool PAPARAN_ONLY { set; get; }

        //RUJUK KEPADA APELC_PENGGUNA_UPNM_FK
        public string? PENGGUNA_UPNM_PK_ENC { set; get; }
        public string? ID_PENGGUNA { set; get; }
        public string? KATA_LALUAN_PENGGUNA { set; get; }
        public string? JENIS_MODUL_PENGGUNA_UPNM_FK { set; get; }
        public string? SESSION_TIMEOUT { set; get; }

        public string? STATUS_AKTIF_PENGGUNA_UPNM_FK { set; get; }

        // Check Session Model
        public bool sessionValue { set; get; }

        public List<SessionModel> Pengguna = new();

    }
}
