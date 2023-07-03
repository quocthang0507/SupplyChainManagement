using Microsoft.AspNetCore.Http;

namespace SupplyChainManagement.Helper
{
    public static class ImageHelper
    {
        public static bool IsValidImage(IFormFile formFile)
        {
            if (!formFile.ContentType.StartsWith("image/"))
            {
                return false;
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();

            if (Array.IndexOf(validExtensions, fileExtension) == -1)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidImage(IList<IFormFile> files)
        {
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length == 0 || !IsValidImage(file))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
