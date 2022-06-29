using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void DestroyAfterTime(float seconds)
    {
        StartCoroutine(DestroyAfterTimeCoroutine(seconds));
    }
    private IEnumerator DestroyAfterTimeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
