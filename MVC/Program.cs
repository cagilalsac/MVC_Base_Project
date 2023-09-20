using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization 1
// Web uygulamas�n�n b�lgesel (k�lt�r) ayar� a�a��daki �ekilde tek seferde konfig�re edilerek t�m projenin
// bu ayar� kullanmas� sa�lanabilir, dolay�s�yla veri formatlama veya d�n��t�rme gibi i�lemlerde her seferinde
// CultureInfo objesinin kullan�m gereksinimi ortadan kalkar. Bu �ekilde sadece tek bir b�lgesel ayar projede
// kullan�labilir. �nce i�erisinde uygulamam�z�n b�lgesel ayar bilgisini i�eren �rne�in �ngilizce i�in
// CultureInfo objemizi i�eren bir liste olu�turuyoruz.
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") // e�er uygulama T�rk�e olacaksa CultureInfo constructor'�n�n
                                // parametresini ("tr-TR") yapmak yeterlidir.
};
// Daha sonra builder (uygulamay� �nce in�a eden) objesinin servislerine Congifure methodu �zerinden
// options Action delegesini kullanarak default (varsay�lan) istek k�lt�r�ne (DefaultRequestCulture) 
// RequestCulture objesini yukar�da olu�turdu�umuz cultures listesinin ilk eleman�n�n ad� �zerinden
// olu�turarak at�yoruz, desteklenen k�lt�rler (SupportedCultures) ve desteklenen aray�z k�lt�rlerine de
// (SupportedUICultures) yukar�da olu�turdu�umuz cultures listesini at�yoruz.
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
#region IoC (Inversion of Control) Container
// Alternatif olarak Business katman�nda Autofac ve Ninject gibi k�t�phaneler de kullan�labilir.
// Unable to resolve service hatalar� burada ��z�mlenmelidir.
// AddScoped: istek (request) boyunca objenin referans�n� (genelde interface veya abstract class) kulland���m�z yerde obje (somut class'tan olu�turulacak)
// bir kere olu�turulur ve yan�t (response) d�nene kadar bu obje hayatta kal�r.
// AddSingleton: web uygulamas� ba�lad���nda objenin referansn� (genelde interface veya abstract class) kulland���m�z yerde obje (somut class'tan olu�turulacak)
// bir kere olu�turulur ve uygulama �al��t��� (IIS �zerinden uygulama durdurulmad��� veya yeniden ba�lat�lmad���) s�rece bu obje hayatta kal�r.
// AddTransient: istek (request) ba��ms�z ihtiya� olan objenin referans�n� (genelde interface veya abstract class) kulland���m�z her yerde bu objeyi new'ler.
// Genelde AddScoped methodu kullan�l�r.
#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Localization 2
// Yukar�da localization'� konf�gure ettikten ve uygulamam�z� da yukar�daki konfig�rasyonlara
// g�re build (derleme, in�a) ettikten sonra olu�an app objesinin UseRequestLocalization
// methodunu kullanarak i�erisinde RequestLocalizationOptions tipindeki objenin default (varsay�lan)
// istek k�lt�r�ne (DefaultRequestCulture) RequestCulture objesini yukar�da olu�turdu�umuz cultures listesinin
// ilk eleman�n�n ad� �zerinden olu�turarak at�yoruz, desteklenen k�lt�rler (SupportedCultures) ve desteklenen
// aray�z k�lt�rlerine de (SupportedUICultures) yukar�da olu�turdu�umuz cultures listesini at�yoruz,
// b�ylece web uygulamam�z�n b�lgesel ayar�n� �ngilizce yapm�� olaca��z. 
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
});
#endregion

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
