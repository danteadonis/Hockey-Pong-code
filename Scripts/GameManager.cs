using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public GameObject buttonSounds;
    public GUISkin layout;

    private MoveRightStick rightStick;
    private MoveLeftStick leftStick;
    private AI aI;

    AudioSource audioData;
    GameObject ball;
    GameObject[] pauseObjects;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

        Time.timeScale = 1;

        rightStick = FindObjectOfType<MoveRightStick>();
        leftStick = FindObjectOfType<MoveLeftStick>();
        aI = FindObjectOfType<AI>();

        audioData = GetComponent<AudioSource>();

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        Debug.Log("quitting game.");
#endif
    }

    //reload level
    public void Reload()
    {
        Time.timeScale = 0;
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        rightStick.ResetStick();
        leftStick.ResetStick();

        //if ai stick is disabled, ignore call.
        if (aI.gameObject.activeInHierarchy == true || aI.isActiveAndEnabled == true)
        {
            aI.ResetStick();
        }
        ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }

    //control pausing of the scene
    public void pauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            audioData.Pause();
            showPaused();
        }else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            audioData.UnPause();
            hidePaused();
        }
    }

    public void PlaySound()
    {
        Instantiate(buttonSounds, transform.position, Quaternion.identity);
    }

    //show objects with ShowOnPause tag
    public void showPaused() { foreach(GameObject g in pauseObjects) { g.SetActive(true); } }

    //hide objects with ShowOnPause tag
    public void hidePaused() { foreach(GameObject g in pauseObjects) { g.SetActive(false); } }

    //load inputted level
    public void LoadLevel(string level) { SceneManager.LoadScene(level); }

    public static void Score(string goalID)
    {
        if(goalID == "goal right back")
        {
            PlayerScore1++;
        }else if(goalID == "goal left back")
        {
            PlayerScore2++;
        }
    }

    private IEnumerator AfterWin()
    {
        yield return new WaitForSeconds(2);
        showPaused();
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 290 - 22, 8, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 290 + 2, 8, 100, 100), "" + PlayerScore2);

        if(GUI.Button(new Rect(Screen.width / 2 - 38, 8, 80, 18), "PAUSE"))
        {
            pauseControl();
        }

        if (PlayerScore1 == 7)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 230, 2000, 1000), "PLAYER 1 WINS!");
            StartCoroutine(AfterWin());

            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }else if (PlayerScore2 == 7)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 230, 2000, 1000), "PLAYER 2 WINS!");
            StartCoroutine(AfterWin());

            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}
