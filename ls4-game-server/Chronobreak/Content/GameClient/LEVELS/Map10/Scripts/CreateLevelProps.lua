local a, b
CreateLevelProps = function()
    local c, d, e, f, g, h
    CreateChildTurret("Turret_T1_C_07", "TT_OrderTurret1", TEAM_ORDER, 1, 0)
    CreateChildTurret("Turret_T1_R_02", "TT_OrderTurret2", TEAM_ORDER, 2, 0)
    CreateChildTurret("Turret_T2_R_01", "TT_ChaosTurret1", TEAM_CHAOS, 1, 0)
    CreateChildTurret("Turret_T2_R_02", "TT_ChaosTurret2", TEAM_CHAOS, 2, 0)
    CreateChildTurret("Turret_T1_C_06", "TT_OrderTurret1", TEAM_ORDER, 1, 2)
    CreateChildTurret("Turret_T1_L_02", "TT_OrderTurret2", TEAM_ORDER, 2, 2)
    CreateChildTurret("Turret_T2_L_01", "TT_ChaosTurret1", TEAM_CHAOS, 1, 2)
    CreateChildTurret("Turret_T2_L_02", "TT_ChaosTurret2", TEAM_CHAOS, 2, 2)
    CreateChildTurret("Turret_T1_C_01", "TT_OrderTurret3", TEAM_ORDER, 4, 0)
    CreateChildTurret("Turret_T2_C_01", "TT_ChaosTurret3", TEAM_CHAOS, 4, 0)
    CreateChildTurret("Turret_OrderTurretShrine", "TT_OrderTurret4", TEAM_ORDER, 0, 1)
    CreateChildTurret("Turret_ChaosTurretShrine", "TT_ChaosTurret4", TEAM_CHAOS, 0, 1)
end
