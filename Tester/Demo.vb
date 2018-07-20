Public Class Demo
    Public Function DomainName() As String
        Return AppDomain.CurrentDomain.FriendlyName
    End Function
    Public Sub SecurityTest1()
        For Each var As DictionaryEntry In System.Environment.GetEnvironmentVariables
            Console.WriteLine("{0} = {1}", var.Key, var.Value)
        Next
    End Sub
    Public Sub SecurityTest2()
        Dim address As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
        Microsoft.Win32.Registry.SetValue(address, "TEST", "TEST", Microsoft.Win32.RegistryValueKind.String)
        Console.WriteLine("Value {0}", Microsoft.Win32.Registry.GetValue(address, "TEST", "").ToString)
    End Sub
    Public Sub SecurityTest3()
        Dim address As String = "C:\Windows\System32\drivers\etc\hosts"
        Console.WriteLine(System.IO.File.ReadAllText(address))
    End Sub
End Class
