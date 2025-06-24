using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CarManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateVehicleDTO>>> GetAllVehiclesAsync()
        {
            var response = await _vehicleService.GetAllVehiclesAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleAsync(CreateVehicleDTO createVehicleDto)
        {
            await _vehicleService.AddVehicleAsync(createVehicleDto);
            return Ok();
        }

        [HttpDelete("/{id}")]
        public async Task<ActionResult> DeleteVehicleAsync(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return Ok();
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<CreateVehicleDTO>> GetVehicleByIdAsync(int id)
        {
            var response = await _vehicleService.GetVehicleByIdAsync(id);
            return Ok(response);
        }

        [HttpPut("/{id}")]
        public async Task<ActionResult> UpdateVehicleAsync(Vehicle vehicle, int id)
        {
            await _vehicleService.UpdateVehicleAsync(vehicle, id);
            return Ok();
        }

        [HttpGet("search/byBrand/{brand}")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehiclesByBrandAsync(string brand)
        {
            var vehicles = await _vehicleService.GetVehiclesByBrandAsync(brand);
            return Ok(vehicles);
        }

        [HttpGet("search/byModel/{model}")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehiclesByModelAsync(string model)
        {
            var response = await _vehicleService.GetVehiclesByModelAsync(model);

            return Ok(response);
        }

        [HttpGet("search/byYear/{year}")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehiclesByYearAsync(int year)
        {
            var response = await _vehicleService.GetVehiclesByYearAsync(year);

            return Ok(response);
        }
    }
}