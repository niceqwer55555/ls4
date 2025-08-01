local L0_1, L1_1
L0_1 = 500
FEAR_WANDER_DISTANCE = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = SetState
  L1_2 = AI_PET_IDLE
  L0_2(L1_2)
  L0_2 = InitTimer
  L1_2 = "TimerScanDistance"
  L2_2 = 0.15
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerFindEnemies"
  L2_2 = 0.15
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerFeared"
  L2_2 = 1
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerFlee"
  L2_2 = 0.5
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = StopTimer
  L1_2 = "TimerFeared"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerFlee"
  L0_2(L1_2)
  L0_2 = false
  return L0_2
end
OnInit = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetState()
  if L2_2 == AI_HALTED then
    return
  end
  if L2_2 == AI_TAUNTED or L2_2 == AI_FEARED or L2_2 == AI_FLEEING then
    return
  end
  if L2_2 == AI_PET_HARDATTACK or L2_2 == AI_PET_HARDMOVE or L2_2 == AI_PET_HARDIDLE or L2_2 == AI_PET_HARDIDLE_ATTACKING or L2_2 == AI_PET_HARDRETURN or L2_2 == AI_PET_HARDSTOP then
    return true
  end
  if A0_2 == ORDER_ATTACKTO or A0_2 == ORDER_MOVETO or A0_2 == ORDER_ATTACKMOVE or A0_2 == ORDER_STOP then
    return true
  end
  L3_2 = GetOwner
  L3_2 = L3_2()
  owner = L3_2
  L3_2 = owner
  if L3_2 == nil then
    L3_2 = Die
    L4_2 = me
    L5_2 = DAMAGESOURCE_INTERNALRAW
    L3_2(L4_2, L5_2)
    L3_2 = false
    return L3_2
  end
  L3_2 = ORDER_ATTACKTO
  if A0_2 == L3_2 then
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_MOVETO
  if A0_2 == L3_2 then
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_ATTACKMOVE
  if A0_2 == L3_2 then
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_STOP
  if A0_2 == L3_2 then
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_PETHARDSTOP
  if A0_2 == L3_2 then
    L3_2 = TurnOffAutoAttack
    L4_2 = STOPREASON_TARGET_LOST
    L3_2(L4_2)
    L3_2 = SetStateAndCloseToTarget
    L4_2 = AI_PET_HARDSTOP
    L5_2 = me
    L3_2(L4_2, L5_2)
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_PETHARDATTACK
  if A0_2 == L3_2 then
    if A1_2 == nil then
      L3_2 = false
      return L3_2
    end
    L3_2 = TurnOffAutoAttack
    L4_2 = STOPREASON_TARGET_LOST
    L3_2(L4_2)
    L3_2 = SetStateAndCloseToTarget
    L4_2 = AI_PET_HARDATTACK
    L5_2 = A1_2
    L3_2(L4_2, L5_2)
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_PETHARDMOVE
  if A0_2 == L3_2 then
    L3_2 = SetStateAndMoveInPos
    L4_2 = AI_PET_HARDMOVE
    L3_2(L4_2)
    L3_2 = true
    return L3_2
  end
  L3_2 = ORDER_PETHARDRETURN
  if A0_2 == L3_2 then
    L3_2 = SetStateAndCloseToTarget
    L4_2 = AI_PET_HARDRETURN
    L5_2 = owner
    L3_2(L4_2, L5_2)
    L3_2 = true
    return L3_2
  end
  L3_2 = Say
  L4_2 = "BAD ORDER DUDE! "
  L3_2(L4_2)
  L3_2 = false
  return L3_2
end
OnOrder = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState()
  if L0_2 == AI_HALTED then
    return
  end
  if L0_2 == AI_PET_HARDMOVE or L0_2 == AI_PET_HARDRETURN or L0_2 == AI_FEARED or L0_2 == AI_FLEEING or L0_2 == AI_PET_HARDSTOP then
    return true
  end
  L1_2 = FindTargetInAcR
  L1_2 = L1_2()
  newTarget = L1_2
  L1_2 = newTarget
  if L1_2 == nil then
    L1_2 = GetOwner
    L1_2 = L1_2()
    owner = L1_2
    L1_2 = owner
    if L1_2 == nil then
      L1_2 = Die
      L2_2 = me
      L3_2 = DAMAGESOURCE_INTERNALRAW
      L1_2(L2_2, L3_2)
      L1_2 = false
      return L1_2
    end
    L1_2 = AI_PET_HARDIDLE_ATTACKING
    if L0_2 == L1_2 then
      L1_2 = NetSetState
      L2_2 = AI_PET_HARDIDLE
      L1_2(L2_2)
      L1_2 = true
      return L1_2
    else
      L1_2 = AI_PET_RETURN_ATTACKING
      if L0_2 == L1_2 then
        L1_2 = SetStateAndCloseToTarget
        L2_2 = AI_PET_RETURN
        L3_2 = owner
        L1_2(L2_2, L3_2)
        L1_2 = true
        return L1_2
      else
        L1_2 = AI_PET_ATTACKMOVE_ATTACKING
        if L0_2 == L1_2 then
          L1_2 = SetStateAndCloseToTarget
          L2_2 = AI_PET_ATTACKMOVE
          L3_2 = owner
          L1_2(L2_2, L3_2)
          L1_2 = true
          return L1_2
        end
      end
    end
  else
    if L0_2 == AI_PET_HARDATTACK or L0_2 == AI_TAUNTED then
      SetStateAndCloseToTarget(AI_PET_ATTACK, newTarget)
      return true
    end
    L1_2 = AI_PET_HARDIDLE_ATTACKING
    if L0_2 == L1_2 then
      L1_2 = NetSetState
      L2_2 = AI_PET_HARDIDLE_ATTACKING
      L1_2(L2_2)
      L1_2 = SetTarget
      L2_2 = newTarget
      L1_2(L2_2)
      L1_2 = true
      return L1_2
    end
  end
  L1_2 = NetSetState
  L2_2 = AI_PET_HARDIDLE
  L1_2(L2_2)
  L1_2 = true
  return L1_2
end
OnTargetLost = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeWanderPoint
  L2_2 = GetFearLeashPoint
  L2_2 = L2_2()
  L3_2 = FEAR_WANDER_DISTANCE
  L1_2 = L1_2(L2_2, L3_2)
  wanderPoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FEARED
  L3_2 = wanderPoint
  L1_2(L2_2, L3_2)
  L1_2 = TurnOffAutoAttack
  L2_2 = STOPREASON_IMMEDIATELY
  L1_2(L2_2)
  L1_2 = ResetAndStartTimer
  L2_2 = "TimerFeared"
  L1_2(L2_2)
end
OnFearBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = StopTimer
  L2_2 = "TimerFeared"
  L1_2(L2_2)
  L1_2 = NetSetState
  L2_2 = AI_PET_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnFearEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeWanderPoint
  L2_2 = GetFearLeashPoint
  L2_2 = L2_2()
  L3_2 = FEAR_WANDER_DISTANCE
  L1_2 = L1_2(L2_2, L3_2)
  wanderPoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FEARED
  L3_2 = wanderPoint
  L1_2(L2_2, L3_2)
end
TimerFeared = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeFleePoint
  L1_2 = L1_2()
  fleePoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FLEEING
  L3_2 = fleePoint
  L1_2(L2_2, L3_2)
  L1_2 = TurnOffAutoAttack
  L2_2 = STOPREASON_IMMEDIATELY
  L1_2(L2_2)
  L1_2 = ResetAndStartTimer
  L2_2 = "TimerFlee"
  L1_2(L2_2)
end
OnFleeBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = StopTimer
  L2_2 = "TimerFlee"
  L1_2(L2_2)
  L1_2 = NetSetState
  L2_2 = AI_PET_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnFleeEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeFleePoint
  L1_2 = L1_2()
  fleePoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FLEEING
  L3_2 = fleePoint
  L1_2(L2_2, L3_2)
end
TimerFlee = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_PET_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnCanMove = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_PET_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnCanAttack = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = GetOwner
  L1_2 = L1_2()
  tempOwner = L1_2
  L1_2 = tempOwner
  if L1_2 == nil then
    L1_2 = Die
    L2_2 = me
    L3_2 = DAMAGESOURCE_INTERNALRAW
    L1_2(L2_2, L3_2)
    return
  end
  L1_2 = IsMoving
  L1_2 = L1_2()
  if not L1_2 then
    L1_2 = AI_PET_HARDMOVE
    if L0_2 == L1_2 then
      L1_2 = NetSetState
      L2_2 = AI_PET_HARDIDLE
      L1_2(L2_2)
      return
    end
  end
  L1_2 = AI_PET_SPAWNING
  if L0_2 == L1_2 then
    L1_2 = IsNetworkLocal
    L1_2 = L1_2()
    if L1_2 then
      L1_2 = NetSetState
      L2_2 = AI_PET_HARDIDLE
      L1_2(L2_2)
    end
  end
end
TimerScanDistance = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = GetOwner
  L1_2 = L1_2()
  if L1_2 == nil then
    L1_2 = Die
    L2_2 = me
    L3_2 = DAMAGESOURCE_INTERNALRAW
    L1_2(L2_2, L3_2)
    return
  end
  L0_2 = GetState()
  if L0_2 == AI_PET_HARDMOVE or L0_2 == AI_PET_HARDSTOP then
    return
  end
  L1_2 = AI_PET_HARDIDLE
  if L0_2 == L1_2 then
    L1_2 = FindTargetInAcR
    L1_2 = L1_2()
    newTarget = L1_2
    L1_2 = newTarget
    if L1_2 == nil then
      L1_2 = TurnOffAutoAttack
      L2_2 = STOPREASON_TARGET_LOST
      L1_2(L2_2)
      return
    end
    L1_2 = NetSetState
    L2_2 = AI_PET_HARDIDLE_ATTACKING
    L1_2(L2_2)
    L1_2 = SetTarget
    L2_2 = newTarget
    L1_2(L2_2)
  end
  L1_2 = AI_PET_HARDATTACK
  if L0_2 ~= L1_2 then
    L1_2 = AI_PET_HARDIDLE_ATTACKING
    if L0_2 ~= L1_2 then
      L1_2 = AI_TAUNTED
      if L0_2 ~= L1_2 then
        return
      end
    end
  end
  L1_2 = TargetInAttackRange
  L1_2 = L1_2()
  if L1_2 then
    L1_2 = TurnOnAutoAttack
    L2_2 = GetTarget
    L2_2, L3_2 = L2_2()
    L1_2(L2_2, L3_2)
  else
    L1_2 = TargetInCancelAttackRange
    L1_2 = L1_2()
    if L1_2 == false then
      L1_2 = TurnOffAutoAttack
      L2_2 = STOPREASON_MOVING
      L1_2(L2_2)
    end
  end
  return
end
TimerFindEnemies = L0_1
function L0_1()
  local L0_2, L1_2
  L0_2 = StopTimer
  L1_2 = "TimerScanDistance"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerFindEnemies"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerFeared"
  L0_2(L1_2)
  L0_2 = TurnOffAutoAttack
  L1_2 = STOPREASON_IMMEDIATELY
  L0_2(L1_2)
  L0_2 = NetSetState
  L1_2 = AI_HALTED
  L0_2(L1_2)
end
HaltAI = L0_1
