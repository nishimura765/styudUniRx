using Scenes.InGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : Item
{
    //獲得するとボールが触れる効果
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InGameManager.Instance.ItemBuff.OnNext(1);
            Destroy(this.gameObject);
        }
    }
}
