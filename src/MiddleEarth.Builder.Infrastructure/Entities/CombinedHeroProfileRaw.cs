﻿namespace MiddleEarth.Builder.Infrastructure.Entities;

public record CombinedHeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[] Equipment,
    int Cost,
    string? Note,
    WarriorProfileRaw[] AdditionalUnits,
    bool CountsAsOne) :
    HeroProfileRaw(ArmyList, Name, Tier, Characteristics,
        Equipment, Cost, Note);