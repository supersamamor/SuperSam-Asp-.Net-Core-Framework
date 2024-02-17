using ProjectNamePlaceHolder.Application.Requests;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}