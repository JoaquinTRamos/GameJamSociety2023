using System.Collections.Generic;
using System.Runtime.InteropServices;
using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    [CreateAssetMenu(fileName = "FireElementAttack", menuName = "Main/ElementAttacks/FireElementAttack", order = 0)]
    public class FireElementAttack : ElementAttack
    {
        private Dictionary<BulletModel, Vector3> m_randomVector3S = new Dictionary<BulletModel, Vector3>();
        public override void InitAttack(BulletModel p_model)
        {
            var RndX = Random.Range(-0.2f, 0.2f);
            var RndY = Random.Range(-0.2f, 0.2f);
            var vector = p_model.GetStats().Dir;
            vector.x += RndX;
            vector.y += RndY;

            m_randomVector3S[p_model] = vector;
        }

        public override void ExecuteAttack(BulletModel p_model)
        {
            p_model.Move(m_randomVector3S[p_model], p_model.GetStats().Speed);
        }

        public override void OnImpact(BulletModel p_model, Collider2D p_collision)
        {
            Debug.Log("PreimpacPreimpact");
            IDamageable target = p_collision.gameObject.GetComponent<IDamageable>();
            if (target == null)
                return;

            Debug.Log("IMpact");

            int damage = p_model.m_stats.Damage;
            Debug.Log("Damage" + damage);
            target.Damage(damage, Element.Fire);
        }
    }
}