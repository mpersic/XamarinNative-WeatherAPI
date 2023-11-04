using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NNChallenge.Services;
using NUnit.Framework;

[TestFixture]
public class WeatherServiceTests
{
    [Test]
    public async Task GetDailyWeather_Success_ReturnsValidForecast()
    {
        // Arrange
        var city = "Berlin";
        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, SampleJsonResponse());
        var weatherService = new OpenWeatherApiService(httpClient);

        // Act
        var result = await weatherService.GetDailyWeather(city);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Berlin", result.City);
        Assert.IsNotEmpty(result.HourForecast);
    }

    [Test]
    public void GetDailyWeather_Failure_ThrowsException()
    {
        // Arrange
        var city = "InvalidCity";
        var httpClient = CreateMockHttpClient(HttpStatusCode.NotFound, null);
        var weatherService = new OpenWeatherApiService(httpClient);

        // Act and Assert
        Assert.ThrowsAsync<Exception>(() => weatherService.GetDailyWeather(city));
    }

    private HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string content)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });

        return new HttpClient(mockHandler.Object);
    }

    private string SampleJsonResponse()
    {
        // Create a sample JSON response for testing
        return @"{
            'location': {
                'name': 'New York'
            },
            'forecast': {
                'forecastday': [
                    {
                        'hour': [
                            {
                                'time': '2023-11-01 12:00',
                                'temp_c': 20,
                                'temp_f': 68,
                                'condition': {
                                    'icon': 'icon_url'
                                }
                            }
                        ]
                    }
                ]
            }
        }";
    }
}
