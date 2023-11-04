using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    [CreateAssetMenu(fileName = "SimpleNonElementAttack", menuName = "Main/ElementAttacks/SimpleNonElementAttack", order = 0)]
    public class SimpleNonElementAttack : ElementAttack
    {
        public override void InitAttack(BulletModel p_model)
        {
        }

        public override void ExecuteAttack(BulletModel p_model)
        {
            p_model.Move(p_model.GetStats().Dir, p_model.GetStats().Speed);
        }

        public override void OnImpact(BulletModel p_model, Collider2D p_collision)
        {
            Destroy(p_model);
        }
    }
}