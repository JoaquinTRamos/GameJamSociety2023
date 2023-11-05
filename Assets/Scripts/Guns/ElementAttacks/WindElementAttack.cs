using Controllers;
using Guns.Bullets;
using UnityEngine;

namespace Guns.ElementAttacks
{
    [CreateAssetMenu(fileName = "WindElementAttack", menuName = "Main/ElementAttacks/WindElementAttack", order = 0)]
    public class WindElementAttack : ElementAttack
    {
        public override void InitAttack(BulletModel p_model)
        {

        }


        public override void ExecuteAttack(BulletModel p_model)
        {
            p_model.Move(p_model.GetStats().Dir, p_model.GetStats().Speed);

        }

        public override void OnImpact(BulletModel p_model, Collider2D p_collision)
        {
            /* //Check if it has a parent
            if (p_collision.transform.parent == null) return;
            if (p_collision.transform.parent.GetComponent<Collider2D>() == null) return;

            p_collision = p_collision.transform.parent.GetComponent<Collider2D>(); */

           if(p_collision.tag == "Hitbox"){
                p_collision = p_collision.transform.parent.GetComponent<Collider2D>();
                p_collision.GetComponent<EnemyController>().Damage(p_model.GetStats().Damage, Element.Wind);}
            //Destroy(p_model);
        }
    }
}