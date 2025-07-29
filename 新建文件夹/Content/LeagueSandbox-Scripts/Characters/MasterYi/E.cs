using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.SpellNS;
using LeagueSandbox.GameServer.GameObjects.StatsNS;

namespace Spells
{
    public class MasterYiE : ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true
            // 可以根据需求添加更多元数据
        };

        private StatsModifier statsModifier = new StatsModifier();

        public void OnSpellPreCast(ObjAIBase owner, Spell spell, AttackableUnit target, Vector2 start, Vector2 end)
        {
            // 在技能预释放时可以添加一些逻辑，例如检查目标是否有效等
        }

        public void OnSpellCast(Spell spell)
        {
            var owner = spell.CastInfo.Owner;
            // 增加攻击伤害
            statsModifier.AttackDamage.PercentBonus = 0.2f + 0.05f * spell.CastInfo.SpellLevel;
            owner.AddStatModifier(statsModifier);
            AddBuff("MasterYiE", 5f, 1, spell, owner, owner); // 假设E技能持续5秒
        }

        public void OnUpdate(float diff)
        {
            // 在技能持续期间的更新逻辑
        }
    }
}