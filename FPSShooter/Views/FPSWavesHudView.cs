
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public partial class FPSWavesHudView
{ 
    public Text _HealthText;
    public Text _KillsText;
    public Text _WaveText;
    public Text _WaveStartText;
    

    /// Subscribes to the state machine property and executes a method for each state.
    public override void WavesStateChanged(Invert.StateMachine.State value) {
        base.WavesStateChanged(value);
    }
    
    public override void OnWave() {
        base.OnWave();
    }
    
    public override void OnGameOver() {
        base.OnGameOver();
    }
    
    public override void OnWaitForNextWave() {
        base.OnWaitForNextWave();
    }

    //public override void KillsChanged(int value)
    //{
    //    _TotalKillsLabel.text = string.Format("Kills: {0}", value);
    //}

    public override void WaveKillsChanged(int kills)
    {
        _KillsText.text = string.Format("Kills: {0}", kills);
    }

    public override void CurrentWaveChanged(int wave)
    {
        _WaveStartText.gameObject.SetActive(true);
        _WaveStartText.text = string.Format("Wave {0} started!", wave);
        _WaveText.text = string.Format("Wave: {0}", wave);
        StartCoroutine(HideLabelInSeconds());
        WaveKillsChanged(WavesFPSGame.WaveKills);
    }

    public override void KillsToNextWaveChanged(int value)
    {
        base.KillsToNextWaveChanged(value);
        CurrentWaveChanged(WavesFPSGame.CurrentWave);
    }

    public IEnumerator HideLabelInSeconds(float seconds = 3f)
    {
        yield return new WaitForSeconds(seconds);
        _WaveStartText.gameObject.SetActive(false);
    }
}
