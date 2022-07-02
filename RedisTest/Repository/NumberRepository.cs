using RedisTest.Models.DBEntity;
using RedisTest.Repository.Interface;

namespace RedisTest.Repository
{
    public class NumberRepository : DBRepository, INumberRepository
    {
        public NumberRepository(NumberDBContext context) : base(context) { }
    }
}
