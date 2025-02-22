using UnityEngine;

public class Outliner : MonoBehaviour
{
    [SerializeField] public Color selectedColor = Color.yellow;
    [SerializeField] public float outlineThickness = 0.025f;
    
    // other - это тот кто справоцировал

    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.SetColor("_OutlineColor", selectedColor);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _material.SetFloat("_OutlineWidth", outlineThickness);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _material.SetFloat("_OutlineWidth", 0f);
    }
}