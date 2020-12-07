using Core.Domain.Entities;
using Core.Domain.Resources.AvailabilityStatuses;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.AvailabilityStatuses
{
    public class GetAllAvailabilityStatusesOkExample : IExamplesProvider<IEnumerable<AvailabilityStatusResource>>
    {
        public IEnumerable<AvailabilityStatusResource> GetExamples()
        {
            return new[]
            {
                new AvailabilityStatusResource
                {
                    AvailabilityStatusId = AvailabilityStatusId.Online,
                    Name = "AvailabilityStatuses.Online",
                    IndicatorColor = "#00CC00",
                    IndicatorOverlay = "check"
                },
                new AvailabilityStatusResource
                {
                    AvailabilityStatusId = AvailabilityStatusId.Away,
                    Name = "AvailabilityStatuses.Away",
                    IndicatorColor = "#FFBB00",
                    IndicatorOverlay = "remove"
                }
            };
        }
    }
}
