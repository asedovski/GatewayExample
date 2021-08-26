namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IImageValidationService
    {
        bool IsImageFile(byte[] fileBytes, string extension, out string mimeType);
        bool IsImageMimeType(string mimeType);
    }
}
