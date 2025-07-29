using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;

namespace CharScripts
{
    public class CharScriptOrderTurretDragon : ICharScript
    {
        // 移除Spell参数的默认值，避免类型依赖
        public void OnActivate(ObjAIBase owner, Spell originSpell)
        {
            // 仅保留确定存在的属性，移除攻速设置（避免错误）
            owner.Stats.AttackDamage.BaseValue = 150f;
            owner.Stats.Armor.BaseValue = 100f;
            owner.Stats.MagicResist.BaseValue = 50f;
        }

        // 简化Deactivate方法，移除Spell参数
        public void OnDeactivate(ObjAIBase owner, Spell originSpell)
        {
        }

        public void OnUpdate(float diff)
        {
        }
    }
}