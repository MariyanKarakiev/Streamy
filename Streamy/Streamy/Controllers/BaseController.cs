using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamy.Common;

namespace Streamy.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class BaseController : Controller
    {
    }
}
