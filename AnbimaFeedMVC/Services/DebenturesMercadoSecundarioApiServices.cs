using AnbimaFeedMVC.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AnbimaFeedMVC.Services
{
    public class DebenturesMercadoSecundarioApiServices : IDebenturesMercadoSecundarioApiServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration configuration;

        public DebenturesMercadoSecundarioApiServices(IConfiguration config, IHttpClientFactory clientFactory)
        {
            configuration = config;
            _clientFactory = clientFactory;
        }

        public async Task<string> GetAccessToken()
        {
            var baseAddress = configuration.GetSection("API:baseAddress");
            var clientID = configuration.GetSection("API:client_id");
            var clientSecret = configuration.GetSection("API:client_secret");

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress.Value);

            // Codifique o access_token como uma string Base64
            var clientIdAndSecret = $"{clientID.Value}:{clientSecret.Value}";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientIdAndSecret));

            // Crie o corpo da solicitação
            var requestBody = new
            {
                grant_type = "client_credentials"
            };
            var requestBodyJson = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            // Adicione os cabeçalhos personalizados
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var tokenEndpoint = "https://api.anbima.com.br/oauth/access-token";

            var response = await client.PostAsync(tokenEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonSerializer.Deserialize<TokenResponseModel>(stringResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return tokenResponse.Access_token;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

            public async Task<List<DebenturesMercadoSecundarioModel>> GetDebenturesMercadoSecundario(DateTime data)
            {
            var accessToken = await GetAccessToken();
            var baseAddress = configuration.GetSection("API:baseAddress");
            var clientID = configuration.GetSection("API:client_id");

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress.Value);

            // Adicione os cabeçalhos personalizados
            client.DefaultRequestHeaders.Add("client_id", clientID.Value);
            client.DefaultRequestHeaders.Add("access_token", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"?Data={data:yyyy-MM-dd}&json=true";

            var result = new List<DebenturesMercadoSecundarioModel>();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<DebenturesMercadoSecundarioModel>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}