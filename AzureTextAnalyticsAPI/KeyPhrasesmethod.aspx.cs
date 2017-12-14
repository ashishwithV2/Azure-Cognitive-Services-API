using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureTextAnalyticsAPI
{
    class subjects
    {
        [JsonProperty("language")]
        public int language { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("subject_class")]
        public int text { get; set; }
       
       
    }

    public partial class KeyPhrasesmethod1 : System.Web.UI.Page
    {
        public struct Document
    {
        public string language;
        public string id;
        public string text;
    }
        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "d303022eb6154ea7acd5b8c16b6995b8";
        const string uriBase = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public async Task<string> GetKeyPhrases(List<Document> documents)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            HttpResponseMessage response;

            // Compose request.
            string body = "";
            foreach (Document doc in documents)
            {
                if (!string.IsNullOrEmpty(body))
                {
                    body = body + ",";
                }

                body = body + "{ \"language\": \"" + doc.language + "\", \"id\":\"" + doc.id + "\",  \"text\": \"" + doc.text + "\"   }";
            }

            body = "{  \"documents\": [" + body + "] }";

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uriBase, content).ConfigureAwait(false);
            }

            // Get the JSON response
            string result = await response.Content.ReadAsStringAsync();

            //Console.WriteLine("\nResponse:\n");
            //Console.WriteLine(JsonPrettyPrint(result));
           // Label1.Text = JsonPrettyPrint(result);
            return result;
        }
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }


       
        protected void BtnextractKey_Click(object sender, EventArgs e)
        {
            List<Document> documents = new List<Document>();
            documents.Add(new Document() { language = "en", id = "1", text = "I really enjoy the new XBox One S. It has a clean look, it has 4K/HDR resolution and it is affordable." });
            documents.Add(new Document() { language = "es", id = "2", text = "Si usted quiere comunicarse con Carlos, usted debe de llamarlo a su telefono movil. Carlos es muy responsable, pero necesita recibir una notificacion si hay algun problema." });
            documents.Add(new Document() { language = "en", id = "3", text = "The Grand Hotel is a new hotel in the center of Seattle. It earned 5 stars in my review, and has the classiest decor I've ever seen." });


            ////var InvokeData = GetKeyPhrases(documents);
            //InvokeData.Wait();
            //string reciveData = InvokeData.Result;
            
            //var obj = JObject.Parse(reciveData);
            //var Val = obj["documents"];
            //foreach (var item in Val)
            //{
            
            //   // Label1.Text = Convert.ToString(ScoredProbabilities);
            //}
            //subjects[] arr = JObject.Parse(reciveData)["subjects"].ToObject<subjects[]>();

            //string jsondting = @"{"documents":[{"keyPhrases":["HDR resolution","new XBox","clean look"],"id":""1""}]}";

            //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            //Dictionary<string, object> childRow;
            //foreach (DataRow row in table.Rows)
            //{
            //    childRow = new Dictionary<string, object>();
            //    foreach (DataColumn col in table.Columns)
            //    {
            //        childRow.Add(col.ColumnName, row[col]);
            //    }
            //    parentRow.Add(childRow);
            //}
            //return jsSerializer.Serialize(parentRow);
            
        }
       
    }
}