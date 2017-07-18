using System;
using System.Collections.Generic;

using Google.Cloud.Language.V1;

namespace TextRecognition.Queries
{
    class NLPQuery
    {
        public List<Objects.Response> GetEntities(string text)
        {
            
            List<Objects.Response> entites = new List<Objects.Response> ();

           

            var client = LanguageServiceClient.Create();
          
                var response = client.AnalyzeEntities(new Document()
                {
                    Content = text,
                    Type = Document.Types.Type.PlainText
                });
            
           
            

            foreach (var entity in response.Entities)
            {
                string[] mentions = new string[entity.Mentions.Count];
                foreach (var mention in entity.Mentions)
                {
                    int index = 0;
                    mentions[index] = mention.Text.Content;
                    index++;
                }
                entites.Add(new Objects.Response(entity.Name, entity.Type.ToString() , entity.Salience,mentions));
            }
            return entites;

        }
        
        
    }
}
