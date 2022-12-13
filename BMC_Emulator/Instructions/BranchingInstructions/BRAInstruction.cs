using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions.BasicInstructions;

public class BRAInstruction : Instruction
{
    public BRAInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public BRAInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        if (Arguments[0].Contains('R'))
        {
            return RegisterToBinary(Arguments[0]);
        }

        return (short)(1 << 9 | ToShort(Arguments[0]));
    }

    public override void Execute(Emulator emulator)
    {
        emulator.LinkRegister = emulator.ProgramCounter + 1;

        if (emulator.ProgramCounter >= Memory.MemorySize)
            ErrorHandler.HandleOutOfBoundsLinkRegister();
        
        if (GetBits(10, 10) == 1)
        {
            emulator.ProgramCounter = InstructionBinary & 0xFF;
        }
        else
        {
            emulator.ProgramCounter = emulator.Registers[InstructionBinary & 0x1];
        }
    }
}