using UnityEngine;
using UnityEngine.Events;

public abstract class IPanel : MonoBehaviour
{
    [Header("STATES")]
    public bool returnable;
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    public virtual void Show()
    {
        OnShow?.Invoke();
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        OnHide?.Invoke();
        gameObject.SetActive(false);
    }
}