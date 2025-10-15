namespace Service.Interfaces;

public interface IFileService
{
    Task<Uri> UploadAvatarAsync(string fileName);
}