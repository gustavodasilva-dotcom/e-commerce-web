using Microsoft.Extensions.Configuration;

namespace Loja.Web.Infra.CrossCutting.Config
{
    public static class Settings
    {
        public static IConfiguration Configuration { get; set; }
    }
}
