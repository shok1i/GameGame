using UnityEngine;

public class itemManager : MonoBehaviour
{
    public ItemBase instance;
    
    void Start()
    {
        Debug.Log($"{instance} was loaded into scene");
    }

    public void Highlight(bool state)
    {
        
    }
}
