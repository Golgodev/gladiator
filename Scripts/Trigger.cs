using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent events;

    public void TriggerEvent() {
        events.Invoke();
    }

}
