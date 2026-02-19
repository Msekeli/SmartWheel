using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SmartWheel.Application.DTOs.Spin;
using SmartWheel.Application.UseCases;
using SmartWheel.Domain.Exceptions;

namespace SmartWheel.Api.Functions;

public sealed class ProcessSpinFunction
{
    private readonly ProcessSpinUseCase _useCase;

    public ProcessSpinFunction(ProcessSpinUseCase useCase)
    {
        _useCase = useCase;
    }

    [Function("ProcessSpin")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        try
        {
            var requestBody = await JsonSerializer.DeserializeAsync<ProcessSpinRequest>(
                req.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (requestBody is null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid request body.");
                return badResponse;
            }

            // For now assume userId comes from request
            var userId = requestBody.UserId;

            var result = await _useCase.ExecuteAsync(userId, requestBody);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
        catch (DomainException ex)
        {
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteStringAsync(ex.Message);
            return response;
        }
        catch (Exception)
        {
            var response = req.CreateResponse(HttpStatusCode.InternalServerError);
            await response.WriteStringAsync("Something went wrong.");
            return response;
        }
    }
}
