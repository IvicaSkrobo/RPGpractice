using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utility;


namespace RPG.Weapons
{

    public class Projectile : MonoBehaviour
    {


        [SerializeField] float projectileSpeed;

        [SerializeField] GameObject shooter; // da mogu vidit dok je pauzano
        float damageCaused;
        const float DESTROY_DELAY = 0.01f;

        public void SetShooter(GameObject shooter)
        {

            this.shooter = shooter;
        }

        public void SetDamage(float damage)
        {
            damageCaused = damage;
        }

        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }
        void OnCollisionEnter(Collision col)
        {

            if (shooter && col.gameObject.layer != shooter.layer)
            {
                DamageIfDamageable(col);
            }

        }

        private void DamageIfDamageable(Collision col)
        {
            Component damageableComponent = col.gameObject.GetComponent(typeof(IDamageable));

            if (damageableComponent)
            {

                (damageableComponent as IDamageable).TakeDamage(damageCaused);


            }
                        Destroy(gameObject, DESTROY_DELAY);

        }
    }
}