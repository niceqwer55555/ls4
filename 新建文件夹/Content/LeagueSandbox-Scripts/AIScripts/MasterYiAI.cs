using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using System.Numerics;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace AIScripts
{
    public class MasterYiAI : IAIScript
    {
        // 字段：用于存储元数据实例
        private LeagueSandbox.GameServer.Scripting.CSharp.AIScriptMetaData _metaData;

        // 修复：显式实现接口的属性，确保get和set均为public
        public LeagueSandbox.GameServer.Scripting.CSharp.AIScriptMetaData AIScriptMetaData
        {
            get => _metaData;
            set => _metaData = value; // set访问器必须为public，完全匹配接口要求
        }

        private ObjAIBase _owner;
        private float _lastAttackTime = 0;
        private float _attackInterval = 1.5f;

        public void OnActivate(ObjAIBase owner)
        {
            _owner = owner;
            // 初始化元数据（触发public set）
            AIScriptMetaData = new LeagueSandbox.GameServer.Scripting.CSharp.AIScriptMetaData();
        }

        public void OnUpdate(float diff)
        {
            if (_owner == null || _owner.IsDead) return;

            var target = GetNearestEnemy(_owner.Position, 600f);
            if (target != null && !target.IsDead)
            {
                _lastAttackTime += diff;
                if (_lastAttackTime >= _attackInterval * 1000)
                {
                    _owner.TargetUnit = target;
                    _lastAttackTime = 0;
                }
            }
        }

        private AttackableUnit GetNearestEnemy(Vector2 pos, float range)
        {
            AttackableUnit nearest = null;
            float minDist = float.MaxValue;
            var units = GetUnitsInRange(pos, range, true);
            foreach (var unit in units)
            {
                if (unit is AttackableUnit a && _owner.Team != a.Team)
                {
                    var dist = Vector2.Distance(pos, a.Position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearest = a;
                    }
                }
            }
            return nearest;
        }
    }
}