﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes.InGame.Ball;
using Scenes.InGame.Stick;
using TMPro;
using UniRx;
using System;

namespace Scenes.InGame.Manager
{
    public class InGameManager : MonoBehaviour
    {
        BallSpawner _ballSpawner;
        BallStatus _ballStatus;
        StickStatus _stickStatus;
        public static InGameManager Instance;
        private int _score = 0;//スコア
        private int _blockSize = 0;//blockの数
        [SerializeField, Tooltip("aaa")]
        TextMeshProUGUI _socreText;
        private int _number;//ドロップ抽選用乱数

        private Subject<Unit> Pause = new Subject<Unit>();
        public IObservable<Unit> OnPause => Pause;
        private Subject<Unit> Restart = new Subject<Unit>();
        public IObservable<Unit> OnRestart => Restart;
        private Subject<Unit> Spawn = new Subject<Unit>();
        public IObservable<Unit> OnSpawn => Spawn;
        private Subject<Vector2> ItemSpawn = new Subject<Vector2>();//アイテム生成イベント
        public IObservable<Vector2> OnItemSpawn => ItemSpawn;
        public Subject<int> ItemBuff = new Subject<int>();//アイテム効果バフイベント



        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            _ballSpawner = GetComponent<BallSpawner>();
            StartCoroutine(BallSpawn());
        }


        IEnumerator BallSpawn()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            Spawn.OnNext(default);
        }

        public void GameOver()
        {
            _ballStatus = FindObjectOfType<BallStatus>();
            _stickStatus = FindObjectOfType<StickStatus>();
            _ballStatus.StopMove();
            _stickStatus.StopMove();
        }
        public void BlockSize(int i)
        {
            _blockSize = i;
        }

        public void GamePause()
        {
            Pause.OnNext(default);
        }

        public void GameRestart()
        {
            Restart.OnNext(default);
        }


        public void BlockDestroy(Vector2 vector2)
        {
            _score += 100;
            _blockSize--;
            _socreText.text = $"SCORE:{_score}";

            if (_blockSize <= 0)
            {
                GameOver();
                return;
            }

            ItemSpawn.OnNext(vector2);//ブロック破壊時にイベント通知

        }
    }
}