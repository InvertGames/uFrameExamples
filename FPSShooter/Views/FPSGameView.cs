using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class FPSGameView
{ 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void CurrentPlayerChanged(FPSPlayerViewModel value) {
        base.CurrentPlayerChanged(value);
    }
 

    public List<FPSEnemyViewBase>  _EnemiesList = new List<FPSEnemyViewBase>();
    
    public override void EnemiesAdded(ViewBase viewBase)
    {
        base.EnemiesAdded(viewBase);
        
        viewBase.transform.position = GetRandomSpawnPoint().position;
    }

    public override void EnemiesRemoved(ViewBase viewBase)
    {
        base.EnemiesRemoved(viewBase);
        Destroy(viewBase.gameObject);
    }

    public Transform _SpawnPointsParent;

    public override void Bind()
    {
        base.Bind();
      
    }

    public override void Write(ISerializerStream stream)
    {
        base.Write(stream);

    }

    public override void Read(ISerializerStream stream)
    {
        base.Read(stream);
        
    }

    public static int enemyCount = 0;

    public override ViewBase CreateEnemiesView(FPSEnemyViewModel fPSEnemy)
    {
        return InstantiateView(fPSEnemy);
    }
    
    public Transform GetRandomSpawnPoint()
    {
        return _SpawnPointsParent.GetChild(Random.Range(0, _SpawnPointsParent.transform.childCount - 1));
    }
}