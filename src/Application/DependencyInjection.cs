namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;


        // // Add services to the container.
        // builder.Services.AddSignalR();

        // var app = builder.Build();

        // // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }

        // app.UseHttpsRedirection();

        // app.UseAuthorization();

        // app.MapHub<GameHub>("");

        // app.Run();
    }
}

