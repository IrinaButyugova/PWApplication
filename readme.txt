To launch PWBlazorApplication project you need:
- execute from solution directory:  docker build -f PWBlazorApplication\Dockerfile --force-rm -t pw_blazor_application .
- execute from PWBlazorApplication: docker-compose up

To launch PWApplication project you need:
- write db conection string to \PWApplication\appsettings.Development.json into ConnectionStrings/DefaultConnection
- execute db migrations 
 
blazor review:
1)В солюшене есть два web клиента: PWBlazorApplication и PWApplication(MVC), которые взаимодействуют с базой данных через сервисы PWApplication.BLL. 
PWBlazorApplication и PWApplication(MVC) не взимодействуют друг с другом и не знают о существовании друг друга.
PWBlazorApplication переиспользует сервисы PWApplication.BLL, которые были написаны во время обучения 2021 году, для работы с базой.
2)Blazor Server был выбран т к 
	2.1) Это прложение для финансового сектора, это не выглядит безопастным если все файлы приложения будут доступны пользователю
	2.2) Приложение должно всегда иметь доступ к базе данных
	2.3) Приложение не будет ограничено браузером
3)Вся работа с авторизацией и аутентификацией может проводиться в собственном провайдере, который необходимо отнаследовать от AuthenticationStateProvider и зарегистрировать в DI. 
Использовать для получения контекста IHttpContextAccessor и при необходимости создать свой сервис для работы с состоянием юзера. Этим можно заменить всяческие BlazorCookieLoginMiddleware.

При использовании провайдера при вызове signInManager.PasswordSignInAsync из AuthenticationStateProvider выпадает исключение "Headers are read-only, response has already started", т к для устатовления кук нужен HTTP response
https://github.com/dotnet/aspnetcore/issues/21199
https://github.com/dotnet/aspnetcore/issues/13601