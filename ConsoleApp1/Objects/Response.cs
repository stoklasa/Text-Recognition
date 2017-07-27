using System.Collections.Generic;
namespace TextRecognition.Objects
{
    public class Response
    {
        public string name { get; set; }
        public string type { get; set; }
        public float salience { get; set; }
        public List<string> mentions { get; set; }

        public string[] mentionsAsString { get; set; }

        public Response(string name, string type, float salience,List<string>mentions)
        {
            this.name = name;
            this.type = type;
            this.salience = salience;
            this.mentions = mentions;
            for(int i = 0; i <mentions.Count; i++)
            {
                mentionsAsString[i] = mentions[i];
            }
        }
        public static List<string> ResponsesToString(List<Response>resp){
            List<string> values = new List<string>();
            foreach(var val in resp)
            {
                values.Add(val.name);
                values.Add(val.salience.ToString());
                for(int i = 0; i < val.mentionsAsString.Length;i++) {
                    values.Add(val.mentions[i]);
                }
                values.AddRange(val.mentionsAsString);
            }
            return values;
        }
    }

}
