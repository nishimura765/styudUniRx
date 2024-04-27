using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Scenes.InGame.Block;
using Scenes.InGame.Manager;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Itemのプレファブを入れる")]
    GameObject[] ItemPrefab = new GameObject[1];
    private int _number;　//アイテム選別抽選用

    void Start()
    {
        InGameManager.Instance.OnItemSpawn
            .Subscribe(x =>
            {
                Spawn(x);
            }
            ).AddTo(this);
    }
    private void Spawn(Vector2 vector2)
    {
        _number = UnityEngine.Random.Range(1, 101);
        Debug.Log($"ドロップ乱数+{_number}");

        if (_number <= 30)//30%でアイテム１ドロップ
        {
            _number = UnityEngine.Random.Range(0, 2); //2つのアイテムからランダムに生成
            switch (_number)
            {
                case 0:
                    Instantiate(ItemPrefab[0], vector2, Quaternion.identity, transform.parent);
                    break;
                case 1:
                    Instantiate(ItemPrefab[1], vector2, Quaternion.identity, transform.parent);
                    break;
            }
            Debug.Log($"アイテム乱数+{_number}");
        }

    }
}

