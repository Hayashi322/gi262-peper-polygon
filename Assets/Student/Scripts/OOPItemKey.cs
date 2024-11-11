using System.Collections;
using System.Collections.Generic;
using Student;
using UnityEngine;

public class OOPItemKey : Identity
{
    public string key;

    public override void Hit()
    {
        mapGenerator.player.inventory.AddItem(key);
        Destroy(gameObject);
    }
}