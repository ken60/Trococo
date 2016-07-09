using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
	public void ButtonClick ()
	{
        SceneManager.LoadScene("Game");
	}
}