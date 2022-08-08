using ESApp.Entities;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace ESApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentStore store;

        public DocumentsController(IDocumentStore store)
        {
            this.store = store;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUser req)
        {
            using var session = store.LightweightSession();

            var user = new User { FirstName = req.FirstName, LastName = req.LastName };

            session.Store(user);

            await session.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var session = store.QuerySession();

            var users = await session.Query<User>().ToListAsync();

            return Ok(users);
        }
    }

    public record AddUser(string FirstName, string LastName);
}