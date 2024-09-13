using APELC.LocalServices.Login;
using APELC.LocalShared;
using APELC.LocalServices;
using APELC.Model;
using System.Net;

namespace APELC.Model
{
    public class ModelParameterAPEL
    {
        public string? Key { set; get; }
        public string? ViewField { set; get; }
        public string? Value { set; get; }
        public string? Text { set; get; }
        public int Nombor { set; get; }
        public string? RESULTSET { set; get; }
        public string? OldText01 { set; get; }
        public string? OldText01Enc { set; get; }
        public string? OldText02 { set; get; }
        public string? OldText02Enc { set; get; }
        public string? PublicText01 { set; get; }
        public string? PublicText01Enc { set; get; }
        public string? PublicText02 { set; get; }
        public string? PublicText02Enc { set; get; }

        internal IEnumerable<ParameterAPELModel> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
