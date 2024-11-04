using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Moq.Protected;
using ChallengeChurnAnalytics.Services;

namespace ChallengeChurnAnalytics.Tests
{
    public class ExternalAuthServiceTests
    {
        // testa se AuthenticateUserAsync retorna o token quando a autenticação é bem-sucedida
        [Fact]
        public async Task AuthenticateUserAsync_ReturnsToken_OnSuccess()
        {
            // Arrange - define o token de exemplo e simula uma resposta bem-sucedida da API externa
            var token = "sample-token";
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                       .Setup<Task<HttpResponseMessage>>(
                           "SendAsync",
                           ItExpr.IsAny<HttpRequestMessage>(),
                           ItExpr.IsAny<System.Threading.CancellationToken>()
                       )
                       .ReturnsAsync(new HttpResponseMessage
                       {
                           StatusCode = System.Net.HttpStatusCode.OK,
                           Content = new StringContent("authenticated")
                       });

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new ExternalAuthService(httpClient);

            // Act - chama o método AuthenticateUserAsync
            var result = await authService.AuthenticateUserAsync(token);

            // Assert - verifica se o resultado é o token "authenticated" esperado
            Assert.Equal("authenticated", result);
        }

        // testa se AuthenticateUserAsync lança uma exceção quando a autenticação falha
        [Fact]
        public async Task AuthenticateUserAsync_ThrowsException_OnFailure()
        {
            // Arrange - define o token de exemplo e simula uma resposta não autorizada da API externa
            var token = "sample-token";
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                       .Setup<Task<HttpResponseMessage>>(
                           "SendAsync",
                           ItExpr.IsAny<HttpRequestMessage>(),
                           ItExpr.IsAny<System.Threading.CancellationToken>()
                       )
                       .ReturnsAsync(new HttpResponseMessage
                       {
                           StatusCode = System.Net.HttpStatusCode.Unauthorized,
                       });

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new ExternalAuthService(httpClient);

            // Act & Assert - chama AuthenticateUserAsync e verifica se uma exceção é lançada
            await Assert.ThrowsAsync<Exception>(() => authService.AuthenticateUserAsync(token));
        }
    }
}
