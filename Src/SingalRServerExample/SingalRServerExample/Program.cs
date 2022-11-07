using SingalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//SingalR modulünü çaðýrarak iþlevsel hale getiriyoruz.
builder.Services.AddSignalR();
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
app.UseEndpoints(endpoints =>
{

    //Uygulamada maphub tarafýndan bir istek geliyorsa bu sýnýf tarafýndan karþýlanýcak diyoruz.
    endpoints.MapHub<MyHub>("/myhub");
}


);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
