using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class SupportedVideoTypesAttribute : SupportedFileTypesBaseAttributeClass
    {
        protected override ValidationResult? IsValid(object? item, ValidationContext validationContext) =>
            AttributeValidation<VideoSettings>(item, validationContext);
    }
}
