namespace ZooPlanet.Tests
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Services;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                AutoMapperConfig.RegisterMappings(typeof(IService).Assembly);
                testsInitialized = true;
            }
        }
    }
}