using SingalRServerExample.Business;
using SingalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//CORS politikalar� sunucular�m�za gelen bilinmedik clientlardan gelen istekleri taray�c�lar tarafd�ndan otomatik engelleyen bir g�venlik �nlemdiir.Biz CORS politikalar� manuel
// olarak de�i�tirerek  var olan taray�c�lar taraf�ndan getirilen bu g�venlik �nlemini seviyesini idareli bir �ekilde d���r�r�z.��nk� bekledi�imiz clientlardan isteklerin gelmesi i�in bu gerekir.
//Policy kurallar�m ile hangi clientlar�n eri�ebilece�ine karar verebilriiz.Biz �imdilik bir genel izin verdik.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)

));

//Business s�nf�nda ctor da istenilen �hubcontext .net i�inden gelen signalr k�t�phanesinden al�nacak ve nesnesi �retilip verilecek.
builder.Services.AddTransient<MyBusiness>();

//SingalR modul�n� �a��rarak i�levsel hale getiriyoruz.
builder.Services.AddSignalR();
builder.Services.AddControllers();  


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCors();//cors politikalar� i�in ekledik
app.UseRouting();
app.UseEndpoints(endpoints =>
{

    //Uygulamada maphub taraf�ndan bir istek geliyorsa bu s�n�f taraf�ndan kar��lan�cak diyoruz.
    endpoints.MapHub<MyHub>("/myhub");
    endpoints.MapControllers(); //AP� CONTROLLER ��LEMELER� ���N 
});


app.Run();
