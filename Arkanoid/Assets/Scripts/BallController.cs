using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float speedIncrease = 0.5f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private UnityAction<BallController> onBallDestroyed;
    private bool isPlayingSoundEffect = false;

    public void Init(UnityAction<BallController> onBallDestroyed)
    {
        this.onBallDestroyed = onBallDestroyed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var r = Random.Range(-1.0f, 1.0f);
        if (r == 0) r = 0.5f; // Evitar que r sea 0
        rb.velocity = new Vector2(r, 2).normalized * initialSpeed;
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            if (!isPlayingSoundEffect)
            {
                GameManager.instance.soundManager.PlayBallDeadSoundEffect();
                isPlayingSoundEffect = true;
            }
            rb.velocity = Vector2.zero;
            gameObject.transform.DOShakePosition(1.0f, 10, 10, 50, true).OnComplete(() =>
            {
                Destroy(this.gameObject);
                onBallDestroyed?.Invoke(this);

            });
        }
        else
        {
            GameManager.instance.soundManager.PlayBounceSoundEffect();

            Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            float currentSpeed = rb.velocity.magnitude;
            float newSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);

            rb.velocity = direction * newSpeed;
        }
    }
}
