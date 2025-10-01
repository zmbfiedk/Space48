using System.Collections;
using TMPro;
using UnityEngine;

public class ShipUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text introductionField;
    [SerializeField] private TMP_Text messageField;

    void Start()
    {
        StartCoroutine(Introduction());
    }

    private IEnumerator Introduction()
    {
        introductionField.enabled = true;
        introductionField.text = "Welcome to Space 4 8.\nMove: Arrows/WASD\nShoot: SPACE\nPickups: 'Left CTRL'\nUse: 'E'";
        yield return new WaitForSeconds(5f);
        introductionField.enabled = false;
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        messageField.enabled = true;
        messageField.text = message;
        yield return new WaitForSeconds(3f);
        messageField.enabled = false;
    }
}
