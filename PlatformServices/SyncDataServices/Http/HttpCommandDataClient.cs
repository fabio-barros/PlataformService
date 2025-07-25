using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Settings;
namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient(HttpClient httpClient, IOptions<CommandServiceSettings> commandServiceOptions) : ICommandDataClient
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string? _commandServiceUrl = commandServiceOptions.Value.Url;

        public async Task SendPlatformToCommandAsync(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_commandServiceUrl, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not send platform to command service");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was successful");
            }
        }
    }
}