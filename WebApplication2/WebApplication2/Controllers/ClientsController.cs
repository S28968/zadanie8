using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using WebApplication2.Exceptions;
using WebApplication2.Models.DTOs;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientsService _clientsService;
        private readonly IClientCreateService _clientCreateService;
        private readonly IClientTripService _clientTripService;
        
        public ClientsController(IClientsService clientsService, IClientCreateService clientCreateService, IClientTripService clientTripService)
        {
            _clientsService = clientsService;
            _clientCreateService = clientCreateService;
            _clientTripService = clientTripService;
        }
        
        [HttpGet("{id}/trips")]
        public async Task<IActionResult> GetTripsByClientId(int id)
        {
            try
            {
                var clients = await _clientsService.GetClientsAsync(id);


                if (clients.Count == 0)
                {
                    return NotFound($"Client of id: {id} doesn't exist");
                }

                return Ok(clients);
            }
            catch (SqlNullValueException e)
            {
                return NotFound($"Client of Id: {id} doesn't have any trips");
            }

        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateDTO newClient)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            
            var newId = await _clientCreateService.PostClientAsync(newClient);

            return Ok($"Created new client of id: {newId}");
        }

        [HttpPut("{id}/trips/{tripId}")]
        public async Task<IActionResult> Put(int id, int tripId)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            try
            {
                var newid = await _clientTripService.PutClientTrip(id, tripId);
                
                return Ok(newid);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }

            
        }
        
    }
    
}
