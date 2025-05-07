using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService; //D z SOLID

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripsService.GetTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            //jesli zasob nie istineje?
            return Ok();
        }


}
}
