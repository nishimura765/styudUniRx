using System;
using UniRx;
using UnityEngine;
namespace Scenes.InGame.Block
{
    public class Block : MonoBehaviour, IDamagable
    {
        [Header("ブロックのパラメーた")]
        [SerializeField, Tooltip("ブロックの耐久度")]
        private int _hp = 1;
        private Vector2 _blockPosition;
        public Vector2 BlockPosition => _blockPosition;

        void Start()
        {
            _blockPosition = this.transform.position;
        }
        public void Break()
        {
            Destroy(gameObject);
        }

        public void Damange(int damage)
        {
            if (damage < 0) return;//ダメージが負の場合は処理を返す
            _hp = _hp - damage;
            if (_hp <= 0)
            {
                Manager.InGameManager.Instance.BlockDestroy(BlockPosition);
                Break();
            }


        }
    }
}