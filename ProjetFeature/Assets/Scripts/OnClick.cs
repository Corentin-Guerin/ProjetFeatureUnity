using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnClick : MonoBehaviour
{
    public UnityEvent _onClick;

    public void OnMouseDown()
    {
        _onClick.Invoke();
    }

}
