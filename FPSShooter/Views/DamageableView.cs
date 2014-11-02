using System;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine;


public abstract partial class DamageableView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void HealthStateChanged(FPSPlayerState value) {
        base.HealthStateChanged(value);
    }

    public override void Bind() {
        base.Bind();

    }

    public override void Read(ISerializerStream stream)
    {
        base.Read(stream);
        transform.localPosition = stream.DeserializeVector3("Position");
    }

    public override void Write(ISerializerStream stream)
    {
        base.Write(stream);
        stream.SerializeVector3("Position", transform.localPosition);
    }

    protected override IObservable<Vector3> GetPositionObservable()
    {
        return PositionAsObservable;
    }

    public override void PositionChanged(Vector3 value)
    {
        base.PositionChanged(value);
        
    }
}
