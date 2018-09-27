using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{

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



    int index = 0;
    public void OnClickedMenu()
    {
        if (index == 0)
        {
            firstButton.SetTrigger(AniList.FirstForward.ToString());
            secondButton.SetTrigger(AniList.SecondForward.ToString());
            index++;
        }
        else if (index == 1)
        {
            firstButton.SetTrigger("BackwardFirstButton");
            secondButton.SetTrigger("BackwardSecondButton");
            index--;
        }

    }
}
