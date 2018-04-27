using UnityEngine;

public abstract class BarEvent : MonoBehaviour
{
    public bool active;

    public abstract bool completed();
    public abstract bool animating();
    public abstract void animateIn(Transform parentTransform);
    public abstract void animateOut(Transform parentTransform);
}
