using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions;

public class MoveInstruction : Instruction
{
    public MoveInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public MoveInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        return (short)((ushort)ToShort(Arguments[1]) | (RegisterToBinary(Arguments[0]) << 9));
    }

    public override void Execute(Emulator emulator)
    {
        ref var destinationRegister = ref emulator.Registers[GetBits(10, 10)];

        destinationRegister = (short)(InstructionBinary & 0xFF);
    }
}