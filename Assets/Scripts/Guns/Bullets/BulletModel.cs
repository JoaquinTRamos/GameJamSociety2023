using System;
using Guns.ElementAttacks;
using UnityEngine;

namespace Guns.Bullets
{
    public class BulletModel : MonoBehaviour
    {
        public class BulletStats
        {
            public Vector2 Dir;
            public float Damage;
            public float Speed;
        }
        

        [SerializeField] private ElementAttack myElementAttack;
        [SerializeField] private float lifeTime = 10f;

        private BulletStats m_stats;
        private bool m_initialized;

        public BulletStats GetStats() => m_stats;
        public void Initialize(Vector2 p_dir, float p_damage, float p_speed)
        {
            m_stats = new BulletStats();
            m_stats.Dir = p_dir;
            m_stats.Damage = p_damage;
            m_stats.Speed = p_speed;
            
            myElementAttack.InitAttack(this);
            m_initialized = true;
            Destroy(gameObject, lifeTime);
        }
        
        private void Update()
        {
            if(!m_initialized)
                return;
            
            myElementAttack.ExecuteAttack(this);
        }

        public void Move(Vector3 p_dir, float p_speed)
        {
            transform.position += p_dir * (p_speed * Time.deltaTime);
        }
        private void OnCollisionStay(Collision collision)
        {
            myElementAttack.OnImpact(this, collision);
        }
    }
}