using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Enums;

namespace Spells
{
    public class Red_Minion_BasicBasicAttack : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            // 这里可以根据需求设置具体的参数
            MissileParameters = new MissileParameters
            {
                Type = MissileType.Target // 假设使用目标型导弹
            }
            // 可以添加更多参数
        };
    }
}