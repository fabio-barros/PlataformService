using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepository
    {
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform platform);
        void UpdatePlatform(Platform platform);
        void DeletePlatform(int id);
        bool SaveChanges();
    }
}