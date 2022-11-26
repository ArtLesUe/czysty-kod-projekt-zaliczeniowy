namespace CkpTodoApp.Task;

public static class UniqueNumber
{
    private static int _counter;
    
    public static int GetUniqueNumber()
    { 
        return _counter++; 
    }
}