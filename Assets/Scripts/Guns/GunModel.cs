using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Guns
{
    public class GunModel : MonoBehaviour, IGun
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GunData data;
        [SerializeField] private List<GunData> TEST_DATAS;


        private bool m_isThrowing;
        public GunData GetData(){
            return data;
        }
        

        float cooldown;

        
        [SerializeField] private float ammo;

        private void Start()
        {
            cooldown = 0;
            ammo = data.ammo;
            m_isThrowing = false;
        }

        public void Shoot(Vector2 p_dir)
        {
            if (ammo == 0) return;

            if (cooldown >= 0)
            {
                cooldown -= Time.deltaTime;
                return;
            }

            cooldown = data.fireRate;
            ammo--;

            var bullet = Instantiate(data.bulletModel, shootPoint.position, Quaternion.identity);
            bullet.Initialize(p_dir, data.damage, data.speed, data.targetMask);
        }

        public void Throw()
        {
            Debug.Log("Lanzao");
            m_isThrowing = true;
            var x = StartCoroutine(TimerForExplosion());
            
        }

        private IEnumerator TimerForExplosion()
        {
            yield return new WaitForSeconds(data.explosionTimer);
            Explosion();
            yield break;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!m_isThrowing)
                return;
            
            
        }

        private void Explosion()
        {
            Debug.Log("EPLOTA");
            var particle = Instantiate(data.particleExplosionSystem);
            particle.transform.position = transform.position;
            particle.Play();
            RaycastHit2D[] hits = new RaycastHit2D[100];
            var count = Physics2D.CircleCastNonAlloc(transform.position.normalized, data.explosionRadius, 
                Vector2.zero, hits);


            for (int i = 0; i < count; i++)
            {
                if (!hits[i].collider.gameObject.TryGetComponent(out IDamageable damageable))
                    continue;
                
                if(damageable == this)
                    continue;
                
                damageable.Damage(data.explosionDamage, data.elementType);
            }
            Debug.Log("Desaparece");
            Destroy(gameObject);
        }
    }
}