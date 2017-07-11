using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.CameraUI
{
[RequireComponent(typeof(CameraRaycaster))]



    public class CursorAffordance : MonoBehaviour
    {

        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D attackCursor = null;
        [SerializeField] Texture2D outOfBoundsCursor = null;


        //TODO solve fight between const and SerializeField
        [SerializeField] const int walkableLayerNumber = 8; //const can't change at runtime
        [SerializeField] const int enemyLayerNumber = 9;


        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

        CameraRaycaster CameraRaycaster;
        // Use this for initialization
        void Start()
        {
            CameraRaycaster = GetComponent<CameraRaycaster>();
            CameraRaycaster.notifyLayerChangeObservers += OnLayerChange;
        }

        // Update is called once per frame
        void OnLayerChange(int newLayer)  //only called when notifyLayerChangeObservers is called in CameraRaycaster
        {

            switch (newLayer)
            {
                case walkableLayerNumber:
                    Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                    break;

                case enemyLayerNumber:
                    Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(outOfBoundsCursor, cursorHotspot, CursorMode.Auto);
                    return;
            }
        }
        //TODO consider de-registering OnLayerChange on leaving all game scenes
    }
}