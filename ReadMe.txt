Bu ASP.NET Core MVC ��z�m� bir web uygulama projesinde ihtiya� duyulabilecek baz� 
third party Javascript - CSS k�t�phaneleriyle (wwwroot -> lib) Clean Blog, Dark 
ve Blue olmak �zere �� �e�it layout (wwwroot -> layouts) i�ermektedir.

Ayr�ca web uygulamas�n�n geli�tirilmesinde production (canl�) ve development (geli�tirme)
ortamlar� Properties -> launchSettings.json dosyas�nda ayr� ayr� tan�mlanm��t�r. 
View'larda yap�lan de�i�ikliklerin web uygulamas� ba�tan ba�lat�lmadan taray�c�dan yeniden 
y�kleme yap�larak g�r�lmesi i�in de ilgili k�t�phane ayn� dosyaya belirtilerek eklenmi�tir.

Scaffolding i�lemleri i�in de Service ve Model kullanan �ablonlar projede 
Templates klas�r� alt�nda bulunmaktad�r.