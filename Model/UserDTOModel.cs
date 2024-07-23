using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APELC.Model
{
    public class UserDTOModel
    {
        public string? RESULTSET { set; get; }
        public string? RESULTSET_TEXT { set; get; }
        public string? USERNAME { set; get; }
        public string? PASSWORD { set; get; }
        public DateTime? PWDCHANGEDATE { set; get; }
        public DateTime? PWDEXPIREDATE { set; get; }
        public DateTime? UIDEXPIREDATE { set; get; }
        public string? COUNTRECORD { set; get; }
        public string? REALNAME { set; get; }
        public string? NEWUSER { set; get; }
        public string? PSPSTAFFK { set; get; }
        public string? HRSTAFFK { set; get; }
        public string? EMAIL { set; get; }
        public string? KLINIKFK { set; get; }
        public string? KODPTJ { set; get; }
        public string? NOPEKERJA { set; get; }
        public DateTime? TARIKH { set; get; }
        public string? FLAG { set; get; }
        public int? BILANGAN { set; get; }
        public string? NOKP { set; get; }
        public string? ROLE { set; get; }
        public string? RESULTTEXT { set; get; }
        public int? PENCIPTA_FK { set; get; }
        public string? NEWPASSWORD01 { set; get; }
        public string? NEWPASSWORD02 { set; get; }
        public string? KATALALUAN { set; get; }
        public string? KOD_PTJ { set; get; }
        public Byte[]? PHOTO { set; get; }
    }
}
