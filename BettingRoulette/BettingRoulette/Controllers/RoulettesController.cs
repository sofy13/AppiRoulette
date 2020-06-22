using BettingRoulette.Business;
using BettingRoulette.Context;
using BettingRoulette.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.SecurityTokenService;

namespace BettingRoulette.Controllers
{
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private readonly RouletteContext _rouletteContext;
        private readonly CrudRoulette _crudRoulette;

        public RoulettesController(RouletteContext context)
        {
            _rouletteContext = context;
            _crudRoulette = new CrudRoulette(_rouletteContext);
        }


        [HttpGet("createdRoulette")]
        public async Task<ActionResult<Roulette>> AddRoulette()
        {
            try
            {
                Roulette roulette = await _crudRoulette.CreateRoulette();
                return Ok(new { id = roulette.IdRoulette });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("openRoulette/{idRoulette}")]
        public async Task<ActionResult<Roulette>> OpenRoulette(long idRoulette)
        {
            try
            {
                return Ok(await _crudRoulette.OpenRoulette(idRoulette));
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

        [HttpGet("closeRoulette/{idRoulette}")]
        public async Task<ActionResult<Roulette>> CloseRoulette(long idRoulette)
        {
            try
            {
                return Ok(await _crudRoulette.CloseRoulette(idRoulette));
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
