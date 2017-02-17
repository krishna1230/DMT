using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PLA.Models
{
    public class PLAPost
    {
        public PlagiarismData GetResult(PlagiarismData model)
        {
            PlagiarismData PLA;
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    var values = new[]
            {
            new KeyValuePair<string, string>("st",model.st),
            };
                    foreach (var keyValuePair in values)
                    {
                        content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                    }
                    //var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes("C:\\PlagScan.NET\\dotNET\\Testfile.txt"));
                    //content.Add(fileContent, "DATA", "Testfilename.txt");
                    var result = client.PostAsync("http://www.duplichecker.com/ajaxAction/action=search_results&type=text&", content).Result;
                    var con = result.Content.ReadAsStringAsync().Result;
                    PLA = JsonConvert.DeserializeObject<PlagiarismData>(con);
                }
                if (PLA.actual_results != null)
                {
                    foreach (var item in PLA.actual_results)
                    {
                        item.st = model.st;
                    }
                }
            }
            return PLA;
        }
    }
}