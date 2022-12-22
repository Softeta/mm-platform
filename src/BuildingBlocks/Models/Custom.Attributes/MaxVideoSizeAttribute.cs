using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class MaxVideoSizeAttribute : MaxFileSizeBaseAttribute
    {
        protected override ValidationResult? IsValid(object? item, ValidationContext validationContext) =>
            AttributeValidation<VideoSettings>(item, validationContext);
    }
}
