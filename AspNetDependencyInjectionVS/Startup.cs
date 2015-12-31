namespace AspNetDependencyInjectionVS
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISingletonService, MyService>();
            services.AddScoped<IScopedService, MyService>();
            services.AddScoped<IOtherService, MyOtherService>();           
            services.AddTransient<ITransientService, MyService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run((RequestDelegate)(async context =>
            {
                var singleton1 = context.RequestServices.GetService<ISingletonService>();
                var singleton2 = context.RequestServices.GetService<ISingletonService>();
                
                var scoped1 = context.RequestServices.GetService<IScopedService>();
                var scoped2 = context.RequestServices.GetService<IScopedService>();
                
                var transient1 = context.RequestServices.GetService<ITransientService>();
                var transient2 = context.RequestServices.GetService<ITransientService>();

                await context.Response.WriteAsync(
                    "<html><body><strong>Default Container</strong><br><br>" +
                    $"ReferenceEquals(singleton1, singleton2): {object.ReferenceEquals(singleton1, singleton2)}<br>" +
                    $"ReferenceEquals(scoped1, scoped2): {object.ReferenceEquals(scoped1, scoped2)}<br>" +
                    $"ReferenceEquals(transient1, transient2): {object.ReferenceEquals(transient1, transient2)}<br><br>" +
                    $"Singleton Id: {singleton1.Id}, Created: {singleton1.Created}, OtherService: {singleton1?.OtherService?.Id.ToString() ?? "null"}<br><br>" +
                    $"Scoped Id: {scoped1.Id}, Created: {scoped1.Created}, OtherService: {scoped1?.OtherService?.Id.ToString() ?? "null"}<br><br>" +
                    $"Transient 1 Id: {transient1.Id}, Created: {transient1.Created}, OtherService: {transient1?.OtherService?.Id.ToString() ?? "null"}<br>" +
                    $"Transient 2 Id: {transient2.Id}, Created: {transient2.Created}, OtherService: {transient2?.OtherService?.Id.ToString() ?? "null"}" +
                    "</body></html>");
            }));
        }
    }
}