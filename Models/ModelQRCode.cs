using APELC.LocalShared;
using System.ComponentModel.DataAnnotations;

namespace APELC.Models
{
    public class ModelQRCode
    {
        [Display(Name = "Enter QRCode Text")]
        public string? QRCodeText { get; set; }

        public string? CEVENT { get; set; }
        public string? CPATH { get; set; }
        public string? CMASA_MULA { get; set; }
        public string? CMASA_TAMAT { get; set; }
        public string? CSEMAKMASA { get; set; }

        public List<ModelParameterHr> ListEventMajlis =  new SecurityConstants().ItemListKampus;
        public List<ModelParameterHr> ListYaTidak = new SecurityConstants().ItemListYaTidak;

    }
}
