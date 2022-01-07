
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using MonsterNamespace;
using UnityEngine.UI;

namespace MonsterRoomMainNamespace
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MonsterRoomMain : UdonSharpBehaviour
    {
        public Text 현재체력텍스트;
        public Monster[] 몬스터데이터;
        public int 체력_최대체력=100;
        public int 체력_현재플레이어체력 = 100;

        void Start()
        {

        }
        private void OnEnable()
        {
            체력_현재플레이어체력 = 체력_최대체력;
        }
        public void 플레이어사망()
        {

        }
        public void 플레이어체력업데이트()
        {
            현재체력텍스트.text = 체력_현재플레이어체력.ToString();
            if (체력_현재플레이어체력 < 0)
            {
                플레이어사망();
            }
        }
        
    }
}

