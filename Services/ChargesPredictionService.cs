using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Asandului_Oana_Maria_Insurance.Models;

namespace Asandului_Oana_Maria_Insurance.Services
{
    public class ChargesPredictionService : IChargesPredictionService
    {
        private readonly HttpClient _httpClient;

        public ChargesPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<float?> PredictAsync(ChargesApiRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/predict", request);

            var raw = await response.Content.ReadAsStringAsync(); // 🔥 vezi exact JSON-ul
            if (!response.IsSuccessStatusCode)
                throw new Exception(raw);

            // Temporar: încearcă să parsezi generic
            var apiResult = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(raw);

            // încearcă câteva nume comune
            if (apiResult.TryGetProperty("prediction", out var p) && p.ValueKind == System.Text.Json.JsonValueKind.Number)
                return p.GetSingle();

            if (apiResult.TryGetProperty("score", out var s) && s.ValueKind == System.Text.Json.JsonValueKind.Number)
                return s.GetSingle();

            // uneori e nested
            if (apiResult.TryGetProperty("prediction", out var pn) && pn.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                if (pn.TryGetProperty("charges", out var pc) && pc.ValueKind == System.Text.Json.JsonValueKind.Number)
                    return pc.GetSingle();
            }

            throw new Exception("Unexpected API response: " + raw);
        }

        private class ApiResponse
        {
            public float Prediction { get; set; }
        }
    }
}
