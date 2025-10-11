using GreenHouseSystem.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseSystem.Services
{
    public class LegacyOpcDaSimulatorServiceStarter
    {
        private readonly RequestDelegate _next;
        private int i = 0; // счетчик запросов
        public LegacyOpcDaSimulatorServiceStarter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDataProvider idp)
        {
            i++;
            httpContext.Response.ContentType = "text/html;charset=utf-8";
            var test = await idp.ReadValueAsync();
            await httpContext.Response.WriteAsync($"Запрос {i}; Counter: {test}");
        }
    }
}
