using BettingRoulette.Context;
using BettingRoulette.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Collections;
using System.Collections.Generic;
namespace BettingRoulette.Business
{
    public class CrudRoulette
    {
        private RouletteContext _rouletteContext;
        private IConnectionMultiplexer _redisService;
        public CrudRoulette(RouletteContext rouletteContext, IConnectionMultiplexer redis)
        {
            _rouletteContext = rouletteContext;
            _redisService = redis;
        }
        public async Task<Roulette> CreateRoulette()
        {
            try
            {
                Roulette roulette = new Roulette();
                roulette.StateRoulette = EnumerationStateRoulette.StateRoulette.Cerrado.ToString();
                _rouletteContext.Roulette.Add(roulette);
                await _rouletteContext.SaveChangesAsync();
                await _redisService.GetDatabase().StringSetAsync($"{roulette.IdRoulette}", roulette.StateRoulette);
                return roulette;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> OpenRoulette(long idRoulette)
        {
            string statusRoulette = await _redisService.GetDatabase().StringGetAsync($"{idRoulette}");
            if (String.IsNullOrEmpty(statusRoulette))
                throw new BadRequestException("No se encontro la ruleta");
            else if (statusRoulette.Equals(EnumerationStateRoulette.StateRoulette.Cerrado.ToString()))
            {
                Roulette roulette = await _rouletteContext.Roulette.FindAsync(idRoulette);
                roulette.StateRoulette = EnumerationStateRoulette.StateRoulette.Abierto.ToString();
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
                await _redisService.GetDatabase().StringSetAsync($"{roulette.IdRoulette}", roulette.StateRoulette);
            }
            catch (Exception)
            {
                throw;
            }
        }        
        public async Task<string> CloseRoulette(long idRoulette)
        {
            string statusRoulette = await _redisService.GetDatabase().StringGetAsync($"{idRoulette}");
            if (string.IsNullOrEmpty(statusRoulette))
                throw new BadRequestException("No se encontro la ruleta");
            else if (statusRoulette.Equals(EnumerationStateRoulette.StateRoulette.Abierto.ToString()))
            {
                Roulette roulette = await _rouletteContext.Roulette.FindAsync(idRoulette);
                roulette.StateRoulette = EnumerationStateRoulette.StateRoulette.Cerrado.ToString();
                await UpdateRoulete(roulette);
                var totalAmountBet = await _redisService.GetDatabase().HashGetAsync("RouletteBets", $"{roulette.IdRoulette}");
                await _redisService.GetDatabase().HashSetAsync("RouletteBets", $"{roulette.IdRoulette}", 0);
                await _rouletteContext.SaveChangesAsync();
                return $"Ruleta cerrada, el total de la apuesta fue: {totalAmountBet}";
            }
            else
                return "Denegado";
        }
        public async Task<List<Roulette>> ListRoulette()
        {
            try
            {
                return await _rouletteContext.Roulette.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
 }