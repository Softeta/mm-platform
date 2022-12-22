using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Customization.Swagger
{
    public class PropertyIgnoreFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation? operation, OperationFilterContext? context)
        {
            if (operation is null || context?.ApiDescription?.ParameterDescriptions is null)
            {
                return;
            }

            var parametersToHide = context.ApiDescription.ParameterDescriptions
                .Where(ParameterHasIgnoreAttribute)
                .ToList();

            if (parametersToHide.Count == 0)
            {
                return;
            }

            foreach (var parameterToHide in parametersToHide)
            {
                var parameter = operation.Parameters.FirstOrDefault(parameter => string.Equals(parameter.Name, parameterToHide.Name, StringComparison.Ordinal));
                if (parameter is not null)
                {
                    operation.Parameters.Remove(parameter);
                }
            }
        }

        private static bool ParameterHasIgnoreAttribute(ApiParameterDescription parameterDescription) =>
            parameterDescription.ModelMetadata is DefaultModelMetadata metadata &&
            metadata.Attributes.PropertyAttributes is not null &&
            metadata.Attributes.PropertyAttributes.Any(attribute => attribute.GetType() == typeof(SwaggerIgnoreAttribute));
    }
}
