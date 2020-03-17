using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    public List<Inventory.Slot> slots;
    bool showingLoot;

    // Start is called before the first frame update
    void Start()
    {
        Trigger trigger = GetComponent<Trigger>();
        trigger.onTriggerAction = (Player player) =>
        {
            if (!showingLoot)
                ShowUI();
            else
                HideUI();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
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
