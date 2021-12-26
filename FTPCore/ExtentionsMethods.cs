using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FTPCore
{
    public static class FileSaveExtension
    {
        public static async Task SaveAsAsync(this IFormFile formFile, string filePath = "wwwroot/upload")
        {
            if (formFile == null || formFile.Length == 0)
            {
                return;
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }

        public static void SaveAs(this IFormFile formFile, string filePath = "wwwroot/upload")
        {
            if (formFile == null || formFile.Length == 0)
            {
                return;
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
        }

        public static void SaveAsToFTP(this IFormFile formFile, string filePath = "/upload")
        {
            FTPManager.UploadFile(formFile, filePath);
        }
        public static async Task SaveAsToFTPAsync(this IFormFile formFile, string filePath = "/upload")
        {
            await FTPManager.UploadFileAsync(formFile, filePath);
        }

        public static void SaveAsToLocalAndFTP(this IFormFile formFile, string filePath = "/upload")
        {
            FTPManager.UploadFile(formFile, filePath);
            formFile.SaveAs("wwwroot" + filePath);
        }
        public static async Task SaveAsToLocalAndFTPAsync(this IFormFile formFile, string filePath = "/upload")
        {
            await FTPManager.UploadFileAsync(formFile, filePath);
            await formFile.SaveAsAsync("wwwroot" + filePath);
        }
    }
}