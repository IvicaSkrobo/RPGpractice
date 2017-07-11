using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters
{

    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]

    public class PlayerMovement : MonoBehaviour
    {


        ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
        CameraRaycaster cameraRaycaster;
        Vector3 clickPoint;
        AICharacterControl aICharacterControl = null;
        GameObject walkTarget = null;
        [SerializeField] const int walkableLayerNumber = 8; //const can't change at runtime
        [SerializeField] const int enemyLayerNumber = 9;

        bool isInDirectMode = false;

        void Start()
        {
            aICharacterControl = GetComponent<AICharacterControl>();
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            walkTarget = new GameObject("walkTarget");
            cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
        }

        void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
        {

            switch (layerHit)
            {
                case enemyLayerNumber:
                    //navigate to the enemy
                    GameObject enemy = raycastHit.collider.gameObject;
                    aICharacterControl.SetTarget(enemy.transform);
                    break;
                case walkableLayerNumber:
                    //navigate to the point on the ground
                    walkTarget.transform.position = raycastHit.point;
                    aICharacterControl.SetTarget(walkTarget.transform);
                    break;
                default:
                    Debug.LogWarning("Don't know how to handle the mouseclick");
                    return;

            }

        }


        //TODO make this get called again
        void ProccessDirectMovement()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");


            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            thirdPersonCharacter.Move(movement, false, false);

        }



    }

}