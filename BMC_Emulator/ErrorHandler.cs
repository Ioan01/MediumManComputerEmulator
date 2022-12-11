namespace BMC_Emulator;

public static class ErrorHandler
{
    public static void HandleBadPush()
    {
        throw new NotImplementedException();
    }
    
    public static void HandleBadPop()
    {
        throw new NotImplementedException();
    }

    public static void HandleOutOfBoundsLinkRegister()
    {
        throw new NotImplementedException();
    }

    public static void HandleDivisonByZero()
    {
        throw new Exception("Divison by zero is not allowed !");
    }
}