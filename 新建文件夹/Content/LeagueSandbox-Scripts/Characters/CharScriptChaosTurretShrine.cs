using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptChaosTurretShrine : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 混乱方圣所炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 170f;
            owner.Stats.Armor.BaseValue = 95f;
            owner.Stats.MagicResist.BaseValue = 75f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}