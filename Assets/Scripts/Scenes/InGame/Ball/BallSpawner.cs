using Scenes.InGame.Manager;
using UniRx;
using UnityEngine;

namespace Scenes.InGame.Ball
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField, Tooltip("Ballのプレファブを入れる")]
        GameObject _ballPrefab;

        [Header("スポーンに関するパラメータ")]
        [SerializeField, Tooltip("スティックからy軸にオフセットする距離")]
        private float _yOffsetDistance = 0.5f;

        void Start()
        {
            InGameManager.Instance.OnSpawn
                .Subscribe(_ =>
                {
                    Spawn();
                }).AddTo(this);
            InGameManager.Instance.ItemBuff
                .Where(x => x == 0)
                .Subscribe(x =>
                {
                    ItemBuffSpawn();
                }).AddTo(this);
        }

        //TODO:現在InGameManagerからスポーンさせています。これをInGameManagerからイベントを発行させ、このスクリプト受け取って自分でSpawnさせるように変更しましょう
        private void Spawn()
        {
            var Stick = GameObject.FindWithTag("Player");
            Instantiate(_ballPrefab, Stick.transform.position + new Vector3(0, _yOffsetDistance, 0), Quaternion.identity, transform.parent);
        }


        private void ItemBuffSpawn()//アイテム取得時にボール追加
        {
            GameObject ball = GameObject.FindWithTag("Ball");
            Instantiate(_ballPrefab, ball.transform.position, Quaternion.identity, transform.parent);
            Instantiate(_ballPrefab, ball.transform.position, Quaternion.identity, transform.parent);
        }

    }
}