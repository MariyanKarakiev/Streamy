using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Streamy.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
