using System;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator
{
    public class NOTInstruction : Instruction
    {
        public NOTInstruction(GroupCollection matchGroups) : base(matchGroups)
        {
        }
        public NOTInstruction(short word) : base(word)
        {
        }

        protected override short GetLast10Bits()
        {
            // The NOT instruction has only one argument, which is the register to negate
            return (short)(RegisterToBinary(Arguments[0]) << 9);
        }

        public override void Execute(Emulator emulator)
        {
            // Get the index of the register to negate
            var regToBeStored = emulator.Registers[GetBits(10, 10)];

            // Negate the value in the register
            var result = (ushort)(regToBeStored == 0 ? 1 : 0);

            // Update the status flags
            emulator.Carry = false;
            emulator.Overflow = false;
            emulator.Zero = (result == 0);
            emulator.Negative = (result < 0);
            emulator.Registers[GetBits(10, 10)] = (short)result;
        }
    }
}