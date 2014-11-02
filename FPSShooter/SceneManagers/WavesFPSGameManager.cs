
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class WavesFPSGameManager : WavesFPSGameManagerBase
{
    public TextAsset _SceneState;

    public override IEnumerator Load(UpdateProgressDelegate progress)
    {
        //yield break;
        //this.FromJson(_SceneState.text);

        yield break;
    }

    public override void OnLoaded()
    {
        base.OnLoaded();

    }

    public override void Setup()
    {
        base.Setup();
    }
}