using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private ItemData itemData;
    private MeshFilter filter;
    private MeshRenderer itemRenderer;
    private MeshCollider mc;
    private Rigidbody rb;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        itemRenderer = GetComponent<MeshRenderer>();

        mc = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(6, 10), Random.Range(-2, 2)), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * .3f, ForceMode.Impulse);
    }

    public void SetupItemObejct(ItemData data)
    {
        itemData = data;

        filter.mesh = itemData.itemMeshData.mesh;
        itemRenderer.material = itemData.itemMeshData.material;
        mc.sharedMesh = itemData.itemMeshData.mesh;
    }

    public void PickupItem()
    {
        if (Manager.Data.inventory.CanAdd())
        {
            Manager.Data.inventory.AddItem(Manager.Data.inventory.inventory.Count, itemData);
            Destroy(gameObject);
        }
    }
}
