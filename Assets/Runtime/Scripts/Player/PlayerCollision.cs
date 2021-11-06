using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    private PlayerController playerController;
    private PlayerAnimationController animationController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.PlayCollisionFeedback(other);
            playerController.Die();
            animationController.Die();
            gameMode.OnGameOver();
        }

        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null)
        {
            gameMode.OnCherryPickedUp();
            pickup.OnPickedUp();
        }
    }
}
