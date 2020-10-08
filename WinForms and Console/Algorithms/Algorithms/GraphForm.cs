using Microsoft.Msagl.Drawing;
using System.Windows.Forms;

namespace Algorithms
{
    public partial class GraphForm : Form
    {

        public GraphForm(Graph graph)
        {
            InitializeComponent();
            graph.Attr.LayerDirection = LayerDirection.None;
            gViewer1.Graph = graph;
            gViewer1.ToolBarIsVisible = false;
        }
    }
}
