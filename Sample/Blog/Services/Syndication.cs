using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
//using Terradue.ServiceModel.Syndication;

namespace BlogiFire.Services
{
    public class Syndication
    {
        public void Import(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            //SyndicationFeed feed = SyndicationFeed.Load(reader);
            //reader.Close();

            //foreach (SyndicationItem item in feed.Items)
            //{
            //    String subject = item.Title.Text;
            //    String summary = item.Summary.Text;
            //    System.Diagnostics.Debug.WriteLine(subject);
            //}
        }

        //public virtual IList<Item> ParseRss(string url)
        //{
        //    try
        //    {
        //        var doc = XDocument.Load(url);
        //        // RSS/Channel/item
        //        var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
        //            select new Item
        //            {
        //                FeedType = FeedType.RSS,
        //                Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
        //                Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
        //                PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
        //                Title = item.Elements().First(i => i.Name.LocalName == "title").Value
        //            };
        //        return entries.ToList();
        //    }
        //    catch
        //    {
        //        return new List<Item>();
        //    }
        //}
    }

    public class Item
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Item()
        {
            Link = "";
            Title = "";
            Content = "";
            PublishDate = DateTime.Today;
        }
    }
}