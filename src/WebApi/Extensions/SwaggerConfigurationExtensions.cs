using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Weblog.WebApi.Swagger;

namespace Weblog.WebApi.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        //More info : https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Weblog.WebApi.xml");
            //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);

            options.EnableAnnotations();

            //options.DescribeAllParametersInCamelCase();
            //options.IgnoreObsoleteActions();
            //options.IgnoreObsoleteProperties();

            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "API V2" });

            #region Filters
            //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
            options.ExampleFilters();

            //Set summary of action if not already set
            //options.OperationFilter<ApplySummariesOperationFilter>();

            #region Add Authentication
            const string securityNameAndScheme = "Bearer";

            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            // options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, securityNameAndScheme);

            // JWT
            options.AddSecurityDefinition(securityNameAndScheme, new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = securityNameAndScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = $"JWT Authorization header using the Bearer scheme. {Environment.NewLine} Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });

            ////OAuth2Scheme
            //options.AddSecurityDefinition(securityNameAndScheme, new OpenApiSecurityScheme
            //{
            //    //Scheme = "Bearer",
            //    //In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.OAuth2,
            //    Flows = new OpenApiOAuthFlows
            //    {
            //        Password = new OpenApiOAuthFlow
            //        {
            //            TokenUrl = new Uri("/api/v1/user/Token", UriKind.Relative),
            //            //AuthorizationUrl = new Uri("/api/v1/users/Token", UriKind.Relative)
            //            //Scopes = new Dictionary<string, string>
            //            //{
            //            //    { "readAccess", "Access read operations" },
            //            //    { "writeAccess", "Access write operations" }
            //            //}
            //        }
            //    }
            //});
            #endregion

            #region Versioning
            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            //Seperate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => docName.Equals($"v{v}", StringComparison.OrdinalIgnoreCase));
            });
            #endregion

            //If use FluentValidation then must be use this package to show validation in swagger (MicroElements.Swashbuckle.FluentValidation)
            //options.AddFluentValidationRules();
            #endregion
        });
    }

    public static IApplicationBuilder UseSwaggerAndSwaggerUI(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));
        
        //More info : https://github.com/domaindrivendev/Swashbuckle.AspNetCore

        //Swagger middleware for generate "Open API Documentation" in swagger.json
        app.UseSwagger(/*options =>
        {
            options.RouteTemplate = "api-docs/{documentName}/swagger.json";
        }*/);

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");

            #region Customizing
            //// Display
            //options.DefaultModelExpandDepth(2);
            //options.DefaultModelRendering(ModelRendering.Model);
            //options.DefaultModelsExpandDepth(-1);
            //options.DisplayOperationId();
            //options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            //options.EnableDeepLinking();
            //options.EnableFilter();
            //options.MaxDisplayedTags(5);
            //options.ShowExtensions();

            //// Network
            //options.EnableValidator();
            //options.SupportedSubmitMethods(SubmitMethod.Get);

            //// Other
            //options.DocumentTitle = "CustomUIConfig";
            //options.InjectStylesheet("/ext/custom-stylesheet.css");
            //options.InjectJavascript("/ext/custom-javascript.js");
            //options.RoutePrefix = "api-docs";
            #endregion
        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        //app.UseReDoc(options =>
        //{
        //    options.SpecUrl("/swagger/v1/swagger.json");
        //    //options.SpecUrl("/swagger/v2/swagger.json");

        //    #region Customizing
        //    //By default, the ReDoc UI will be exposed at "/api-docs"
        //    //options.RoutePrefix = "docs";
        //    //options.DocumentTitle = "My API Docs";

        //    options.EnableUntrustedSpec();
        //    options.ScrollYOffset(10);
        //    options.HideHostname();
        //    options.HideDownloadButton();
        //    options.ExpandResponses("200,201");
        //    options.RequiredPropsFirst();
        //    options.NoAutoAuth();
        //    options.PathInMiddlePanel();
        //    options.HideLoading();
        //    options.NativeScrollbars();
        //    options.DisableSearch();
        //    options.OnlyRequiredInSamples();
        //    options.SortPropsAlphabetically();
        //    #endregion
        //});

        return app;
    }
}