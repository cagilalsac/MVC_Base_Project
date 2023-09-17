Bu ASP.NET Core MVC Çözümü bir web uygulama projesinde ihtiyaç duyulabilecek bazý 
third party Javascript - CSS kütüphaneleriyle (wwwroot -> lib) Clean Blog, Dark 
ve Blue olmak üzere üç çeþit layout (wwwroot -> layouts) içermektedir.

Ayrýca web uygulamasýnýn geliþtirilmesinde production (canlý) ve development (geliþtirme)
ortamlarý Properties -> launchSettings.json dosyasýnda ayrý ayrý tanýmlanmýþtýr. 
View'larda yapýlan deðiþikliklerin web uygulamasý baþtan baþlatýlmadan tarayýcýdan yeniden 
yükleme yapýlarak görülmesi için de ilgili kütüphane ayný dosyaya belirtilerek eklenmiþtir.

Scaffolding iþlemleri için de Service ve Model kullanan þablonlar projede 
Templates klasörü altýnda bulunmaktadýr.