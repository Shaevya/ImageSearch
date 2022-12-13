using ImageSearchServer.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearchServer.MVVM.ViewModel
{
    public interface IMainViewModel
    {
        void SearchImages();

        void GetNextImages();
    }
}