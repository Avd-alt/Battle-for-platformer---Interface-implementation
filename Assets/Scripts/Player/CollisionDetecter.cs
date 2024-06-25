using UnityEngine;

[RequireComponent(typeof(Health))]
public class CollisionDetecter : MonoBehaviour
{
    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            Destroy(collision.gameObject);
        }

        if (collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            _playerHealth.Heal(firstAidKit.Heal);

            Destroy(collision.gameObject);
        }
    }
}