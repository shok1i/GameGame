using UnityEngine;

public class Outliner : MonoBehaviour
{
    private Renderer _spriteRenderer; 
    
    private Shader _shaderDefault;
    private Shader _shaderOutline;
    
    // Run once a time Before Start()
    private void Awake()
    {
        _shaderDefault = Shader.Find("Sprites/Default");
        _shaderOutline = Shader.Find("Custom/OutlineShader");
        
        _spriteRenderer = GetComponent<Renderer>();
    }

    public void ChangeHighlightStatus(bool status)
    {
        _spriteRenderer.material.shader = status ? _shaderOutline : _shaderDefault;
        Debug.Log($"ChangeHighlightStatus({status})");
        Debug.Log($"ChangeHighlightStatus({_spriteRenderer.material.shader.name})");
    }
}