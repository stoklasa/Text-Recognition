using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;

using Google.Cloud.Vision.V1;
using Google.Cloud.Language.V1;

namespace TextRecognition.Objects
{
    class NLPQuery
    {

        

        public List<string[]> GetEntities(string text)
        {


            List<string[]> entites = new List<string[]>();


            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntities(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });

            foreach (var entity in response.Entities)
            {
                string[] val = new string[5];
                val[0] = entity.Name;
                val[1] = entity.Type.ToString();
                val[2] = entity.Metadata.Values.ToString();
                val[3] = entity.Salience.ToString();
                val[4] = entity.Mentions.ToString();

                entites.Add(val);
            }
            return entites;

        }
        
        
    }
}
