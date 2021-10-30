using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSystemdConsole(options =>
                {
                    options.TimestampFormat = ".yyyy-MM-dd HH:mm:ss.fff";
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureKestrel((context, serverOptions) =>
                {
                    serverOptions.ConfigureHttpsDefaults(options => options.SslProtocols = SslProtocols.Tls12);

                    serverOptions.ConfigurationLoader.Endpoint("Https", endPointConfiguration =>
                    {
                        var _appSettings = context.Configuration.Get<AppSettings>(
                            options => options.BindNonPublicProperties = true);
                        var _SSLSertificateEncryptedPassword = _appSettings.SSLEncryptedPassword;

                        endPointConfiguration.ListenOptions.UseHttps(_appSettings.SiteSSLCertificatePath, _SSLSertificateEncryptedPassword);
                    });
                });
            });
    }
}
