﻿global using static Chronobreak.GameServer.API.GameAnnouncementManager;
global using static Chronobreak.GameServer.API.ApiFunctionManager;
global using static Chronobreak.GameServer.API.ApiMapFunctionManager;
global using static Chronobreak.GameServer.Scripting.Lua.Functions;
global using System.Linq;
global using System.Numerics;
global using MapScripts.Mutators;
global using MapScripts.GameModes;
global using GameServerCore.Enums;
global using System.Collections.Generic;
global using Chronobreak.GameServer.API;
global using Chronobreak.GameServer.Content;
global using Chronobreak.GameServer.GameObjects;
global using Chronobreak.GameServer.Scripting.CSharp;
global using Chronobreak.GameServer.Scripting.CSharp.Converted;
global using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
global using Chronobreak.GameServer.GameObjects.AttackableUnits;
global using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

global using OldAPI = Chronobreak.GameServer.API.ApiFunctionManager;
global using Time = Chronobreak.GameServer.Game.Time;
global using CharScript = ScriptsCore.Scripts.CharScript;
global using SpellScript = ScriptsCore.Scripts.SpellScript;
global using BuffScript = ScriptsCore.Scripts.BuffScript;
global using ItemScript = ScriptsCore.Scripts.ItemScript;
global using TalentScript = ScriptsCore.Scripts.TalentScript;