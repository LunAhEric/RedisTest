using RedisTest.Models.Dto;
using System.Collections.Generic;

namespace RedisTest.Service.Interface
{
    public interface INumberService
    {
        List<NumberDto> GetAll();
        List<NumberDto> GetTop(int topMax);
        public void CreateNumber(NumberDto request);
    }
}
