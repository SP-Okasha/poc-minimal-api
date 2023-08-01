using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Model.Common
{
    public class ApplicationSettingsModel
    {
        public const string ApplicationSettings = "ApplicationSettings";
        public ConnectionString ConnectionString { get; set; }
        public JWT Jwt { get; set; }

    }

    public class ConnectionString
    {
        public string SampleCRUD_Employee { get; set; }
    }

    public class JWT
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ValidityInMinutes { get; set; }
    }

}
