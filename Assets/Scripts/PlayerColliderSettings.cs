using UnityEngine;

public class PlayerCollisionSettings : MonoBehaviour
{
    public Collider2D wallCollider;
    public Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");
        Physics2D.IgnoreCollision(playerCollider, wallCollider, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT");
        Physics2D.IgnoreCollision(playerCollider, wallCollider, false);
    }
}