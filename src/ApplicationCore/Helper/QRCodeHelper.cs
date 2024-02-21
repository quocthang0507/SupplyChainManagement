using ApplicationCore.Entities;
using QRCoder;

namespace ApplicationCore.Helper
{
    public static class QRCodeHelper
    {
        public static string Create(QRCodeModel qRCodeModel)
        {
            QRCodeGenerator generator = new();
            QRCodeData info = generator.CreateQrCode(qRCodeModel.QRCodeText, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode bitmapQRCode = new(info);
            byte[] bytes = bitmapQRCode.GetGraphic(20, qRCodeModel.DarkColor, qRCodeModel.LightColor);
            string uri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes));
            return uri;
        }
    }
}
