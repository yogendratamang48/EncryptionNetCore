using System.IO;
using Microsoft.Extensions.Configuration;

namespace EncryptionTest
{
    public class EncryptionConfigLoad
    {
public EncryptionConfig LoadEncryptionFromJson(string filename, string section)
{
    var builder=new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(filename, optional:false, reloadOnChange:true)
           .AddEnvironmentVariables();
           IConfiguration configuration=builder.Build();
           var encryptionConfig=new EncryptionConfig();
           configuration.GetSection(section).Bind(encryptionConfig);
           return encryptionConfig;

}
    }

}