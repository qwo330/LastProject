using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    int index = 0;

    [SerializeField]
    private Animator firstButton, secondButton, thirdButton, fourthButton, fifthButton;

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
            thirdButton.SetTrigger(AniList.ThirdForward.ToString());
            fourthButton.SetTrigger(AniList.FourthForward.ToString());
            fifthButton.SetTrigger(AniList.FifthForward.ToString());
            index++;
        }
        else if (index == 1)
        {
            firstButton.SetTrigger(AniList.FirstBackward.ToString());
            secondButton.SetTrigger(AniList.SecondBackward.ToString());
            thirdButton.SetTrigger(AniList.ThirdBackward.ToString());
            fourthButton.SetTrigger(AniList.FourthBackward.ToString());
            fifthButton.SetTrigger(AniList.FifthBackward.ToString());
            index--;
        }

    }
}
