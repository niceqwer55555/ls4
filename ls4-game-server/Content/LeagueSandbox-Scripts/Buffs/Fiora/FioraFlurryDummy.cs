﻿using GameServerCore.Enums;
using LeagueSandbox.GameServer.GameObjects.StatsNS;
using LeagueSandbox.GameServer.API;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using System.Numerics;

namespace Spells
{
    public class FioraFlurryDummy : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true
        };
    }
}
namespace Buffs
{
    internal class FioraFlurryDummy : IBuffGameScript
    {
        ObjAIBase Fiora;
        public BuffScriptMetaData BuffMetaData { get; set; } = new BuffScriptMetaData
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.STACKS_AND_RENEWS,
            MaxStacks = 3
        };
        public StatsModifier StatsModifier { get; private set; } = new StatsModifier();
        public void OnActivate(AttackableUnit unit, Buff buff, Spell ownerSpell)
        {
            Fiora = ownerSpell.CastInfo.Owner as Champion;
            Fiora.RemoveStatModifier(StatsModifier);
            StatsModifier.AttackSpeed.PercentBonus = StatsModifier.MoveSpeed.PercentBonus += (0.05f + (Fiora.Spells[2].CastInfo.SpellLevel * 0.02f)) * buff.StackCount;
            Fiora.AddStatModifier(StatsModifier);
        }
    }
}