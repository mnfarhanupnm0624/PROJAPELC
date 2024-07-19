using System.ComponentModel.DataAnnotations;

namespace APEL.Models
{
    public class CarianMessage
    {
        public string? CNAMA { set; get; }
        public string? CNOADUAN { set; get; }

        [DataType(DataType.MultilineText)]
        public string? CATATAN { set; get; }

        public ModelHrMessage pegChat = new();
        public List<ModelHrMessage> ListMessage = new();
    }
}
