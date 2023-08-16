namespace WinFormsCustomControl
{
    internal class MyButton : Button
    {
        int paintCount = 0;
        protected override void OnPaint(PaintEventArgs pevent)
        {
            

            pevent.Graphics.FillRectangle(Brushes.Blue, ClientRectangle);
            pevent.Graphics.DrawString($"Paint: {paintCount++}",SystemFonts.DefaultFont,Brushes.BlanchedAlmond,ClientRectangle);

        }
        
    }
}
