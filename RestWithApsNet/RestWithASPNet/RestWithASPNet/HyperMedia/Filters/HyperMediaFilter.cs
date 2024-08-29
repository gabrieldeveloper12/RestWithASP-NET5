using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASPNet.HyperMedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _options;

        public HyperMediaFilter(HyperMediaFilterOptions options)
        {
            _options = options;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult objectResult)
            {
                var enricher = _options.ContentResponsEnricherList.FirstOrDefault(x => x.CanEnrich(context));

                if(enricher != null)
                {
                    Task.FromResult(enricher.Enrich(context));
                }
            }
        }
    }
}
