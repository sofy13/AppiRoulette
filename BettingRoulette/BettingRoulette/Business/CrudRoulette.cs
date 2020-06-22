using BettingRoulette.Context;
using BettingRoulette.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingRoulette.Business
{
    public class CrudRoulette
    {
        private RouletteContext _rouletteContext;

        public CrudRoulette(RouletteContext rouletteContext)
        {
            _rouletteContext = rouletteContext;
        }

        public async Task<Roulette> CreateRoulette()
        {
            try
            {
                Roulette roulette = new Roulette();
                roulette.StateRoulette = Enumerations.StateRoulette.Cerrado.ToString();
                _rouletteContext.Roulette.Add(roulette);
                await _rouletteContext.SaveChangesAsync();
                return roulette;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
 }
