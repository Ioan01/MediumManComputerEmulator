using System;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator
{
    public class DIVInstruction : Instruction
    {
        public DIVInstruction(GroupCollection matchGroups) : base(matchGroups)
        {
        }

        public DIVInstruction(short word) : base(word)
        {
        }

        protected override short GetLast10Bits()
        {
            if (Arguments[1].Contains('R'))
            {
                return (short)((RegisterToBinary(Arguments[0]) << 9) |
                                (ushort)RegisterToBinary(Arguments[1]));
            }

            return (short)((RegisterToBinary(Arguments[0]) << 9) | 1 << 8 | (ushort)ToShort(Arguments[1]));
        }

        public override void Execute(Emulator emulator)
        {
            var regToBeStored = emulator.Registers[GetBits(10, 10)];
            var valueToDivide = 0;

            if (GetBits(9, 9) == 1)
            {
                valueToDivide = GetBits(0, 8);
            }
            else
            {
                valueToDivide = emulator.Registers[GetBits(0, 1)];
            }

            if (valueToDivide == 0)
            {
                ErrorHandler.HandleDivisonByZero();
            }

            var result = (ushort)(regToBeStored / valueToDivide);

            emulator.Carry = (result > short.MaxValue);
            emulator.Overflow = (result > short.MaxValue) || (result < short.MinValue);
            emulator.Zero = (result == 0);
            emulator.Negative = (result < 0);
            emulator.Registers[GetBits(10, 10)] = (short)result;
        }
    }
}