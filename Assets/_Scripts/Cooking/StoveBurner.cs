﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{

    public class StoveBurner : HeatSource
    {
        [Header("FX")]
        public ParticleSystem flame;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateStove(bool on)
        {
            if (on)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
        public override void TurnOn()
        {
            flame.Play();
            isOn = true;
        }

        public override void TurnOff()
        {
            flame.Stop();
            isOn = false;
        }
        
    }
}