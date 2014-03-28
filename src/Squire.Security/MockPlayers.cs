namespace Squire.Security
{
    using System;

    public static class MockPlayers
    {
        private static readonly MockPlayer _malcomReynolds;

        private static readonly MockPlayer _janeCobb;

        private static readonly MockPlayer _kayleeFrye;

        static MockPlayers()
        {
            MockPlayers._malcomReynolds = new MockPlayer("mreynolds@serenity.com", name: "mreynolds");
            MockPlayers._janeCobb = new MockPlayer("jcobb@serenity.com", name: "jcobb");
            MockPlayers._kayleeFrye = new MockPlayer("kfrye@serenity.com", name: "kfrye");
        }

        public static MockPlayer MalcomReynolds
        {
            get
            {
                return MockPlayers._malcomReynolds;
            }
        }

        public static MockPlayer JayneCobb
        {
            get
            {
                return MockPlayers._janeCobb;
            }
        }

        public static MockPlayer KayleeFrye
        {
            get
            {
                return MockPlayers._kayleeFrye;
            }
        }
    }
}
