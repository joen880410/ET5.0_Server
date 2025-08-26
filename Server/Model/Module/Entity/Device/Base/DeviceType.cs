namespace ETModel
{
    public static class DeviceType
    {
        public const int Max = 10000;
        public const int FeatureMin = Main;
        public const int FeatureMax = Event;
        public const int TypeMin = Sport;
        public const int TypeMax = Pet;
        
        // Feature 1 ~ 100
        public const int Main = 1;
        public const int Record = 2;
        public const int Character = 3;
        public const int Community = 4;
        public const int Event = 5;
        
        // Type 101 ~ 200
        public const int Sport = 101;
        public const int Pet = 102;

        public const int SportOnMain = Sport * 10 + Main;
        public const int SportOnRecord = Sport * 10 + Record;
        public const int SportOnCharacter = Sport * 10 + Character;
        public const int SportOnCommunity = Sport * 10 + Community;
        public const int SportOnEvent = Sport * 10 + Event;
        
        public const int PetOnMain = Pet * 10 + Main;
        public const int PetOnRecord = Pet * 10 + Record;
    }
}