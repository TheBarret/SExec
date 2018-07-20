Imports SExec.Interfaces
Imports System.Reflection

Namespace Domain
    Public Class Target
        Inherits MarshalByRefObject
        Implements ITarget
        Public Property Name As String
        Public Property Target As Lazy(Of Object)
        Sub New( Name As String)
            Me.Name = Name
            Me.Target = New Lazy(Of Object)(AddressOf Me.CreateInstance)
        End Sub
        Private Function CreateInstance() As Object
            Return Activator.CreateInstance((From assembly In AppDomain.CurrentDomain.GetAssemblies() Let type = assembly.GetType(Me.Name) Where type IsNot Nothing Select type).Single)
        End Function
        Public Function [Get]( Name As String) As Object Implements ITarget.Get
            Return Me.Target.Value.GetType.GetProperty(Name, BindingFlags.Instance Or BindingFlags.Public).GetValue(Me.Target.Value)
        End Function
        Public Sub [Set]( Name As String,  Value As Object) Implements ITarget.Set
            Me.Target.Value.GetType.GetProperty(Name, BindingFlags.Instance Or BindingFlags.Public).SetValue(Me.Target.Value, Value)
        End Sub
        Public Function Invoke( Name As String, ParamArray Parameters As Object()) As Object Implements ITarget.Invoke
            Dim ParamTypes As Type() = If(Parameters Is Nothing, Type.EmptyTypes, Parameters.Select(Function(param) param.GetType).ToArray)
            Return Me.Target.Value.GetType.GetMethod(Name, BindingFlags.Instance Or BindingFlags.Public, Nothing, ParamTypes, Nothing).Invoke(Me.Target.Value, Parameters)
        End Function
    End Class
End Namespace