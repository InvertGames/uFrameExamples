using System.Collections.Generic;
using System.Linq;

public partial class WavesFPSGameViewModel
{
    public override bool ComputeWaveIsComplete()
    {
        return WaveKills >= KillsToNextWave;
    }

}