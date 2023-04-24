namespace PocketGPT.Webapi.Controllers
{
    [ApiController]               // Muss bei jedem Controller stehen
    [Route("/api/[controller]")]  // Muss bei jedem Controller stehen
    public class NewsController : ControllerBase
    {
        private readonly PocketDB _db;

        public PocketController(PocketDB db)
        {
            _db = db;
        }
    }
}