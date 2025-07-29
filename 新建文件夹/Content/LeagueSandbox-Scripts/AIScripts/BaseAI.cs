using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Scripting.CSharp; // 确保命名空间正确

namespace AIScripts
{
    // 基础AI脚本，实现IAIScript接口
    public class BaseAI : IAIScript
    {
        // 完全匹配接口的public get和set
        public AIScriptMetaData AIScriptMetaData { get; set; }

        public void OnActivate(ObjAIBase owner)
        {
            // 初始化元数据
            AIScriptMetaData = new AIScriptMetaData();
        }

        public void OnUpdate(float diff)
        {
            // 基础更新逻辑（可空）
        }
    }
}