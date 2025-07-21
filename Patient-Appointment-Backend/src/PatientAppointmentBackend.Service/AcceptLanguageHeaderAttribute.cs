using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PatientAppointmentBackend.Service
{
    public class AcceptLanguageHeaderAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            Dictionary<string, OpenApiExample> examples = new Dictionary<string, OpenApiExample>();
            examples.Add("en-US", new OpenApiExample() { Value = new OpenApiString("en-US")});
            examples.Add("fr-FR", new OpenApiExample() { Value = new OpenApiString("fr-FR") });

            var acceptLanguageParam = new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }, 
                Examples = examples
            };
            
            operation.Parameters.Add(acceptLanguageParam);
        }
    }
}
