namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class Mapper
{
    public readonly AllianceMapper AllianceMapper;
    public readonly ArmyListMapper ArmyListMapper;
    public readonly ArmyMapper ArmyMapper;
    public readonly CharacteristicsMapper CharacteristicsMapper;
    public readonly EquipmentMapper EquipmentMapper;
    public readonly EquipmentProfileMapper EquipmentProfileMapper;
    public readonly HeroMapper HeroMapper;
    public readonly HeroProfileMapper HeroProfileMapper;
    public readonly SpecialRuleMapper SpecialRuleMapper;
    public readonly UnitProfileEquipmentMapper UnitProfileEquipmentMapper;
    public readonly WarbandMapper WarbandMapper;
    public readonly WarriorMapper WarriorMapper;
    public readonly WarriorProfileMapper WarriorProfileMapper;

    public Mapper(BuilderContext context)
    {
        AllianceMapper = new AllianceMapper(context, this);
        ArmyListMapper = new ArmyListMapper(context, this);
        ArmyMapper = new ArmyMapper(context, this);
        CharacteristicsMapper = new CharacteristicsMapper(context, this);
        EquipmentMapper = new EquipmentMapper(context, this);
        EquipmentProfileMapper = new EquipmentProfileMapper(this);
        HeroMapper = new HeroMapper(context, this);
        HeroProfileMapper = new HeroProfileMapper(context, this);
        SpecialRuleMapper = new SpecialRuleMapper();
        UnitProfileEquipmentMapper = new UnitProfileEquipmentMapper(context, this);
        WarbandMapper = new WarbandMapper(context, this);
        WarriorMapper = new WarriorMapper(context, this);
        WarriorProfileMapper = new WarriorProfileMapper(context, this);
    }
}