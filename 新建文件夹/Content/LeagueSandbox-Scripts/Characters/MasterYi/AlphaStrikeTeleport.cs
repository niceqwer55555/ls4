using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Missile; // 引入SpellMissile命名空间

namespace Spells
{
    public class AlphaStrikeTeleport : ISpellScript
    {
        // 技能元数据（定义技能触发机制等）
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            MissileParameters = new MissileParameters // 若有弹道，定义弹道参数
            {
                Type = MissileType.Target
            }
        };

        // 技能预释放时触发（如记录目标位置、播放动画）
        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            // 播放传送前的粒子效果
            AddParticleTarget(owner, owner, "AlphaStrike_Teleport_Pre", owner, 1.0f);
        }

        // 技能释放时触发（核心传送逻辑）
        public void OnSpellCast(Spell spell)
        {
            var owner = spell.CastInfo.Owner; // 获取技能释放者（Master Yi）
            var targetPos = new Vector2(spell.CastInfo.TargetPosition.X, spell.CastInfo.TargetPosition.Z); // 目标位置

            // 执行传送：将英雄移动到目标位置
            TeleportTo(owner, targetPos.X, targetPos.Y);

            // 播放传送后的粒子效果
            AddParticleTarget(owner, owner, "AlphaStrike_Teleport_Post", owner, 1.0f);
        }

        // 技能释放后触发（如后续效果）
        public void OnSpellPostCast(Spell spell)
        {
            // 可选：添加传送后的增益效果（如短暂无敌）
            AddBuff("AlphaStrike_Teleport_Buff", 0.5f, 1, spell, spell.CastInfo.Owner, spell.CastInfo.Owner);
        }

        // 弹道命中目标时触发（若有弹道）
        public void ApplyEffects(ObjAIBase owner, AttackableUnit target, Spell spell, SpellMissile missile)
        {
            if (target != null && !target.IsDead)
            {
                // 若传送附带伤害，可在此处添加
                var damage = 50 + spell.CastInfo.SpellLevel * 20;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
        }
    }
}