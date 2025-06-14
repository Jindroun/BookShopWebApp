using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Mocks;

namespace UnitTests;

public class RatingServiceTests
{
    private readonly MockedDependencyInjectionBuilder mockedDependencyInjectionBuilder;
    private readonly IRatingService ratingService;
    public RatingServiceTests()
    {
        mockedDependencyInjectionBuilder = new MockedDependencyInjectionBuilder()
            .AddMockdDBContext()
            .AddServices()
            .AddMapper();

        var serviceProvider = mockedDependencyInjectionBuilder.Create();

        ratingService = serviceProvider.GetRequiredService<IRatingService>();
    }

    [Fact]
    public void GetRatings_ShouldReturn()
    {
        // Act
        var ratings = ratingService.GetRatings();

        // Assert
        Assert.NotNull(ratings);
        Assert.True(ratings.Any());
    }

    [Fact]
    public async Task DeleteRating_ShouldReturn()
    {
        // Arrange
        var ratingId = 1;

        // Act
        var result = await ratingService.DeleteRating(ratingId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddRating_ShouldReturn()
    {
        // Arrange
        var ratingDto = new RatingDto
        {
            BookId = 1,
            UserId = 5,
            StarRating = 5,
            Note = "Test note"
        };

        // Act
        var result = await ratingService.AddRating(ratingDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        var rating = result.Data!;
        Assert.Equal(ratingDto.BookId, rating.BookId);
        Assert.Equal(ratingDto.UserId, rating.UserId);
        Assert.Equal(ratingDto.StarRating, rating.StarRating);
        Assert.Equal(ratingDto.Note, rating.Note);
    }
}
