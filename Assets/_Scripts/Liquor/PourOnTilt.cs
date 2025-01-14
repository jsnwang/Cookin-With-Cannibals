﻿using System;
using UnityEngine;

namespace Liquor
{
    class PourOnTilt : MonoBehaviour
    {
        public ParticleSystem pourFX;
        public GameObject pourArea;

        void Start()
        {
            
        }

        void Update()
        {
            if (IsTilted())
            {
                StartPour();
            }
            else
            {
                //StopPour();
            }
        }

        private bool IsTilted()
        {
            float z = transform.localEulerAngles.z;
            Debug.Log($"z: {z}");

            // INVESTIGATE
            if (!(z > 90 && z < 270))
            {
                return true;
            }

            return false;
        }

        void StartPour()
        {
            Debug.Log("Pouring");
            pourArea.SetActive(true);
            pourFX.Play();
        }

        void StopPour()
        {
            Debug.Log("Stopped Pouring");
            pourArea.SetActive(false);
            pourFX.Stop();
        }
    }
}
