namespace StarForce
{
public class EquipmentData :  EntityData
{
       public EquipmentData(int entityId, int typeId) : base(entityId, typeId)
    {
    }
    // 添加装备特有的属性
    public int Defense { get; set; }
    public string Slot { get; set; }
    // 其他相关属性...
}
}
