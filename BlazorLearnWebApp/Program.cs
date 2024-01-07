using BlazorLearnWebApp.Components;
using BlazorLearnWebApp.Service;
using BootstrapBlazor.Components;
using FreeSql;

var builder = WebApplication.CreateBuilder(args);

IFreeSql fsql = new FreeSql.FreeSqlBuilder()
	.UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=document.db")
	.UseAutoSyncStructure(true) //automatically synchronize the entity structure to the database
	.Build(); //be sure to define as singleton mode
BaseEntity.Initialization(fsql, null);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddBootstrapBlazor();
builder.Services.AddScoped(typeof(IDataService<>), typeof(FreesqlDataService<>));
builder.Services.AddSingleton(typeof(ILookupService), typeof(LookupService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
