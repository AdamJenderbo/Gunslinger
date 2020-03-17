using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Medicin, Revolver, Rifle
    }

    public ItemType itemType;

    public Sprite Sprite;

    public virtual bool Stackable { get { return true; } }

    public virtual void Use() { }

    public virtual bool IsGun() { return false; }

    public override bool Equals(object other)
    {
        if(other is Item)
        {
            return itemType == ((Item)other).itemType;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return itemType.GetHashCode();
    }

}
