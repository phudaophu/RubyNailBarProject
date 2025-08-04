namespace RubyNailBarWeb.Utilities
{
    public class FileUploadSettings
    {
        public string RootImagePath { get; set; } = "wwwroot/assets/img";
        public string UserAvatarDefaultImage { get; set; } = "wwwroot/assets/img/default-image.png";
        public List<string> AllowedFileTypes { get; set; } = new List<string> { ".png", ".jpeg", ".jpg" };
        public int MaxFileSizeMB { get; set; } = 10;
        public long MaxFileSizeInBytes => MaxFileSizeMB * 1024L * 1024L;
    }
}
