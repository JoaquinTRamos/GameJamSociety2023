using System;
using UnityEngine;

namespace Guns
{
    public class GunModel : MonoBehaviour, IGun
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GunData data;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot(Vector2.right);
            }
        }

        public void Shoot(Vector2 p_dir)
        {
            var bull = Instantiate(data.bulletModel, shootPoint.position, Quaternion.identity);
            bull.Initialize(p_dir, data.damage, data.speed);
        }

        public void Throw()
        {
            throw new System.NotImplementedException();
        }
    }
}