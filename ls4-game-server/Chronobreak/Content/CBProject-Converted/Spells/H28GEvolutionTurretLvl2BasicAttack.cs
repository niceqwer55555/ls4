﻿namespace Spells
{
    public class H28GEvolutionTurretLvl2BasicAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Heimerdinger", teamID);
            float dmg = GetTotalAttackDamage(owner);
            if (target is BaseTurret)
            {
                dmg /= 2;
            }
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.35f, 1, false, false);
        }
    }
}