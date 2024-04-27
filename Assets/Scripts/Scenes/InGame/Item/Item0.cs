using Scenes.InGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item0 : Item
{
    //獲得するとバーを延長する効果
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InGameManager.Instance.ItemBuff.OnNext(0);
            Destroy(this.gameObject);
        }
    }
}
