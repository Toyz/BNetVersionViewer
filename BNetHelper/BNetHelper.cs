using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BNetHelper
{
    public class BNetHelper
    {
        private WebClient bnetWebClient;

        public BNetHelper()
        {
            bnetWebClient = new WebClient();
        }

        public (List<Dictionary<string, string>> games, List<string> headers) GetData(string gameCode, DownloadMode mode)
        {
            var url = $"http://us.patch.battle.net:1119/{gameCode}/{mode.ToString()}?nocache={DateTime.Now.Millisecond}";

            List<string> fileData = bnetWebClient.DownloadString(url).Trim().Lines().ToList();

            List<Dictionary<string, string>> CreatedFileData = new List<Dictionary<string, string>>();
            string[] headers = fileData[0].Split('|');
            fileData.RemoveAt(0);

            for(int i = 0; i < headers.Length; i++)
            {
                headers[i] = headers[i].Split('!')[0].SplitCamelCase();
            }

            for (int i = 0; i < fileData.Count; i++)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                string[] line = fileData[i].Split('|');
                for (int h = 0; h < headers.Length; h++)
                {
                    item.Add(headers[h], line[h]);
                }

                CreatedFileData.Add(item);
            }

            return (games: CreatedFileData, headers: headers.ToList());
        }
    }
}
