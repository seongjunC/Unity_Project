using System.Collections;
using UnityEngine;

public class PlayerCutSceneController : MonoBehaviour
{
    private Camera main;
    private Player player;
    [SerializeField] private float camMoveSpeed;
    public void PlayUltimateCutScnen() => StartCoroutine(UltimateRoutine());

    private void Awake()
    {
        main = Camera.main;
        player = GetComponent<Player>();
    }

    IEnumerator UltimateRoutine()
    {
        Vector3 camPos = Camera.main.transform.position;

        while(Vector3.Distance(main.transform.position, player.ultTargetTransform.position) > .5f)
        {
            main.transform.position = Vector3.Lerp(main.transform.position, player.ultTargetTransform.position, camMoveSpeed * Time.deltaTime);
        }

        yield return new WaitForSeconds(1.5f);

        main.transform.position = camPos;
        yield return null;
    }
}
