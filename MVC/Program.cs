using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization 1
// Web uygulamasýnýn bölgesel (kültür) ayarý aþaðýdaki þekilde tek seferde konfigüre edilerek tüm projenin
// bu ayarý kullanmasý saðlanabilir, dolayýsýyla veri formatlama veya dönüþtürme gibi iþlemlerde her seferinde
// CultureInfo objesinin kullaným gereksinimi ortadan kalkar. Bu þekilde sadece tek bir bölgesel ayar projede
// kullanýlabilir. Önce içerisinde uygulamamýzýn bölgesel ayar bilgisini içeren örneðin Ýngilizce için
// CultureInfo objemizi içeren bir liste oluþturuyoruz.
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") // eðer uygulama Türkçe olacaksa CultureInfo constructor'ýnýn
                                // parametresini ("tr-TR") yapmak yeterlidir.
};
// Daha sonra builder (uygulamayý önce inþa eden) objesinin servislerine Congifure methodu üzerinden
// options Action delegesini kullanarak default (varsayýlan) istek kültürüne (DefaultRequestCulture) 
// RequestCulture objesini yukarýda oluþturduðumuz cultures listesinin ilk elemanýnýn adý üzerinden
// oluþturarak atýyoruz, desteklenen kültürler (SupportedCultures) ve desteklenen arayüz kültürlerine de
// (SupportedUICultures) yukarýda oluþturduðumuz cultures listesini atýyoruz.
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
#region IoC (Inversion of Control) Container
// Alternatif olarak Business katmanýnda Autofac ve Ninject gibi kütüphaneler de kullanýlabilir.
// Unable to resolve service hatalarý burada çözümlenmelidir.
// AddScoped: istek (request) boyunca objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve yanýt (response) dönene kadar bu obje hayatta kalýr.
// AddSingleton: web uygulamasý baþladýðýnda objenin referansný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve uygulama çalýþtýðý (IIS üzerinden uygulama durdurulmadýðý veya yeniden baþlatýlmadýðý) sürece bu obje hayatta kalýr.
// AddTransient: istek (request) baðýmsýz ihtiyaç olan objenin referansýný (genelde interface veya abstract class) kullandýðýmýz her yerde bu objeyi new'ler.
// Genelde AddScoped methodu kullanýlýr.
#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Localization 2
// Yukarýda localization'ý konfügure ettikten ve uygulamamýzý da yukarýdaki konfigürasyonlara
// göre build (derleme, inþa) ettikten sonra oluþan app objesinin UseRequestLocalization
// methodunu kullanarak içerisinde RequestLocalizationOptions tipindeki objenin default (varsayýlan)
// istek kültürüne (DefaultRequestCulture) RequestCulture objesini yukarýda oluþturduðumuz cultures listesinin
// ilk elemanýnýn adý üzerinden oluþturarak atýyoruz, desteklenen kültürler (SupportedCultures) ve desteklenen
// arayüz kültürlerine de (SupportedUICultures) yukarýda oluþturduðumuz cultures listesini atýyoruz,
// böylece web uygulamamýzýn bölgesel ayarýný Ýngilizce yapmýþ olacaðýz. 
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
