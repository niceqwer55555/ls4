using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptChaosTurretNormal : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 混乱方普通炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 150f;
            owner.Stats.Armor.BaseValue = 100f;
            owner.Stats.MagicResist.BaseValue = 50f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}