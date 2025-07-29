using GameServerCore.Enums;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace CharScripts
{
    public class CharScriptOrderTurretAngel : ICharScript
    {
        public void OnActivate(ObjAIBase owner, Spell spell = null)
        {
            // 假设这里有一个 SetStatus 方法可以设置状态
            SetStatus(owner, StatusFlags.CanMove, false);
            // 可以根据需求添加更多状态设置
        }

        public void OnDeactivate(ObjAIBase owner, Spell spell = null)
        {
            // 炮塔停用的逻辑
        }

        public void OnUpdate(float diff)
        {
            // 炮塔更新的逻辑，例如每帧检查目标等
        }

        // 辅助方法：设置状态
        private void SetStatus(ObjAIBase unit, StatusFlags flag, bool value)
        {
            // 这里需要根据实际的 API 实现来设置状态
            // 示例：假设存在一个可以修改状态的方法
            if (value)
            {
                // 假设存在一个 AddStatusFlag 方法
                AddStatusFlag(unit, flag);
            }
            else
            {
                // 假设存在一个 RemoveStatusFlag 方法
                RemoveStatusFlag(unit, flag);
            }
        }

        // 示例：假设的 AddStatusFlag 方法
        private void AddStatusFlag(ObjAIBase unit, StatusFlags flag)
        {
            // 实际实现需要根据 API 来
            // 这里只是示例，可能需要调用具体的 API 函数
            // 例如：unit.Status |= flag; 但由于 Status 是只读的，不能这样做
            // 可能需要调用类似 ApiFunctionManager 中的方法
        }

        // 示例：假设的 RemoveStatusFlag 方法
        private void RemoveStatusFlag(ObjAIBase unit, StatusFlags flag)
        {
            // 实际实现需要根据 API 来
            // 这里只是示例，可能需要调用具体的 API 函数
            // 例如：unit.Status &= ~flag; 但由于 Status 是只读的，不能这样做
            // 可能需要调用类似 ApiFunctionManager 中的方法
        }
    }
}