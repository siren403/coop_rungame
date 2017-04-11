using System;

public static class ActionExtension
{
    public static void SafeInvoke(this Action self)
    {
        if (self != null)
        {
            self.Invoke();
        }
    }
    public static void SafeInvoke<T>(this Action<T> self,T value)
    {
        if(self != null)
        {
            self.Invoke(value);
        }
    }
}
