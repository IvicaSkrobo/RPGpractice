using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public class SpinMe : MonoBehaviour
    {

        [SerializeField] float xRotationsPerMinute = 1f;
        [SerializeField] float yRotationsPerMinute = 1f;
        [SerializeField] float zRotationsPerMinute = 1f;

        void Update()
        {
            //xDegreesPerFrame   = Time.DeltaTime , 60, 360, xRotationsPerMinute
            //degrees frame^-1  = sec  frame^-1 ,seconds minute^-1



            float xDegreesPerFrame = 0; // TODO COMPLETE ME

            xDegreesPerFrame = Time.deltaTime / 60f * 360f * xRotationsPerMinute;


            transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);  //right up and forward, local relativ direction je to

            float yDegreesPerFrame = 0; // TODO COMPLETE ME
            yDegreesPerFrame = Time.deltaTime / 60f * 360f * yRotationsPerMinute;
            transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);

            float zDegreesPerFrame = 0; // TODO COMPLETE ME
            zDegreesPerFrame = Time.deltaTime / 60f * 360f * zRotationsPerMinute;

            transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);
        }
    }
}