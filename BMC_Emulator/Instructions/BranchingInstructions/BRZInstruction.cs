using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions.BranchingInstructions;

public class BRZInstruction : Instruction
{
    public BRZInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public BRZInstruction(short word) : base(word)
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
        // if accumulator != 0 return
        if (emulator is not { Zero: true })
            return;

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