using System;

[Serializable]
public class DelayedSound : ICloneable
{
    public float Delay;
    public SoundPlayer Sound;

    public object Clone() => MemberwiseClone();
}