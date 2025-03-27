using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PulsePlaylist.Integration.Tests.Infrastructure;

namespace PulsePlaylist.Integration.Tests.Features.Stock;

public class StockEndpointTests : ApiTestBase
{
    private const string ApiBaseUrl = "/stocks";

    [Fact]
    public async Task FullStockProcess_ShouldSucceed()
    {
        // Arrange & Act - Step 1: Query Pagination
        var query = new
        {
            keywords = "",
            pageNumber = 0,
            pageSize = 15,
            orderBy = "Id",
            sortDirection = "Descending",
        };

        var paginationResponse = await HttpClient.PostAsJsonAsync($"{ApiBaseUrl}/pagination", query);

        // Assert Step 1
        paginationResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var paginatedResult = await paginationResponse.Content.ReadFromJsonAsync<StockPaginationResponse>();
        paginatedResult.Should().NotBeNull();
        paginatedResult!.TotalItems.Should().BeGreaterThan(0);

        var productId = paginatedResult.Items.First().ProductId;
        productId.Should().NotBeNullOrEmpty();

        // Act Step 2: Receive Stock
        var receiveCommand = new
        {
            ProductId = productId,
            Quantity = 50,
            Location = "WH-02"
        };

        var receiveResponse = await HttpClient.PostAsJsonAsync($"{ApiBaseUrl}/receive", receiveCommand);

        // Assert Step 2
        receiveResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act Step 3: Dispatch Stock
        var dispatchCommand = new
        {
            ProductId = productId,
            Quantity = 20,
            Location = "WH-02"
        };

        var dispatchResponse = await HttpClient.PostAsJsonAsync($"{ApiBaseUrl}/dispatch", dispatchCommand);

        // Assert Step 3
        dispatchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

public class StockPaginationResponse
{
    public int TotalItems { get; set; }
    public List<StockItemResponse> Items { get; set; } = new();
}

public class StockItemResponse
{
    public string ProductId { get; set; } = string.Empty;
}
