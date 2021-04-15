using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Windows.Forms;

namespace Algorithms
{
    public partial class GraphForm : Form
    {

        public GraphForm(Graph graph)
        {
            InitializeComponent();
            GViewer gViewer = new GViewer();
            graph.Attr.LayerDirection = LayerDirection.None;
            gViewer.Graph = graph;
            gViewer.ToolBarIsVisible = false;
            gViewer.Dock = DockStyle.Fill;
            gViewer.Enabled = false;
            Controls.Add(gViewer);
            ResumeLayout();
        }
    }
}
