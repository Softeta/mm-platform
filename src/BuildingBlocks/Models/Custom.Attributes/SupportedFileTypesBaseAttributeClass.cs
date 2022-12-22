using Custom.Attributes.Settings;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public abstract class SupportedFileTypesBaseAttributeClass : ValidationAttribute
    {
        protected ValidationResult? AttributeValidation<T>(
            object? item, 
            ValidationContext validationContext) where T : FileSettings
        {
            var supportedTypes = validationContext
                .GetRequiredService<IOptions<T>>()
                .Value
                .SupportedTypes;

            return AttributeValidation(item, supportedTypes);
        }

        private static ValidationResult? AttributeValidation(object? item, string[] supportedTypes)
        {
            if (item is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            new FileExtensionContentTypeProvider()
                .TryGetContentType(file.FileName, out string? contentType);

            if (!supportedTypes.Contains(contentType))
            {
                throw new BadRequestException($"{contentType} file is not allowed", 
                    ErrorCodes.BadRequest.FileExtensionIsNotAllowed,
                    new string[] { contentType });
            }

            return ValidationResult.Success;
        }
    }
}
