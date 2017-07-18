using System.Collections.Generic;
namespace TextRecognition.Objects
{
    public class Response
    {
        public string name { get; set; }
        public string type { get; set; }
        public float salience { get; set; }
        public string[] mentions { get; set; }

        public Response(string name, string type, float salience, string[] mentions)
        {
            this.name = name;
            this.type = type;
            this.salience = salience;
            this.mentions = mentions;
        }
        public List<string> ResponsesToString(List<Response>resp){
            List<string> values = new List<string>();
            foreach(var val in resp)
            {
                values.Add(val.name);
                values.Add(val.salience.ToString());
                for(int i = 0; i < val.mentions.Length;i++) {
                    values.Add(val.mentions[i]);
                }
            }
            return values;
        }
    }

}
