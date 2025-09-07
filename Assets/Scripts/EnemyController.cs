using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreControllerObj;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyHealth;
    [SerializeField] private bool isHit;

    private Animator enemyAnimator;
    [SerializeField] private AnimationClip hitAnim;

    [SerializeField] private AudioSource dieAudioSource;

    void Start()
    {
        transform.position = new Vector2(9.25f, Random.Range(-2.2f, -4.7f));
        enemyAnimator = GetComponent<Animator>();

        scoreControllerObj = FindFirstObjectByType<ScoreController>();
    }

    private void FixedUpdate()
    {
        if (isHit == false)
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }

        if (enemyHealth <= 0)
        {
            dieAudioSource.Play();
            Destroy(gameObject);
            scoreControllerObj.score = scoreControllerObj.score + 100;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHealth--;
            enemyAnimator.Play(hitAnim.name);
            StartCoroutine(getHit());
        }
    }

    IEnumerator getHit()
    {
        isHit = true;
        transform.Translate(new Vector3(.2f, 0, 0));
        yield return new WaitForSeconds(1);
        isHit = false;
    }
}
