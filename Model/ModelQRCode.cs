using APELC.LocalShared;
using System.ComponentModel.DataAnnotations;

namespace APELC.Model
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

        public List<ModelParameterAPEL> ListEventMajlis =  new SecurityConstants().ItemListKampus;
        public List<ModelParameterAPEL> ListYaTidak = new SecurityConstants().ItemListYaTidak;

    }
}
