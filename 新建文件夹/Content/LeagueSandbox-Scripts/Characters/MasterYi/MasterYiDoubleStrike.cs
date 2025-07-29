using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Missile; // 添加缺失的引用

namespace Spells
{
    public class MasterYiDoubleStrike : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true
        };

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            AddParticleTarget(owner, owner, "MasterYiDoubleStrike_Cast_Particle", owner);
        }

        public void OnSpellCast(Spell spell)
        {
        }

        public void OnSpellPostCast(Spell spell)
        {
        }

        public void ApplyEffects(ObjAIBase owner, AttackableUnit target, Spell spell, SpellMissile missile) // 修复类型引用
        {
            if (target != null && !target.IsDead)
            {
                var ad = owner.Stats.AttackDamage.Total * 1.2f;
                var damage = 80 + spell.CastInfo.SpellLevel * 20 + ad;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                AddParticleTarget(owner, target, "MasterYiDoubleStrike_Hit_Particle", target);
            }

            missile.SetToRemove(); // 修复类型引用
        }
    }
}
