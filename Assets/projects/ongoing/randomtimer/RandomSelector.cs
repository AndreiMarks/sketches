using System;
using System.Collections.Generic;
using Prime31.ZestKit;
using UnityEngine;
using UnityEngine.UI;

public class RandomSelector : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private RectTransform _indicatorHolder;
    [SerializeField] private RandomSelectionIndicator _randomSelectionIndicatorPrefab;
    [SerializeField] private RandomSelectionIndicator _selectionIndicator;

    private List<RandomSelectionIndicator> _indicators = new List<RandomSelectionIndicator>();

    void Awake()
    {
        PopulateIndicators(3);
    }

    private void PopulateIndicators(int indicatorCount)
    {
        for (int i = 0; i < indicatorCount; i++)
        {
            RandomSelectionIndicator indicator = _indicatorHolder.InstantiateChild(_randomSelectionIndicatorPrefab);
            _indicators.Add(indicator);
        }
        
        Recolor();
    }

    private void Recolor()
    {
        HSBColor baseColor = _baseColor.ToHSBColor();
        float hueOffset = UnityEngine.Random.value;

        for (int i = 0; i < _indicators.Count; i++)
        {
            RandomSelectionIndicator indicator = _indicators[i];
            float hue = hueOffset + (float)i / _indicators.Count;
            if (hue > 1f) hue -= 1f;
            Color color = HSBColor.ToColor( new HSBColor(hue, baseColor.s, baseColor.b));
            indicator.SetColor(color);
        }
    }
    
    public void MakeSelection()
    {
        Recolor();
        RandomSelectionIndicator selected = _indicators.RandomElement();
        _selectionIndicator.SetColor(selected.Color);
    }
}
