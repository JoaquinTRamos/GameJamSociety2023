using Guns.Bullets;
using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Main/Guns/Data/GunData", order = 0)]
    public class GunData : ScriptableObject
    {
        public BulletModel bulletModel;
        public int damage;
        public float speed;
        public LayerMask targetMask;
        public GameObject gunSkin;
        public float fireRate;
        public float ammo;
    }



}