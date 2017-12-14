using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace AzureTextAnalyticsAPI
{  
    public struct Document
    {
        public string id;
        public string text;
    }

  
    public partial class DetectLanguage : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "d303022eb6154ea7acd5b8c16b6995b8";

        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        const string uriBase = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages";


        /// <summary>
        /// Queries the language for a set of document and outputs the information to the console.
        /// </summary>
        public async Task<string> GetLanguage(List<Document> documents)
       
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

                body = body + "{ \"id\":\"" + doc.id + "\",  \"text\": \"" + doc.text + "\"   }";
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

            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine("\nResponse:\n");
            //Console.WriteLine(JsonPrettyPrint(result));
            lbltext.Text = JsonPrettyPrint(result);
            return result;
        }

        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
       public static string JsonPrettyPrint(string json)
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

        protected void BtnDetectLanuage_Click(object sender, EventArgs e)
        {
            List<Document> documents = new List<Document>();
            documents.Add(new Document() { id = "1", text = "This is a document written in English." });
            documents.Add(new Document() { id = "2", text = "Este es un document escrito en Español." });
            documents.Add(new Document() { id = "3", text = "这是一个用中文写的文件" });

            var InvokeData = GetLanguage(documents);
            InvokeData.Wait();
            string reciveData = InvokeData.Result;


//        string jsonString = @"
//        {
//            ""JsonValues"": {
//                ""id"": ""MyID"",
//                ""values"": {
//                    ""value1"": {
//                        ""id"": ""100"",
//                        ""diaplayName"": ""MyValue1""
//                    },
//                    ""value2"": {
//                        ""id"": ""200"",
//                        ""diaplayName"": ""MyValue2""
//                    }
//                }
//            }
//        }";

          //  string jsonString = @"{""documents"":[{""id"":""1"",""detectedLanguages"":[{""name"":""English"",""iso6391Name"":""en"",""score"":1.0}]}]}";
          //var abc=  JsonConvert.DeserializeObject<Document>(jsonString);

         // Document tmp = JsonConvert.DeserializeObject<Document>(jsonString);
          //foreach (string typeStr in tmp)
          //{
          //    // Do something with typeStr
          //}

            //JavaScriptSerializer js = new JavaScriptSerializer();
           // Document[] persons = js.Deserialize<Document[]>(jsonrecord);

        }


    }
}