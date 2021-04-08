﻿using Core.Constants;
using Core.Utilities.FileHelper.Abstract;
using Core.Utilities.Result;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper.Concrete
{
    public class FileHelper : IFileHelper
    {

        public IDataResult<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ErrorDataResult<string>(Messages.FileNotFound);
            var result = WriteFile(file);
            return new SuccessDataResult<string>(result.Result, Messages.SuccessFileUpload);
        }

        public IDataResult<string> UploadFileUpdate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ErrorDataResult<string>(Messages.FileNotFound);
            var result = WriteFile(file);
            return new SuccessDataResult<string>(result.Result, Messages.SuccessFileUpload);
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName;

            //TODO -> Hata yönetiminden sonra refactor edilecek.
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fileName;
        }
    }
}
