namespace StarForce
{
    public class EnemyInfo
    {
        public int TypeId { get; private set; }

        public EnemyInfo(int typeId)
        {
            TypeId = typeId;
        }
    }
} 