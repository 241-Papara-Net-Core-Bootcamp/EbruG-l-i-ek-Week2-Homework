using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PaparaSecondWeek.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //Response requestle ilgili tüm işlemler HttpContext'ten gelir.
        //Bu yüzden islemleri invoke içinde yazariz.
        public async Task Invoke(HttpContext httpContext)
        {
                //try catch ekleyemedim çünkü dogru cikti vermedi.
                //Hata alınmış mı onun kontrolü yapildi.
                var errormes =httpContext.Response.StatusCode.ToString();
                if (errormes!=null)
                {
                    //Hata alindiysa 500 dön.
                    httpContext.Response.StatusCode = 500;
                    await httpContext.Response.WriteAsync(httpContext.Response.StatusCode.ToString());
                  
                    return;
                }
                else
                {
                }

                await _next(httpContext);  
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
