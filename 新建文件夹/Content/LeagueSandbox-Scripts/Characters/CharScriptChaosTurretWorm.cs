using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptChaosTurretWorm : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 混乱方蠕虫炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 160f;
            owner.Stats.Armor.BaseValue = 110f;
            owner.Stats.MagicResist.BaseValue = 50f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}