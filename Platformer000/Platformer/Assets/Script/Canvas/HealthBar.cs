using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float _fill;
    private float _hp;

    private void Start()
    {
        _fill = 1f;
        _image.fillAmount = _fill;
    }

    private void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(HPBar());
        }
    }

    private IEnumerator HPBar()
    {
        _hp = 0;
        for (float i = 0; i <= 0.2f; i += Time.deltaTime * 0.01f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            _hp += i;
            if (_hp <= 0.2f)
            {
                _image.fillAmount -= i;
            }
            else 
                yield break;
        }
    }
}

