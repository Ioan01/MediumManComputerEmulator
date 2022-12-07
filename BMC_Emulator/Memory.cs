using System.Collections;

namespace BMC_Emulator;

public class Memory
{
    public static int MemorySize { get; } = 256;

    private readonly short[] words = new short[MemorySize];

    public Memory()
    {
        for (int i = 0; i < MemorySize; i++)
        {
            words[i] = 0;
        }
    }

    public void Load(short word, int index)
    {
        words[index] = word;
    }
    
    public short Read(int index)
    {
        return words[index];
    }
}