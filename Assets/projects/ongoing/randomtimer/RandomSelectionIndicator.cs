using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSelectionIndicator : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public Color Color
    {
        get { return _fill.color; }
    }

    public void SetColor(Color color)
    {
        _fill.color = color;
    }
}
