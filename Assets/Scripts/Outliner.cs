using System;
using Unity.VisualScripting;
using UnityEngine;

public class Outliner : MonoBehaviour
{
    [SerializeField] public Color selectedColor = Color.black;
    [SerializeField] public float outlineThickness = 0.5f;

    private void Start()
    {
        GameObject childObject = new GameObject("Outliner");
        SpriteRenderer childRenderer = childObject.AddComponent<SpriteRenderer>();
        
        childRenderer.sprite = transform.GetComponent<SpriteRenderer>().sprite;
        childRenderer.material.color = selectedColor;

        childObject.transform.position = transform.position + new Vector3(0, 0, 1f);
        childObject.transform.localScale = new Vector3(1f + outlineThickness, 1f + outlineThickness, 1f);
        childObject.transform.SetParent(transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}