using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnter : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
            gameObject.GetComponent<Animator>().SetTrigger("destroy");
    }

    public void AnimationEventDestroy()
    {
        Destroy(gameObject);
    }
}
