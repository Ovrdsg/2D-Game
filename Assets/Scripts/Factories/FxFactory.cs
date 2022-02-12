namespace SunnyLand
{
    internal sealed class FxFactory : IFxFactory
    {
        private readonly Data _data;

        internal FxFactory(Data data)
        {
            _data = data;
        }
        
        public FxView GetFx(FxType type)
        {
            if(type == FxType.PICKUP)
                return _data.PickUpFX;
            if (type == FxType.ENEMYDEATH)
                return _data.EnemyDeathFx;
            return null;
        }
    }
}