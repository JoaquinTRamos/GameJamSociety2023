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
        private void Update()
        {
            /* if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                data = TEST_DATAS[0];
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                data = TEST_DATAS[1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                data = TEST_DATAS[2];
            }

            
            
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot(Vector2.right);
            } */
        }

        public void Shoot(Vector2 p_dir)
        {
            var bull = Instantiate(data.bulletModel, shootPoint.position, Quaternion.identity);
            bull.Initialize(p_dir, data.damage, data.speed, data.targetMask);
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