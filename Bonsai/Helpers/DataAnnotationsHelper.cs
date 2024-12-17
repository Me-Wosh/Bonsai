using System.ComponentModel.DataAnnotations;

namespace Bonsai.Helpers;

public static class DataAnnotationsHelper
{
    public static int GetMaxLengthAttribute(Type objectType, string propertyName)
    {
        var property = objectType.GetProperty(propertyName);

        if (property == null)
        {
            throw new ArgumentException($"Object of type {objectType} does not have property \"{propertyName}\"");
        }

        var attribute = (MaxLengthAttribute?)Attribute.GetCustomAttribute(property, typeof(MaxLengthAttribute));
        
        if (attribute == null)
        {
            throw new ArgumentException($"Property \"{propertyName}\" of object {objectType} does not have attribute {nameof(MaxLengthAttribute)}");
        }
        
        return attribute.Length;
    }
}