using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Invert.StateMachine;


public class FPSWeaponStateMachine : FPSWeaponStateMachineBase {
    
    public FPSWeaponStateMachine(ViewModel vm, string propertyName) : 
            base(vm, propertyName) {
    }
}
