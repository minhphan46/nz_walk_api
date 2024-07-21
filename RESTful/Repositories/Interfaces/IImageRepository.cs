using NZWalks.Entities;

namespace NZWalks.RESTful.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
