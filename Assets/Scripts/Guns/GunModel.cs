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
private bool isThrowing = false;
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
            if(Input.GetKeyDown(KeyCode.Mouse1)&&!isThrowing){
                isThrowing = true;
                StartCoroutine(Throwing());
            }
            else if(Input.GetKeyUp(KeyCode.Mouse1)&&isThrowing){
                isThrowing = false;
            }
        }
        private IEnumerator Throwing(){
            while(isThrowing){
                transform.Rotate(0,0,1);
                yield return null;
            }
        }
    }
}