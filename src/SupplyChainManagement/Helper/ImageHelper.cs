namespace SupplyChainManagement.Helper
{
    public static class ImageHelper
    {
        public static bool IsValidImage(IFormFile formFile)
        {
            // Check the content type
            if (!formFile.ContentType.StartsWith("image/"))
            {
                return false; // Not a valid image
            }

            // Alternatively, you can check the file extension
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more extensions as needed
            string fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();

            if (Array.IndexOf(validExtensions, fileExtension) == -1)
            {
                return false; // Not a valid image
            }

            return true; // Valid image
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
