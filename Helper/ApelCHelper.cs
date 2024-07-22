using Net6HrPublicLibrary.Model;
//using APELC.LocalServices.ApelC;
using APELC.Models;
using System.Xml.Linq;

namespace APELC.Helper
{
    public class ApelCHelper
    {
        public SessionModel GetApelInfo(CarianSiasatanMain aduanInfo)
        {
            CarianSiasatanMain _data = new();
            var aduan = new SessionModel();

            if (aduanInfo.CADUAN_PK_ENC != null) {
                //_data = SiasatanProcess.MtdGetMaklumatAduanSession(aduanInfo.CADUAN_PK_ENC);
                if (_data != null)
                {
                    aduan.ADUAN_PK_ENC = _data.Pengadu.ADUAN_PK_ENC;
                    aduan.ApelC_PK_ENC = _data.Pengadu.ApelC_PK_ENC;
                    aduan.REPORT_NO = _data.Pengadu.REPORT_NO;
                    aduan.STATUS_SSTN = _data.Pengadu.STATUS_SSTN;
                    aduan.STS_RNGKSAN = _data.Pengadu.STS_RNGKSAN;
                    aduan.STS_LAPORAN = _data.Pengadu.STS_LAPORAN;
                }
            }
            return aduan;
        }
    }
}
