Imports SExec.Interfaces
Imports System.Dynamic
Public Class Proxy
    Inherits DynamicObject
    Public Property Target As ITarget
    Sub New(Target As ITarget)
        Me.Target = Target
    End Sub
    Public Function TryGet(Name As String, ByRef Result As Object) As Boolean
        Try
            Result = Me.Target.Get(Name)
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.GetInformation)
            Return False
        End Try
    End Function
    Public Function TrySet(Name As String, Value As Object) As Boolean
        Try
            Me.Target.Set(Name, Value)
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.GetInformation)
            Return False
        End Try
    End Function
    Public Function Invoke(Name As String, ParamArray Parameters As Object()) As Object
        Try
            Return Me.Target.Invoke(Name, Parameters)
        Catch ex As Exception
            Console.WriteLine(ex.GetInformation)
            Return Nothing
        End Try
    End Function
End Class