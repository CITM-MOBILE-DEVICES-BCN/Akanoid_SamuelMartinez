using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterController : MonoBehaviour
{
    public enum boostertype
    {
        SpawnBallsOnBar,
        SpawnBalls,
        ReduceVelocity,
        BiggerBar,
        None
    }

    [SerializeField] private Image icon;
    [SerializeField] private float speed;

    private boostertype type;
    private Vector2 velocity;
    private Rigidbody2D rb;

    private UnityAction<boostertype> onTriggerBooster;

    public void Init(BoosterConfig config, UnityAction<boostertype> onTriggerBooster)
    {
        type = config.boostertype;
        icon.sprite = config.icon;
        this.onTriggerBooster = onTriggerBooster;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetInitialVelocity();
    }

    public void SetInitialVelocity()
    {
        velocity.y = -100;

        rb.AddForce(velocity * speed);

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            GameManager.instance.soundManager.PlayPowerUpSoundEffect();
            onTriggerBooster?.Invoke(type);
            Destroy(this.gameObject);
        }
    }

    public boostertype GetBoosterType()
    {
        return type;
    }
}
