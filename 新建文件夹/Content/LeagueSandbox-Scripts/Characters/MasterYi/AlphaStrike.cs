using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Missile;

namespace Spells
{
    public class AlphaStrike : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            // 可根据实际需求添加更多元数据，如 MissileParameters 等
        };

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            // 在技能预释放时添加粒子效果
            AddParticleTarget(owner, owner, "AlphaStrike_Cast_Particle", owner);
        }

        public void OnSpellPostCast(Spell spell)
        {
            var current = spell.CastInfo.Owner.Position;
            var spellPos = new Vector2(spell.CastInfo.TargetPosition.X, spell.CastInfo.TargetPosition.Z);
            var to = Vector2.Normalize(spellPos - current);
            var range = to * spell.SpellData.CastRangeDisplayOverride;
            var trueCoords = current + range;

            // 添加技能弹道，可根据实际情况修改弹道名称
            // spell.AddProjectile("AlphaStrike_Missile", current, current, trueCoords);
        }

        public void ApplyEffects(ObjAIBase owner, AttackableUnit target, Spell spell, SpellMissile missile)
        {
            if (target != null && !target.IsDead)
            {
                // 计算伤害，可根据技能机制修改伤害计算公式
                var ad = owner.Stats.AttackDamage.Total * 1.2f;
                var ap = owner.Stats.AbilityPower.Total * 0.3f;
                var damage = 50 + spell.CastInfo.SpellLevel * 30 + ad + ap;

                // 造成伤害
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

                // 添加粒子效果
                AddParticleTarget(owner, target, "AlphaStrike_Hit_Particle", target);

                // 添加增益效果，可根据技能机制修改增益名称和持续时间
                AddBuff("AlphaStrike_Buff", 2.0f, 1, spell, target, owner);
            }

            // 移除弹道
            missile.SetToRemove();
        }
    }
}