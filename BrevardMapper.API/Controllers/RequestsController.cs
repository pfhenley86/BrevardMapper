using BrevardMapper.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BrevardMapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public RequestsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceRequests>> Get()
        {
            using var conn = new SqlConnection(_config.GetConnectionString("Default"));

            return await conn.QueryAsync<ServiceRequests>(
                "sp_GetRequests",
                commandType: CommandType.StoredProcedure
            );

        }

        // ARC GIS Data
        [HttpGet("map")]
        public async Task<IEnumerable<object>> GetMapPoints()
        {
            using var conn = new SqlConnection(_config.GetConnectionString("Default"));

            var data = await conn.QueryAsync<ServiceRequests>(
                "sp_GetRequests",
                commandType: CommandType.StoredProcedurea
            );

            return data.Select(x => new
            {
                id = x.Id,
                title = x.Title,
                description = x.Description,
                latitude = x.Latitude,
                longitude = x.Longitude,
                status = x.Status,
            });
        }
    }
}
