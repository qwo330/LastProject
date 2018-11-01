using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    int index = 0;

    [SerializeField]
    private Animator firstButton, secondButton, fifthButton;

    private enum AniList
    {
        FirstForward, FirstBackward,
        SecondForward, SecondBackward,
        ThirdForward, ThirdBackward,
        FourthForward, FourthBackward,
        FifthForward, FifthBackward,
    }
    
    public void OnClickedMenu()
    {
        if (index == 0)
        {
            firstButton.SetTrigger(AniList.FirstForward.ToString());
            secondButton.SetTrigger(AniList.SecondForward.ToString());
            fifthButton.SetTrigger(AniList.FifthForward.ToString());
            index++;
        }
        else if (index == 1)
        {
            firstButton.SetTrigger(AniList.FirstBackward.ToString());
            secondButton.SetTrigger(AniList.SecondBackward.ToString());
            fifthButton.SetTrigger(AniList.FifthBackward.ToString());
            index--;
        }

    }
}
