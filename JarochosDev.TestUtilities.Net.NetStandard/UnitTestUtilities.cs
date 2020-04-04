namespace JarochosDev.TestUtilities.Net.NetStandard
{
    public static class UnitTestUtilities
    {
        private static ISingletonService _singletonService;
        private static Helper _helper;
        public static ISingletonService SingletonService => _singletonService ?? (_singletonService = new SingletonService());
        public static Helper Helper => _helper ?? (_helper = new Helper());
    }
}
