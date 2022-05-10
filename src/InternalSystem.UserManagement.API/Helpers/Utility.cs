using IntrenalSystem.UserManagement.Model.APIResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Reflection;

namespace InternalSystem.UserManagement.API.Helpers
{
    public static class Utility
    {
        //public static void RegisterServices(this IServiceCollection services)
        //{
        //    // Get class library
        //    var serviceLibraries = Assembly.Load("PuzzleBreaks.Services")
        //        .GetTypes()
        //        .Where(x => x.IsClass && x.GetInterfaces().Any(i => i.Name.Contains("Service")) && x.Namespace.Contains(".Services.Services") && !string.IsNullOrEmpty(x.Name) && x.Name.Contains("Service"))
        //        .ToList();

        //    if (serviceLibraries != null && serviceLibraries.Count > 0)
        //    {
        //        foreach (var service in serviceLibraries)
        //        {
        //            var interfaceType = service.GetInterfaces().FirstOrDefault();
        //            Service.Attributes.ServiceLifetimeAttribute attribute = service.GetCustomAttribute<Services.Attributes.ServiceLifetimeAttribute>();
        //            ServiceLifetime lifetime = attribute == null ? ServiceLifetime.Scoped : attribute.LifeTime;


        //            services.Add(new ServiceDescriptor(interfaceType, service, lifetime));
        //        }
        //    }
        //}

        public static void RegisterRepository(this IServiceCollection services)
        {
            // Get class library
            var repositoryLibraries = Assembly.Load("PuzzleBreaks.Repository")
                .GetTypes()
                .Where(x => x.IsClass && x.GetInterfaces().Any() && x.Namespace.Contains(".Repository.Repository"))
                .ToList();

            if (repositoryLibraries != null && repositoryLibraries.Count > 0)
            {
                foreach (var repository in repositoryLibraries)
                {
                    var interfaceType = repository.GetInterfaces().FirstOrDefault();
                    services.AddScoped(interfaceType, repository);
                }
            }
        }
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            string messages = string.Join(" ", modelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return messages;
        }
        public static IActionResult GetResponse<T>(this ControllerBase controller, JsonResponseModel<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return controller.Ok(response);
                case HttpStatusCode.BadRequest:
                    return controller.BadRequest(response);
                case HttpStatusCode.Unauthorized:
                    return controller.StatusCode(401, response);
                case HttpStatusCode.NotFound:
                    return controller.NotFound(response);
                case HttpStatusCode.InternalServerError:
                    return controller.StatusCode(500, response);
                default:
                    return controller.StatusCode((int)response.StatusCode, response);
            }
        }

    }
}
