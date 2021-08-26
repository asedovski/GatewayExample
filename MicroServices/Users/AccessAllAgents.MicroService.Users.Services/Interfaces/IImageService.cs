namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IImageService
    {
        byte[] ScaleImage(byte[] fileBytes);
    }
}
