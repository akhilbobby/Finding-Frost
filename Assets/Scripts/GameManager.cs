using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStageChanged;

    private State state;

    private float waitingToStartTimer = 3f;
    private float gamePlayingTimer = 10f;
    private float countdownToStartTimer = 3f;

    WordBreakdown wordBreakdown;

    private enum State
    {
        WaitingToStart,
        ReadingPoem,
        CountdownToStart,
        GamePlaying,
        PlayerScore,
        GameOver,
    }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        wordBreakdown = GetComponent<WordBreakdown>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.ReadingPoem;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.ReadingPoem:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:

                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.PlayerScore;
                    OnStageChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
        Debug.Log(state);
    }
     
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

}
