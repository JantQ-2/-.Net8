using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
