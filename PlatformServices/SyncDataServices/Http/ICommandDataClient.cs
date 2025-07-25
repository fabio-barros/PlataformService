using System.Threading.Tasks;
using PlatformService.Dtos;
using PlatformService.Models;
namespace PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommandAsync(PlatformReadDto platform);
    }
}