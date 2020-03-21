using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    public List<Inventory.Slot> slots;
    bool showingLoot;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = null;
        showingLoot = false;
        Trigger trigger = GetComponent<Trigger>();
        trigger.onTriggerAction = (Player player) =>
        {
            if (this.player == null) this.player = player;
            if (!showingLoot)
                ShowUI();
            else
                HideUI();
        };
    }

    public void SlotClicked(Inventory.Slot slot)
    {
        player.PickUp(slot.Item);
        slot.Clear();
    }

    private void ShowUI()
    {
        showingLoot = true;
        UI.instance.ShowLoot(this);
    }

    private void HideUI()
    {
        showingLoot = false;
        UI.instance.HideLoot();
    }
}
