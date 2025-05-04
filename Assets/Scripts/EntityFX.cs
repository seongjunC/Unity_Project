using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [SerializeField] private GameObject popUpText;
    [SerializeField] private float offsetY = 2;
    public void CreatePopUpText(int amount, bool isCrit = false)
    {
        if (isCrit)
            CreatePopUp(amount, Color.yellow);
        else
            CreatePopUp(amount, Color.white);
    }

    private void CreatePopUp(int amount, Color color)
    {
        Vector3 randOffset = new Vector3(RandomFloat(-1, 1), RandomFloat(-.2f, .2f), RandomFloat(-1, 1));
        randOffset.y += offsetY;
        GameObject newPopUpText = Manager.Resources.Instantiate(popUpText, transform.position + randOffset, true);
        newPopUpText.GetComponent<PopUpText>().SetupText(amount.ToString(), color);
    }

    private float RandomFloat(float minIndex, float maxIndex)
    {
        return Random.Range(minIndex, maxIndex);
    }
}
