using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using RPG.CameraUI;
using RPG.Utility;
using RPG.Weapons;


namespace RPG.Characters
{

    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] int enemyLayer = 9;
        [SerializeField] float maxHealthPoints;
        [SerializeField] float damagePerHit = 10f;
        
        [SerializeField] Weapon weaponInUse;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Animator anim;


        CameraRaycaster cameraRaycaster;
        float currentHealthPoints = 100f;
        float lastHitTime = 0;


        void Start()
        {

            RegisterForMouseClick();
            SetCurrentMaxHealth();
            PutWeaponInHand();
            SetupRuntimeAnimator();

        }

        private void SetCurrentMaxHealth()
        {
            currentHealthPoints = maxHealthPoints;
        }

        private void SetupRuntimeAnimator()
        {
            anim = GetComponent<Animator>();
            anim.runtimeAnimatorController = animatorOverrideController;
            //animatorOverrideController["DEFAULT_ATTACK"] = weaponInUse.GetAttackAnimClip();

        }

        private void PutWeaponInHand()
        {
            var weaponPrefab = weaponInUse.GetWeaponPrefab();
            var weaponGrip = weaponInUse.GetWeaponGrip();
            GameObject dominantHand = RequestDominantHand();
            var weapon = Instantiate(weaponPrefab, dominantHand.transform);  //TODO: move to correct place and child to hand
            weapon.transform.localPosition = weaponGrip.position;
            weapon.transform.localRotation = weaponGrip.rotation;
        }

        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int numberOfDominantHands = dominantHands.Length;
            Assert.IsFalse(numberOfDominantHands <= 0, "No dominant hand found on player, pls add one");
            Assert.IsFalse(numberOfDominantHands > 1, "Multiple dominant hand scripts on Player, pls remove one");
            return dominantHands[0].gameObject;
        }

        private void RegisterForMouseClick()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.notifyMouseClickObservers += onMouseClicked;
        }

        public float healthAsPercentage
        {
            get
            {
                return currentHealthPoints / maxHealthPoints;
            }
        }


        public void TakeDamage(float damage)
        {

            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);


        }


        void onMouseClicked(RaycastHit raycastHit, int layerHit)
        {

            if (layerHit == enemyLayer)
            {
                var enemy = raycastHit.collider.gameObject;
                //check enemy is in maxAttackRange
                if (IsTargetInRange(enemy))
                {
                    AttackTarget(enemy);

                }




            }
        }

        private void AttackTarget(GameObject enemy)
        {
            var enemyComponent = enemy.GetComponent<Enemy>();
            if (Time.time - lastHitTime > weaponInUse.MinTimeBetweenHits)
            {
                ChooseARandomAnimation();

                enemyComponent.TakeDamage(damagePerHit);

                lastHitTime = Time.time;

            }
        }

        private void ChooseARandomAnimation()
        {
                int choiceOfAnimation = (int) Random.Range(1f,6f);
                anim.SetInteger("AttackVariation",choiceOfAnimation);
                anim.SetTrigger("Attack");
        }

        private bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponInUse.MaxAttackRange;
        }
    }
}

