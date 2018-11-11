using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Web.Extensions.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GameStore.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UserExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<UserExceptionMiddleware>();
        }
    }

}
