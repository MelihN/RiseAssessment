using RAUI.ServiceHelper;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IHostedService, KafkaConsumerService>();
// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
