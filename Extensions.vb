Imports System.Text
Imports System.Security
Imports System.Security.Permissions
Imports System.Runtime.CompilerServices

Public Module Extensions
    <Extension>
    Public Function Truncate(str As String, length As Integer) As String
        If (str.Length > length) Then Return String.Format("{0}...", str.Substring(0, length))
        Return str
    End Function
    <Extension>
    Public Function GetValues(table As Hashtable, ParamArray Parameters() As String) As String
        Dim values As New List(Of String)
        For Each pair As DictionaryEntry In table
            If (Not Parameters.Contains(pair.Key.ToString)) Then
                values.Add(pair.Value.ToString)
            End If
        Next
        Return String.Join(",", values.ToArray)
    End Function
    <Extension>
    Public Function GetInformation(ex As Exception) As String
        Dim report As New StringBuilder
        Do While ex.InnerException IsNot Nothing
            ex = ex.InnerException
        Loop
        If (TypeOf ex Is SecurityException) Then
            Dim sex As SecurityException = CType(ex, SecurityException)
            Dim table As Hashtable = CType(sex.Demanded, CodeAccessPermission).ToXml.Attributes
            report.AppendLine("")
            report.AppendLine(String.Format("Assembly         : {0}", sex.FailedAssemblyInfo.FullName))
            report.AppendLine(String.Format("Type             : {0}", sex.Demanded.GetType.Name))
            If (table.ContainsKey("Read")) Then
                report.AppendLine(String.Format("Action           : {0} Read", sex.Action.ToString))
                report.AppendLine(String.Format("Target           : {0}", table("Read").ToString.Truncate(80)))
            ElseIf (table.ContainsKey("Write")) Then
                report.AppendLine(String.Format("Action           : {0} Write", sex.Action.ToString))
                report.AppendLine(String.Format("Target           : {0}", table("Write").ToString.Truncate(80)))
            Else
                report.AppendLine(String.Format("Action           : {0} ", String.Join(",", table.GetValues({"version", "class"}))).Truncate(80))
            End If
        ElseIf (Not TypeOf ex Is SecurityException) AndAlso (ex IsNot Nothing) Then
            report.AppendLine(String.Format("Exception caught: {0}", ex.Message))
        End If
        Return report.ToString
    End Function
End Module
