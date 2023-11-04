using System.Collections.Generic;
using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    [CreateAssetMenu(fileName = "EarthElementAttack", menuName = "Main/ElementAttacks/EarthElementAttack", order = 0)]
    public class EarthElementAttack : ElementAttack
    {
        [SerializeField] private BulletModel tinyRock;
        public override void InitAttack(BulletModel p_model)
        {
        }

        public override void ExecuteAttack(BulletModel p_model)
        {
            p_model.Move(p_model.GetStats().Dir, p_model.GetStats().Speed);
        }

        public override void OnImpact(BulletModel p_model, Collider2D p_collision)
        {
            var rnd = Random.Range(2, 11);
            for (int i = 0; i < rnd; i++)
            {
                var bullet = Instantiate(tinyRock, p_model.transform.position, p_model.transform.rotation);
                var rndVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                bullet.Initialize(rndVector.normalized, p_model.GetStats().Damage, p_model.GetStats().Speed, p_model.GetStats().TargetsLayerMask);
            }
            
            Destroy(p_model);
        }
    }
}