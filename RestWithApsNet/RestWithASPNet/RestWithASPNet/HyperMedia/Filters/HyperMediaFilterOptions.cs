using RestWithASPNet.HyperMedia.Abstract;

namespace RestWithASPNet.HyperMedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponsEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
