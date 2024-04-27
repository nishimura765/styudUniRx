using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TMPro;

namespace Scenes.InGame.Ball
{
    public class BallStatus : MonoBehaviour
    {
        [Header("ボールのパラメータ")]
        [SerializeField, Tooltip("ボールの移動速度")]
        private float _ballMoveSpeed;//ボールの移動速度を決めるパラメータです
        public float BallMoveSpeed { get => _ballMoveSpeed; }//他のスクリプトから_ballMoveSpeedの値を参照したい場合はこの関数を使います

        private ReactiveProperty<bool> _isMovable = new ReactiveProperty<bool>(true);//_isMovableの値が変わるとイベント発行
        public IReadOnlyReactiveProperty<bool> IsMovable => _isMovable;
        public void StopMove()
        {
            _isMovable.Value = false;
        }
    }
}