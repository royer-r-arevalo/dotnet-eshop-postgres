using Domain.Products;

namespace Domain.UnitTests.Products;

public class SkuTests
{
    // [ThingUnderTest]_Should_[ExpectResult]_[Conditions]
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_ReturnNull_WhenValueIsNullOrEmpty(string? value)
    {
        //Arrange

        //Act
        var sku = Sku.Create(value);

        //Assert
        Assert.Null(sku);
    }

    public static IEnumerable<object[]> InvalidSkuLengthData = new List<object[]>
    {
        new object[] { "invalid_sku" },
        new object[] { "invalid_sku_1" },
        new object[] { "invalid_sku_2" },
    };

    [Theory]
    [MemberData(nameof(InvalidSkuLengthData))]
    public void Create_Should_ReturnNull_WhenValueLengthIsInvalid(string value)
    {
        //Arrange
        //Act
        var sku = Sku.Create(value);
        Assert.Null(sku);
    }
}