﻿using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class SpecialRuleMapper
{
    public SpecialRuleRaw Map(SpecialRule value) => new(
        value.Name,
        value.Description);

    public SpecialRule Map(SpecialRuleRaw raw) => new(raw.Name)
    {
        Description = raw.Description
    };
}