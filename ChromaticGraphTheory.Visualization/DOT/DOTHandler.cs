using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaticGraphTheory.Visualization.DOT
{
    public static class DOTHandler
    {
        public static void ConvertDotToSvg(string dotFile, string svgFile)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C dot {dotFile} -Tsvg -o {svgFile}";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
