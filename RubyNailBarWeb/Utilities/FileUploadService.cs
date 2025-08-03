

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace RubyNailBarWeb.Utilities
{
    public class FileUploadService
    {

        FileUploadSettings _fileUploadSettings;
        public FileUploadService(IOptions<FileUploadSettings> fileUploadSettings)
        {
            _fileUploadSettings = fileUploadSettings.Value;
        }

        public async Task<string> SaveOrReplaceImageAsync( IBrowserFile providedFile, string subFolderName, string fileNameWithoutExtension)
        {
            if (providedFile == null)
                throw new ArgumentNullException("To uploading, the provided file must not to be null ! "+ nameof(providedFile));

            string extension = Path.GetExtension(providedFile.Name).ToLower();

            if (!_fileUploadSettings.AllowedFileTypes.Contains(extension))
                throw new InvalidOperationException($"To uploading, the file type {extension} is not allowed");

            if(providedFile.Size > _fileUploadSettings.MaxFileSizeInBytes)
            {
                throw new InvalidOperationException($"To uploading, the file size {providedFile.Size} exceeds the maximum allowed size of {_fileUploadSettings.MaxFileSizeInBytes} bytes.");
            }

         
            string finalExtension = extension is ".jpg" ? "jpeg" : extension.TrimStart('.');

            string finalFileName = $"{fileNameWithoutExtension}.{finalExtension}";

            string fullFolder = Path.Combine(_fileUploadSettings.RootImagePath, subFolderName);

            string fullPath = Path.Combine(fullFolder, finalFileName);

            try
            {
                //  Create folder (if missing)
                if (!Directory.Exists(fullFolder))
                {
                    Directory.CreateDirectory(fullFolder);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to create folder: {fullFolder}", ex);
            }

            try
            {

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                
                //  Save new file
                await using var stream = new FileStream(fullPath, FileMode.Create);
                await providedFile.OpenReadStream(_fileUploadSettings.MaxFileSizeInBytes).CopyToAsync(stream);
                

            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save the uploaded file: {providedFile.Name}", ex);
            }

            return $"/img/{subFolderName}/{finalFileName}";
        }



    }
}
