using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("Rpg/Weapon"))]
    public class Weapon : ScriptableObject
    {


        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        [SerializeField] Transform weaponGrip;
        [SerializeField] float minTimeBetweenHits = 0.5f;
        [SerializeField] float maxAttackRange = 2f;

        public float MinTimeBetweenHits { 
            get { return minTimeBetweenHits;}       //TODO: vrijeme animacije uzet u obzir
        }

        public float MaxAttackRange {
            get { return maxAttackRange;}   
        }        
                    
                

            
        public GameObject GetWeaponPrefab()
        {

            return weaponPrefab;
        }

        public Transform GetWeaponGrip()
        {
            return weaponGrip;
        }

        public AnimationClip GetAttackAnimClip()
        {

            attackAnimation.events = new AnimationEvent[0]; //remove Animation events, kak ne bi bilo problema s assetima koji imaju evente unaprijed 
            return attackAnimation;

        }
    }
}