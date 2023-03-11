namespace ChromaticGraphTheory.Visualization.DOT
{
    public static class DOTHandler
    {
        public static void ConvertDotToSvg(string dotFile)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new()
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C dot {dotFile} -Tsvg -o {dotFile}.svg"
            };
            System.Diagnostics.Process process = new()
            {
                StartInfo = startInfo
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
