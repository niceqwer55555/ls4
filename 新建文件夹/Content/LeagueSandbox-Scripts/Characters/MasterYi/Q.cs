using GameServerCore.Enums;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.Scripting.CSharp; // 添加该 using 指令
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Missile;
using LeagueSandbox.GameServer.GameObjects.SpellNS.Sector;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace Spells
{
    public class MasterYiQ : ISpellScript
    {
        // 确保返回类型为 SpellScriptMetadata
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            CastingBreaksStealth = true
        };

        private static Random _random = new Random();
        private static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private int _strikeCount = 0;
        private Dictionary<AttackableUnit, int> _targetHitCount = new Dictionary<AttackableUnit, int>();

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            // 假设可以通过其他方式设置这些值
            // 这里需要根据实际情况修改
            // spell.SetCooldownArray(new float[] { 18f, 17.5f, 17f, 16.5f, 16f });
            // spell.SetManaCostArray(new float[] { 50f, 55f, 60f, 65f, 70f });
            // spell.SetCastRangeDisplayOverride(600f);

            // 易变得【不可被选取】
            owner.SetStatus(StatusFlags.Targetable, false);
            AddParticleTarget(owner, owner, "MasterYi_Q_Cast", owner);
        }

        public void OnSpellCast(Spell spell)
        {
            var owner = spell.CastInfo.Owner;
            var target = spell.CastInfo.Targets[0].Unit;
            var targetPos = target.Position;

            // 移动易到目标旁边
            ForceMovement(owner, "RUN", targetPos, 1200f, 0, 0, 0);

            // 获取范围内的敌人
            var enemies = GetUnitsInRange(owner.Position, 600f, true);
            enemies.RemoveAll(x => x.Team == owner.Team);

            _strikeCount = 0;
            _targetHitCount.Clear();

            foreach (var enemy in enemies)
            {
                if (enemy is AttackableUnit)
                {
                    _strikeCount++;
                    if (!_targetHitCount.ContainsKey(enemy))
                    {
                        _targetHitCount[enemy] = 1;
                    }
                    else
                    {
                        _targetHitCount[enemy]++;
                    }

                    // 计算伤害
                    var baseDamage = new float[] { 30f, 60f, 90f, 120f, 150f }[spell.CastInfo.SpellLevel - 1];
                    var adDamage = owner.Stats.AttackDamage.Total * 0.6f;
                    var damage = baseDamage + adDamage;

                    // 第4击之后的伤害
                    if (_strikeCount > 4)
                    {
                        // 攻击特效伤害
                        var attackEffectDamage = owner.Stats.AttackDamage.Total * 0.75f;
                        if (_targetHitCount[enemy] > 1)
                        {
                            attackEffectDamage *= 0.25f;
                            damage *= 0.25f;
                        }

                        // 暴击处理
                        if (owner.Stats.CriticalChance.Total > 0 && RandomNumber(0, 100) < owner.Stats.CriticalChance.Total * 100)
                        {
                            var critBonus = 0.5f + (owner.Stats.CriticalDamage.Total - 1f);
                            damage *= (1f + critBonus);
                        }

                        // 野怪额外伤害
                        // 假设可以通过其他方式判断是否为野怪
                        // 这里需要根据实际情况修改
                        // if (enemy is Minion && enemy.Team == TeamId.Neutral)
                        // {
                        //     var bonusDamage = new float[] { 75f, 100f, 125f, 150f, 175f }[spell.CastInfo.SpellLevel - 1];
                        //     damage += bonusDamage;
                        // }

                        enemy.TakeDamage(owner, damage + attackEffectDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        AddParticleTarget(owner, enemy, "MasterYi_Q_Hit", enemy);
                    }
                }
            }

            // 易恢复可被选取状态
            owner.SetStatus(StatusFlags.Targetable, true);
        }

        public void OnSpellPostCast(Spell spell)
        {
            // 假设可以通过其他方式监听自动攻击命中事件
            // 这里需要根据实际情况修改
            // ApiEventManager.OnAutoAttackHit(spell.CastInfo.Owner, (source, target, damage) =>
            // {
            //     var cooldownReduction = 1f / (1f + (spell.CastInfo.Owner.Stats.CooldownReduction.Total * 1f));
            //     spell.Cooldown -= cooldownReduction;
            // });
        }

        public void OnUpdate(float diff)
        {
            // 可以在这里处理技能的持续效果，例如持续伤害等
        }

        public void OnActivate(ObjAIBase owner, Spell spell)
        {
            // 接口方法实现
        }

        public void OnDeactivate(ObjAIBase owner, Spell spell)
        {
            // 接口方法实现
        }

        public void OnSpellChannel(Spell spell)
        {
            // 接口方法实现
        }

        public void OnSpellChannelCancel(Spell spell, ChannelingStopSource reason)
        {
            // 接口方法实现
        }

        public void OnSpellPostChannel(Spell spell)
        {
            // 接口方法实现
        }
    }
}