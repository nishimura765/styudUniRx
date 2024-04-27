using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Scenes.InGame.Manager;

namespace Scenes.InGame.Stick
{
    public class StickStatus : MonoBehaviour
    {
        [Header("スティックの可変パラメーター")]
        [SerializeField, Tooltip("スティックが移動する速度")]
        private float _moveSpeed;//スティックの移動速度を決めるパラメータです
        private static StickStatus _instance;

        public float MoveSpeed { get => _moveSpeed; }//他のスクリプトから_moveSpeedの値を参照したい場合はこの関数を使います

        private bool _isMovable = true;

        public bool IsMovable => _isMovable;

        private void Start()
        {
            InGameManager.Instance.ItemBuff
                .Where(x => x == 1)
                .Subscribe(x =>
                {
                    StickExtension();
                }
                ).AddTo(this);

        }
        public void StopMove()
        {
            _isMovable = false;
        }

        public void StickExtension()//アイテム取得時にバー延長
        {
            Transform myTransform = this.gameObject.GetComponent<Transform>();
            myTransform.localScale = new Vector3(2, 0.2f, 1);
        }
    }
}