using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ParallaxSplitView.Models;

namespace ParallaxSplitView.Services
{
    public class Ch9Service
    {
        public static async Task<List<Session>> GetBuild2015Sessions()
        {
            var uriPath = "Data/Build2015_Channel9_1.htm";
            var sessions = await GetBuild2015Sessions(uriPath);

            uriPath = "Data/Build2015_Channel9_2.htm";
            sessions.AddRange(await GetBuild2015Sessions(uriPath));

            return sessions;
        }

        private static async Task<List<Session>> GetBuild2015Sessions(string uriPath)
        {
            var page = await GetPageFromContent(uriPath);

            var ret = await Task.Run(() =>
            {
                var sessions = new List<Session>();

                //var body = page.DocumentNode.SelectSingleNode("//body"); ¿?
                var html = page.DocumentNode.ChildNodes.First(n => n.Name == "html");
                var body = html.ChildNodes.First(n => n.Name == "body");
                var allsesionsdiv = body.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "tab-content allSessions").First();

                var divSessions = allsesionsdiv.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "entry-meta");

                Func<string, string> Clean = (s) => { return System.Net.WebUtility.HtmlDecode(s).Replace("\r\n", "").Replace("\t", ""); };

                foreach (var div in divSessions)
                {
                    try
                    {
                        var session = new Session();

                        var titleTag = div.ChildNodes.First(n => n.Name == "a");
                        session.Title = Clean(titleTag.InnerText);

                        var detailsTag = div.Descendants("ul").First();
                        var speakerTag = detailsTag.Descendants("li").FirstOrDefault(li => li.Attributes.Contains("class") && li.Attributes["class"].Value == "grouping speaker");
                        session.Speaker = Clean(speakerTag.InnerText);

                        var codeTag = detailsTag.Descendants("li").FirstOrDefault(li => li.Attributes.Contains("class") && li.Attributes["class"].Value == "timing code");
                        session.Code = Clean(codeTag.InnerText);

                        if (session.Code.Contains("200"))
                        {
                            string k = "";
                        }

                        var downloadsTag = div.Descendants("ul").Last();
                        var link = downloadsTag.Descendants("a").FirstOrDefault((a) => a.InnerText.Contains("Medium"));

                        if (link != null)
                        {
                            var uri = link.Attributes[0].Value;
                            session.VideoUri = uri;
                            sessions.Add(session);
                        }

                    }
                    catch (Exception ex)
                    {
                        //There is no medium video
                    }

                }

                return sessions;
            });

            return ret;

            
        }

        private static Task<HtmlDocument> GetPageFromContent(string filepath)
        {
            return Task.Run<HtmlDocument>(() =>
            {
                string document = System.IO.File.ReadAllText(filepath, Encoding.GetEncoding("iso-8859-1"));

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.OptionFixNestedTags = true;
                htmlDocument.LoadHtml(document);

                return htmlDocument;
            });

        }

    }
}
