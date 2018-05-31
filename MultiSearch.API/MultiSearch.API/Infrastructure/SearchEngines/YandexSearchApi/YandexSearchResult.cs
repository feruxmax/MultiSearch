using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MultiSearch.API.Infrastructure.SearchEngines.YandexSearchApi
{
    public class YandexSearchResult
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public int Found { get; set; }
        public Results Results { get; set; }
    }

    public class Results
    {
        public Grouping Grouping { get; set; }
    }

    public class Grouping
    {
        [XmlElement("group")]
        public List<Group> Groups { get; set; }
    }

    public class Group
    {
        public Doc Doc { get; set; }
    }

    public class Doc
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Headline {get;set;}
    }
}
