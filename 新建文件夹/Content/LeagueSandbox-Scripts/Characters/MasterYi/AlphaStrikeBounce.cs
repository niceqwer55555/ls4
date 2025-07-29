using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;

namespace Spells
{
    public class AlphaStrikeBounce : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true
        };

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
        }

        public void OnSpellCast(Spell spell)
        {
            var ownerPos = spell.CastInfo.Owner.Position;
            // 修复：添加isAlive参数
            var nearbyUnits = GetUnitsInRange(ownerPos, 300f, true); // 添加isAlive参数
            foreach (var unit in nearbyUnits)
            {
                if (unit is AttackableUnit && unit != spell.CastInfo.Owner)
                {
                    var ad = spell.CastInfo.Owner.Stats.AttackDamage.Total * 0.8f;
                    var damage = 30 + spell.CastInfo.SpellLevel * 10 + ad;
                    unit.TakeDamage(spell.CastInfo.Owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    AddParticleTarget(spell.CastInfo.Owner, unit, "AlphaStrikeBounce_Hit_Particle", unit);
                }
            }
        }

        public void OnSpellPostCast(Spell spell)
        {
        }
    }
}