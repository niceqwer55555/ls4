﻿namespace Buffs
{
    public class MordekaiserCOTGDeath : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 pos = GetRandomPointInAreaUnit(owner, 400, 200);
            Pet other1 = CloneUnitPet(attacker, nameof(Buffs.MordekaiserCOTGPetBuff), 0, pos, 0, 0, false); // UNUSED
        }
    }
}