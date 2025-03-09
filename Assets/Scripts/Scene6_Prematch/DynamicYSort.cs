using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicYSort : MonoBehaviour
{

    private int _baseSortinOrder;
    private float _ySortingOffset;

    [SerializeField] private SortableSprite[] _sortableSprites;
    [SerializeField] private Transform _sortOffsetPivot;
    // Start is called before the first frame update
    private void Start()
    {
        _ySortingOffset = _sortOffsetPivot.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _baseSortinOrder = transform.GetSortingOrder(/*_ySortingOffset*/);
        foreach (var sortableSprite in _sortableSprites)
        {
            sortableSprite.spriteRenderer.sortingOrder =
                _baseSortinOrder + sortableSprite.relativeOrder;
        }
    }   
    
    [Serializable] 
    public struct SortableSprite
    {
        public SpriteRenderer spriteRenderer;
        public int relativeOrder;
        
    }
}
