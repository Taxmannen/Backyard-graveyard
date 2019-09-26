using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationValues : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] public VibrationSettings pickUpVibration;
    [SerializeField] public VibrationSettings putDownVibration;

    [Header("Tools")]
    [SerializeField] public VibrationSettings weaponHit;
    [SerializeField] public VibrationSettings pushingButton;
    [SerializeField] public VibrationSettings burningStuffWithTorch;

    [Header("Other")]
    [SerializeField] public VibrationSettings objectThiefPullingObjectFromYourHand;
    [SerializeField] public VibrationSettings objectThiefPullingObjectFromYourHandJump;
    [SerializeField] public VibrationSettings touchingStuff;

    [Serializable]
    public struct VibrationSettings
    {
        [Tooltip("The duration of the vibration")]
        public float duration;
        
        [Tooltip("The strenght of every pulse in the vibration")]
        [Range(0.0f, 1.0f)]
        public float amplitude;

        [Tooltip("The frequency (speed) of every pulse in the vibration")]
        [Range(0.0f, 320.0f)]
        public float frequency;
    }

}


