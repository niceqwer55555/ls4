﻿using System;
using System.Collections.Generic;
using LeaguePackets.Game.Common;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public abstract class Replication
    {
        public class Replicate
        {
            public uint Value { get; set; }
            public bool IsFloat { get; set; }
            public bool Changed { get; set; }
        }

        protected Replication(AttackableUnit owner)
        {
            Owner = owner;
        }

        public readonly AttackableUnit Owner;
        public Stats Stats => Owner.Stats;

        public uint NetID => Owner.NetId;
        public Replicate[,] Values { get; private set; } = new Replicate[6, 32];
        public bool Changed { get; private set; }

        private void DoUpdate(uint value, int primary, int secondary, bool isFloat)
        {
            if (Values[primary, secondary] == null)
            {
                Values[primary, secondary] = new Replicate
                {
                    Value = value,
                    IsFloat = isFloat,
                    Changed = true
                };
                Changed = true;
            }
            else if (Values[primary, secondary].Value != value)
            {
                Values[primary, secondary].IsFloat = isFloat;
                Values[primary, secondary].Value = value;
                Values[primary, secondary].Changed = true;
                Changed = true;
            }
        }

        protected void UpdateUint(uint value, int primary, int secondary)
        {
            DoUpdate(value, primary, secondary, false);
        }

        protected void UpdateInt(int value, int primary, int secondary)
        {
            DoUpdate((uint)value, primary, secondary, false);
        }

        protected void UpdateBool(bool value, int primary, int secondary)
        {
            DoUpdate(value ? 1u : 0u, primary, secondary, false);
        }

        protected void UpdateFloat(int digits, float value, int primary, int secondary)
        {
            DoUpdate(BitConverter.ToUInt32(BitConverter.GetBytes(MathF.Round(value, digits)), 0), primary, secondary, true);
        }

        public void MarkAsUnchanged()
        {
            foreach (var x in Values)
            {
                if (x != null)
                {
                    x.Changed = false;
                }
            }

            Changed = false;
        }

        internal abstract void Update();

        public ReplicationData GetData(bool partial = true)
        {
            var data = new ReplicationData()
            {
                UnitNetID = Owner.NetId
            };

            for (byte primaryId = 0; primaryId < 6; primaryId++)
            {
                uint secondaryIdArray = 0;
                List<byte> bytes = new List<byte>(8);
                for (byte secondaryId = 0; secondaryId < 32; secondaryId++)
                {
                    var rep = Values[primaryId, secondaryId];
                    if (rep != null && (!partial || rep.Changed))
                    {
                        secondaryIdArray |= 1u << secondaryId;

                        if (rep.IsFloat)
                        {
                            var source = BitConverter.GetBytes(rep.Value);

                            if (source[0] >= 0xFE)
                            {
                                bytes.Add(0xFE);
                            }

                            bytes.AddRange(source);
                        }
                        else
                        {
                            var num = rep.Value;
                            while (num >= 0x80)
                            {
                                bytes.Add((byte)(num | 0x80));
                                num >>= 7;
                            }

                            bytes.Add((byte)num);
                        }
                    }
                }

                if (bytes.Count > 0)
                {
                    data.Data[primaryId] = new Tuple<uint, byte[]>(secondaryIdArray, bytes.ToArray());
                }
            }

            return data;
        }
    }
}