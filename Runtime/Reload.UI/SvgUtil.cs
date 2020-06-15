namespace Reload.UI
{
    using Svg;
    using System.Collections.Generic;
    using System.Drawing;

    public static class SvgUtil
    {
        public static Bitmap SvgFileToBmp(string filepath)
        {
            var svgDoc = SvgDocument.Open<SvgDocument>(filepath, null);

            ProcessNodes(svgDoc.Descendants(), new SvgColourServer(Color.DarkGreen));

            return svgDoc.Draw();
        }

        private static void ProcessNodes(IEnumerable<SvgElement> nodes, SvgPaintServer colorServer)
        {
            foreach (var node in nodes)
            {
                if (node.Fill != SvgPaintServer.None)
                {
                    node.Fill = colorServer;
                }
                if (node.Color != SvgPaintServer.None)
                {
                    node.Color = colorServer;
                }
                if (node.Stroke != SvgPaintServer.None)
                {
                    node.Stroke = colorServer;
                }

                ProcessNodes(node.Descendants(), colorServer);
            }
        }
    }
}
