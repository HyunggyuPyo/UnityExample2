using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeControllerTest : MonoBehaviour
{
    [Color(0,1,0,1)]
    public new Renderer renderer; //new ±ª¿Ã æ» Ω·µµ µ 
    
    [SerializeField, Color(r:1, b:.5f)]
    private Graphic graphic;

    //[Color]
    //public float notRendererOrGraphic;

    [SerializeField, Size(x: 2, y: .5f, z: .5f)]
    public new Transform transform;

    [SerializeField, Size(x: .5f, y: .5f, z: .5f)]
    public RectTransform rectTransform;

    [SerializeField, Size(width: 60, height: 80)]
    public RectTransform rectSize;
}
