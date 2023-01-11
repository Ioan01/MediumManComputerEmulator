using System.Collections;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;
using BMC_Emulator.Instructions.BasicInstructions;
using BMC_Emulator.Instructions.BranchingInstructions;

namespace BMC_Emulator;

public static class InstructionDecoder
{
    private static Regex instructionRegex = new Regex("[ \t]*([a-z]+)[ \t]*([0-9a-z]+)?,?([-0-9a-z]+)?,?([0-9a-z]+)?",
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
            case "STR":
                return typeof(StoreInstruction);
            case "LDR":
                return typeof(LoadInstruction);
            case "MOV":
                return typeof(MoveInstruction);
            case "PSH":
                return typeof(PushInstruction);
            case "POP":
                return typeof(PopInstruction);
            case "JMS":
                return typeof(JumpInstruction);
            case "RET":
                return typeof(ReturnInstruction);
            case "ADD":
                return typeof(ADDInstruction);
            case "SUB":
                return typeof(SUBInstruction);
            case "MUL":
                return typeof(MULInstruction);
            case "OR":
                return typeof(ORInstruction);
            case "AND":
                return typeof(ANDInstruction);
            case "XOR":
                return typeof(XORInstruction);
            case "NOT":
                return typeof(NOTInstruction);
            case "MOD":
                return typeof(MODInstruction);
            case "DIV":
                return typeof(DIVInstruction);
            case "CMP":
                return typeof(CMPInstruction);
            case "BRA":
                return typeof(JumpInstruction);
            case "BEQ":
                return typeof(BEQInstruction);
            case "BRZ":
                return typeof(BRZInstruction);
            case "BMI":
                return typeof(BMIInstruction);
            case "BPL":
                return typeof(BPLInstruction);
            case "BGT":
                return typeof(BPLInstruction);
            case "BLT":
                return typeof(BPLInstruction);
            default:
                throw new Exception("Bad instruction provided " + code);
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