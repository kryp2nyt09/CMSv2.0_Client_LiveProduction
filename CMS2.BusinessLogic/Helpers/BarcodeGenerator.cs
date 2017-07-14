using System.Drawing;
using BarcodeLib;

namespace CMS2.BusinessLogic.Helpers
{
    public static class BarcodeGenerator
    {
        
        public static byte[] GetBarcode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            else
            {
                Barcode barcode = new Barcode();
                barcode.IncludeLabel = false;
                barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
                var image = barcode.Encode(TYPE.CODE128, code);
                ImageConverter converter = new ImageConverter();
                return (byte[]) converter.ConvertTo(image, typeof (byte[]));
            }

        }
    }
}
