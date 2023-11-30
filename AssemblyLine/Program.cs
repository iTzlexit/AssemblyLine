
using AssemblyLine.ApplicationLayer.Operations.Interfaces; 

using AssemblyLine.ApplicationLayer.PluginInterfaces;
using AssemblyLine.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Plugins.InMemory;
using AssemblyLine.ApplicationLayer.Devices.Interfaces;
using ApplicationLayer.Assembly;
using AssemblyLine.ApplicationLayer.Assembly.Interface;
using AssemblyLine.ApplicationLayer.Devices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<IOperationRepository, OperationRepository>();
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IAssemblyRepository, AssemblyRepository>();


//Device use cases 
builder.Services.AddTransient<IAddDeviceUseCase, AddDeviceUseCase>(); 
builder.Services.AddTransient<IFetchDeviceTypes, FetchDeviceTypes>(); 

//Operations Use Cases

builder.Services.AddTransient<IAddOperationUseCase, AddOperationUseCase>();
builder.Services.AddTransient<IDeleteOperationUseCase, DeleteOperationUseCase>();
builder.Services.AddTransient<IGetListOfOperationsUseCase, GetListOfOperationsUseCase>();
builder.Services.AddTransient<IFetchAssembliesUseCase, FetchAssembliesUseCase>(); 

//----------Pipeline ----------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
