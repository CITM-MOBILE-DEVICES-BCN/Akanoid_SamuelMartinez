using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class ObstacleData
{
    public Sprite icon;
    public BoosterConfig booster;
    public int health;
}

public class ObstacleController : MonoBehaviour
{
    [SerializeField] Image icon;
    private BoosterConfig booster;
    private int health;
    private bool isShaking = false;
    private UnityAction<Vector2, BoosterConfig, ObstacleController> onBoosterSpawning;

    public void Init(ObstacleConfig config, UnityAction<Vector2, BoosterConfig, ObstacleController> onBoosterSpawning)
    {
        booster = config.booster;
        icon.sprite = config.icon;
        health = config.health;
        this.onBoosterSpawning = onBoosterSpawning;
    }
    public void Init(ObstacleData data, UnityAction<Vector2, BoosterConfig, ObstacleController> onBoosterSpawning)
    {
        booster = data.booster;
        icon.sprite = data.icon;
        health = data.health;
        this.onBoosterSpawning = onBoosterSpawning;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")&& !isShaking)
        {
            health--;
            if (health > 0)
            {
                transform.DOShakePosition(0.3f, 10, 20, 50, false, true);               
                return;
            }
            else if(health <= 0)
            {
                isShaking = true;
                transform.DOShakePosition(1.0f, 10, 50, 90, false, true).OnComplete(() =>
                {
                    GameManager.instance.soundManager.PlayScoreEfect();
                    isShaking = false;
                    onBoosterSpawning?.Invoke(new Vector2(transform.position.x, transform.position.y), booster, this);
                    GameManager.instance.facade.ObstacleDestroyed();
                    Destroy(gameObject);
                });
            }
        }
    }
    
    public ObstacleData GetObstacleData()
    {
        return new ObstacleData
        {
            icon = icon.sprite,
            booster = booster,
            health = health
        };
    }
}
