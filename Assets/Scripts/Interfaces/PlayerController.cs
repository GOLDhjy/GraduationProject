using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
namespace MyService
{
    public class PlayerController : Singleton<PlayerController>
    {
        public PlayerEntity Player;

        //public PlayerController()
        //{

        //}
        //public PlayerController(PlayerEntity playerEntity)
        //{
        //    Player = playerEntity;
        //}
        public float GetPlayerHp()
        {
            return Player.hp.Value;
        }
        public void ChangePlayerHp(float num)
        {
            Player.hp.Value+=num;
        }
        public float GetPlayerATK()
        {
            return Player.aTK.Value;
        }
        public void ChangePlayerATK(float num)
        {
            Player.aTK.Value += num;
        }
    }
}
