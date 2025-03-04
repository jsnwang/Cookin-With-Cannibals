﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class GradePoster : MonoBehaviour
    {
        [Header("Scores")]
        public int A_score;
        public int B_score;
        public int C_score;
        public int numCustomers;

        [Header("UI Elements")]
        public Text A_text;
        public Text B_text;
        public Text C_text;
        public Text numCustomersText;
        
        
        public void SetScores(int A, int B, int C)
        {
            A_score = A;
            B_score = B;
            C_score = C;

            SetPosterTextGrades();
        }

        public void SetPosterTextGrades()
        {
            A_text.text = $"{A_score}";
            B_text.text = $"{B_score}";
            C_text.text = $"{C_score}";
        }

        public void SetNumCustomers(int count)
        {
            numCustomers = count;
            numCustomersText.text = $"{numCustomers}";
        }

    }
}