using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptChaosTurretGiant : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 混乱方巨型炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 220f;
            owner.Stats.Armor.BaseValue = 150f;
            owner.Stats.MagicResist.BaseValue = 70f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}