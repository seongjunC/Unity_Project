using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private MonsterStatusController statusCon;
    [SerializeField] private BoxCollider bc;

    private void Awake()
    {
        ColliderDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable.TakeDamage(statusCon.status.damage);
        }
    }   

    public void ColliderEnable()
    {
        bc.enabled = true;
    }
    public void ColliderDisable()
    {
        bc.enabled = false;
    }
}
