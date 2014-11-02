using UniRx;

public partial class FPSDamageableViewModel 
{
    private IObservable<Unit> _died;

    public IObservable<Unit> DiedAsObservable
    {
        get { return _died ?? (_died = HealthStateProperty.Where(p => p == FPSPlayerState.Dead).First().Select(_=>Unit.Default)); }
    } 

    public override FPSPlayerState ComputeHealthState()
    {
        return Health > 0 ? FPSPlayerState.Alive : FPSPlayerState.Dead;
    }
}