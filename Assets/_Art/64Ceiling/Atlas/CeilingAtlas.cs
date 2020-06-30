using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

enum SpriteTypeCeiling { een, twee, drie, vier, vijf, zes, zeven, acht, negen, tien, elf, twaalf, dertien, veertien, vijftien}

public class CeilingAtlas : MonoBehaviour
{
    [SerializeField]
    private SpriteTypeCeiling _CurrentlySelected;

    private SpriteRenderer _ObjRenderer;

    [SerializeField]
    private SpriteAtlas _AtlasCeiling;

    // Start is called before the first frame update
    void Start()
    {
        _ObjRenderer = GetComponent<SpriteRenderer>();
        _ObjRenderer.sprite = _AtlasCeiling.GetSprite(_CurrentlySelected.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
