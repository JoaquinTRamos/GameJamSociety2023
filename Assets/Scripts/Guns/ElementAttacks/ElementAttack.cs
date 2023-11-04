using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    public abstract class ElementAttack : ScriptableObject
    {
        public abstract void InitAttack(BulletModel p_model);
        public abstract void ExecuteAttack(BulletModel p_model);
        public abstract void OnImpact(BulletModel p_model, Collision p_collision);
    }
}