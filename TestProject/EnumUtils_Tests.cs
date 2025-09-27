using System;

namespace TestProject
{
    using Enums;
    using Shouldly;
    enum TicketStatus
    {
        PendingInvestigation = 0,
        Assigned,
        Active,
        WorkComplete,
        FailedInspection,
        RejectedByClient,
        Cancelled
    }

    [Flags]
    enum PlayerFlags
    {
        Admin = 0,
        Invulerable = 1,
        NoClip = 2,
        Flying = 4,
        Invisible = 8,
        Frozen = 32,
    }
    public class EnumUtils_Tests
    {
        [Fact]
        public void ToText_Breaks_Apart_Words()
        {
            var status = TicketStatus.PendingInvestigation;
            status.ToText().ShouldBe("Pending Investigation");
        }

        [Fact]
        public void ToEnum_Generates_EnumValues()
        {
            var status = 0;
            status.ToEnum<TicketStatus>().ShouldBe(TicketStatus.PendingInvestigation);
        }

        [Fact]
        public void ToTextList_Breaks_ApartWords()
        {
            var textList = EnumExtensions.ToTextList(TicketStatus.PendingInvestigation);
            textList.ShouldContain("Pending Investigation");
            textList.ShouldContain("Failed Inspection");
            textList.ShouldContain("Rejected By Client");
        }
        [Fact]
        public void Flag_Enums_Produce_Correct_Results()
        {
            var flags = 3.ToEnum<PlayerFlags>();
            flags.ToText().ShouldBe("Invulerable, No Clip");
        }

        [Fact]
        public void Can_Detect_Flags_attribute()
        {
            var enumType = typeof(PlayerFlags);
            var detected = enumType.IsDefined(typeof(FlagsAttribute), false);
            detected.ShouldBeTrue();
        }

        [Fact]
        public void ToEnum_Can_Detect_Bad_Integer_Values()
        {
            var status = 12;
            Should.Throw<ArgumentException>(() => status.ToEnum<TicketStatus>());
        }

        [Fact]
        public void ToEnum_Can_Detect_BadCombinations()
        {
            var playerFlag = 31;
            Should.Throw<ArgumentException>(() => playerFlag.ToEnum<PlayerFlags>());
        }


        [Fact]
        public void ToText_Can_Detect_Bad_Integer_Values()
        {
            var status = 12;
            Should.Throw<ArgumentException>(() => status.ToText<TicketStatus>());
        }

        [Fact]
        public void ToText_Can_Detect_BadCombinations()
        {
            var playerFlag = 31;
            Should.Throw<ArgumentException>(() => playerFlag.ToText<PlayerFlags>());
        }
    }
}
