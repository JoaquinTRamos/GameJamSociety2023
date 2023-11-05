using System;
using Guns.ElementAttacks;
using Unity.VisualScripting;
using UnityEngine;

namespace Guns.Bullets
{
    public class BulletModel : MonoBehaviour
    {
        public class BulletStats
        {
            public Vector2 Dir;
            public int Damage;
            public float Speed;
            public LayerMask TargetsLayerMask;
        }


        [SerializeField] private ElementAttack myElementAttack;
        [SerializeField] private float lifeTime = 10f;

        private BulletStats m_stats;
        private bool m_initialized;

        public BulletStats GetStats() => m_stats;
        public void Initialize(Vector2 p_dir, int p_damage, float p_speed, LayerMask p_targetMask)
        {
            m_stats = new BulletStats();
            m_stats.Dir = p_dir.normalized;
            m_stats.Damage = p_damage;
            m_stats.Speed = p_speed;
            m_stats.TargetsLayerMask = p_targetMask;

            myElementAttack.InitAttack(this);
            m_initialized = true;
            Destroy(gameObject, lifeTime);

        }

    

        private void Update()
        {
            if (!m_initialized)
                return;

            myElementAttack.ExecuteAttack(this);
        }

        public void Move(Vector3 p_dir, float p_speed)
        {
            transform.position += p_dir * (p_speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != 11)
                return;

            myElementAttack.OnImpact(this, collision);
        }
    }
}