using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarSmooth : Bar
{
    private float _changeSpeed = 30f;
    private Coroutine _coroutineChange;

    public override void ChangeDisplay()
    {
        if(_coroutineChange != null)
        {
            StopCoroutine(_coroutineChange);
        }

        _coroutineChange = StartCoroutine(ChangingValue());
    }

    private IEnumerator ChangingValue()
    {
        while(_slider.value != _health.CurrentHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.CurrentHealth, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}