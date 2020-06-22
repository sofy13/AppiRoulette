using BettingRoulette.Context;
using BettingRoulette.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.SecurityTokenService;

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

        public async Task<string> OpenRoulette(long idRoulette)
        {
            Roulette roulette = await _rouletteContext.Roulette.FindAsync(idRoulette);
            if (roulette == null)
                throw new BadRequestException("No se encontro la ruleta");
            else if (roulette.StateRoulette.Equals(Enumerations.StateRoulette.Cerrado.ToString()))
            {
                roulette.StateRoulette = Enumerations.StateRoulette.Abierto.ToString();
                await UpdateRoulete(roulette);
                return "Exitoso";
            }
            else
                return "Denegado";
        }

        public async Task UpdateRoulete(Roulette roulette)
        {
            try
            {
                _rouletteContext.Update(roulette);
                await _rouletteContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
 }
