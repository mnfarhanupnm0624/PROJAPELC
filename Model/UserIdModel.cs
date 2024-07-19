using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6HrPublicLibrary.Model
{
    public class UserIdModel
    {
        public string? KOD { set; get; }
        public int? PENCIPTA_FK { set; get; }
        public string? RESULTSET { set; get; }
        public string? ID_PENGGUNA { set; get; }
        public string? KOD_PERANAN { set; get; }
        public string? KATALALUAN { set; get; }
        public int? STAF_FK { set; get; }
        public string? STAF_FK_ENC { set; get; }
        public string? NAMA { set; get; }
        public string? KAD_PENGENALAN { set; get; }
        public string? EMAIL { set; get; }
        public DateTime? TKH_UBAH_KATALALUAN { set; get; }
        public DateTime? TKH_LUPUT_KATALALUAN { set; get; }
        public DateTime? TKH_LUPUT_ID { set; get; }
        public int? BIL_GAGAL_LOGIN { set; get; }
        public string? AKTIF_FLAG { set; get; }
        public string? PENGGUNA_BARU_FLAG { set; get; }
        public int KLINIK_FK { set; get; }
        public string? AKADEMIK { set; get; }
        public string? KUMPULAN { set; get; }
        public string? USER_GROUP { set; get; }
        public string? MULTI_PTJ { set; get; }
        public string? SSO_ID { set; get; }
        public string? PASSWORD { set; get; }
        public string? NOPEKERJA { set; get; }
        public string? KOD_PTJ { set; get; }
        public string? KOD_JAWATAN { set; get; }
        public string? JAWATAN_DESC { set; get; }

        public DateTime? MODAL_TKH_MULA { set; get; }

        public DateTime? MODAL_TKH_TAMAT { set; get; }
        public string? MODAL_KOD_PERANAN { set; get; }
        public string? MODAL_KOD_PERANAN_DD { set; get; }
        public string? SPR_NOKP { set; get; }
        
        public string? ENC_FROM_DASHBOARD { set; get; }
        public string? ARRAY_FROM_ENC { set; get; }
    }
}
