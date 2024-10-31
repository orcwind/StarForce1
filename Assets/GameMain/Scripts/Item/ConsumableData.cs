namespace StarForce
{
    public class ConsumableData : EntityData
    {
          public ConsumableData(int entityId, int typeId) : base(entityId, typeId)
    {
    }
        public int HealAmount { get; set; }
        // 添加其他消耗品特有的属性
    }
}
