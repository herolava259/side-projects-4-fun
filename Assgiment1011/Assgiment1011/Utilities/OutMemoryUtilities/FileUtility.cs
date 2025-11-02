using Assgiment1011.Utilities.Constants;
using System.Security.Cryptography;
using System;
using System.IO;
namespace Assgiment1011.Utilities.OutMemoryUtilities
{
    public static class FileUtility
    {
        public static async Task<string> WriteFile(IFormFile file)
        {
            var componentNames = file.FileName.Split('.');
            if (componentNames.Length == 1)
            {
                componentNames.Append("txt");
            }
            var extComps = componentNames.Skip(1);
            string fullExtension = "." + String.Join('.', extComps);
            var fileName = await GenerateRandomFileName(fullExtension);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), PathConstant.RelativeUploadFilePath);



            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            else
            {
                int tryCreate = 1000;
                var existedFiles = Directory.GetFiles(filePath);
                while (CheckDuplicate(fileName, existedFiles) && tryCreate > 0)
                {
                    fileName = await GenerateRandomFileName(fullExtension);
                    tryCreate--;
                }

                if (tryCreate <= 0)
                {
                    throw new Exception("Cannot random file name");
                }



            }
            var exactPath = Path.Combine(Directory.GetCurrentDirectory(), PathConstant.RelativeUploadFilePath, fileName);

            using (var stream = new FileStream(exactPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        private static bool CheckDuplicate(string newFileName, string[]? files = null, string directoryPath = "")
        {
            if (files == null)
                files = Directory.GetFiles(directoryPath);
            if (files.Length == 0) return false;
            return files.Contains(newFileName);
        }
        private static async Task<string> GenerateRandomFileName(string fileExtension)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[8];
                rng.GetBytes(randomBytes);

                string randomFileName = BitConverter.ToUInt64(randomBytes, 0).ToString("X16");

                return $"{randomFileName}{fileExtension}";
            }
        }

        public static bool IsExist(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
