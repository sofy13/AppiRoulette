using BettingRoulette.Context;
using BettingRoulette.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.SecurityTokenService;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingRoulette.Business
{
    public class CrudBet
    {
        private RouletteContext _betContext;
        private IConnectionMultiplexer _redisService;
        public CrudBet(RouletteContext betContext, IConnectionMultiplexer redis)
        {
            _betContext = betContext;
            _redisService = redis;
        }
        public async Task<Bet> CreateBet(Bet bet)
        {
            try
            {
                ValidateRequestStructure(bet);
                await ValidateRoulette(bet.IdRouletteBet);
                _betContext.Bet.Add(bet);
                await _betContext.SaveChangesAsync();
                await _redisService.GetDatabase().HashIncrementAsync("RouletteBets", $"{bet.IdRouletteBet}", bet.AmountBet);
                return bet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ValidateRoulette(long idRouletteBet)
        {
            string statusRoulette = await _redisService.GetDatabase().StringGetAsync($"{idRouletteBet}");
            if (String.IsNullOrEmpty(statusRoulette))
                throw new BadRequestException("No se encontro la ruleta");
            if (statusRoulette.Equals(Enumerations.StateRoulette.Cerrado.ToString()))
                throw new BadRequestException("La ruleta ya se encuentra cerrada");
        }

        public void ValidateRequestStructure(Bet bet)
        {
            string color = bet.ColorBet != null ? bet.ColorBet.ToString().ToLower() : null;
            if (bet.AmountBet > 10000 | bet.AmountBet <= 0)
                throw new BadRequestException("El monto de la apuesta debe estar entre 1 y 10.000 dolares");
            if (bet.IdRouletteBet <= 0)
                throw new BadRequestException("Id de Ruleta no valido");
            if (String.IsNullOrEmpty(bet.idUserBet))
                throw new BadRequestException("El id del usuario no fue enviado o esta vacio");
            if (!String.IsNullOrEmpty(color) && (-1 != bet.NumberBet))
                throw new BadRequestException("Solo puede apuestar al numero o al color, no a ambos");
            if (!(0 <= bet.NumberBet && bet.NumberBet <= 36) && bet.NumberBet != -1)
                throw new BadRequestException("El número de la apuesta debe estar entre 0 y 36");
            if (color != null)
                if (!color.Equals(Enumerations.ColorBet.Rojo.ToString().ToLower())
                && !color.Equals(Enumerations.ColorBet.Negro.ToString().ToLower()))
                    throw new BadRequestException("El color a apostar debe ser rojo o negro");
        }
    }
}
