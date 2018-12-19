using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Mono.Options;

namespace Guid
{
    using Guid = System.Guid;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var toLower = false;
            var showHelp = false;
            var empty = false;
            var options = new OptionSet {
                { "l|lower-case", "guid to lower case.", a => toLower = a != null },
                { "u|upper-case", "guid to upper case.", a => toLower = a == null },
                { "e|empty", "a guid whose value is all zeros.", a => empty = a != null },
                { "h|help", "shows this message.", a => showHelp = a != null },
            };
            options.Parse(args);

            if (showHelp)
            {
                ShowHelp(options);
                return;
            }
            
            var text = empty ? Guid.Empty.ToString().ToUpper() : Guid.NewGuid().ToString().ToUpper();
            if (toLower)
                text = text.ToLower();

            Console.WriteLine(text);
            Clipboard.SetText(text);
        }

        static void ShowHelp(OptionSet options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Console.WriteLine("Usage: guid [options...]");

            var stringBuilder = new StringBuilder();
            using (TextWriter writer = new StringWriter(stringBuilder))
                options.WriteOptionDescriptions(writer);

            Console.WriteLine(stringBuilder.ToString().ToLower());
        }
    }
}
