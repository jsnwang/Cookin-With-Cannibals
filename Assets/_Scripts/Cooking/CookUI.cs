﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cooking
{
    public class CookUI : MonoBehaviour
    {
        [Header("Slider")]
        public Image fill;

        [Header("Icons")]
        public Image uncooked;
        public Image cooked;
        public Image overcooked;

        private float minimumFill = 0.2f;

        // Start is called before the first frame update
        void Start()
        {
            fill.fillAmount = minimumFill;
        }

        public void UpdateFill(float totalTime, float currentTime)
        {
            // Find the percentage amount to fill considering minimum fill
            // Example: if minimum fill is 0.2, then only 80% is left
            // Scale value to 80% (multiply by 1 - 0.2 = 0.8)
            // Then add minimum fill
            float percentage = currentTime / totalTime;
            float offset = 1 - minimumFill;
            float offsetFill = (percentage * offset) + minimumFill;

            fill.fillAmount = offsetFill;
        }

        public void SetUncookedIcon()
        {
            uncooked.gameObject.SetActive(true);
            cooked.gameObject.SetActive(false);
            overcooked.gameObject.SetActive(false);
        }

        public void SetCookedIcon()
        {
            uncooked.gameObject.SetActive(false);
            cooked.gameObject.SetActive(true);
            overcooked.gameObject.SetActive(false);
        }

        public void SetOvercookedIcon()
        {
            uncooked.gameObject.SetActive(false);
            cooked.gameObject.SetActive(false);
            overcooked.gameObject.SetActive(true);
        }
    }
}