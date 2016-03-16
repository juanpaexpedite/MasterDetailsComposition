using CompPac.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace CompPac.Managers
{
    public class GameManager
    {
        private List<Composable> composables = new List<Composable>();

        public GameManager()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;   
        }


        private bool pause = true;

        private void CompositionTarget_Rendering(object sender, object e)
        {
            if (pause)
                return;

            var size = WindowManager.Bounds();

            foreach (var composable in composables)
            {
                composable.UpdateComposition(size);
            }
        }

        public void Start()
        {
            pause = false;
        }

        public void AddComposable(Composable newitem)
        {
            composables.Add(newitem);
        }
    }
}
