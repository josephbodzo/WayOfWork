using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using WayOfWork.AppCode.Attributes;

namespace WayOfWork.AppCode.SwaggerFilters
{
    public class AuthorisationKeyHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var isOptional = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<UserKeyOptionalAttribute>().Any();
            var isRequired = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<UserKeyRequiredAttribute>().Any();


            if (isOptional || isRequired)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorisation",
                    In = "header",
                    Description = "access token",
                    Required = isRequired,
                    Type = "string",
                    Default = isRequired ? "Just Showing this as a sample" : ""
                });
            }
        }
    }
}
