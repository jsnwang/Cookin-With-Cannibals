﻿using UnityEngine;
using System.Collections;

namespace AI
{
    public class HeroIntroOutro : MonoBehaviour
    {

        private enum OutroType { BEST, GOOD, BAD }

        private OutroType outroType;

        [Header("Audio Times")]
        public float introTime;
        public float bestOutroTime;
        public float goodOutroTime;
        public float badOutroTime;

        private float timeTracker;

        [Header("Audio Clips")]
        public AudioClip introClip;
        public AudioClip bestOutroClip;
        public AudioClip goodOutroClip;
        public AudioClip badOutroClip;

        [HideInInspector]
        public bool introDone;
        [HideInInspector]
        public bool outroDone;

        [HideInInspector]
        public bool isPlayingIntro;
        [HideInInspector]
        public bool isPlayingOutro;

        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (isPlayingIntro)
            {
                timeTracker += Time.deltaTime;
                if (timeTracker >= introTime)
                {
                    isPlayingIntro = false;
                    introDone = true;
                    timeTracker = 0f;

                    Debug.Log("Hero Intro Done");
                }
            }
            else if (isPlayingOutro)
            {
                timeTracker += Time.deltaTime;
                float outroTime = GetOutroTime();

                if (timeTracker >= outroTime)
                {
                    isPlayingOutro = false;
                    outroDone = true;
                    timeTracker = 0f;

                    Debug.Log("Hero Outro Done");
                }
            }
        }

        public void PlayIntro()
        {
            audioSource.clip = introClip;
            audioSource.Play();

            isPlayingIntro = true;
            timeTracker = 0f;
        }

        public void PlayOutro()
        {
            Debug.Log("Playing Hero Outro");

            // ToDo: choose outro properly
            PlayGoodOutro();

            isPlayingOutro = true;
            timeTracker = 0f;
        }

        public void PlayBestOutro()
        {
            outroType = OutroType.BEST;
        }

        public void PlayGoodOutro()
        {
            outroType = OutroType.GOOD;
        }

        public void PlayBadOutro()
        {
            outroType = OutroType.BAD;
        }

        private float GetOutroTime()
        {
            if (outroType == OutroType.BEST)
            {
                return bestOutroTime;
            }
            else if (outroType == OutroType.GOOD)
            {
                return goodOutroTime;
            }
            else if (outroType == OutroType.BAD)
            {
                return badOutroTime;
            }
            else
            {
                return 0f;
            }
        }
    }
}