using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    [CreateAssetMenu(fileName = "WaterElementAttack", menuName = "Main/ElementAttacks/WaterElementAttack", order = 0)]
    public class WaterElementAttack : ElementAttack
    {
        public override void InitAttack(BulletModel p_model)
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteAttack(BulletModel p_model)
        {
            throw new System.NotImplementedException();
        }

        public override void OnImpact(BulletModel p_model, Collision p_collision)
        {
            throw new System.NotImplementedException();
        }
    }
}