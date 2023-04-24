using Microsoft.AspNetCore.Mvc;

namespace PocketGPT.Webapi.Controllers
{
    [ApiController]               // Muss bei jedem Controller stehen
    [Route("/api/[controller]")]  // Muss bei jedem Controller stehen
    public class PocketController : ControllerBase
    {
        private readonly PocketContext _db;

        public PocketController(PocketContext db)
        {
            _db = db;
        }
    }
}