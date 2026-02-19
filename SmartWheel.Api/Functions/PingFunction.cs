using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SmartWheel.Application.Interfaces;
using System.Net;

namespace SmartWheel.Api.Functions;

public class PingFunction
{
    private readonly IUserRepository _userRepository;

    public PingFunction(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Function("Ping")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var message = await _userRepository.GetHelloAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync(message);

        return response;
    }
}
