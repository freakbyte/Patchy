using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace Patchy
{
    static class Upload
    {
        public static void UploadPatch(Patches.PatchInfo info)
        {
            string hostFile = AppDomain.CurrentDomain.BaseDirectory + "\\host.txt";

            if (File.Exists(hostFile))
            {               
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (sender, args) =>
                {
                    Log.W("Uploading " + info.name + "...");
                    Uri uri = new Uri(File.ReadAllText(hostFile).Trim());
                
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.Timeout = TimeSpan.FromHours(1);
                    List<Patches.PatchInfo.PFile> files = new List<Patches.PatchInfo.PFile>();
                    foreach (KeyValuePair<string, Patches.PatchInfo.PFile> file in info.files)
                    {
                        files.Add(file.Value);
                    }

                    var content = new StringContent(JsonConvert.SerializeObject(new object[]
                    {
                        new KeyValuePair<string, string>("name", info.name),
                        new KeyValuePair<string, string>("parent", info.parentName),
                        new KeyValuePair<string, string>("created", ToEpochTime(info.creationTime).ToString()),
                        new KeyValuePair<string, List<Patches.PatchInfo.PFile>> ("files", files),
                        new KeyValuePair<string, List<string>> ("new", info.newFiles),
                        new KeyValuePair<string, List<string>> ("modified", info.modifiedFiles),
                        new KeyValuePair<string, List<string>> ("deleted", info.deletedFiles)

                    }), Encoding.UTF8, "application/json");

                    //Console.WriteLine(content.ReadAsStringAsync().Result);
                    HttpResponseMessage response = client.PostAsync(uri, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                        Log.W("Done Uploading " + info.name + "...");

                    }
                    else
                    {
                        //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                        Log.W("Done Uploading " + info.name + "... ERROR!");
                    }
                };
                bw.RunWorkerAsync();
            }
            else
            {
                Log.W("Please create a file named host.txt next to the executable containing the url you want to post the data to.");
            }

        }

        public static string Base64Encode(string plainText)
        {
            if(String.IsNullOrEmpty(plainText))
            {
                return "";
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static long ToEpochTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }
    }
}
