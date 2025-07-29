using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects;

namespace Spells
{
    public class ChaosTurretWorm2BasicAttack : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata();

        public void OnSpellPostCast(Spell spell)
        {
            var target = spell.CastInfo.Targets[0].Unit;
            if (target != null)
            {
                // 混乱方进阶蠕虫炮塔攻击伤害
                target.TakeDamage(spell.CastInfo.Owner, 190f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            }
        }

        // 接口其他方法空实现
        public void OnActivate(ObjAIBase owner, Spell spell) { }
        public void OnDeactivate(ObjAIBase owner, Spell spell) { }
        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, System.Numerics.Vector2 start, System.Numerics.Vector2 end) { }
        public void OnSpellCast(Spell spell) { }
        public void OnSpellChannel(Spell spell) { }
        public void OnSpellChannelCancel(Spell spell, ChannelingStopSource source) { }
        public void OnSpellPostChannel(Spell spell) { }
        public void OnUpdate(float diff) { }
    }
}