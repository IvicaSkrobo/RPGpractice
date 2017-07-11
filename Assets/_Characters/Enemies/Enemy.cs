using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.Utility;
using RPG.Weapons;

namespace RPG.Characters
{
    public class Enemy : MonoBehaviour, IDamageable
    {

        [SerializeField] float maxHealthPoints;
        [SerializeField] float attackRadius = 5f;
        [SerializeField] float chaseRadius = 7f;
        [SerializeField] float damagePerShot = 9f;
        [SerializeField] float secondsBetweenShots = 0.5f;
        [SerializeField] GameObject projectileToUse;
        [SerializeField] GameObject projectileSocket;
        [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);


        bool isAttacking = false;
        AICharacterControl aICharacterControl = null;
        GameObject player;
        float currentHealthPoints = 100f;


        public float healthAsPercentage
        {
            get
            {
                return currentHealthPoints / maxHealthPoints;
            }
        }

        void Start()
        {
            aICharacterControl = GetComponent<AICharacterControl>();
            player = GameObject.FindGameObjectWithTag("Player");
            currentHealthPoints = maxHealthPoints;

        }
        void Update()
        {

            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= attackRadius && !isAttacking)
            {

                isAttacking = true;
                print(gameObject.name + " attackingPlayer");

                InvokeRepeating("FireProjectile", 0f, secondsBetweenShots);
                //TODO make a proper coroutine
            }

            if (distance > attackRadius)
            {
                CancelInvoke();
                isAttacking = false;
            }
            else
            {
                aICharacterControl.SetTarget(transform);
            }


            if (distance < chaseRadius)
            {                                 //shift alt a comment all

                aICharacterControl.SetTarget(player.transform);
            }
            else
            {
                aICharacterControl.SetTarget(transform);
            }





        }
        // odvoji firing u odvojenu klasu
        private void FireProjectile()
        {
            GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
            var projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.SetShooter(gameObject);
            projectileComponent.SetDamage(damagePerShot);


            Vector3 unitVectorToPLayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPLayer * projectileComponent.GetDefaultLaunchSpeed();


        }

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            if (currentHealthPoints <= 0)
            {
                Destroy(gameObject);
            }

        }


        void OnDrawGizmos()
        {
            //Draw attack radius sphere
            Gizmos.color = new Color(255f, 0, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.position, attackRadius);
            //Draw chase radius sphere
            Gizmos.color = new Color(0, 0, 255f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, chaseRadius);

        }
    }
}