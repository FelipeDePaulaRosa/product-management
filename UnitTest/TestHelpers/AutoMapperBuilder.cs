using AutoMapper;
using Presentation;

namespace UnitTest.TestHelpers
{
    public static class AutoMapperBuilder
    {
        public static IMapper Build()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => 
                cfg.AddMaps(typeof(Startup).Assembly));
            return mapperConfiguration.CreateMapper();
        }
    }
}