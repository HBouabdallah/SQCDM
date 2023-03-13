using IndustryIncident.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IndustryIncident.Helpers
{
    public class MyClaimsTransformation : IClaimsTransformation
    {
        public IndustryIncidentContext _context { get; }
        public IHttpContextAccessor _httpContext { get; }

        public MyClaimsTransformation(IndustryIncidentContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor;
        }


        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            string userid = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("/name")).Value;
            var claimType = "Role";
            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                var role = from userRole in _context.UserRoles
                           join r in _context.Roles on userRole.Idrole equals r.Id
                           join u in _context.Users on userRole.Iduser equals u.Id
                           where u.Id == userid
                           select r;
                if (role.Count()>0)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.FirstOrDefault().Role1));
                }
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
