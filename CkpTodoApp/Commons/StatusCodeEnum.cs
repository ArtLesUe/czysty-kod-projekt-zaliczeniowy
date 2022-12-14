namespace CkpTodoApp.Commons;

public struct StatusCodeEnum
{
    public const string Ok = "OK";
    public const string AuthFailed = "auth-failed";
    public const string WrongData = "wrong-data";
    public const string Deleted = "deleted";
    public const string Checked = "checked";
    public const string DeletingNotExistingForbidden = "deleting-not-existing-forbidden";
    public const string CheckingNotExistingForbidden = "checking-not-existing-forbidden";
    public const string SelfDeletionForbidden = "self-deletion-forbidden";
    public const string UserDoesNotExist = "user-not-exists";
    public const string EmptyPasswordNotPermitted = "empty-password-not-permitted";
}