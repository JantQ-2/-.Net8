using System.ComponentModel;

namespace Asteroids
{
    public partial class Movement : Component
    {
        public Movement()
        {
            InitializeComponent();
        }

        public Movement(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
