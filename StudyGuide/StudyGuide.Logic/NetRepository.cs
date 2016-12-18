using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.Logic.DTO;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;

namespace StudyGuide.Logic
{
    public static class NetRepository
    {
        public static async Task<string> Translate(string word)
        {
            const string ApiKeyYandex = "trnsl.1.1.20161203T190734Z.48c01778001360ab.57265feec38e9cd1b9b9f5ea83440c4e4fad1a91";

            var query = string.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&text={1}&lang=en-ru&format=plain",
                 ApiKeyYandex, word);

            using (var client = new HttpClient())
            {
               string result = await client.GetStringAsync(query);
               var translation = JsonConvert.DeserializeObject<TranslatingData>(result);
               return translation.Text[0];
            }
        }

        public static async Task<string> GetDefinition(string word)
        {
            var query = string.Format("http://en.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles={0}&redirects=true", word);

            using (var webClient = new WebClient())
            {
                var pageSourceCode = await webClient.DownloadStringTaskAsync(query);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(pageSourceCode);
                var fnode = doc.GetElementsByTagName("extract")[0];
                string ss = fnode.InnerText;
                ss = ss.Substring(ss.IndexOf("</b>"), ss.IndexOf("</p>") - ss.IndexOf("</b>"));
                Regex regex = new Regex("\\<[^\\>]*\\>");
                ss = regex.Replace(ss, string.Empty);
                string result = String.Format(ss);
                return (result);
            }
        }
    }
}
