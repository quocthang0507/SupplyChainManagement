using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class QRCodeModel
    {
        [Display(Name = "Nhập văn bản để tạo mã QR")]
        public required string QRCodeText { get; set; }

        [Display(Name = "Chọn màu mã QR")]
        public string DarkColor { get; set; } = "#000000";

        [Display(Name = "Chọn màu nền")]
        public string LightColor { get; set; } = "#FFFFFF";
    }
}
