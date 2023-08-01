using Sample.CRUD.API.Extension;

var builder = WebApplication.CreateBuilder(args);

AppConfigurationExtension.RegisterAllServices(builder.Services, builder.Configuration);
var app = builder.Build();
AppConfigurationExtension.RegisterMiddleWares(app);
AppConfigurationExtension.RegisterRoutes(app);
app.Run();
