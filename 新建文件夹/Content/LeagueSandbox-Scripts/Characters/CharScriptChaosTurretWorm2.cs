using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptChaosTurretWorm2 : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 混乱方进阶蠕虫炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 190f;
            owner.Stats.Armor.BaseValue = 130f;
            owner.Stats.MagicResist.BaseValue = 65f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}