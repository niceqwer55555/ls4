using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Missile; // 添加缺失的引用

namespace Spells
{
    public class MasterYiBasicAttack2 : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true
            // 可根据实际需求添加更多元数据
        };

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            // 技能预释放逻辑，例如添加粒子效果
            AddParticleTarget(owner, owner, "MasterYiBasicAttack2_Cast_Particle", owner);
        }

        public void OnSpellCast(Spell spell)
        {
            // 技能释放逻辑
        }

        public void OnSpellPostCast(Spell spell)
        {
            // 技能释放后逻辑
        }

        public void ApplyEffects(ObjAIBase owner, AttackableUnit target, Spell spell, SpellMissile missile)
        {
            if (target != null && !target.IsDead)
            {
                // 计算伤害
                var ad = owner.Stats.AttackDamage.Total * 1.1f;
                var damage = ad;

                // 造成伤害
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);

                // 添加粒子效果
                AddParticleTarget(owner, target, "MasterYiBasicAttack2_Hit_Particle", target);
            }

            // 移除弹道
            missile.SetToRemove();
        }
    }
}