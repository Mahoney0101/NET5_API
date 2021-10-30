using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class AppSettings : IAppSettings
    {
        public string SSLEncryptedPassword { get; private set; }
        public string SiteSSLCertificatePath { get; private set; }
    }
}
