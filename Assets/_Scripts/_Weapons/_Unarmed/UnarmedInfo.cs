using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Unarmed Info", menuName = "Equipment/Unarmed")]
public class UnarmedInfo : MeleeWeaponInfo
{
    #region Internal
    [Serializable]
    public class UnarmedSettingsInfo
    {
        [BHeader("( Arm Show )")]

        public bool AlwaysShowArms;

        [EnableIf("AlwaysShowArms", false, 10f)]
        [Tooltip("How much time the arms will be on the screen if the Player punches")]
        public float ArmsShowDuration = 3f;

        public DelayedSound ShowArmsAudio;

        [BHeader("( Running )")]
            
        public float RunAnimSpeed = 1f;
        public float RunAnimStartTime = 0.5f;
    }

    #endregion

    [Group("5: ")] public UnarmedSettingsInfo UnarmedSettings;
}