using System;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator
{
    public class MODInstruction : Instruction
    {
        public MODInstruction(GroupCollection matchGroups) : base(matchGroups)
        {
        }

        public MODInstruction(short word) : base(word)
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
            // Get the index of the register to which we will add the value
            var regToBeStored = emulator.Registers[GetBits(10, 10)];
            // Get the value by which we will modulo the register
            var valueToModulo = 0;

            // If the 9th bit is set to 1, the value to modulo is the value in the register whose index is specified in the last two bits of the instruction
            if (GetBits(9, 9) == 1)
            {
                valueToModulo = GetBits(0, 8);
            }
            else
            {
                // Otherwise, the value to modulo is the value specified in the last 8 bits of the instruction
                valueToModulo = emulator.Registers[GetBits(0, 1)];
            }

            // If the value by which we modulo is 0, we throw an exception
            if (valueToModulo == 0)
            {
                throw new DivideByZeroException("Can't divide by zero");
            }

            var result = (ushort)(regToBeStored % valueToModulo);

            emulator.Carry = (result > short.MaxValue);
            emulator.Overflow = (result > short.MaxValue) || (result < short.MinValue);
            emulator.Zero = (result == 0);
            emulator.Negative = (result < 0);
            emulator.Registers[GetBits(10, 10)] = (short)result;
        }
    }
}