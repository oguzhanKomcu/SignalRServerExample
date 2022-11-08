using SingalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//CORS politikalarý sunucularýmýza gelen bilinmedik clientlardan gelen istekleri tarayýcýlar tarafdýndan otomatik engelleyen bir güvenlik önlemdiir.Biz CORS politikalarý manuel
// olarak deðiþtirerek  var olan tarayýcýlar tarafýndan getirilen bu güvenlik önlemini seviyesini idareli bir þekilde düþürürüz.Çünkü beklediðimiz clientlardan isteklerin gelmesi için bu gerekir.
//Policy kurallarým ile hangi clientlarýn eriþebileceðine karar verebilriiz.Biz þimdilik bir genel izin verdik.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)

));

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


app.UseCors();//cors politikalarý için ekledik
app.UseRouting();
app.UseEndpoints(endpoints =>
{

    //Uygulamada maphub tarafýndan bir istek geliyorsa bu sýnýf tarafýndan karþýlanýcak diyoruz.
    endpoints.MapHub<MyHub>("/myhub");
});


app.Run();
