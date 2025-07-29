using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;

namespace CharScripts
{
    public class CharScriptMasterYi : ICharScript
    {
        public void OnActivate(ObjAIBase owner, Spell spell = null)
        {
            // 可在此处添加英雄激活时的逻辑，例如设置初始属性等
        }

        public void OnUpdate(float diff)
        {
            // 可在此处添加每帧更新的逻辑
        }
    }
}