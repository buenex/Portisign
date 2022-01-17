using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Json;

namespace Portifolio_API.Utils{
    public class ReadJson{
        public static void GetConfig()
        {
            string json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"/Configuration/config.json");

            JsonObject jsonConfigurations = JsonConvert.DeserializeObject<JsonObject>(json);

            Configuration.host = jsonConfigurations["host"];
            Configuration.email = jsonConfigurations["email"];
            Configuration.senha = jsonConfigurations["senha"];
            Configuration.enable_ssl = jsonConfigurations["enable_ssl"];
        }
    }
}