using System;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator
{
    public class ANDInstruction : Instruction
    {
        public ANDInstruction(GroupCollection matchGroups) : base(matchGroups)
        {
        }
        public ANDInstruction(short word) : base(word)
        {
        }

        protected override short GetLast10Bits()
        {
            // If the second argument is a register, we set the 9th bit to 1, otherwise we set it to 0
            if (Arguments[1].Contains('R'))
            {
                return (short)((RegisterToBinary(Arguments[0]) << 9) |
                                (ushort)RegisterToBinary(Arguments[1]));
            }

            return (short)((RegisterToBinary(Arguments[0]) << 9) | 1 << 8 | (ushort)ToShort(Arguments[1]));
        }

        public override void Execute(Emulator emulator)
        {
            // Get the index of the register to which we will AND the value
            var regToBeStored = emulator.Registers[GetBits(10, 10)];
            // Get the value that will be ANDed with the register
            var valueToAnd = 0;

            // If the 9th bit is set to 1, the value to AND is the value in the register whose index is specified in the last two bits of the instruction
            if (GetBits(9, 9) == 1)
            {
                valueToAnd = GetBits(0, 8);
            }
            else
            {
                // Otherwise, the value to AND is the value specified in the last 8 bits of the instruction
                valueToAnd = emulator.Registers[GetBits(0, 1)];
            }

            var result = regToBeStored != 0 && valueToAnd != 0 ? (short)1 : (short)0;

            emulator.Carry = (result > short.MaxValue);
            emulator.Overflow = (result > short.MaxValue) || (result < short.MinValue);
            emulator.Zero = (result == 0);
            emulator.Negative = (result < 0);
            emulator.Registers[GetBits(10, 10)] = result;
        }
    }

}