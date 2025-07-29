using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace CharScripts
{
    public class CharScriptOrderTurretShrine : ICharScript
    {
        public void OnActivate(ObjAIBase owner)
        {
            // 秩序方圣所炮塔基础属性
            owner.Stats.AttackDamage.BaseValue = 160f;
            owner.Stats.Armor.BaseValue = 90f;
            owner.Stats.MagicResist.BaseValue = 80f;
        }

        public void OnDeactivate(ObjAIBase owner) { }
        public void OnUpdate(float diff) { }
    }
}