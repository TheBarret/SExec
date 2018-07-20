Namespace Interfaces
    Public Interface ITarget
        Function [Get]( Name As String) As Object
        Sub [Set]( Name As String,  value As Object)
        Function Invoke( Name As String, ParamArray Parameters As Object()) As Object
    End Interface
End Namespace