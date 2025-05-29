using UnityEngine;

public class WeaponOutline : MonoBehaviour
{
    [SerializeField] private Color outlineColor = Color.yellow;
    [SerializeField] private float outlineScaleMultiplier = 1.1f;

    public void enableOutline()
    {
        SpriteRenderer weaponRenderer = GetComponent<SpriteRenderer>();
        if (weaponRenderer == null) return;
        GameObject outline = new GameObject("Outline");
        outline.transform.SetParent(transform);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.identity;
        outline.transform.localScale = Vector3.one * outlineScaleMultiplier;
        SpriteRenderer outlineRenderer = outline.AddComponent<SpriteRenderer>();
        outlineRenderer.flipY = gameObject.GetComponent<SpriteRenderer>().flipY;
        outlineRenderer.sprite = weaponRenderer.sprite;
        outlineRenderer.color = outlineColor;
        outlineRenderer.sortingLayerID = weaponRenderer.sortingLayerID;
        outlineRenderer.sortingOrder = weaponRenderer.sortingOrder - 1;
    }

    public void disableOutline()
{
    Transform outline = transform.Find("Outline");
    if (outline != null)
    {
        Destroy(outline.gameObject);
    }
}
}
