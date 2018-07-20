Imports System.Reflection

Namespace Interfaces
    Public Interface IEnvironment
        Property Name As String
        Sub Load(Name As AssemblyName)
        Function ToList() As List(Of Object)
    End Interface
End Namespace
