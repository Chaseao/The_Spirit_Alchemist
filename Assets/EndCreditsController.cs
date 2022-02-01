using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCreditsController : MonoBehaviour
{
    [SerializeField] AudioSource soundEffect;
    [SerializeField] TextMeshProUGUI main;
    [SerializeField] TextMeshProUGUI caroline;
    [SerializeField] TextMeshProUGUI and;
    [SerializeField] TextMeshProUGUI chase;
    [SerializeField] TextMeshProUGUI thankYou;
    [SerializeField] int timeDelay;

    private IEnumerator WaitForAFewSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void Start()
    {
        StartCoroutine(StartEnding());
    }

    private IEnumerator StartEnding()
    {
        soundEffect.Play();
        main.enabled = true;
        yield return WaitForAFewSeconds(timeDelay);
        soundEffect.Play();
        caroline.enabled = true;
        yield return WaitForAFewSeconds(timeDelay);
        soundEffect.Play();
        and.enabled = true;
        yield return WaitForAFewSeconds(timeDelay);
        soundEffect.Play();
        chase.enabled = true;
        yield return WaitForAFewSeconds(timeDelay);
        soundEffect.Play();
        thankYou.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
