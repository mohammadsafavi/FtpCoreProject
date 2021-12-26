using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPCore
{
    public class FTPManager
    {
        public static void UploadFile(IFormFile formFile , string filePath = "/upload/")
        {
            if (formFile == null || formFile.Length == 0)
            {
                return;
            }
            using (var ftp = new FtpClient(UploadConfiguration.FtpAddress, UploadConfiguration.UserName, UploadConfiguration.Password))
            {
                ftp.Connect();

                // define the progress tracking callback
                Action<FtpProgress> progress = delegate (FtpProgress p)
                {
                    if (p.Progress == 1)
                    {
                        // all done!
                    }
                    else
                    {
                        // percent done = (p.Progress * 100)
                    }
                };

                // upload a file with progress tracking
                //ftp.UploadFile(formFile, "/public_html/temp/README.md", FtpRemoteExists.Overwrite, false, FtpVerify.None, progress);
                ftp.Upload(formFile.OpenReadStream(), filePath + "/" + formFile.FileName, FtpRemoteExists.Overwrite, true, progress);

            }
        }

        public static async Task UploadFileAsync(IFormFile formFile, string filePath = "/upload/")
        {
            if (formFile == null || formFile.Length == 0)
            {
                return;
            }
            var token = new CancellationToken();
            using (var ftp = new FtpClient(UploadConfiguration.FtpAddress, UploadConfiguration.UserName, UploadConfiguration.Password))
            {
                await ftp.ConnectAsync(token);

                // define the progress tracking callback
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                {
                    if (p.Progress == 1)
                    {
                        // all done!
                    }
                    else
                    {
                        // percent done = (p.Progress * 100)
                    }
                });

                // upload a file with progress tracking
                //   await ftp.UploadFileAsync(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpRemoteExists.Overwrite, false, FtpVerify.None, progress, token);
                await ftp.UploadAsync(formFile.OpenReadStream(), filePath + "/" + formFile.FileName, FtpRemoteExists.Overwrite, true, progress, token);


            }
        }
    }
}
