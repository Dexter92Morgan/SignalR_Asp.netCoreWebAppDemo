using Microsoft.EntityFrameworkCore;
using SignalR_Asp.netCoreWebAppDemo.Context;
using SignalR_Asp.netCoreWebAppDemo.DBClass;
using SignalR_Asp.netCoreWebAppDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SignalRConnection");
builder.Services.AddDbContext<SignalRDbContext>(option =>
option.UseSqlServer(connectionString)
);

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);

builder.Services.AddScoped<ConnectionManager>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<MessageHub>("/messages");



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
});

app.Run();


//Reference
//https://www.youtube.com/watch?v=yr_-MArHXUM&list=PLThyvG1mlMzltDxuQj0uQw1TDu1gJUNeG&ab_channel=CodeOpinion