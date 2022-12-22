using Custom.Attributes.Settings;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public class SupportedDocumentTypesAttribute : SupportedFileTypesBaseAttributeClass
    {
        protected override ValidationResult? IsValid(object? item, ValidationContext validationContext) =>
            AttributeValidation<DocumentSettings>(item, validationContext);
    }
}
