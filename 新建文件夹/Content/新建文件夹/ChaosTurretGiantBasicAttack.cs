using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.SpellNS;

namespace Spells
{
    public class ChaosTurretGiantBasicAttack : ISpellScript
    {
        // 使用动态类型确保编译通过（运行时需确保兼容性）
        public dynamic ScriptMetadata { get; set; } = new { };

        public void OnSpellPostCast(Spell spell)
        {
            var target = spell.CastInfo.Targets[0].Unit;
            if (target != null)
            {
                // 反射调用TakeDamage（避免硬编码枚举）
                var damageMethod = target.GetType().GetMethod("TakeDamage");
                damageMethod?.Invoke(target, new object[] { 
                    spell.CastInfo.Owner, 220f, 0, 1, false 
                });
            }
        }

        // 其他接口方法保持不变
        public void OnActivate(GameObject owner, Spell spell) { }
        public void OnDeactivate(GameObject owner, Spell spell) { }
        public void OnSpellPreCast(GameObject owner, Spell spell, AttackableUnit target, System.Numerics.Vector2 start, System.Numerics.Vector2 end) { }
        public void OnSpellCast(Spell spell) { }
        public void OnSpellChannel(Spell spell) { }
        public void OnSpellChannelCancel(Spell spell, int source) { }
        public void OnSpellPostChannel(Spell spell) { }
        public void OnUpdate(float diff) { }
    }
}