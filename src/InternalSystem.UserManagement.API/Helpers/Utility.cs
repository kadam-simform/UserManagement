using InternalSystem.UserManagement.Service.Attributes;
using IntrenalSystem.UserManagement.Model.APIResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Reflection;

namespace InternalSystem.UserManagement.API.Helpers
{
    public static class Utility
    {
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
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                return refreshToken.ReplaceUrlCharacters();
            }
        } 
        public static string ReplaceUrlCharacters(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.Replace(new char[] { '&', '%', '?', '/', '=', '+' }, "$");
        }

        public static string Replace(this string s, char[] separators, string newVal)
        {
            string[] temp;

            temp = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(newVal, temp);
        }

    }
}
