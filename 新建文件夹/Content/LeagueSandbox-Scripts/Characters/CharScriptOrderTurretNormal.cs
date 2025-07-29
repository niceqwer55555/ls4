using GameServerCore.Enums;
using GameServerCore.Scripting.CSharp;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;

namespace CharScripts
{
    public class CharScriptOrderTurretNormal : ICharScript
    {
        public void OnActivate(ObjAIBase owner, Spell spell = null)
        {
            // 设置炮塔状态
            SetStatus(owner, StatusFlags.CanMove, false);
            SetStatus(owner, StatusFlags.Targetable, true);

            // 添加炮塔相关的增益效果，可根据实际情况修改
            AddBuff("S5Test_TowerWrath", 25000.0f, 1, null, owner, owner, false);
        }

        public void OnDeactivate(ObjAIBase owner, Spell spell = null)
        {
            // 炮塔停用的逻辑
        }

        public void OnUpdate(float diff)
        {
            // 炮塔更新的逻辑，例如每帧检查目标等
        }
    }
}