
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using MonsterRoomMainNamespace;
using UnityEngine.UI;

namespace MonsterNamespace
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Monster : UdonSharpBehaviour
    {
        public MonsterRoomMain 메인;
        Animator 몬스터애니메이션;
        bool PlayerTracking;
        float Timer = 0;
        Vector3 PlayerPosition;
        bool 플레이어가콜라이더안에있음;
        int 몬스터_공격력=10;
        int 몬스터_몬스터체력=100;
        public Text 몬스터현재체력텍스트;

        private void OnEnable()
        {
            몬스터애니메이션 = (Animator)this.gameObject.GetComponent(typeof(Animator));
            PlayerPosition = Networking.LocalPlayer.GetPosition();
        }

        void 몬스터사망()
        {

        }
        public override void Interact()
        {
            //PlayerTracking = !PlayerTracking;
            //플레이어쫒아가기(PlayerTracking);
        }
        void 공격()
        {
            Debug.Log("몬스터공격"+메인.체력_현재플레이어체력.ToString());
            메인.체력_현재플레이어체력 = 메인.체력_현재플레이어체력 - 몬스터_공격력;
            메인.플레이어체력업데이트();
            if (플레이어가콜라이더안에있음)
            {
                SendCustomEventDelayedSeconds("공격", 2);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains("weapon"))
            {
                Debug.Log("플레이어가 몬스터를 공격함" + 몬스터_몬스터체력.ToString());
                몬스터현재체력텍스트.text = 몬스터_몬스터체력.ToString();
                몬스터_몬스터체력 = 몬스터_몬스터체력 - (int)((UdonBehaviour)other.gameObject.GetComponent(typeof(UdonBehaviour))).GetProgramVariable("무기_데미지");
                if (몬스터_몬스터체력 < 0)
                {
                    몬스터사망();
                }
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                플레이어쫒아가기(false);
                몬스터애니메이션.SetBool("attack", true);
                공격();
                플레이어가콜라이더안에있음 = true;
            }
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                플레이어쫒아가기(true);
                몬스터애니메이션.SetBool("attack", false);
                플레이어가콜라이더안에있음 = false;
            }
        }

        void 플레이어쫒아가기(bool Checker)
        {
            if (Checker)
            {
                PlayerTracking = true;
                몬스터애니메이션.SetBool("run", true);
            }
            else
            {
                PlayerTracking = false;
                몬스터애니메이션.SetBool("run", false);
            }

        }
        private void FixedUpdate()
        {
            // if (player)
            if (PlayerTracking)
            {
                if (Timer > 1)     //1초에한번씩실행
                {
                    PlayerPosition = Networking.LocalPlayer.GetPosition();
                    Timer = 0;

                }
                transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(PlayerPosition.x, this.gameObject.transform.position.y, PlayerPosition.z), 0.02f);
                transform.LookAt(PlayerPosition);
                Timer = Timer + 0.02f;
            }

        }
    }

}
