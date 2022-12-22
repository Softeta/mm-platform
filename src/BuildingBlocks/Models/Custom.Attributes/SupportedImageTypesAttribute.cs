using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class SupportedImageTypesAttribute :  SupportedFileTypesBaseAttributeClass
    {
        protected override ValidationResult? IsValid(object? file, ValidationContext validationContext) =>
            AttributeValidation<ImageSettings>(file, validationContext);
    }
}
