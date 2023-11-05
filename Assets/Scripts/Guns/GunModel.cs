using System;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{
    public class GunModel : MonoBehaviour, IGun
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GunData data;
        [SerializeField] private List<GunData> TEST_DATAS;

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
            throw new System.NotImplementedException();
        }
    }
}