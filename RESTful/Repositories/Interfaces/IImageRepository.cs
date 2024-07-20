using UdemyProject1.Entities;

namespace UdemyProject1.RESTful.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
