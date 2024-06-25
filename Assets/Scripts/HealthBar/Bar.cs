using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Health _health;
    [SerializeField] protected Slider _slider;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        _health.ChangedHealth += ChangeDisplay;
    }

    private void OnDisable()
    {
        _health.ChangedHealth += ChangeDisplay;
    }

    public abstract void ChangeDisplay();
}