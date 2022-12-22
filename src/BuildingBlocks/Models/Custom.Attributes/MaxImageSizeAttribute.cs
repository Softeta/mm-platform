using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class MaxImageSizeAttribute : MaxFileSizeBaseAttribute
    {
        protected override ValidationResult? IsValid(object? item, ValidationContext validationContext) =>
            AttributeValidation<ImageSettings>(item, validationContext);
    }
}
