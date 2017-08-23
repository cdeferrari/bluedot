using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

[assembly: OwinStartup(typeof(ApiCore.Startup))]

namespace ApiCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration
                  .EnableSwagger(c =>
                  {
                      c.SingleApiVersion("v1", "SomosTechies API")
                 .Description("A sample API for testing")
                 .TermsOfService("Some terms")
             .Contact(cc => cc
               .Name("Jesus Angulo")
               .Url("https://somostechies.com/contact")
               .Email("jesus.angulo@outlook.com"))
             .License(lc => lc
               .Name("Some License")
               .Url("https://somostechies.com/license"));
                  })
              .EnableSwaggerUi();

        }
    }
}
