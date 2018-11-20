using System;
using System.Threading.Tasks;
using System.Threading;
using IdGen;

namespace Galaxy.Infrastructure
{
    internal class Snowflake : IIdGenerator<long>
    {
        readonly IdGenerator _idGenerator;
        public Snowflake(GalaxyOptions options)
        {
            _idGenerator = new IdGenerator(options.GeneratorId, options.IdGeneratorEpoch);
        }
        public long NextIdentity()
        {
            return _idGenerator.CreateId();
        }
    }
}
