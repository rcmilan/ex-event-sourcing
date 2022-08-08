using ESApp.Events;
using ESApp.Projections;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace ESApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProjection([FromServices] IDocumentSession session)
        {
            var result = session.Events.StartStream(
                new CreateUser("user 1", "lastname 1"),
                new SetDepartment("dpto"),
                new AddPermission("permission 1"),
                new AddPermission("permission 2"),
                new AddPermission("permission 3"),
                new AddPermission("permission 4"),
                new AddPermission("permission 5")
            );

            await session.SaveChangesAsync();

            return Ok(result.Id);
        }

        [HttpGet("{projectionId}")]
        public async Task<IActionResult> GetProjection([FromServices] IQuerySession session, Guid projectionId)
        {
            var aggregateStream = await session.Events.AggregateStreamAsync<UserProjection>(projectionId);

            return Ok(aggregateStream);
        }
    }
}