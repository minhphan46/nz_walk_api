using UdemyProject1.Models.Domain;

namespace UdemyProject1.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
