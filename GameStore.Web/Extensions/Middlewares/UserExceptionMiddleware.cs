using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GameStore.Web.Extensions.Middlewares
{
    public class UserExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public UserExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/404");
                }
            }
            catch (UserException)
            {
                context.Response.Redirect("/404");
            }
        }

    }
}
