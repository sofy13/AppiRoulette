using System;
using System.Threading.Tasks;
using BettingRoulette.Context;
using BettingRoulette.Entities;
using BettingRoulette.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;
using StackExchange.Redis;
namespace BettingRoulette.Controllers
{
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly RouletteContext _rouletteContext;
        private IConnectionMultiplexer _redisService;
        private readonly CrudBet _crudBet;
        public BetsController(RouletteContext context, IConnectionMultiplexer redis)
        {
            _rouletteContext = context;
            _redisService = redis;
            _crudBet = new CrudBet(_rouletteContext, _redisService);
        }
        [HttpPost("createdBet")]
        public async Task<ActionResult<Bet>> AddBet(Bet bet)
        {
            try
            {
                bet.idUserBet = Request.Headers["idUser"];
                await _crudBet.CreateBet(bet);
                return Ok("Apuesta realizada");
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}