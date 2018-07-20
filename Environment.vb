Imports SExec.Interfaces
Imports System.Reflection

Public Class Environment
    Implements IEnvironment
    Public Property Name As String Implements IEnvironment.Name
    Public Property Objects As List(Of Object)
    Sub New(Name As String)
        Me.Name = Name
        Me.Objects = New List(Of Object)
    End Sub
    Public Shared Function Create(Name As String, Filename As String) As Environment
        Dim env As New Environment(Name)
        env.Load(AssemblyName.GetAssemblyName(Filename))
        Return env
    End Function
    Public Sub Load(Name As AssemblyName) Implements IEnvironment.Load
        Me.Objects.Add(Name)
    End Sub
    Private Function ToList() As List(Of Object) Implements IEnvironment.ToList
        Return Me.Objects
    End Function
End Class
