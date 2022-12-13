using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions.BranchingInstructions;

public class CMPInstruction : Instruction
{
    public CMPInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public CMPInstruction(short word) : base(word)
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
        // reset flags
        emulator.Carry = false;
        emulator.Overflow = false;
        emulator.Zero = false;
        emulator.Negative = false;
        
        // "CMP A, B"
        // Get the index of the register to which we will compare the second value/register
        var valueA = emulator.Registers[GetBits(10, 10)];
        
        var valueB = 0;

        // If the 9th bit is set to 1, B is the value in the register whose index is specified in the last two bits of the instruction
        if (GetBits(9, 9) == 1) valueB = GetBits(0, 8);
        
        // Otherwise, B is the value specified in the last 8 bits of the instruction
        else valueB = emulator.Registers[GetBits(0, 1)];

        if (valueA == valueB)
        {
            emulator.Carry = true;
            emulator.Zero = true;
            return;
        }
    }
}