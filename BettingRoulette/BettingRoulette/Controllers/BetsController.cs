using System;
using System.Threading.Tasks;
using BettingRoulette.Context;
using BettingRoulette.Entities;
using BettingRoulette.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

namespace BettingRoulette.Controllers
{
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly RouletteContext _rouletteContext;
        private readonly CrudBet _crudBet;

        public BetsController(RouletteContext context)
        {
            _rouletteContext = context;
            _crudBet = new CrudBet(_rouletteContext);
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
