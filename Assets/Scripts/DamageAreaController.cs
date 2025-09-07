using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageAreaController : MonoBehaviour
{
    public GameObject scoreControllerObj;
    public int health;
    public TMP_Text healthText;

    static public bool once_call;

    [SerializeField] private AudioSource damageSound;

    private void Start()
    {
        scoreControllerObj = GameObject.Find("ScoreController");
        health = 3;
        healthText.text = health.ToString();

        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(health <= 0)
        {
            health = 3;
            scoreControllerObj.GetComponent<ScoreController>().score = 0;
            SceneManager.LoadScene("SampleScene");
        }

        healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            health--;
            healthText.text = health.ToString();
            damageSound.Play();
        }
    }
}
