using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobaPay.Helpers
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key  
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        private static readonly Dictionary<string, string> config;
        //Constructor  
        static PaypalConfiguration()
        {
            config = new Dictionary<string, string>();
            config[BaseConstants.HttpConnectionTimeoutConfig] = "30000";
            config[BaseConstants.HttpConnectionRetryConfig] = "3";
            config[BaseConstants.ApplicationModeConfig] = BaseConstants.SandboxMode;

            ClientId = "";
            ClientSecret = "";
        }
        // getting properties from the web.config  
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.GetConfigWithDefaults(config);
        }
        private static string GetAccessToken()
        {
            // getting accesstocken from paypal  
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken  
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}
