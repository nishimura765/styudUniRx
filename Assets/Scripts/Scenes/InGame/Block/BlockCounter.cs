using Scenes.InGame.Manager;
using UnityEngine;

namespace Scenes.InGame.Block
{
    /// <summary>
    /// �u���b�N�̐���Manager�ɓ`�B����
    /// </summary>
    public class BlockCounter : MonoBehaviour
    {
        void Start()
        {
            InGameManager.Instance.BlockSize(transform.childCount);
        }
    }
}