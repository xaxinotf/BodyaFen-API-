using System.Net;
using BodyaFen_API_.Models;
using Newtonsoft.Json;

namespace BodyaFen_API_;

public class BonkRequester
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly Uri _exchangeUri = new($"https://api.privatbank.ua/p24api/exchange_rates?date={DateTime.Now.ToString("dd.MM.yyyy")}");

    public BonkRequester(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<BonkModel> BonkRequest(CancellationToken token)
    {
        var client = _httpClientFactory.CreateClient("Privat24");

        var response = await client.GetAsync(_exchangeUri, token);
        if (response.StatusCode != HttpStatusCode.OK)
            return new BonkModel();

        var content = await response.Content.ReadAsStringAsync(token);
        var privat24Model = JsonConvert.DeserializeObject<BonkModel>(content);

        return privat24Model ?? new BonkModel();
    }
}