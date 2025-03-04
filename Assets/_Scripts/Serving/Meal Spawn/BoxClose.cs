﻿using UnityEngine;

namespace Serving
{
    public class BoxClose : MealReadyCheck
    {
        public override void MakeNotReady()
        {
            isReady = false;
        }

        public override void MakeReady()
        {
            isReady = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Styrofoam_Top"))
            {
                MakeReady();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Styrofoam_Top"))
            {
                MakeNotReady();
            }
        }
    }
}