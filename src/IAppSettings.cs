namespace API;

public interface IAppSettings
{
    public string? SSLEncryptedPassword { get; }
    public string? SiteSSLCertificatePath { get; }
}
