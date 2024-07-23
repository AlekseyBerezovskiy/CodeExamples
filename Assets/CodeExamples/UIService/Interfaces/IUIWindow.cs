using System;

namespace CodeExamples.UIService 
{
    public interface IUIWindow : IDisposable
    {
        void Show();    
        void Hide();
    }
}
