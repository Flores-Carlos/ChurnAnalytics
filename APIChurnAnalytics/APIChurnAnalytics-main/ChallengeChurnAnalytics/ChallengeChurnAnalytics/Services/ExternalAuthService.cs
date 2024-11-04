using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeChurnAnalytics.Services
{
    public class ExternalAuthService
    {
        // cliente HTTP para enviar solicitações ao serviço externo
        private readonly HttpClient _httpClient;

        // construtor que inicializa o cliente HTTP
        public ExternalAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // método assíncrono para autenticar o usuário enviando o token ao serviço externo
        public async Task<string> AuthenticateUserAsync(string token)
        {
            // define a URL da API de autenticação externa
            var apiUrl = "https://api.external-service.com/authenticate";

            // cria uma requisição HTTP POST com o token de autenticação no cabeçalho
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            request.Headers.Add("Authorization", $"Bearer {token}");

            // envia a requisição ao serviço externo e aguarda a resposta
            var response = await _httpClient.SendAsync(request);

            // verifica se a resposta foi bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                // retorna o conteúdo da resposta como string se a autenticação for bem-sucedida
                return await response.Content.ReadAsStringAsync();
            }

            // lança uma exceção se a autenticação falhar
            throw new Exception("Authentication failed");
        }
    }
}
