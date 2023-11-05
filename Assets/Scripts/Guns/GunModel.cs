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
        public GunData GetData(){
            return data;
        }
        

        float cooldown;

        
        [SerializeField] private float ammo;

        private void Start()
        {
            cooldown = 0;
            ammo = data.ammo;
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
            //This function is only called after it was thrown, so it just handles the explosion after the fact
            
            //TODO add VFX
        }
        
    }
}