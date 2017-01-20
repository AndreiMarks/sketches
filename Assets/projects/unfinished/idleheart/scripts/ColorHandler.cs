using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour 
{
    [SerializeField] private Color _shopColor;
    public Color ShopColor { get { return _shopColor; } }
}
