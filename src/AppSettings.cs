namespace API;

public class AppSettings : IAppSettings
{
    public string? SSLEncryptedPassword { get; private set; }
    public string? SiteSSLCertificatePath { get; private set; }
}
