using Net6HrPublicLibrary.Model;
using APEL.LocalServices.Siasatan;
using APEL.Models;
using System.Xml.Linq;

namespace APEL.Helper
{
    public class ApelHelper
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
                    aduan.SIASATAN_PK_ENC = _data.Pengadu.SIASATAN_PK_ENC;
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
