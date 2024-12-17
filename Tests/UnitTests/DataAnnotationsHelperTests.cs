using System.ComponentModel.DataAnnotations;
using Bonsai.Helpers;
using Bonsai.Models;

namespace UnitTests;

public class DataAnnotationsHelperTests
{
    [Fact]
    public void GetMaxLengthAttribute_ReturnsMaxLengthAttributeValue_IfGivenTypeHasGivenPropertyAndPropertyHasMaxLengthAttribute()
    {
        // Arrange
        var user = new User();
        var propertyName = nameof(user.Username);

        // Act
        var length = DataAnnotationsHelper.GetMaxLengthAttribute(user.GetType(), propertyName);

        // Assert
        Assert.NotEqual(0, length);
        Assert.True(length > 0, $"Length of {nameof(MaxLengthAttribute)} should be greater than 0");
    }

    [Fact]
    public void GetMaxLengthAttribute_ThrowsArgumentException_IfGivenTypeDoesNotHaveGivenProperty()
    {
        // Arrange
        var user = new User();
        var propertyName = "Usernamee";

        // Act
        var action = new Action(() => DataAnnotationsHelper.GetMaxLengthAttribute(user.GetType(), propertyName));

        // Assert
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
        Assert.Equal($"Object of type {user.GetType()} does not have property \"{propertyName}\"", exception.Message);
    }

    [Fact]
    public void GetMaxLengthAttribute_ThrowsArgumentException_IfGivenPropertyDoesNotHaveMaxLengthAttribute()
    {
        // Arrange
        var user = new User();
        var propertyName = nameof(user.DateJoined);

        // Act
        var action = new Action(() => DataAnnotationsHelper.GetMaxLengthAttribute(user.GetType(), propertyName));

        // Assert
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
        Assert.Equal($"Property \"{propertyName}\" of object {user.GetType()} does not have attribute {nameof(MaxLengthAttribute)}", exception.Message);
    }
}