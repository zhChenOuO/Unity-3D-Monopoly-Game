using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public void OnStartGame(string ScneneName)
    {
        SceneManager.LoadScene(ScneneName); //讀取場景,場景名稱
    }
}
