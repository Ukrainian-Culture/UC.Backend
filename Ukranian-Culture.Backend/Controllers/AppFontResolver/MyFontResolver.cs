using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Fonts;
using System;
using System.IO;
using PdfSharpCore.Utils;

namespace FontResolver
{
    public class AppFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("MacPaw", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold)
                {
                    return new FontResolverInfo("MacPawFixelDisplay-Bold.ttf");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("MacPawFixelDisplay-Light.ttf");
                }
                else
                {
                    return new FontResolverInfo("MacPawFixelDisplay-Regular.ttf");
                }
            }

            return null;
        }

        public byte[] GetFont(string faceName)
        {
            var path = Environment.CurrentDirectory + "\\Controllers\\AppFontResolver\\Fonts\\";
            var faceNamePath = Path.Join(path, faceName);
            using (var ms = new MemoryStream())
            {
                try
                {
                    using (var fs = File.OpenRead(faceNamePath))
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        return ms.ToArray();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception($"No Font File Found - " + faceNamePath);
                }
            }
        }
    }
}