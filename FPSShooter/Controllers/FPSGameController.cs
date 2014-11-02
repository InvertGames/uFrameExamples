using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using UniRx;

public class FPSGameController : FPSGameControllerBase
{
    public int _NumberOfEnemiesAtStart = 2;
    public int _NumberOfEnemiesMultiplier = 2;
    public float _SpawnWait = 5f;
    public float _SpawnWaitMultiplier = 0.9f;

    public virtual void EndGame()
    {
        
    }

    
    public void SpawnEnemy()
    {
        var enemy = FPSEnemyController.CreateFPSEnemy();
        enemy.Identifier = Guid.NewGuid().ToString();
        enemy.HealthStateProperty.Where(p => p == FPSPlayerState.Dead)
            .Subscribe(p => EnemyDied(enemy)).DisposeWith(enemy);
        FPSGame.Enemies.Add(enemy);
    }

    public virtual void EnemyDied(FPSEnemyViewModel enemy)
    {
        FPSGame.Kills++;
        FPSGame.Enemies.Remove(enemy);
    }

    public override void InitializeFPSGame(FPSGameViewModel fPSGame)
    {
        
    }

    public override void MainMenu(FPSGameViewModel fPsGame)
    {
        
    }

}