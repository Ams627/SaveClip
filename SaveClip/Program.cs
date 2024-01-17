using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SaveClip;

internal static class Extensions
{
    public static void RunSta(this Thread thread)
    {
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        try
        {

            IDataObject content = null;
            new Thread(() =>
            {
                content = Clipboard.GetDataObject();
                string[] formats = content.GetFormats();
                foreach (string format in formats)
                {
                    Console.WriteLine(format);
                }
                Image i = Clipboard.GetImage();
                i.Save("cb.png");
            }
            ).RunSta();

            Console.WriteLine("got");

        }
        catch (Exception ex)
        {
            var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
            var progname = Path.GetFileNameWithoutExtension(fullname);
            Console.Error.WriteLine($"{progname} Error: {ex.Message}");
        }
    }
}
