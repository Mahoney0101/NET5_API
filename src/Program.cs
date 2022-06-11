var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.ConfigureHttpsDefaults(options => options.SslProtocols = SslProtocols.Tls12);

    ArgumentNullException.ThrowIfNull(serverOptions.ConfigurationLoader);
    serverOptions.ConfigurationLoader.Endpoint("Https", endPointConfiguration =>
    {
        var _appSettings = context.Configuration.Get<API.AppSettings>(
            options => options.BindNonPublicProperties = true);
        var _SSLSertificateEncryptedPassword = _appSettings.SSLEncryptedPassword;
        ArgumentNullException.ThrowIfNull(_appSettings.SiteSSLCertificatePath);
        endPointConfiguration.ListenOptions.UseHttps(_appSettings.SiteSSLCertificatePath, _SSLSertificateEncryptedPassword);
    });
});

ConfigurationManager c_configuration = builder.Configuration;


var _appSettings = c_configuration.Get<API.AppSettings>(options => options.BindNonPublicProperties = true);

builder.Services.AddSingleton<API.AppSettings>()
    .AddSingleton<IBookstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value)
    .AddSingleton<BookService>()
    .AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value)
    .AddSingleton<UserService>()
    .AddSingleton<API.IAppSettings>(_appSettings);


builder.Services.Configure<BookstoreDatabaseSettings>(c_configuration.GetSection(nameof(BookstoreDatabaseSettings)))
    .Configure<UserDatabaseSettings>(c_configuration.GetSection(nameof(UserDatabaseSettings)));

builder.Services.AddControllers();

builder.Services.AddHealthChecks();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsEnvironment("local"))
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
