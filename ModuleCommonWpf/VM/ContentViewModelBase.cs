using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCommon.VM
{
    public abstract class ContentViewModelBase:BindableBase
    {
        public ContentViewModelBase(string text)
        {
            Text = text;
        }

        public string Text
        {
            get { return backing_text; }
            set { SetProperty(ref backing_text, value); }
        }
        private string backing_text = String.Empty;

    }
}
