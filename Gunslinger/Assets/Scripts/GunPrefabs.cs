using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPrefabs : MonoBehaviour
{
    public static GunPrefabs Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Gun revolverPrefab;
    public Gun riflePrefab;

    public Gun GetGunPrefab(Item.ItemType gunType)
    {
        switch(gunType)
        {
            case Item.ItemType.Revolver: return revolverPrefab;
            case Item.ItemType.Rifle: return riflePrefab;
            default: return null;
        }
    }
}
