using Microsoft.AspNetCore.Mvc;
using RestWithASPNet.Data.VO;
using RestWithASPNet.HyperMedia.Constants;
using System.Text;

namespace RestWithASPNet.HyperMedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/Person/v1";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.defaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.defaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.defaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerbs.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return null;
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2f", "/").ToString();
            }
        }
    }
}
