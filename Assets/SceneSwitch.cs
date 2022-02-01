using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchSceneToCredits()
    {
        DialogueManager.StopConversation();
        StartCoroutine(Delay(2));
    }
    
    private IEnumerator Delay(int delayAmount)
    {
        yield return new WaitForSeconds(delayAmount);
        SceneManager.LoadScene(1);
    }
}
