namespace CkpTodoApp.Constants;

public enum StatusCodeEnum
{
    Ok,
    AuthFailed,
    WrongData,
    Deleted,
    Checked,
    DeletingNotExistingForbidden,
    CheckingNotExistingForbidden,
    SelfDeletionForbidden,
    UserDoesNotExist,
    EmptyPasswordNotPermitted
}