using System.Collections;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace BMC_Emulator;

public static class InstructionDecoder
{
    private static Regex instructionRegex = new Regex("[ \t]*([a-z]+)[ \t]*([0-9a-z]+)?,?([0-9a-z]+)?,?([0-9a-z]+)?",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);


    private static Type MatchInstructionCode(string code)
    {
        switch (code)
        {
            case "INP":
                return typeof(InputInstruction);
            case "OUT":
                return typeof(OutputInstruction);
            case "HLT":
                return typeof(HaltInstruction);
            default:
                return typeof(Object);
        }
    }
    
    
    public static Instruction DecodeInstruction(string instruction)
    {
        var match = instructionRegex.Match(instruction);

        var type = MatchInstructionCode(match.Groups[1].Value);

        Instruction _instruction = (Activator.CreateInstance(type,match.Groups) as Instruction)!;
        
        
        
        return _instruction!;
    }

    public static Instruction DecodeInstructionBinary(short instructionBinary)
    {
        short opcode = (short)(instructionBinary & 0xFC00);

        var type = MatchInstructionCode(Instruction.OpcodesReverse[opcode]);
        return (Activator.CreateInstance(type,instructionBinary) as Instruction)!;

    }
    
    


}