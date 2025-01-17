using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace myApp
{
    public class Program
    {
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
    }
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
    
    [ApiController]
    [Route("calc")]
    public class GreetingController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get(string formula)
        {
            // Try the following URLs:
            // http://localhost:5000/calc?formula=5
            // http://localhost:5000/calc?formula="Hello".ToUpper()
            // http://localhost:5000/calc?formula="Hello"+5 -> crashes!
            // http://localhost:5000/calc?formula="Hello"%2B5

            var result = await CSharpScript.EvaluateAsync(formula);
            return result.ToString();
        }
    }
}
