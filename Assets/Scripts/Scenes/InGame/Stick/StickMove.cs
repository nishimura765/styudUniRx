using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Scenes.InGame.Stick
{
    [RequireComponent(typeof(StickStatus), typeof(Rigidbody2D))]
    public class StickMove : MonoBehaviour
    {
        private StickStatus _stickStatus;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        private const int CORRECTIONVALUE = 10;//数値を調整するための補正値です
        void Start()
        {
            _stickStatus = GetComponent<StickStatus>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

        }

        void FixedUpdate()
        {
            //TODO:現在はStickMoveが毎回StickStatusに参照しています。この部分をUniRxを使って、値が変わった時だけアクセスするようにしてみましょう
            if (!_stickStatus.IsMovable)
            {
                _velocity = Vector2.zero;
                return;
            }

            _velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _velocity.x--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _velocity.x++;
            }

            //TODO:現在はStickMoveが毎回StickStatusに参照しています。この部分をUniRxを使って、値が変わった時だけアクセスするようにしてみましょう
            Vector2 _mooveVelocity = _velocity * _stickStatus.MoveSpeed;
            _rigidbody2D.velocity = _mooveVelocity * Time.fixedDeltaTime * CORRECTIONVALUE;
        }
    }
}