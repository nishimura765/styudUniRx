using Scenes.InGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UniRx;

public class Item : MonoBehaviour
{
    //アイテムの基底クラス
    [SerializeField, Tooltip("アイテム落下速度")]
    private int _movespeed;
    private Rigidbody2D _rb;
    private Vector2 _velocity;
    private bool _isMovable = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = new Vector2(0, -_movespeed * Time.deltaTime);
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
    }
    void FixedUpdate()
    {
        if (_isMovable)
        {
            _rb.velocity = _velocity;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void Pause()
    {
        _isMovable = false;
    }

    private void Restart()
    {
        _isMovable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadFrame"))
        {
            Destroy(this.gameObject);
        }

    }

}
