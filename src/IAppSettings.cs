using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public interface IAppSettings
    {
        public string SSLEncryptedPassword { get; }
        public string SiteSSLCertificatePath { get; }
    }
}
