using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Material playerMat;
    public Material enemyMat;
    public Material planeMat;
    public Color[] colorsArray;
    List<int> numbers = new List<int>();
    public GameObject winPopUp;
    public static int levelNo = 0;
    public GameObject[] levelArray;
    public GameObject currentLevel;
    public Vector3 ballStartPos;
    public Transform ball;
    public static bool isWin;
    public GameObject loadingScreen;
    public GameObject RestartGame;
    private void Start()
    {
        ballStartPos = ball.transform.position;
        SetLevel();
        SelectMaterial();
    }
    private void SetLevel()
    {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        isWin = false;
        Time.timeScale = 1;
        currentLevel = Instantiate(levelArray[levelNo]);
        ball.transform.position = ballStartPos;
        loadingScreen.SetActive(false);
    }
    void SelectMaterial()
    {
        for (int i = 0; i < colorsArray.Length; i++)
        {
            numbers.Add(i);
        }
        SetMat(playerMat, false);
        SetMat(enemyMat, false);
        SetMat(planeMat, false);
        SetMat(planeMat, true);

    }
    void SetMat(Material mat, bool isCam)
    {
        int index = numbers[UnityEngine.Random.Range(0, numbers.Count)];
        if (isCam)
            Camera.main.GetComponent<Camera>().backgroundColor = colorsArray[index];
        else
            mat.color = colorsArray[index];
        numbers.Remove(index);
    }
    public void Win()
    {
        isWin = true;
        Time.timeScale = 0.25f;
        winPopUp.SetActive(true);
    }
    public void NextLevel()
    {
        //Destroy(currentLevel);
        currentLevel.SetActive(false);
        levelNo++;
        if (levelNo < levelArray.Length)
        {
            loadingScreen.SetActive(true);
            Invoke("SetLevel", .2f);
            winPopUp.SetActive(false);
        }
        else
        {
            RestartGame.SetActive(true);
        }
    }
    //public void RestartBtnPressed()
    //{
    //    SceneManager.LoadScene("Main Menu");
    //}
}