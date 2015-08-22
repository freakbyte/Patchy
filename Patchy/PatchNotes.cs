using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Xml.XPath;
using HtmlAgilityPack;
using System.IO;
using System.IO.Compression;

namespace Patchy
{
    public static class PatchNotes
    {
        private static string baseUrl = @"http://forums.firefall.com/community/";       
        private static string notesUrl = baseUrl + "forums/patch-notes.441/";
        private static HttpClient http = new HttpClient();
        private static Dictionary<string, string> urls = new Dictionary<string, string>();
        private static Dictionary<string, Note> notes = new Dictionary<string, Note>();
        private static bool done = false;
        private static string file = Patches.PatchyFolder + "db\\notes.ndb";
        public delegate void LoadedDelegate();
        public static LoadedDelegate OnLoaded = null;
        public static bool AutoUpdate = true;

        public static void Load()
        {
            if(File.Exists(file))
            {
                notes = ReadFromBinaryFile<Dictionary<string, Note>>(file);
            }
            if(OnLoaded != null)
            {
                OnLoaded();
            }
            if(AutoUpdate)
            {
                GrabUrls();
            }       
        }
        private static void Save()
        {
            WriteToBinaryFile<Dictionary<string, Note>>(file, notes);
        }
        public static Note Get(string patch)
        {
            if(notes.ContainsKey(patch))
            {
                return notes[patch];
            }

            Note n = new Note();
            n.data = "No notes for this patch I guess :c";
            n.url = "#";
            n.writtenByUrl = "";
            n.writtenBy = "nobody";
            n.patch = patch;

            return n;
        }

        public async static void GrabUrls()
        {
            done = false;
            Log.W("Trying to grab all new patch notes..");
            try
            {
                byte[] response = await http.GetByteArrayAsync(notesUrl);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(source);

                List<string> pages = new List<string>();
                List<string> threads = new List<string>();
                

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string href = link.Attributes["href"].Value;
                    if (href.StartsWith("threads/") && href.EndsWith("/") && !threads.Contains(href))
                    {
                        threads.Add(href);
                    }
                    else if (href.StartsWith("forums/patch-notes.441/page-") && !pages.Contains(href))
                    {
                        pages.Add(href);
                    }
                }

                foreach (string s in pages)
                {
                    response = await http.GetByteArrayAsync(baseUrl + s);
                    source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                    source = WebUtility.HtmlDecode(source);
                    doc = new HtmlDocument();
                    doc.LoadHtml(source);

                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        string href = link.Attributes["href"].Value;
                        if (href.StartsWith("threads/") && href.EndsWith("/") && !threads.Contains(href))
                        {
                            threads.Add(href);
                        }
                    }
                }

                urls = new Dictionary<string,string>();
                foreach(string s in threads)
                {
                    if(s.Equals("threads/hotfix-for-v0-5-1494.64955/"))
                    {
                        urls.Add("beta-14941", baseUrl + s);
                    }
                    else
                    {
                        string[] split = s.Split('.')[0].Split('-');
                        string patch = "";

                        foreach (string sp in split)
                        {
                            if (sp.Length == 6 && sp.Count(Char.IsDigit) == 5 && sp.Count(Char.IsLetter) == 1)
                            {
                                patch = sp.Replace("r", "").Replace("p", "");
                                break;
                            }
                            else if (sp.Length == 4 && sp.Count(Char.IsDigit) == 4)
                            {
                                patch = sp + "0";
                                break;
                            }
                        }

                        if (patch != "")
                        {
                            if (!urls.ContainsKey("beta-" + patch))
                            {
                                urls.Add("beta-" + patch, baseUrl + s);
                            }
                            else
                            {
                                Log.W("--");
                                Log.W("beta-" + patch + " - " + baseUrl + s);
                                Log.W("beta-" + patch + " - " + urls["beta-" + patch]);
                                Log.W("--");
                            }
                        }
                    }
                }

                int newNotes = 0;
                foreach(KeyValuePair<string, string> n in urls)
                {
                    if(!notes.ContainsKey(n.Key))
                    {
                        newNotes++;
                        Log.W("Grabbing notes for patch " + n.Key + "..");
                        response = await http.GetByteArrayAsync(n.Value);
                        source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                        source = WebUtility.HtmlDecode(source);
                        doc = new HtmlDocument();
                        doc.LoadHtml(source);

                        HtmlNode li = doc.DocumentNode.SelectSingleNode("//li[@data-author]");
                        HtmlNode uLink = li.SelectSingleNode("//a[@class=\"username\"]");
                        HtmlNode block = li.SelectSingleNode("//blockquote");

                        StringBuilder sb = new StringBuilder();
                        foreach (HtmlNode node in block.DescendantsAndSelf())
                        {
                            if (!node.HasChildNodes)
                            {

                                if (node.ParentNode.Name.Equals("li"))
                                {
                                    sb.Append("-> ");
                                }
                                
                                string text = node.InnerText;
                                if (!string.IsNullOrEmpty(text))
                                    sb.AppendLine(text.Trim());
                            }
                        }

                        string author = li.Attributes["data-author"].Value;
                        string authorUrl = uLink.Attributes["href"].Value;
                        string data = sb.ToString();

                        data = data.Replace("  "," ");
                        while(data.IndexOf("\r\n") != -1)
                        {
                            data = data.Replace("\r\n", "\n");
                        }

                        while (data.IndexOf("\n\n") != -1)
                        {
                            data = data.Replace("\n\n", "\n");
                        }

                        Note note = new Note();
                        note.patch = n.Key;
                        note.url = n.Value;
                        note.writtenBy = author;
                        note.writtenByUrl = baseUrl + authorUrl;
                        note.data = data;
                        note.html = block.OuterHtml;

                        notes.Add(n.Key, note);
                    }
                }

                if(newNotes == 0)
                {
                    Log.W("No new notes to get!");
                }
                else
                {
                    Log.W("Got " + newNotes + " new notes.");
                    Save();
                }
                
                done = true;
            }
            catch (Exception ex)
            {
                Log.W("A network error occured while grabbing the patch notes :c");
                done = true;
            }
            if (OnLoaded != null)
            {
                OnLoaded();
            }
        }

        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            using (Stream gZipStream = new GZipStream(stream, System.IO.Compression.CompressionMode.Compress))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(gZipStream, objectToWrite);
            }
        }
        private static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            using (Stream gZipStream = new GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(gZipStream);
            }
        }

        [Serializable]
        public class Note
        {
            public string patch;
            public string url;
            public string data;
            public string writtenBy;
            public string writtenByUrl;
            public string html;
        }

    }
}
