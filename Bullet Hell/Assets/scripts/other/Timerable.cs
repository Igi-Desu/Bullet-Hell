using System.Collections;
using UnityEngine.Events;
using UnityEngine;
/// <summary>
/// Class containing functions for making timers that loop 
/// </summary>
public class Timerable : MonoBehaviour
{
    protected float tickSpeed=0.25f;
    private float timer; 
    /// <summary>
    /// returns current timer speed (how often event happens in seconds 2 -> every 2 seconds)
    /// </summary>
    public float Timer => timer;
    private float timerBase;

    UnityEvent onTimerEndEvents;
    Coroutine timerCoroutine;
    /// <summary>
    /// Returns true if timer is currently running false if it is not
    /// </summary>
    public bool TimerRunning =>timerCoroutine!=null;
    /// <summary>
    /// Add event that should happen when timer ends
    /// </summary>
    /// <param name="callback"></param>
    public void AddTimerEndEvent(UnityAction callback){
        onTimerEndEvents.AddListener(callback);
    }   
    /// <summary>
    /// initialize timer
    /// </summary>
    /// <param name="timer">how often should function be called in seconds 2 -> event happens every 2 seconds</param>
    /// <param name="tickspeed">how often timer ticks, leave as default if you don't need specific ticks </param>
    public void Init(float timer, float tickspeed=0){ 
        onTimerEndEvents=new UnityEvent();
        this.timer=timer;
        timerBase=timer;
        if(tickspeed<=0){
            tickSpeed=timer/10;
            return;
        }   
        this.tickSpeed=tickspeed;
    }
    /// <summary>
    /// Updates timer by new values without stopping it
    /// </summary>
    /// <param name="timer">new timer speed 2 -> event happens every 2 seconds</param>
    /// <param name="tickspeed">new tick speed (again leave as default if not needed)</param>
    public void UpdateTimer(float timer, float tickspeed=0){
        this.timer=timer;
        timerBase=timer;
        if(tickspeed==0){
            tickSpeed=timer/10;
            return;
        }
        this.tickSpeed=tickspeed;
    }
    /// <summary>
    /// starts timer
    /// </summary>
    /// <returns>timer coroutine</returns>
    public Coroutine StartTimer(){
        if(timerCoroutine!=null){
            // Debug.LogWarning("Timer cor is already assigned");
            return timerCoroutine;
        }
        return timerCoroutine = StartCoroutine(TimerLoop());
    }
    IEnumerator TimerLoop(){
        while(true){
            timer-=tickSpeed;
            yield return new WaitForSeconds(tickSpeed);
            if(timer<=0){
                timer=timerBase;
                onTimerEndEvents.Invoke();
            }
        }
        
    }
    /// <summary>
    /// stops timer
    /// </summary>
    public void StopTimer(){
        if(timerCoroutine!=null){
            StopCoroutine(timerCoroutine);
            timerCoroutine=null;
        }
    }
}
