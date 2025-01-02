using APELC.LocalServices.Login;
using APELC.Model;
using System.Xml.Linq;
using APELC.LocalShared;
using Microsoft.AspNetCore.Mvc;

namespace APELC.Model
{
    public class SessionModel
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public bool PAPARAN_ONLY { set; get; }

        //RUJUK KEPADA APELC_PENGGUNA_UPNM
        public string? PENGGUNA_UPNM_PK_ENC { set; get; }
        
        public string? ID_PENGGUNA { set; get; }
        public string? KATA_LALUAN_PENGGUNA { set; get; }

        public string? JENIS_MODUL_PENGGUNA_UPNM_FK { set; get; }
        public string? SESSION_TIMEOUT { set; get; }

        public string? BIL_GAGAL_LOGIN { set; get; }

        public string? FLAG { set; get; }

        public string? STATUS_AKTIF_PENGGUNA_UPNM_FK { set; get; }

        // RUJUK KEPADA APELC_PENGGUNA_KAT_UPNM
        public string? PENGGUNA_KAT_UPNM_PK_ENC { set; get; }

        public string? PENGGUNA_KAT_UPNM_FK { set; get; }

        public string? KAT_PENGGUNA_UPNM_FK { set; get; }

        public string? NO_UNIQUE_ID { set; get; }

        public string? STATUS_AKTIF_PENGGUNA_KAT_UPNM_FK { set; get; }

        // RUJUK KEPADA APELC_PERANAN_UPNM

        public string? PERANAN_UPNM_PK_ENC { set; get; }

        public string? PERANAN_SJRH_UPNM_FK { set; get; }

        public string? JENIS_PERANAN_FK { set; get; }

        public string? PERANAN_UPNM_KURSUS_FK { set; get; }

        public string? CAJ_MOHON_UPNM_FK { set; get; }

        public string? CAJ_RAYUAN_US_UPNM_FK { set; get; }

        public string? CAJ_RAYUAN_PS_UPNM_FK { set; get; }

        public string? CAJ_PERANAN_UPNM_FK { set; get; }

        public string? STATUS_AKTIF_PERANAN_UPNM_FK { set; get; }

        public string? STATUS_PELANTIKAN_FK { set; get; }

        public DateTime? TKH_CIPTA { set; get; }

        public string? PENCIPTA_PERANAN_UPNM_FK { set; get; }
        
        public DateTime? TKH_KEMASKINI { set; get; }
        
        public string? KEMASKINI_PERANAN_UPNM_FK { set; get; }

        public DateTime? TKH_HAPUS { set; get; }
        
        public string? PENGHAPUS_PERANAN_UPNM_FK { set; get; }

        // Check Session Model
        public bool SessionValue { set; get; }

        //public List<SessionModel> Pengguna = new();
        public SessionModel Pengguna = new();

    }
}
