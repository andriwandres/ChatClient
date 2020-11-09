using Core.Domain.Resources.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Session
{
    public class LoginResponseExample : IExamplesProvider<AuthenticatedUserResource>
    {
        public AuthenticatedUserResource GetExamples()
        {
            return new AuthenticatedUserResource
            {
                UserId = 1,
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXIxQHRlc3QuY2giLCJ1bmlxdWVfbmFtZSI6IlVzZXIgMSIsIm5hbWVpZCI6IjEiLCJuYmYiOjE2MDI2MDg5MjgsImV4cCI6MTYwMzIxMzcyOCwiaWF0IjoxNjAyNjA4OTI4fQ.KeSLWq1n353Edpd913oxxQ_hzNrTkzbz39MwaF1NR10",
                Email = "alfred.miller@gmail.com",
                UserName = "alfred_miller"
            };
        }
    }
}
