using Custom.Attributes.Settings;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Custom.Attributes
{
    public abstract class MaxFileSizeBaseAttribute : ValidationAttribute
    {
        protected ValidationResult? AttributeValidation<T>(
            object? item,
            ValidationContext validationContext) where T : FileSettings
        {
            var maxSizeInKylobites = validationContext
                .GetRequiredService<IOptions<T>>()
                .Value
                .MaxSizeInKilobytes;

            return AttributeValidation(item, maxSizeInKylobites);
        }

        private static ValidationResult? AttributeValidation(object? item, int maxSizeInBytes)
        {
            if (item is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            if (file.Length > maxSizeInBytes * 1024)
            {
                throw new BadRequestException("Maximum allowed file size is {maxSizeInBytes} bytes", 
                    ErrorCodes.BadRequest.FileMaxSizeExceeded, 
                    new string[] { maxSizeInBytes.ToString() });
            }

            return ValidationResult.Success;
        }
    }
}
