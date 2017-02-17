using UnityEngine;

public class Sort_GUI : MonoBehaviour
{
    string[] sortList = new string[]
    {
        "Panel_MainScreen",
        "DialogBox_CharOpen",

        "CountDown",
        "Tutorial",
        "Squid_ink_A",
        "Squid_ink_B",
        "ScoreText",
        "Game_UI",
        "Panel_Pause",

        "Result",

        "DialogBox_Signin",
        "DialogBox",
    };

    enum eSortType
    {
        Auto,
        Maunal,
        First,
        Last
    }

    [SerializeField]
    private eSortType sortType;
    [SerializeField]
    private int index = 0;

    void Start()
    {
        switch (sortType)
        {
            case eSortType.Auto:
                for (int i = 0; i < sortList.Length; i++)
                {
                    if (this.gameObject.name == sortList[i] || this.gameObject.name == sortList[i] + "(Clone)")
                    {
                        index = i;
                        this.transform.SetSiblingIndex(index);
                        //print(this.gameObject.name + " index: " + this.transform.GetSiblingIndex() + " " + i);
                    }
                }
                break;

            case eSortType.Maunal:
                this.transform.SetSiblingIndex(index);
                break;

            case eSortType.First:
                this.transform.SetAsFirstSibling();

                break;
            case eSortType.Last:
                this.transform.SetAsLastSibling();
                break;
        }
    }
}