using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace ApiCore.SwaggerExtensions.OperationFilter
{
    public class ComplexTypeOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                foreach (var item in operation.parameters)
                {
                    if (item.name.Contains("."))
                        item.name = item.name.Split('.')[1];
                }
            }
        }
    }
}