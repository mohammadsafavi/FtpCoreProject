using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPCore
{
    public class UploadFileManager
    {
        private readonly static string FtpAddress = "ftp://localhost";
        private readonly static string UserName = "safavi";
        private readonly static string Password = "safavi123";
        public static void UploadFile(IFormFile formFile)
        {
            using (var ftp = new FtpClient(FtpAddress, UserName, Password))
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
                ftp.Upload(formFile.NewFile().Content, "/" + formFile.FileName, FtpRemoteExists.Overwrite, false, progress);

            }
        }

        public static async Task UploadFileAsync(IFormFile formFile)
        {
            var token = new CancellationToken();
            using (var ftp = new FtpClient(FtpAddress, UserName, Password))
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
               await ftp.UploadAsync( formFile.NewFileAsynk().Result.Content, "/" + formFile.FileName, FtpRemoteExists.Overwrite, false, progress, token);


            }
        }
    }


    public class File
    {
        public File()
        {
            this.Content = (Stream)new MemoryStream();
        }

        public string Name { get; set; }

        public Stream Content { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        public string Extension
        {
            get
            {
                return Path.GetExtension(this.Name);
            }
        }
    }
    public static class FormFileExtensions
    {
        public static File NewFile(this IFormFile formFile)
        {
            File file = new File()
            {
                ContentLength = formFile.Length,
                ContentType = formFile.ContentType,
                Name = formFile.FileName
            };
            formFile.CopyToAsync(file.Content, new CancellationToken());
            return file;
        }
        public static async Task<File> NewFileAsynk(this IFormFile formFile)
        {
            File file = new File()
            {
                ContentLength = formFile.Length,
                ContentType = formFile.ContentType,
                Name = formFile.FileName
            };
            await formFile.CopyToAsync(file.Content, new CancellationToken());
            return file;
        }
    }
}
