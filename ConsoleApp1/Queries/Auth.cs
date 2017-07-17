using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;

namespace TextRecognition.Objects

{
    class Auth
    {
        public static async Task Run()
        {

            string CRED_PATH = new Factory.FileFactory().GetFolder() + @"\api_key\My First Project-ca909aeb1219.json";


            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CRED_PATH);

            GoogleCredential credential = await GoogleCredential.GetApplicationDefaultAsync();

            var service = new Oauth2Service(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = "Oauth2 Sample",
            }
            );

        }
    }
}
