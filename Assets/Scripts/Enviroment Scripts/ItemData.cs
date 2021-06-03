using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string ItemName = "default item name";

    public int ItemID = 0;

    public Sprite ItemIcon;
   

    public virtual void Use()
        {
            //use 
            
        }
}
