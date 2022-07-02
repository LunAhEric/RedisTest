using RedisTest.Models.DBEntity;
using RedisTest.Models.Dto;
using RedisTest.Repository.Interface;
using RedisTest.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace RedisTest.Service
{
    public class NumberService : INumberService
    {
        private readonly IMemoryCacheRepository _memoryCache;
        private readonly IDBRepository _dbRepository;

        public NumberService(IMemoryCacheRepository memoryCache, IDBRepository dBRepository)
        {
            _memoryCache = memoryCache;
            _dbRepository = dBRepository;
        }

        public List<NumberDto> GetAll()
        {
            var result = _dbRepository.GetAll<Number>().Select(x => new NumberDto()
            {
                Id = x.Id,
                NumberSet = x.NumberSet,
            }).ToList();
            if (result.Count != 0)
            {
                _memoryCache.Set("NumberSet.Num", result);
            }
            return result;
        }

        public List<NumberDto> GetTop(int topMax)
        {
            var result = _memoryCache.Get<List<NumberDto>>("NumberSet.Num");
            if (result != null) return result;
            result = _dbRepository.GetAll<Number>()
                .OrderByDescending(x => x.NumberSet)
                .Skip(0).Take(topMax)
                .Select(x => new NumberDto()
                {
                    Id = x.Id,
                    NumberSet = x.NumberSet,
                }).ToList();
            _memoryCache.Set("NumberSet.Num", result);
            return result;
        }

        public void CreateNumber(NumberDto request)
        {
            var Num = new NumberDto
            {
                Id = request.Id,
                NumberSet = request.NumberSet,
            };

            _dbRepository.Create(Num);
            _memoryCache.Set("NumberSet.Num", Num);
            _dbRepository.Save();
        }


    }
}
