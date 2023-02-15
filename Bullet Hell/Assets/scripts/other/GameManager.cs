using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    private int currentSecond = 0;
    private int currentMinutes = 0;
    public int CurrentMinutes => currentMinutes;
    UnityEvent onMinuteChange = new();
    public UnityEvent onGameOver;
    UnityEvent onSecondChange = new();
    public int CurrentSeconds => currentSecond;
    public bool GameOver => gameOver;
    private bool gameOver = false;
    private Coroutine timerCor;
    private bool paused;

    public const int ENEMYLIMIT = 500;
    
    public bool Paused => paused;
    new void Awake(){
        base.Awake();
        //onMinuteChange= new UnityEvent();
        //onSecondChange= new UnityEvent();
        currentSecond = 0;
    }
    void Start(){
        timerCor = StartCoroutine(UpdateSeconds());
        Time.timeScale = 1;
    }
    public void AddMinuteEvent(UnityAction call){
        onMinuteChange.AddListener(call);
    }
    public void RemoveMinuteEvent(UnityAction call){
        onMinuteChange.RemoveListener(call);
    }
    public void AddSecondEvent(UnityAction call){
        onSecondChange.AddListener(call);
    }
    public void RemoveSecondEvent(UnityAction call){
        onSecondChange.RemoveListener(call);
    }
    public void Lose(){
        gameOver = true;
        onGameOver.Invoke();
        //Other things that happen when game over is true
        Invoke ("SceneTransition", 0);
    }
    private void SceneTransition(){ //tymczasowo zmienieo na slowmotion
        //Time.timeScale = 0;
        StartCoroutine(SlowMotion());
    }

    IEnumerator SlowMotion(){
        while (Time.timeScale > 0){
            Time.timeScale = Time.timeScale - 0.25f*Time.unscaledDeltaTime <0 ? 0:Time.timeScale - 0.25f*Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        var parameters = new LoadSceneParameters(LoadSceneMode.Single);
        SceneManager.LoadScene("Forest", parameters);
    }
    IEnumerator UpdateSeconds(){
        while (true){
            yield return new WaitForSeconds(1);
            if (paused){
                continue;
            }
            currentSecond++;
            onSecondChange.Invoke();
            if(currentSecond%60==0){
                currentMinutes++;
                // Debug.Log("Invoking Event");
                onMinuteChange.Invoke();
            }
        }
    }
}
