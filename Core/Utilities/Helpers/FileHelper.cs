using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static IDataResult<string> Add(IFormFile file, string targetFolder = null, string newFileName = null)
        {
            string sourcePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            var targetFilePath = CreateNewFilePath(file, targetFolder, newFileName);
            File.Move(sourcePath, targetFilePath["path"]);
            return new SuccessDataResult<string>(data: targetFilePath["dbPath"]);
        }

        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public static IDataResult<string> Update(string sourcePath, IFormFile file, string targetFolder = null, string newFileName = null)
        {
            var result = CreateNewFilePath(file, targetFolder, newFileName);

            if (sourcePath.Length > 0)
            {
                using (var stream = new FileStream(result["path"], FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            if (sourcePath != "") File.Delete(sourcePath);
            return new SuccessDataResult<string>(data: result["dbPath"]);
        }

        public static Dictionary<string, string> CreateNewFilePath(IFormFile file, string targetFolder = null, string newFileName = null)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;
            if (targetFolder == null) targetFolder = @"Images"; // Environment.CurrentDirectory + @"\Images";
            string path = Environment.CurrentDirectory + @"\wwwroot\" + targetFolder;
            if (newFileName == null) newFileName = Guid.NewGuid().ToString() + fileExtension;

            resultDictionary["path"] = $@"{path}/{newFileName}";
            resultDictionary["dbPath"] = $@"{targetFolder}/{newFileName}";
            return resultDictionary;
        }
    }
}
