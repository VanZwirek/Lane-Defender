using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Sprite Explosion;
    [SerializeField] private bool isFlying = true;

    [SerializeField] private AudioSource enemyDamaged;

    void Update()
    {
        if (isFlying == true)
        {
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        } 
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(RocketBoom());
        }
    }

    IEnumerator RocketBoom()
    {
        isFlying = false;
        enemyDamaged.Play();
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = Explosion;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}