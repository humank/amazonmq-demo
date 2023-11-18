using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace amazonmq_consumer;

public class SSMUtils
{

    public static string? GetValue(string parameterName)
    {

        // 使用默認的 AWS 憑證和配置創建客戶端
        var ssmClient = new AmazonSimpleSystemsManagementClient();
        string? value = null;

        try
        {
            var request = new GetParameterRequest()
            {
                Name = parameterName,
                WithDecryption = false // 標準存儲機制，無需解密
            };

            var response = ssmClient.GetParameterAsync(request).Result;
            value = response.Parameter.Value;
            Console.WriteLine((string?)$"Parameter Value: {value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching parameter: {ex.Message}");
        }

        return value;
    }

}