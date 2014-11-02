using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[DiagramInfoAttribute("FPSShooterProject")]
public class FPSDamageableViewModelBase : ViewModel {
    
    private IDisposable _HealthStateDisposable;
    
    public P<Single> _HealthProperty;
    
    public P<Vector3> _PositionProperty;
    
    public P<FPSPlayerState> _HealthStateProperty;
    
    protected CommandWithSenderAndArgument<FPSDamageableViewModel, Int32> _ApplyDamage;
    
    public FPSDamageableViewModelBase(FPSDamageableControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSDamageableViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _HealthProperty = new P<Single>(this, "Health");
        _PositionProperty = new P<Vector3>(this, "Position");
        _HealthStateProperty = new P<FPSPlayerState>(this, "HealthState");
        this.ResetHealthState();
    }
    
    public virtual void ResetHealthState() {
        if (_HealthStateDisposable != null) _HealthStateDisposable.Dispose();
        _HealthStateDisposable = _HealthStateProperty.ToComputed( ComputeHealthState, this.GetHealthStateDependents().ToArray() ).DisposeWith(this);
    }
    
    public virtual FPSPlayerState ComputeHealthState() {
        return default(FPSPlayerState);
    }
    
    public virtual IEnumerable<IObservableProperty> GetHealthStateDependents() {
        yield return _HealthProperty;
        yield break;
    }
}

public partial class FPSDamageableViewModel : FPSDamageableViewModelBase {
    
    public FPSDamageableViewModel(FPSDamageableControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSDamageableViewModel() : 
            base() {
    }
    
    public virtual P<Single> HealthProperty {
        get {
            return this._HealthProperty;
        }
    }
    
    public virtual Single Health {
        get {
            return _HealthProperty.Value;
        }
        set {
            _HealthProperty.Value = value;
        }
    }
    
    public virtual P<Vector3> PositionProperty {
        get {
            return this._PositionProperty;
        }
    }
    
    public virtual Vector3 Position {
        get {
            return _PositionProperty.Value;
        }
        set {
            _PositionProperty.Value = value;
        }
    }
    
    public virtual P<FPSPlayerState> HealthStateProperty {
        get {
            return this._HealthStateProperty;
        }
    }
    
    public virtual FPSPlayerState HealthState {
        get {
            return _HealthStateProperty.Value;
        }
        set {
            _HealthStateProperty.Value = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<FPSDamageableViewModel, Int32> ApplyDamage {
        get {
            return _ApplyDamage;
        }
        set {
            _ApplyDamage = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var fPSDamageable = controller as FPSDamageableControllerBase;
        this.ApplyDamage = new CommandWithSenderAndArgument<FPSDamageableViewModel, Int32>(this, fPSDamageable.ApplyDamage);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeFloat("Health", this.Health);
        stream.SerializeVector3("Position", this.Position);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.Health = stream.DeserializeFloat("Health");;
        		this.Position = stream.DeserializeVector3("Position");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_HealthProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_PositionProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_HealthStateProperty, false, false, true, true));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("ApplyDamage", ApplyDamage) { ParameterType = typeof(Int32) });
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class FPSEnemyViewModelBase : FPSDamageableViewModel {
    
    public P<Single> _SpeedProperty;
    
    public P<Single> _DistanceToPlayerProperty;
    
    public FPSEnemyViewModelBase(FPSEnemyControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSEnemyViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _SpeedProperty = new P<Single>(this, "Speed");
        _DistanceToPlayerProperty = new P<Single>(this, "DistanceToPlayer");
    }
}

public partial class FPSEnemyViewModel : FPSEnemyViewModelBase {
    
    private FPSGameViewModel _ParentFPSGame;
    
    public FPSEnemyViewModel(FPSEnemyControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSEnemyViewModel() : 
            base() {
    }
    
    public virtual P<Single> SpeedProperty {
        get {
            return this._SpeedProperty;
        }
    }
    
    public virtual Single Speed {
        get {
            return _SpeedProperty.Value;
        }
        set {
            _SpeedProperty.Value = value;
        }
    }
    
    public virtual P<Single> DistanceToPlayerProperty {
        get {
            return this._DistanceToPlayerProperty;
        }
    }
    
    public virtual Single DistanceToPlayer {
        get {
            return _DistanceToPlayerProperty.Value;
        }
        set {
            _DistanceToPlayerProperty.Value = value;
        }
    }
    
    public virtual FPSGameViewModel ParentFPSGame {
        get {
            return this._ParentFPSGame;
        }
        set {
            _ParentFPSGame = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeFloat("Speed", this.Speed);
        stream.SerializeFloat("DistanceToPlayer", this.DistanceToPlayer);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.Speed = stream.DeserializeFloat("Speed");;
        		this.DistanceToPlayer = stream.DeserializeFloat("DistanceToPlayer");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_SpeedProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_DistanceToPlayerProperty, false, false, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class FPSGameViewModelBase : ViewModel {
    
    public P<FPSPlayerViewModel> _CurrentPlayerProperty;
    
    public P<Int32> _ScoreProperty;
    
    public P<Int32> _KillsProperty;
    
    public P<String> _String1Property;
    
    public ModelCollection<FPSEnemyViewModel> _EnemiesProperty;
    
    protected CommandWithSender<FPSGameViewModel> _MainMenu;
    
    protected CommandWithSender<FPSGameViewModel> _QuitGame;
    
    public FPSGameViewModelBase(FPSGameControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSGameViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _CurrentPlayerProperty = new P<FPSPlayerViewModel>(this, "CurrentPlayer");
        _ScoreProperty = new P<Int32>(this, "Score");
        _KillsProperty = new P<Int32>(this, "Kills");
        _String1Property = new P<String>(this, "String1");
        _EnemiesProperty = new ModelCollection<FPSEnemyViewModel>(this, "Enemies");
        _EnemiesProperty.CollectionChanged += EnemiesCollectionChanged;
    }
    
    protected virtual void EnemiesCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
    }
}

public partial class FPSGameViewModel : FPSGameViewModelBase {
    
    public FPSGameViewModel(FPSGameControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSGameViewModel() : 
            base() {
    }
    
    public virtual P<FPSPlayerViewModel> CurrentPlayerProperty {
        get {
            return this._CurrentPlayerProperty;
        }
    }
    
    public virtual FPSPlayerViewModel CurrentPlayer {
        get {
            return _CurrentPlayerProperty.Value;
        }
        set {
            _CurrentPlayerProperty.Value = value;
            if (value != null) value.ParentFPSGame = this;
        }
    }
    
    public virtual P<Int32> ScoreProperty {
        get {
            return this._ScoreProperty;
        }
    }
    
    public virtual Int32 Score {
        get {
            return _ScoreProperty.Value;
        }
        set {
            _ScoreProperty.Value = value;
        }
    }
    
    public virtual P<Int32> KillsProperty {
        get {
            return this._KillsProperty;
        }
    }
    
    public virtual Int32 Kills {
        get {
            return _KillsProperty.Value;
        }
        set {
            _KillsProperty.Value = value;
        }
    }
    
    public virtual P<String> String1Property {
        get {
            return this._String1Property;
        }
    }
    
    public virtual String String1 {
        get {
            return _String1Property.Value;
        }
        set {
            _String1Property.Value = value;
        }
    }
    
    public virtual ModelCollection<FPSEnemyViewModel> Enemies {
        get {
            return this._EnemiesProperty;
        }
    }
    
    public virtual CommandWithSender<FPSGameViewModel> MainMenu {
        get {
            return _MainMenu;
        }
        set {
            _MainMenu = value;
        }
    }
    
    public virtual CommandWithSender<FPSGameViewModel> QuitGame {
        get {
            return _QuitGame;
        }
        set {
            _QuitGame = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var fPSGame = controller as FPSGameControllerBase;
        this.MainMenu = new CommandWithSender<FPSGameViewModel>(this, fPSGame.MainMenu);
        this.QuitGame = new CommandWithSender<FPSGameViewModel>(this, fPSGame.QuitGame);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
		if (stream.DeepSerialize) stream.SerializeObject("CurrentPlayer", this.CurrentPlayer);
        stream.SerializeInt("Score", this.Score);
        stream.SerializeInt("Kills", this.Kills);
        stream.SerializeString("String1", this.String1);
        if (stream.DeepSerialize) stream.SerializeArray("Enemies", this.Enemies);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
		if (stream.DeepSerialize) this.CurrentPlayer = stream.DeserializeObject<FPSPlayerViewModel>("CurrentPlayer");
        		this.Score = stream.DeserializeInt("Score");;
        		this.Kills = stream.DeserializeInt("Kills");;
        		this.String1 = stream.DeserializeString("String1");;
if (stream.DeepSerialize) {
        this.Enemies.Clear();
        this.Enemies.AddRange(stream.DeserializeObjectArray<FPSEnemyViewModel>("Enemies"));
}
    }
    
    public override void Unbind() {
        base.Unbind();
        _EnemiesProperty.CollectionChanged -= EnemiesCollectionChanged;
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_CurrentPlayerProperty, true, false, false));
        list.Add(new ViewModelPropertyInfo(_ScoreProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_KillsProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_String1Property, false, false, false));
        list.Add(new ViewModelPropertyInfo(_EnemiesProperty, true, true, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("MainMenu", MainMenu) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("QuitGame", QuitGame) { ParameterType = typeof(void) });
    }
    
    protected override void EnemiesCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
        foreach (var item in args.OldItems.OfType<FPSEnemyViewModel>()) item.ParentFPSGame = null;;
        foreach (var item in args.NewItems.OfType<FPSEnemyViewModel>()) item.ParentFPSGame = this;;
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class FPSPlayerViewModelBase : FPSDamageableViewModel {
    
    private IDisposable _CurrentWeaponDisposable;
    
    public P<Int32> _CurrentWeaponIndexProperty;
    
    public P<FPSWeaponViewModel> _CurrentWeaponProperty;
    
    public ModelCollection<FPSWeaponViewModel> _WeaponsProperty;
    
    protected CommandWithSender<FPSPlayerViewModel> _NextWeapon;
    
    protected CommandWithSenderAndArgument<FPSPlayerViewModel, FPSWeaponViewModel> _PickupWeapon;
    
    protected CommandWithSender<FPSPlayerViewModel> _PreviousWeapon;
    
    protected CommandWithSenderAndArgument<FPSPlayerViewModel, Int32> _SelectWeapon;
    
    public FPSPlayerViewModelBase(FPSPlayerControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSPlayerViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _CurrentWeaponIndexProperty = new P<Int32>(this, "CurrentWeaponIndex");
        _CurrentWeaponProperty = new P<FPSWeaponViewModel>(this, "CurrentWeapon");
        _WeaponsProperty = new ModelCollection<FPSWeaponViewModel>(this, "Weapons");
        _WeaponsProperty.CollectionChanged += WeaponsCollectionChanged;
        this.ResetCurrentWeapon();
    }
    
    public virtual void ResetCurrentWeapon() {
        if (_CurrentWeaponDisposable != null) _CurrentWeaponDisposable.Dispose();
        _CurrentWeaponDisposable = _CurrentWeaponProperty.ToComputed( ComputeCurrentWeapon, this.GetCurrentWeaponDependents().ToArray() ).DisposeWith(this);
    }
    
    protected virtual void WeaponsCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
    }
    
    public virtual FPSWeaponViewModel ComputeCurrentWeapon() {
        return default(FPSWeaponViewModel);
    }
    
    public virtual IEnumerable<IObservableProperty> GetCurrentWeaponDependents() {
        yield return _CurrentWeaponIndexProperty;
        yield break;
    }
}

public partial class FPSPlayerViewModel : FPSPlayerViewModelBase {
    
    private FPSGameViewModel _ParentFPSGame;
    
    public FPSPlayerViewModel(FPSPlayerControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSPlayerViewModel() : 
            base() {
    }
    
    public virtual P<Int32> CurrentWeaponIndexProperty {
        get {
            return this._CurrentWeaponIndexProperty;
        }
    }
    
    public virtual Int32 CurrentWeaponIndex {
        get {
            return _CurrentWeaponIndexProperty.Value;
        }
        set {
            _CurrentWeaponIndexProperty.Value = value;
        }
    }
    
    public virtual P<FPSWeaponViewModel> CurrentWeaponProperty {
        get {
            return this._CurrentWeaponProperty;
        }
    }
    
    public virtual FPSWeaponViewModel CurrentWeapon {
        get {
            return _CurrentWeaponProperty.Value;
        }
        set {
            _CurrentWeaponProperty.Value = value;
        }
    }
    
    public virtual ModelCollection<FPSWeaponViewModel> Weapons {
        get {
            return this._WeaponsProperty;
        }
    }
    
    public virtual CommandWithSender<FPSPlayerViewModel> NextWeapon {
        get {
            return _NextWeapon;
        }
        set {
            _NextWeapon = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<FPSPlayerViewModel, FPSWeaponViewModel> PickupWeapon {
        get {
            return _PickupWeapon;
        }
        set {
            _PickupWeapon = value;
        }
    }
    
    public virtual CommandWithSender<FPSPlayerViewModel> PreviousWeapon {
        get {
            return _PreviousWeapon;
        }
        set {
            _PreviousWeapon = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<FPSPlayerViewModel, Int32> SelectWeapon {
        get {
            return _SelectWeapon;
        }
        set {
            _SelectWeapon = value;
        }
    }
    
    public virtual FPSGameViewModel ParentFPSGame {
        get {
            return this._ParentFPSGame;
        }
        set {
            _ParentFPSGame = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
        var fPSPlayer = controller as FPSPlayerControllerBase;
        this.NextWeapon = new CommandWithSender<FPSPlayerViewModel>(this, fPSPlayer.NextWeapon);
        this.PickupWeapon = new CommandWithSenderAndArgument<FPSPlayerViewModel, FPSWeaponViewModel>(this, fPSPlayer.PickupWeapon);
        this.PreviousWeapon = new CommandWithSender<FPSPlayerViewModel>(this, fPSPlayer.PreviousWeapon);
        this.SelectWeapon = new CommandWithSenderAndArgument<FPSPlayerViewModel, Int32>(this, fPSPlayer.SelectWeapon);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeInt("CurrentWeaponIndex", this.CurrentWeaponIndex);
        if (stream.DeepSerialize) stream.SerializeArray("Weapons", this.Weapons);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.CurrentWeaponIndex = stream.DeserializeInt("CurrentWeaponIndex");;
if (stream.DeepSerialize) {
        this.Weapons.Clear();
        this.Weapons.AddRange(stream.DeserializeObjectArray<FPSWeaponViewModel>("Weapons"));
}
    }
    
    public override void Unbind() {
        base.Unbind();
        _WeaponsProperty.CollectionChanged -= WeaponsCollectionChanged;
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_CurrentWeaponIndexProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_CurrentWeaponProperty, true, false, false, true));
        list.Add(new ViewModelPropertyInfo(_WeaponsProperty, true, true, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("NextWeapon", NextWeapon) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("PickupWeapon", PickupWeapon) { ParameterType = typeof(FPSWeaponViewModel) });
        list.Add(new ViewModelCommandInfo("PreviousWeapon", PreviousWeapon) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("SelectWeapon", SelectWeapon) { ParameterType = typeof(Int32) });
    }
    
    protected override void WeaponsCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
        foreach (var item in args.OldItems.OfType<FPSWeaponViewModel>()) item.ParentFPSPlayer = null;;
        foreach (var item in args.NewItems.OfType<FPSWeaponViewModel>()) item.ParentFPSPlayer = this;;
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class FPSWeaponViewModelBase : ViewModel {
    
    private IDisposable _IsEmptyDisposable;
    
    public P<Int32> _ZoomIndexProperty;
    
    public P<Int32> _MaxZoomsProperty;
    
    public P<WeaponType> _WeaponTypeProperty;
    
    public P<Single> _ReloadTimeProperty;
    
    public P<Int32> _RoundSizeProperty;
    
    public P<Int32> _MinSpreadProperty;
    
    public P<Int32> _BurstSizeProperty;
    
    public P<Single> _RecoilSpeedProperty;
    
    public P<Single> _FireSpeedProperty;
    
    public P<Single> _BurstSpeedProperty;
    
    public P<Single> _SpreadMultiplierProperty;
    
    public FPSWeaponStateMachine _StateProperty;
    
    public P<Int32> _AmmoProperty;
    
    public P<Boolean> _IsEmptyProperty;
    
    protected CommandWithSender<FPSWeaponViewModel> _BeginFire;
    
    protected CommandWithSender<FPSWeaponViewModel> _NextZoom;
    
    protected CommandWithSender<FPSWeaponViewModel> _EndFire;
    
    protected CommandWithSender<FPSWeaponViewModel> _Reload;
    
    protected CommandWithSender<FPSWeaponViewModel> _BulletFired;
    
    protected CommandWithSender<FPSWeaponViewModel> _FinishedReloading;
    
    public FPSWeaponViewModelBase(FPSWeaponControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSWeaponViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _ZoomIndexProperty = new P<Int32>(this, "ZoomIndex");
        _MaxZoomsProperty = new P<Int32>(this, "MaxZooms");
        _WeaponTypeProperty = new P<WeaponType>(this, "WeaponType");
        _ReloadTimeProperty = new P<Single>(this, "ReloadTime");
        _RoundSizeProperty = new P<Int32>(this, "RoundSize");
        _MinSpreadProperty = new P<Int32>(this, "MinSpread");
        _BurstSizeProperty = new P<Int32>(this, "BurstSize");
        _RecoilSpeedProperty = new P<Single>(this, "RecoilSpeed");
        _FireSpeedProperty = new P<Single>(this, "FireSpeed");
        _BurstSpeedProperty = new P<Single>(this, "BurstSpeed");
        _SpreadMultiplierProperty = new P<Single>(this, "SpreadMultiplier");
        _StateProperty = new FPSWeaponStateMachine(this, "State");
        _AmmoProperty = new P<Int32>(this, "Ammo");
        _IsEmptyProperty = new P<Boolean>(this, "IsEmpty");
        this.ResetIsEmpty();
        this._BeginFire.Subscribe(_StateProperty.BeginFiring);
        this._EndFire.Subscribe(_StateProperty.EndFiring);
        this._Reload.Subscribe(_StateProperty.OnReload);
        this._FinishedReloading.Subscribe(_StateProperty.FinishedReloading);
        this._StateProperty.OnEmpty.AddComputer(_IsEmptyProperty);
    }
    
    public virtual void ResetIsEmpty() {
        if (_IsEmptyDisposable != null) _IsEmptyDisposable.Dispose();
        _IsEmptyDisposable = _IsEmptyProperty.ToComputed( ComputeIsEmpty, this.GetIsEmptyDependents().ToArray() ).DisposeWith(this);
    }
    
    public virtual Boolean ComputeIsEmpty() {
        return default(Boolean);
    }
    
    public virtual IEnumerable<IObservableProperty> GetIsEmptyDependents() {
        yield return _AmmoProperty;
        yield break;
    }
}

public partial class FPSWeaponViewModel : FPSWeaponViewModelBase {
    
    private FPSPlayerViewModel _ParentFPSPlayer;
    
    public FPSWeaponViewModel(FPSWeaponControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSWeaponViewModel() : 
            base() {
    }
    
    public virtual P<Int32> ZoomIndexProperty {
        get {
            return this._ZoomIndexProperty;
        }
    }
    
    public virtual Int32 ZoomIndex {
        get {
            return _ZoomIndexProperty.Value;
        }
        set {
            _ZoomIndexProperty.Value = value;
        }
    }
    
    public virtual P<Int32> MaxZoomsProperty {
        get {
            return this._MaxZoomsProperty;
        }
    }
    
    public virtual Int32 MaxZooms {
        get {
            return _MaxZoomsProperty.Value;
        }
        set {
            _MaxZoomsProperty.Value = value;
        }
    }
    
    public virtual P<WeaponType> WeaponTypeProperty {
        get {
            return this._WeaponTypeProperty;
        }
    }
    
    public virtual WeaponType WeaponType {
        get {
            return _WeaponTypeProperty.Value;
        }
        set {
            _WeaponTypeProperty.Value = value;
        }
    }
    
    public virtual P<Single> ReloadTimeProperty {
        get {
            return this._ReloadTimeProperty;
        }
    }
    
    public virtual Single ReloadTime {
        get {
            return _ReloadTimeProperty.Value;
        }
        set {
            _ReloadTimeProperty.Value = value;
        }
    }
    
    public virtual P<Int32> RoundSizeProperty {
        get {
            return this._RoundSizeProperty;
        }
    }
    
    public virtual Int32 RoundSize {
        get {
            return _RoundSizeProperty.Value;
        }
        set {
            _RoundSizeProperty.Value = value;
        }
    }
    
    public virtual P<Int32> MinSpreadProperty {
        get {
            return this._MinSpreadProperty;
        }
    }
    
    public virtual Int32 MinSpread {
        get {
            return _MinSpreadProperty.Value;
        }
        set {
            _MinSpreadProperty.Value = value;
        }
    }
    
    public virtual P<Int32> BurstSizeProperty {
        get {
            return this._BurstSizeProperty;
        }
    }
    
    public virtual Int32 BurstSize {
        get {
            return _BurstSizeProperty.Value;
        }
        set {
            _BurstSizeProperty.Value = value;
        }
    }
    
    public virtual P<Single> RecoilSpeedProperty {
        get {
            return this._RecoilSpeedProperty;
        }
    }
    
    public virtual Single RecoilSpeed {
        get {
            return _RecoilSpeedProperty.Value;
        }
        set {
            _RecoilSpeedProperty.Value = value;
        }
    }
    
    public virtual P<Single> FireSpeedProperty {
        get {
            return this._FireSpeedProperty;
        }
    }
    
    public virtual Single FireSpeed {
        get {
            return _FireSpeedProperty.Value;
        }
        set {
            _FireSpeedProperty.Value = value;
        }
    }
    
    public virtual P<Single> BurstSpeedProperty {
        get {
            return this._BurstSpeedProperty;
        }
    }
    
    public virtual Single BurstSpeed {
        get {
            return _BurstSpeedProperty.Value;
        }
        set {
            _BurstSpeedProperty.Value = value;
        }
    }
    
    public virtual P<Single> SpreadMultiplierProperty {
        get {
            return this._SpreadMultiplierProperty;
        }
    }
    
    public virtual Single SpreadMultiplier {
        get {
            return _SpreadMultiplierProperty.Value;
        }
        set {
            _SpreadMultiplierProperty.Value = value;
        }
    }
    
    public virtual FPSWeaponStateMachine StateProperty {
        get {
            return this._StateProperty;
        }
    }
    
    public virtual Invert.StateMachine.State State {
        get {
            return _StateProperty.Value;
        }
        set {
            _StateProperty.Value = value;
        }
    }
    
    public virtual P<Int32> AmmoProperty {
        get {
            return this._AmmoProperty;
        }
    }
    
    public virtual Int32 Ammo {
        get {
            return _AmmoProperty.Value;
        }
        set {
            _AmmoProperty.Value = value;
        }
    }
    
    public virtual P<Boolean> IsEmptyProperty {
        get {
            return this._IsEmptyProperty;
        }
    }
    
    public virtual Boolean IsEmpty {
        get {
            return _IsEmptyProperty.Value;
        }
        set {
            _IsEmptyProperty.Value = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> BeginFire {
        get {
            return _BeginFire;
        }
        set {
            _BeginFire = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> NextZoom {
        get {
            return _NextZoom;
        }
        set {
            _NextZoom = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> EndFire {
        get {
            return _EndFire;
        }
        set {
            _EndFire = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> Reload {
        get {
            return _Reload;
        }
        set {
            _Reload = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> BulletFired {
        get {
            return _BulletFired;
        }
        set {
            _BulletFired = value;
        }
    }
    
    public virtual CommandWithSender<FPSWeaponViewModel> FinishedReloading {
        get {
            return _FinishedReloading;
        }
        set {
            _FinishedReloading = value;
        }
    }
    
    public virtual FPSPlayerViewModel ParentFPSPlayer {
        get {
            return this._ParentFPSPlayer;
        }
        set {
            _ParentFPSPlayer = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var fPSWeapon = controller as FPSWeaponControllerBase;
        this.BeginFire = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.BeginFire);
        this.NextZoom = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.NextZoom);
        this.EndFire = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.EndFire);
        this.Reload = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.Reload);
        this.BulletFired = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.BulletFired);
        this.FinishedReloading = new CommandWithSender<FPSWeaponViewModel>(this, fPSWeapon.FinishedReloading);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeInt("ZoomIndex", this.ZoomIndex);
        stream.SerializeInt("MaxZooms", this.MaxZooms);
		stream.SerializeInt("WeaponType", (int)this.WeaponType);
        stream.SerializeFloat("ReloadTime", this.ReloadTime);
        stream.SerializeInt("RoundSize", this.RoundSize);
        stream.SerializeInt("MinSpread", this.MinSpread);
        stream.SerializeInt("BurstSize", this.BurstSize);
        stream.SerializeFloat("RecoilSpeed", this.RecoilSpeed);
        stream.SerializeFloat("FireSpeed", this.FireSpeed);
        stream.SerializeFloat("BurstSpeed", this.BurstSpeed);
        stream.SerializeFloat("SpreadMultiplier", this.SpreadMultiplier);
        stream.SerializeString("State", this.State.Name);;
        stream.SerializeInt("Ammo", this.Ammo);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.ZoomIndex = stream.DeserializeInt("ZoomIndex");;
        		this.MaxZooms = stream.DeserializeInt("MaxZooms");;
		this.WeaponType = (WeaponType)stream.DeserializeInt("WeaponType");
        		this.ReloadTime = stream.DeserializeFloat("ReloadTime");;
        		this.RoundSize = stream.DeserializeInt("RoundSize");;
        		this.MinSpread = stream.DeserializeInt("MinSpread");;
        		this.BurstSize = stream.DeserializeInt("BurstSize");;
        		this.RecoilSpeed = stream.DeserializeFloat("RecoilSpeed");;
        		this.FireSpeed = stream.DeserializeFloat("FireSpeed");;
        		this.BurstSpeed = stream.DeserializeFloat("BurstSpeed");;
        		this.SpreadMultiplier = stream.DeserializeFloat("SpreadMultiplier");;
        this._StateProperty.SetState(stream.DeserializeString("State"));
        		this.Ammo = stream.DeserializeInt("Ammo");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_ZoomIndexProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_MaxZoomsProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_WeaponTypeProperty, false, false, true));
        list.Add(new ViewModelPropertyInfo(_ReloadTimeProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_RoundSizeProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_MinSpreadProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_BurstSizeProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_RecoilSpeedProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_FireSpeedProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_BurstSpeedProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_SpreadMultiplierProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_StateProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_AmmoProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_IsEmptyProperty, false, false, false, true));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("BeginFire", BeginFire) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("NextZoom", NextZoom) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("EndFire", EndFire) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("Reload", Reload) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("BulletFired", BulletFired) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("FinishedReloading", FinishedReloading) { ParameterType = typeof(void) });
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class WavesFPSGameViewModelBase : FPSGameViewModel {
    
    private IDisposable _WaveIsCompleteDisposable;
    
    public FPSShooterGamePlay _WavesStateProperty;
    
    public P<Int32> _WaveKillsProperty;
    
    public P<Int32> _KillsToNextWaveProperty;
    
    public P<Int32> _CurrentWaveProperty;
    
    public P<Int32> _SpawnWaitSecondsProperty;
    
    public P<Int32> _EnemiesSpawnedProperty;
    
    public P<Boolean> _WaveIsCompleteProperty;
    
    protected CommandWithSender<WavesFPSGameViewModel> _PlayerDied;
    
    protected CommandWithSender<WavesFPSGameViewModel> _Retry;
    
    protected CommandWithSender<WavesFPSGameViewModel> _NextWaveReady;
    
    protected CommandWithSender<WavesFPSGameViewModel> _Spawn;
    
    public WavesFPSGameViewModelBase(WavesFPSGameControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public WavesFPSGameViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _WavesStateProperty = new FPSShooterGamePlay(this, "WavesState");
        _WaveKillsProperty = new P<Int32>(this, "WaveKills");
        _KillsToNextWaveProperty = new P<Int32>(this, "KillsToNextWave");
        _CurrentWaveProperty = new P<Int32>(this, "CurrentWave");
        _SpawnWaitSecondsProperty = new P<Int32>(this, "SpawnWaitSeconds");
        _EnemiesSpawnedProperty = new P<Int32>(this, "EnemiesSpawned");
        _WaveIsCompleteProperty = new P<Boolean>(this, "WaveIsComplete");
        this.ResetWaveIsComplete();
        this._WavesStateProperty.WaveComplete.AddComputer(_WaveIsCompleteProperty);
        this._PlayerDied.Subscribe(_WavesStateProperty.PlayerDied);
        this._Retry.Subscribe(_WavesStateProperty.OnRetry);
        this._NextWaveReady.Subscribe(_WavesStateProperty.NextWaveReady);
    }
    
    public virtual void ResetWaveIsComplete() {
        if (_WaveIsCompleteDisposable != null) _WaveIsCompleteDisposable.Dispose();
        _WaveIsCompleteDisposable = _WaveIsCompleteProperty.ToComputed( ComputeWaveIsComplete, this.GetWaveIsCompleteDependents().ToArray() ).DisposeWith(this);
    }
    
    public virtual Boolean ComputeWaveIsComplete() {
        return default(Boolean);
    }
    
    public virtual IEnumerable<IObservableProperty> GetWaveIsCompleteDependents() {
        yield return _WaveKillsProperty;
        yield return _KillsToNextWaveProperty;
        yield return _CurrentWaveProperty;
        yield break;
    }
}

public partial class WavesFPSGameViewModel : WavesFPSGameViewModelBase {
    
    public WavesFPSGameViewModel(WavesFPSGameControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public WavesFPSGameViewModel() : 
            base() {
    }
    
    public virtual FPSShooterGamePlay WavesStateProperty {
        get {
            return this._WavesStateProperty;
        }
    }
    
    public virtual Invert.StateMachine.State WavesState {
        get {
            return _WavesStateProperty.Value;
        }
        set {
            _WavesStateProperty.Value = value;
        }
    }
    
    public virtual P<Int32> WaveKillsProperty {
        get {
            return this._WaveKillsProperty;
        }
    }
    
    public virtual Int32 WaveKills {
        get {
            return _WaveKillsProperty.Value;
        }
        set {
            _WaveKillsProperty.Value = value;
        }
    }
    
    public virtual P<Int32> KillsToNextWaveProperty {
        get {
            return this._KillsToNextWaveProperty;
        }
    }
    
    public virtual Int32 KillsToNextWave {
        get {
            return _KillsToNextWaveProperty.Value;
        }
        set {
            _KillsToNextWaveProperty.Value = value;
        }
    }
    
    public virtual P<Int32> CurrentWaveProperty {
        get {
            return this._CurrentWaveProperty;
        }
    }
    
    public virtual Int32 CurrentWave {
        get {
            return _CurrentWaveProperty.Value;
        }
        set {
            _CurrentWaveProperty.Value = value;
        }
    }
    
    public virtual P<Int32> SpawnWaitSecondsProperty {
        get {
            return this._SpawnWaitSecondsProperty;
        }
    }
    
    public virtual Int32 SpawnWaitSeconds {
        get {
            return _SpawnWaitSecondsProperty.Value;
        }
        set {
            _SpawnWaitSecondsProperty.Value = value;
        }
    }
    
    public virtual P<Int32> EnemiesSpawnedProperty {
        get {
            return this._EnemiesSpawnedProperty;
        }
    }
    
    public virtual Int32 EnemiesSpawned {
        get {
            return _EnemiesSpawnedProperty.Value;
        }
        set {
            _EnemiesSpawnedProperty.Value = value;
        }
    }
    
    public virtual P<Boolean> WaveIsCompleteProperty {
        get {
            return this._WaveIsCompleteProperty;
        }
    }
    
    public virtual Boolean WaveIsComplete {
        get {
            return _WaveIsCompleteProperty.Value;
        }
        set {
            _WaveIsCompleteProperty.Value = value;
        }
    }
    
    public virtual CommandWithSender<WavesFPSGameViewModel> PlayerDied {
        get {
            return _PlayerDied;
        }
        set {
            _PlayerDied = value;
        }
    }
    
    public virtual CommandWithSender<WavesFPSGameViewModel> Retry {
        get {
            return _Retry;
        }
        set {
            _Retry = value;
        }
    }
    
    public virtual CommandWithSender<WavesFPSGameViewModel> NextWaveReady {
        get {
            return _NextWaveReady;
        }
        set {
            _NextWaveReady = value;
        }
    }
    
    public virtual CommandWithSender<WavesFPSGameViewModel> Spawn {
        get {
            return _Spawn;
        }
        set {
            _Spawn = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
        var wavesFPSGame = controller as WavesFPSGameControllerBase;
        this.PlayerDied = new CommandWithSender<WavesFPSGameViewModel>(this, wavesFPSGame.PlayerDied);
        this.Retry = new CommandWithSender<WavesFPSGameViewModel>(this, wavesFPSGame.Retry);
        this.NextWaveReady = new CommandWithSender<WavesFPSGameViewModel>(this, wavesFPSGame.NextWaveReady);
        this.Spawn = new CommandWithSender<WavesFPSGameViewModel>(this, wavesFPSGame.Spawn);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeString("WavesState", this.WavesState.Name);;
        stream.SerializeInt("WaveKills", this.WaveKills);
        stream.SerializeInt("KillsToNextWave", this.KillsToNextWave);
        stream.SerializeInt("CurrentWave", this.CurrentWave);
        stream.SerializeInt("SpawnWaitSeconds", this.SpawnWaitSeconds);
        stream.SerializeInt("EnemiesSpawned", this.EnemiesSpawned);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        this._WavesStateProperty.SetState(stream.DeserializeString("WavesState"));
        		this.WaveKills = stream.DeserializeInt("WaveKills");;
        		this.KillsToNextWave = stream.DeserializeInt("KillsToNextWave");;
        		this.CurrentWave = stream.DeserializeInt("CurrentWave");;
        		this.SpawnWaitSeconds = stream.DeserializeInt("SpawnWaitSeconds");;
        		this.EnemiesSpawned = stream.DeserializeInt("EnemiesSpawned");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_WavesStateProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_WaveKillsProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_KillsToNextWaveProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_CurrentWaveProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_SpawnWaitSecondsProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_EnemiesSpawnedProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_WaveIsCompleteProperty, false, false, false, true));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("PlayerDied", PlayerDied) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("Retry", Retry) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("NextWaveReady", NextWaveReady) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("Spawn", Spawn) { ParameterType = typeof(void) });
    }
}

[DiagramInfoAttribute("FPSShooterProject")]
public class FPSMenuViewModelBase : ViewModel {
    
    protected CommandWithSender<FPSMenuViewModel> _Play;
    
    public FPSMenuViewModelBase(FPSMenuControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSMenuViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class FPSMenuViewModel : FPSMenuViewModelBase {
    
    public FPSMenuViewModel(FPSMenuControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FPSMenuViewModel() : 
            base() {
    }
    
    public virtual CommandWithSender<FPSMenuViewModel> Play {
        get {
            return _Play;
        }
        set {
            _Play = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var fPSMenu = controller as FPSMenuControllerBase;
        this.Play = new CommandWithSender<FPSMenuViewModel>(this, fPSMenu.Play);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("Play", Play) { ParameterType = typeof(void) });
    }
}

public enum FPSPlayerState {
    
    Alive,
    
    Dead,
}

public enum FPSGameState {
    
    Active,
    
    Paused,
    
    Done,
}

public enum WeaponType {
    
    MP5,
    
    UMP5,
    
    Colt,
}
