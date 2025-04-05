using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerClosestItem : MonoBehaviour
{
    public float pickableDistance;

    public GameObject currentHighlight;
    private LayerMask _searchLayer;

    // Call once before Start()
    private void Awake()
    {
        _searchLayer = LayerMask.GetMask("Items");
    }

    void Update()
    {
        HighlightItem(FindClosest());
    }

    private void HighlightItem(GameObject closestItem)
    {
        if (currentHighlight != closestItem)
        {
            if (currentHighlight) currentHighlight.GetComponent<itemManager>().Highlight(false);
            if (closestItem) closestItem.GetComponent<itemManager>().Highlight(true);
            
            currentHighlight = closestItem;
        }
    }
    
    private GameObject FindClosest()
    {
        GameObject closest = null;
        RaycastHit2D[] listOfItems = Physics2D.CircleCastAll(this.transform.position, pickableDistance, Vector2.zero, 0f, _searchLayer);

        if (listOfItems != null)
        {
            float minDistance = float.MaxValue;
            foreach (var item in listOfItems)
            {
                float distance = Vector2.Distance(item.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = item.collider.gameObject;
                }
            }
        }

        return closest;
    }

    // For Debug
    // Draw radius of pickable area
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, pickableDistance);
    }
}