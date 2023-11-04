using Guns.Bullets;
using UnityEngine;

namespace Guns
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Main/Guns/Data/GunData", order = 0)]
    public class GunData : ScriptableObject
    {
        public BulletModel bulletModel;
        public float damage;
        public float speed;
        public LayerMask targetMask;
    }
    
    
    
}