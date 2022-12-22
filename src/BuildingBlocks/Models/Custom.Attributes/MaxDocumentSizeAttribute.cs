using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class MaxDocumentSizeAttribute : MaxFileSizeBaseAttribute
    {
        protected override ValidationResult? IsValid(object? item, ValidationContext validationContext) =>
            AttributeValidation<DocumentSettings>(item, validationContext);
    }
}
