Public Class Help
    Private Sub Help_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Update icon to Info
        Dim bitmap As System.Drawing.Bitmap = System.Drawing.SystemIcons.Information.ToBitmap()
        picHelp.Image = bitmap

        'Play success jingle
        If Main.chkAudio.Checked Then My.Computer.Audio.Play(My.Resources.success, AudioPlayMode.Background)
    End Sub
End Class