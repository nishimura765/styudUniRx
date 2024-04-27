using Scenes.InGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Scenes.InGame.Ball;



namespace Scenes.InGame.Ball
{
    [RequireComponent(typeof(BallStatus), typeof(Rigidbody2D))]
    public class BallMove : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private BallStatus _ballStatus;
        private Vector2 _velocity;
        [SerializeField, Tooltip("初速")]
        private float _power;
        private Vector2 _pastverocity;
        private GameObject[] _ballSize;
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _ballStatus = GetComponent<BallStatus>();
            _velocity = new Vector2(1, 1).normalized;
            _rigidbody2D.AddForce(_velocity * _power, ForceMode2D.Impulse);
            InGameManager.Instance.OnPause
                .Subscribe(_ =>
                {
                    Pause();
                }).AddTo(this);

            InGameManager.Instance.OnRestart
                .Subscribe(_ =>
                {
                    Restart();
                }).AddTo(this);

            _ballStatus.IsMovable.Skip(1) //値変更時に実行する関数を呼び出し(subscribe時のみ通知無効)
                .Subscribe(_ =>
                {
                    StopMove();
                }).AddTo(this);

        }
        //TODO:現在UpdateでずっとBallStatusを参照し続けています。イベント機能を使って、IsMovableの値が変更されたときだけ下の処理を実行するように変更してみましょう
        private void StopMove()
        {
            Debug.Log(_ballStatus.IsMovable);
            Debug.Log("とまった!");
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void Pause()
        {
            _pastverocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void Restart()
        {
            _rigidbody2D.AddForce(_pastverocity.normalized * _power, ForceMode2D.Impulse);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DeadFrame"))
            {
                Destroy(this.gameObject);
                _ballSize = GameObject.FindGameObjectsWithTag("Ball");
                Debug.Log($"ボールの数   {_ballSize.Length}");
                if (_ballSize.Length == 0)
                {
                    InGameManager.Instance.GameOver();
                }
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                _rigidbody2D.velocity = Vector2.zero;
                var boundVelocity = transform.position - collision.gameObject.transform.position;
                _rigidbody2D.AddForce(boundVelocity.normalized * _power, ForceMode2D.Impulse);
            }
        }
    }
}