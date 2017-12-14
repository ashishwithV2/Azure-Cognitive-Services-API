using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureTextAnalyticsAPI
{
   
   
    public partial class KeyPhrasesmethod : System.Web.UI.Page
    {
        public struct Document
        {
            public string language;
            public string id;
            public string text;
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {

        }



         // Actual API COde
        // Replace the subscriptionKey string value with your valid subscription key.
            const string subscriptionKey = "d303022eb6154ea7acd5b8c16b6995b8";

        // Replace or verify the region.
        //
        // You must use the same region in your REST API call as you used to obtain your subscription keys.
        // For example, if you obtained your subscription keys from the westus region, replace 
        // "westcentralus" in the URI below with "westus".
        //
        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        //
        // https://westcentralus.api.cognitive.microsoft.com/text/analytics/v2.0
          const string uriBase = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
        /// <summary>
        /// Queries the language for a set of document and outputs the information to the console.
        /// </summary>
       public async Task<string> GetSentiment(List<Document> documents)
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
                response = await client.PostAsync(uriBase, content).ConfigureAwait(false); ;
            }

            // Get the JSON response
            string result = await response.Content.ReadAsStringAsync();

           // Console.OutputEncoding = System.Text.Encoding.UTF8;
           // Console.WriteLine("\nResponse:\n");
           // Console.WriteLine(JsonPrettyPrint(result));
            Label1.Text = jsonprettyprint(result);
            return result;
        }


        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
       static string jsonprettyprint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentlength = 3;

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
                            sb.Append(new string(' ', ++offset * indentlength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentlength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentlength));
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
    

        ////static async Task<string> MakeRequest()
        ////{
        ////    var client = new HttpClient();
        ////    var queryString = HttpUtility.ParseQueryString(string.Empty);

        ////    // Request headers
        ////    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "50617948aab545e9a91bd91a0e8bc018");

        ////    var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment?" + queryString;

        ////    HttpResponseMessage response;
        ////    string body = "";

        ////    List<Document> documents = new List<Document>();
        ////    documents.Add(new Document() { language = "en", id = "1", text = "I really enjoy the new XBox One S. It has a clean look, it has 4K/HDR resolution and it is affordable." });
        ////    documents.Add(new Document() { language = "es", id = "2", text = "Este ha sido un dia terrible, llegué tarde al trabajo debido a un accidente automobilistico." });
        ////     foreach (Document doc in documents)
        ////     {
        ////         if (!string.IsNullOrEmpty(body))
        ////         {
        ////             body = body + ",";
        ////         }

        ////         body = body + "{ \"language\": \"" + doc.language + "\", \"id\":\"" + doc.id + "\",  \"text\": \"" + doc.text + "\"   }";
        ////     }

        ////     body = "{  \"documents\": [" + body + "] }";

        ////     // Request body
        ////     byte[] byteData = Encoding.UTF8.GetBytes(body);

        ////    //// Request body
        ////    //byte[] byteData = Encoding.UTF8.GetBytes("{body}");
        ////    string output = "";

        ////    using (var content = new ByteArrayContent(byteData))
        ////    {
        ////        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        ////        response = await client.PostAsync(uri, content).ConfigureAwait(false);

        ////        // Get the JSON response
        ////        output = await response.Content.ReadAsStringAsync();
        ////        //output = Convert.ToString(response);
        ////    }
           

        ////    return output;

        ////}

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            // Previous Code

            List<Document> documents = new List<Document>();
            documents.Add(new Document() { language = "en", id = "1", text = "I really enjoy the new XBox One S. It has a clean look, it has 4K/HDR resolution and it is affordable." });
            documents.Add(new Document() { language = "es", id = "2", text = "Este ha sido un dia terrible, llegué tarde al trabajo debido a un accidente automobilistico." });

            var InvokeData = GetSentiment(documents);
            InvokeData.Wait();
            string reciveData = InvokeData.Result;
            //InvokeData.Wait();
            //string reciveData = InvokeData.Result;
            //;
            //var InvokeData = MakeRequest();
            ////InvokeData.Wait();
            //string reciveData = InvokeData.Result;
            //Label1.Text = reciveData;
        }
    }
}