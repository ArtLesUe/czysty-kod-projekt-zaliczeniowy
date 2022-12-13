namespace CkpTodoApp.Commons;

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