using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PopUpText : BillbordObejct
{
    [SerializeField] private TMP_Text damage;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float startPosSpeed;
    [SerializeField] private float endPosSpeed;
    [SerializeField] private float colorLossSpeed;
    [SerializeField] private float endDelay;
    private bool isEnded;

    private void OnEnable()
    {
        isEnded = false;
    }

    protected override void Update()
    {
        if (!gameObject.activeSelf) return;

        base.Update();

        transform.localScale = Vector3.Lerp(transform.localScale, finalScale, scaleSpeed * Time.deltaTime);

        if(!isEnded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * startPosSpeed, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * endPosSpeed, transform.position.z);
            Color color = ColorLoss(damage);
            damage.color = color;

            if (damage.color.a < 0.01f)
                Destroy(gameObject);
        }
    }

    public void SetupText(string _text, Color color)
    {
        damage.text = _text;
        damage.color = color;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(endDelay);
        isEnded = true;
    }

    private Color ColorLoss(TMP_Text text)
    {
        Color color = text.color;
        color.a -= colorLossSpeed * Time.deltaTime;
        color.a = Mathf.Max(color.a, 0f);
        return color;
    }
}
