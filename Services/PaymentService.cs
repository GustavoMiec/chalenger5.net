using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class PaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentResponse> ProcessPayment(PaymentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("https://api.paymentgateway.com/process", request);
        response.EnsureSuccessStatusCode();

        var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponse>();

        // Retorne um objeto padrão se a resposta for nula
        return paymentResponse ?? new PaymentResponse(); // Assumindo que PaymentResponse tem um construtor padrão
    }
}
