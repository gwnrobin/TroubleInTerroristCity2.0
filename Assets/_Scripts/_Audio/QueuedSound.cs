public class QueuedSound
{
    public DelayedSound DelayedSound { get; private set; }
    public float PlayTime { get; private set; }

    public QueuedSound(DelayedSound clip, float playTime)
    {
        DelayedSound = clip;
        PlayTime = playTime;
    }
}