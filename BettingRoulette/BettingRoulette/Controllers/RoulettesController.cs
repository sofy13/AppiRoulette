﻿using BettingRoulette.Business;
using BettingRoulette.Context;
using BettingRoulette.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}