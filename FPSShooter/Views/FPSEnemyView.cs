using System;
using UnityEngine;
using UniRx;
public partial class FPSEnemyView
{
    public NavMeshAgent _NavAgent;
  
    public override void Bind()
    {
        base.Bind();
        FPSEnemy.ParentFPSGame.CurrentPlayer.PositionProperty.Subscribe(_ =>
        {
            _NavAgent.SetDestination(_);
            transform.LookAt(_);
        }).DisposeWith(this.gameObject);

        UpdateAsObservable()
            .Subscribe(_ => transform.Translate(this.transform.forward*Time.deltaTime*FPSEnemy.Speed))
            .DisposeWith(this.gameObject);
    }


    public override void HealthChanged(float value)
    {
        base.HealthChanged(value);
        gameObject.renderer.material.SetColor("_Color", new Color(1.0f, 1.0f - (1.0f - value), 1.0f - (1.0f - value)));
    }
    public override void HealthStateChanged(FPSPlayerState value)
    {
        gameObject.SetActive(value != FPSPlayerState.Dead);
    }

}