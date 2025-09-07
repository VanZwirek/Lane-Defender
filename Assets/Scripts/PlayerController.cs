using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private bool HasShot = false;
    [SerializeField] private bool isShooting = false;
    public Animator fireAnimator;
    [SerializeField] private GameObject TankBullet;
    [SerializeField] private GameObject BulletAnchor;
    private Coroutine shootBulletRef;
    [SerializeField] private GameObject damageController;
    [SerializeField] private AudioSource shootAudioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        playerPos = BulletAnchor.transform.position;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("shoot started");
            isShooting = true;
            if (shootBulletRef == null)
            shootBulletRef = StartCoroutine(ShootBullet());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("shoot stopped");
            isShooting = false;
            shootBulletRef = null;
        }
    }

    IEnumerator ShootBullet()
    {
        while (isShooting == true)
        {
            if (HasShot == false)
            {
                HasShot = true;
                shootAudioSource.Play();
                fireAnimator.Play("FireBullet");
                Instantiate(TankBullet, playerPos, Quaternion.identity);
                yield return new WaitForSeconds(1);
                HasShot = false;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("damage");
            damageController.GetComponent<DamageAreaController>().health--;
            Destroy(collision.gameObject);
        }
    }
}