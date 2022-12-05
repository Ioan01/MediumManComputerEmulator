namespace BMC_Emulator;

public class Emulator
{
    public Memory Memory { get; } = new();

    public IO Io { get; } = new();

    public short[] Registers { get; set; } = new short[2];
    

    private int programCounter;

    private bool stopped;

    public void LoadProgram(string[] instructions)
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            var instruction = InstructionDecoder.DecodeInstruction(instructions[i]);
            Memory.Load(instruction.ToBinary(),i);
        }
    }


    public void Run()
    {
        while (!stopped)
        {
            var instruction = Memory.Read(programCounter);
            var _instruction = InstructionDecoder.DecodeInstructionBinary(instruction);
            _instruction.Execute(this);
            programCounter++;
        }
    }

    public void Stop()
    {
        stopped = true;
    }
}